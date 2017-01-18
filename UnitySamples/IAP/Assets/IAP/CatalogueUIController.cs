using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.ObjectModel;

/// Responsible for populating the catalogue UI with the available items.
/// Allows the purchasing of items which operates via Unity IAP and ChilliConnect "Real Money Payments".
/// 
/// Purchase flow is:
/// 
/// 1. Request purchaseable items from ChilliConnect dashboard (display them to the user).
/// 2. Attempt to purchase an item via Unity IAP, when user selects an item from the display.
/// 
public class CatalogueUIController : MonoBehaviour 
{
	private const string k_catalogueElementPrefabPath = "CatalogueElement";
	private const float k_fadeTime = 1.0f;

	private enum State
	{
		NONE,
		FETCHING_ITEMS,
		READY_TO_PURCHASE,
		PURCHASING
	}

	private State m_currentState = State.NONE;
	private CanvasGroup m_canvasGroup;

	/// Entry point
	/// 
	private void Start () 
	{
		m_canvasGroup = GetComponent<CanvasGroup>();

		// TODO: Display fetching UI
		m_currentState = State.FETCHING_ITEMS;
	}

	/// Uses the available items from the IAP system and displays
	/// each one as an UI element in the catalogue. If none displays that the store is empty
	/// 
	/// @param items
	/// 	Available items - could be empty if not configured on dashboard or network issues
	/// 
	private void PopulateCatalogue(ReadOnlyCollection<IAPSystem.Item> items)
	{
		Debug.Assert(m_currentState == State.FETCHING_ITEMS);

		GameObject catalogueElement = Resources.Load<GameObject>(k_catalogueElementPrefabPath);

		foreach(var item in items)
		{
			var capturedItem = item;
			GameObject uiElement = (GameObject)GameObject.Instantiate(catalogueElement, Vector3.zero, Quaternion.identity);
			uiElement.transform.SetParent(transform);
			uiElement.transform.localScale = Vector3.one;
			uiElement.name = capturedItem.ProductId;
			uiElement.transform.FindChild("Name").GetComponent<Text>().text = capturedItem.Name;
			uiElement.transform.FindChild("Cost").GetComponent<Text>().text = capturedItem.LocalisedPrice;
			uiElement.GetComponent<Button>().onClick.AddListener(() => OnCatalogueItemSelected(capturedItem));
		}

		IAPSystem.Get().OnPurchaseCompleteEvent += OnPurchaseComplete;
		IAPSystem.Get().OnPurchaseFailedEvent += OnPurchaseFailed;

		m_currentState = State.READY_TO_PURCHASE;
	}

	/// Pressing on a catalogue item will hide the purchase menu and
	/// start the process of purchasing the item
	/// 
	/// @param item
	/// 	Definition of the item to purchase
	/// 
	private void OnCatalogueItemSelected(IAPSystem.Item item)
	{
		Debug.Assert(m_currentState == State.READY_TO_PURCHASE);

		m_currentState = State.PURCHASING;

		//Hide the Catalogue so that no more purchases can be made
		m_canvasGroup.interactable = false;
		m_canvasGroup.alpha = 0.0f;

		IAPSystem.Get().Purchase(item);
	}
		
	/// Called when the purchase has been validated by ChilliConnect and the inventory has been updated.
	/// Switches state to allow another purchase.
	/// 
	/// @param request
	/// 	Information about the request, including the URL endpoint.
	/// @param response
	/// 	Response from the request. Holds the information on all rewarded items
	///
	private void OnPurchaseComplete()
	{
		Debug.Assert(m_currentState == State.PURCHASING);

		m_currentState = State.READY_TO_PURCHASE;

		//Show the Catalogue so that more purchases can be made
		m_canvasGroup.interactable = true;
		m_canvasGroup.alpha = 1.0f;
	}

	/// Called if the purchase fails.
	/// 
	private void OnPurchaseFailed()
	{
		//You may wish to pass the error in here and display it to the user
		
		m_currentState = State.READY_TO_PURCHASE;

		//Show the Catalogue so that more purchases can be made
		m_canvasGroup.interactable = true;
		m_canvasGroup.alpha = 1.0f;
	}

	/// Wait for ChilliConnect to download the catalogue and then display
	/// it to the user.
	/// 
	private void Update()
	{
		if(m_currentState == State.FETCHING_ITEMS)
		{
			if(IAPSystem.Get().IsInitialised())
			{
				PopulateCatalogue(IAPSystem.Get().GetAvailableItems());
			}
		}
	}
}
