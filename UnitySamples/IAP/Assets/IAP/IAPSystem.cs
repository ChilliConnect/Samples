using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Purchasing;
using ChilliConnect;

/// Allows the querying of available items and the purchasing of items which via Unity IAP and ChilliConnect "Real Money Payments".
/// 
/// NOTE: Requires the Unity IAP service be setup in your project. More information on Unity IAP can be found here: https://unity3d.com/learn/tutorials/topics/analytics/integrating-unity-iap-your-game
/// 
/// Purchase flow is:
/// 
/// 1. Request purchaseable items from ChilliConnect dashboard.
/// 2. Initialise Unity IAP with available items
/// 3. Attempt to purchase an item via Unity IAP.
/// 4. On successful purchase, redeem the receipt with ChilliConnect (which will validate the payment and credit the account).
/// 5. Update the local inventory so it once again matcehs what's held on ChilliConnect.
/// 
/// 
public class IAPSystem : IStoreListener
{
	public event System.Action OnPurchaseCompleteEvent = delegate {};
	public event System.Action OnPurchaseFailedEvent = delegate {};

	private static IAPSystem s_singletonInstance = null;

	/// Simple container item that holds display information for purchaseable items.
	/// 
	public class Item
	{
		public Item(string productId, string chilliRMPKey)
		{
			ProductId = productId;
			RealMoneyPurchaseKey = chilliRMPKey;
		}

		public void AddStoreData(string name, string price)
		{
			
			Name = name;
			LocalisedPrice = price;
		}

		public string ProductId;
		public string Name;
		public string LocalisedPrice;
		public string RealMoneyPurchaseKey;
	}

	private IStoreController m_unityStoreController;
	private List<Item> m_availableItems = new List<Item>();
	private ChilliConnectSdk m_chilliConnect = null;

	/// @return Singleton instance if system has been created (not lazily created)
	/// 
	public static IAPSystem Get()
	{
		return s_singletonInstance;
	}

	/// 
	public IAPSystem()
	{
		s_singletonInstance = this;
	}

	/// Initialises the IAP system by pulling the available IAP items from ChilliConnect.
	/// Like most of the IAP calls this is asynchronous and completion can be polled using IsInitialised()
	/// 
	/// @param chilliConnect
	/// 	SDK instance
	/// 
	public void Initialise(ChilliConnectSdk chilliConnect)
	{
		Debug.Log("Initialising IAP system");

		m_chilliConnect = chilliConnect;

		var desc = new GetRealMoneyPurchaseDefinitionsRequestDesc();

		// Make a request for all "Real money purchase" items that have been registed on the ChilliConnect dashboard.
		m_chilliConnect.Economy.GetRealMoneyPurchaseDefinitions(desc, OnAvailableItemsFetched, (request, error) => Debug.LogError(error.ErrorDescription));
	}

	/// Called when the available items have been pulled from ChilliConnect.
	/// Builds the Unity purchasing system using the available items
	/// 
	/// @param request
	/// 	Information about the request, including the URL endpoint.
	/// @param response
	/// 	Response from the request. Holds the information on all purchaseable items
	/// 
	private void OnAvailableItemsFetched(GetRealMoneyPurchaseDefinitionsRequest request, GetRealMoneyPurchaseDefinitionsResponse response)
	{
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
	
		Debug.Log("Available items fetched:");

		//Initialise Unity purchasing with the product ids available on ChilliConnect
		foreach(var item in response.Items)
		{
			Debug.Log(string.Format("Product: {0}", item.Key));
			builder.AddProduct(item.Key, ProductType.Consumable, new IDs
			{
				{item.GoogleId, GooglePlay.Name},
				{item.IosId, AppleAppStore.Name}
			});
					
			m_availableItems.Add(new Item(GetPlatformProductId(item), item.Key));
		}

		builder.Configure<IGooglePlayConfiguration>().SetPublicKey("ADD YOUR GOOGLE PUBLIC KEY");
			
		// Expect a response either in OnInitialized or OnInitializeFailed.
		UnityPurchasing.Initialize(this, builder);
	}
		
	/// @return Collection of available items. Empty if none available or not yet ready
	///
	public ReadOnlyCollection<Item> GetAvailableItems()
	{
		return IsInitialised() ? m_availableItems.AsReadOnly() : new ReadOnlyCollection<Item>(new List<Item>());
	}

	/// Only say we are initialised if Unity IAP has initialised (post available item fetch).
	/// This method can be polled.
	/// 
	/// @return True if initialised, False otherwise
	/// 
	public bool IsInitialised()
	{
		return m_unityStoreController != null;
	}

	/// Purchase the given item and validate the receipt with ChilliConnect. Successful purchases
	/// will be added to the inventory. System must be intialised prior to calling this
	/// 
	/// @param item
	/// 	Item to purchase
	/// 
	public void Purchase(Item item)
	{
		Debug.Assert(IsInitialised());

		Product product = m_unityStoreController.products.WithStoreSpecificID(item.ProductId);

		Debug.Log(string.Format("Purchasing product: {0}", item.ProductId));
		// Buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed.
		m_unityStoreController.InitiatePurchase(product);
	}
		
#region IStoreListener Delegates

	/// Called when Unity IAP has initialised
	/// 
	/// @param controller
	/// 	Used to initiate purchases
	/// @param extensions
	/// 
	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		// Overall Purchasing system, configured with products for this application.
		m_unityStoreController = controller;

		//Update our items with metadata held on the store
		foreach(var product in m_unityStoreController.products.all)
		{
			GetItem(product.definition.storeSpecificId).AddStoreData(product.metadata.localizedTitle, product.metadata.localizedPriceString);
		}
	}

	/// Called when Unity IAP has failed to initialise
	/// 
	/// @param error
	/// 	Reason for failure
	/// @param extensions
	/// 
	public void OnInitializeFailed(InitializationFailureReason error)
	{
		// Consider sharing this reason with the user.
		Debug.LogError("Intitialisation failed. Reason:" + error);
	}

	/// Called by Unity IAP when it has finished with the transaction and wants us
	/// to credit the user. We want to validate the purchase with ChilliConnect so tell
	/// Unity the purchase is still pending and will close if post validation.
	/// 
	/// NOTE: If the purchase is still pending on next start of the application, Unity will
	/// call this method again
	/// 
	/// @param args
	/// 	Purchase event
	/// 
	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
	{
		string rmpKey = GetItem(args.purchasedProduct.definition.storeSpecificId).RealMoneyPurchaseKey;
		Debug.Log(string.Format("Redeeming product: {0} with RMP Key: {1}", args.purchasedProduct.definition.storeSpecificId, rmpKey));

		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			m_chilliConnect.Economy.RedeemAppleIap(rmpKey, ParseAppleReceipt(args.purchasedProduct.receipt), (request, response) => OnPurchaseRedeemed(args.purchasedProduct, response.Status, response.Rewards), (request, error) => OnPurchaseRedeemFailedApple(args.purchasedProduct, error));
		}
		else if(Application.platform == RuntimePlatform.Android)
		{
			string purchaseData, dataSignature;
			ParseGooglePlayReceipt(args.purchasedProduct.receipt, out purchaseData, out dataSignature);
			m_chilliConnect.Economy.RedeemGoogleIap(rmpKey, purchaseData, dataSignature, (request, response) => OnPurchaseRedeemed(args.purchasedProduct, response.Status, response.Rewards), (request, error) => OnPurchaseRedeemFailedGoogle(args.purchasedProduct, error));
		}
		else
		{
			Debug.Log("Cannot redeem on this platform or on editor.");
			OnPurchaseCompleteEvent();
			return PurchaseProcessingResult.Complete;
		}
			
		// We return pending as we manually close off the purchase after ChilliConnect has verified and credited our account
		return PurchaseProcessingResult.Pending;
	}

	/// We need to parse the apple json receipt to get the information needed
	/// by ChilliConnect.
	/// 
	private string ParseAppleReceipt(string receipt)
	{
		var wrapper = (Dictionary<string, object>)SdkCore.MiniJSON.Json.Deserialize(receipt);

		var payload = (string)wrapper["Payload"];
		return payload;
	}

	/// We need to parse the google play json receipt to get the information needed
	/// by ChilliConnect.
	/// 
	private void ParseGooglePlayReceipt(string receipt, out string purchaseData, out string dataSignature)
	{
		var wrapper = (Dictionary<string, object>)SdkCore.MiniJSON.Json.Deserialize(receipt);

		var payload = (string)wrapper["Payload"];

		var details = (Dictionary<string, object>)SdkCore.MiniJSON.Json.Deserialize(payload);
		purchaseData = (string)details ["json"];
		dataSignature = (string)details ["signature"];
	}

	/// Called by ChilliConnect when a purchase has been redeemed on the server.
	/// We close of the transaction, award the items and notify the listener that the purchase has finished.
	/// 
	/// NOTE: We have to handle duplicate purchases here
	/// 
	/// @param product
	/// 	Purchased product
	/// @param status
	/// 	Status of the product
	/// @param rewards
	/// 	Items to award for the given product as specified on the ChilliConnect dashboard
	/// 
	public void OnPurchaseRedeemed(Product product, string status, PurchaseExchange rewards)
	{
		Debug.Log(string.Format("Product redeemed: {0}. Status: {1}", product.definition.storeSpecificId, status));

		// NOTE: If the status is "InvalidRedeemed" then the user has already been awarded the IAP and this is
		// a duplicate purchase. In this case you might want to explain that to the user rather than just closing
		// the transaction silently. 
		// If it is "InvalidVerificationFailed" then probably a fraudulent purchase
		if(status != "InvalidRedeemed" && status != "InvalidVerificationFailed")
		{
			foreach(var reward in rewards.Items)
			{
				InventorySystem.Get().AddItem(reward.Key, reward.Amount);
			}
		}

		m_unityStoreController.ConfirmPendingPurchase(product);
		OnPurchaseCompleteEvent();
	}

	/// Called by ChilliConnect if the redemption fails. This could be for innocent reasons
	/// in which case we keep the transaction open and Unity will try again. However it could also
	/// be down to fraud, in which case we close the transaction and award nothing.
	/// 
	/// @param product
	/// 	Unredeemed product
	/// @param error
	/// 	Reason for failure
	/// 
	public void OnPurchaseRedeemFailedApple(Product product, RedeemAppleIapError error)
	{
		Debug.LogError(string.Format("Product {0} redeem failed. Reason {1}", product.definition.storeSpecificId, error.ErrorDescription));

		if(error.ErrorCode == RedeemAppleIapError.Error.IapValidationServiceResponseInvalid)
		{
			//Fraud or duplicate. Just close off without awarding
			m_unityStoreController.ConfirmPendingPurchase(product);
		}

		OnPurchaseFailedEvent();
	}

	/// Called by ChilliConnect if the redemption fails. We keep the transaction open and Unity will try again.
	/// 
	/// @param product
	/// 	Unredeemed product
	/// @param error
	/// 	Reason for failure
	/// 
	public void OnPurchaseRedeemFailedGoogle(Product product, RedeemGoogleIapError error)
	{
		Debug.LogError(string.Format("Product {0} redeem failed. Reason {1}", product.definition.storeSpecificId, error.ErrorDescription));
		OnPurchaseFailedEvent();
	}

	/// Called when purchasing has failed via Unity IAP
	/// 
	/// @param product
	/// 	Product that failed to purchase
	/// @param error
	/// 	Reason for failure
	/// 
	public void OnPurchaseFailed(Product product, PurchaseFailureReason error)
	{
		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
		// this reason with the user to guide their troubleshooting actions.
		Debug.LogError(string.Format("Product {0} purchase failed. Reason: {1}", product.definition.storeSpecificId, error));
		OnPurchaseFailedEvent();
	}

#endregion

	/// @param productId
	/// 	Store product id
	/// @return Item with given product id
	/// 
	private Item GetItem(string productId)
	{
		foreach(var item in m_availableItems)
		{
			if(item.ProductId == productId)
				return item;
		}

		return null;
	}

	/// ChilliConnect products hold an id for each platform. This method
	/// returns the correct id for the current build target
	/// 
	/// @param item
	/// 	Product to pull id from
	/// @return Id for current platform
	/// 
	private static string GetPlatformProductId(RealMoneyPurchaseDefinition item)
	{
#if UNITY_EDITOR
		return item.Key;
#elif UNITY_IOS
		return item.IosId;
#elif UNITY_ANDROID
		return item.GoogleId;
#endif
	}
}
