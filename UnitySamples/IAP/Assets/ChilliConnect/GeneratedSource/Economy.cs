//
//  This file was auto-generated using the ChilliConnect SDK Generator.
//
//  The MIT License (MIT)
//
//  Copyright (c) 2015 Tag Games Ltd
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using SdkCore;

namespace ChilliConnect 
{
	/// <summary>
	/// <para>The ChillConnect Economy Management module. Provides the means to retrieve and
	/// modify player currencies and inventory.</para>
	///
	/// <para>This is thread-safe.</para>
	/// </summary>
	public sealed class Economy
	{
		private const int SuccessHttpResponseCode = 200;
		
		private Logging m_logging;
		private TaskScheduler m_taskScheduler;
		private ServerRequestSystem m_serverRequestSystem;
		private DataStore m_dataStore;
		
		/// <summary>
		/// Initialises a new instance of the module with the given logger, task scheduler
		/// and server request system.
		/// </summary>
		///
		/// <param name="logging">Provides basic logging functionality.</param>
		/// <param name="taskScheduler">The system which allows scheduling of tasks on different threads.</param>
		/// <param name="serverRequestSystem">The system which processes all server requests.</param>
		/// <param name="dataStore">The data store used for persisting data across the session.</param>
		public Economy(Logging logging, TaskScheduler taskScheduler, ServerRequestSystem serverRequestSystem, DataStore dataStore)
		{
			ReleaseAssert.IsNotNull(logging, "Logging cannot be null.");
			ReleaseAssert.IsNotNull(taskScheduler, "Task Scheduler cannot be null.");
			ReleaseAssert.IsNotNull(serverRequestSystem, "Server Request System cannot be null.");
			ReleaseAssert.IsNotNull(dataStore, "Data Store cannot be null.");
		
			m_logging = logging;
			m_taskScheduler = taskScheduler;
			m_serverRequestSystem = serverRequestSystem;
			m_dataStore = dataStore;
		}
		
		/// <summary>
		/// Returns a list of currency balances for the currently logged in player.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetCurrencyBalance(GetCurrencyBalanceRequestDesc desc, Action<GetCurrencyBalanceRequest, GetCurrencyBalanceResponse> successCallback, Action<GetCurrencyBalanceRequest, GetCurrencyBalanceError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Currency Balance request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetCurrencyBalanceRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetCurrencyBalanceSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetCurrencyBalanceError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Sets the balance of a specified Currency for the currently logged in player.
		/// Direct access to this method from the SDKs is disabled by default and must be
		/// enabled from the ChilliConnect dashboard.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void SetCurrencyBalance(SetCurrencyBalanceRequestDesc desc, Action<SetCurrencyBalanceRequest, SetCurrencyBalanceResponse> successCallback, Action<SetCurrencyBalanceRequest, SetCurrencyBalanceError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Set Currency Balance request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new SetCurrencyBalanceRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifySetCurrencyBalanceSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifySetCurrencyBalanceError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Convert a currency using a defined currency conversion rule.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void ConvertCurrency(ConvertCurrencyRequestDesc desc, Action<ConvertCurrencyRequest, ConvertCurrencyResponse> successCallback, Action<ConvertCurrencyRequest, ConvertCurrencyError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Convert Currency request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new ConvertCurrencyRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyConvertCurrencySuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyConvertCurrencyError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Add currency for the currently logged in player. Direct access to this method
		/// from the SDKs is disabled by default and must be enabled from the ChilliConnect
		/// dashboard.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void AddCurrencyBalance(AddCurrencyBalanceRequestDesc desc, Action<AddCurrencyBalanceRequest, AddCurrencyBalanceResponse> successCallback, Action<AddCurrencyBalanceRequest, AddCurrencyBalanceError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Add Currency Balance request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new AddCurrencyBalanceRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyAddCurrencyBalanceSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyAddCurrencyBalanceError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Remove currency for the currently logged in player. Direct access to this method
		/// from the SDKs is disabled by default and must be enabled from the ChilliConnect
		/// dashboard.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void RemoveCurrencyBalance(RemoveCurrencyBalanceRequestDesc desc, Action<RemoveCurrencyBalanceRequest, RemoveCurrencyBalanceResponse> successCallback, Action<RemoveCurrencyBalanceRequest, RemoveCurrencyBalanceError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Remove Currency Balance request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new RemoveCurrencyBalanceRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyRemoveCurrencyBalanceSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyRemoveCurrencyBalanceError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Get the inventory of the currently logged in player.
		/// </summary>
		///
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetInventory(Action<GetInventoryResponse> successCallback, Action<GetInventoryError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Inventory request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetInventoryRequest(connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetInventorySuccess(serverResponse, successCallback);
				} 
				else 
				{
					NotifyGetInventoryError(serverResponse, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Get the inventory of the currently logged in player for a given set of keys.
		/// </summary>
		///
		/// <param name="keys">Return only items with these Keys from the player's inventory.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetInventoryForKeys(IList<string> keys, Action<GetInventoryForKeysRequest, GetInventoryForKeysResponse> successCallback, Action<GetInventoryForKeysRequest, GetInventoryForKeysError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Inventory For Keys request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetInventoryForKeysRequest(keys, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetInventoryForKeysSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetInventoryForKeysError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Get the inventory of the currently logged in player for a given set of Item IDs.
		/// </summary>
		///
		/// <param name="itemIds">Return only these items witihin the player's inventory.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetInventoryForItemIds(IList<string> itemIds, Action<GetInventoryForItemIdsRequest, GetInventoryForItemIdsResponse> successCallback, Action<GetInventoryForItemIdsRequest, GetInventoryForItemIdsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Inventory For Item Ids request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetInventoryForItemIdsRequest(itemIds, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetInventoryForItemIdsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetInventoryForItemIdsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Add an item to a player's inventory. Direct access to this method from the SDKs
		/// is disabled by default and must be enabled from the ChilliConnect dashboard.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void AddInventoryItem(AddInventoryItemRequestDesc desc, Action<AddInventoryItemRequest, AddInventoryItemResponse> successCallback, Action<AddInventoryItemRequest, AddInventoryItemError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Add Inventory Item request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new AddInventoryItemRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyAddInventoryItemSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyAddInventoryItemError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Update the instance data of an item in the currently logged in player's
		/// inventory. Direct access to this method from the SDKs is disabled by default and
		/// must be enabled from the ChilliConnect dashboard.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void UpdateInventoryItem(UpdateInventoryItemRequestDesc desc, Action<UpdateInventoryItemRequest, UpdateInventoryItemResponse> successCallback, Action<UpdateInventoryItemRequest, UpdateInventoryItemError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Update Inventory Item request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new UpdateInventoryItemRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyUpdateInventoryItemSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyUpdateInventoryItemError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Remove an item from the currently logged in player's inventory. Direct access to
		/// this method from the SDKs is disabled by default and must be enabled from the
		/// ChilliConnect dashboard.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void RemoveInventoryItem(RemoveInventoryItemRequestDesc desc, Action<RemoveInventoryItemRequest> successCallback, Action<RemoveInventoryItemRequest, RemoveInventoryItemError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Remove Inventory Item request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new RemoveInventoryItemRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyRemoveInventoryItemSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyRemoveInventoryItemError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Perform a purchase defined by a Virtual Purchase item.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void MakeVirtualPurchase(MakeVirtualPurchaseRequestDesc desc, Action<MakeVirtualPurchaseRequest, MakeVirtualPurchaseResponse> successCallback, Action<MakeVirtualPurchaseRequest, MakeVirtualPurchaseError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Make Virtual Purchase request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new MakeVirtualPurchaseRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyMakeVirtualPurchaseSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyMakeVirtualPurchaseError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Validate a Receipt from a successful purchase on an Amazon device and apply the
		/// rewards to the players account.
		/// </summary>
		///
		/// <param name="key">The key of the real money purchase that defines the rewards to be applied to the
		/// players account on successful verification. The real money purchase should
		/// specify an amazon product id that matches the product id of the submitted
		/// Receipt.</param>
		/// <param name="receiptId">ReceiptID returned from the Amazon App Store as a result of a successful
		/// purchase. See the Amazon Documentation at
		/// 'https://developer.amazon.com/public/apis/earn/in-app-purchasing/docs-v2/implementing-iap-2.0'
		/// for more information to on how to access this value from your app.</param>
		/// <param name="userId">UserID returned from the Amazon App Store as a result of a successful purchase.
		/// See the Amazon Documentation at
		/// 'https://developer.amazon.com/public/apis/earn/in-app-purchasing/docs-v2/implementing-iap-2.0'
		/// for more information to on how to access this value from your app.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void RedeemAmazonIap(string key, string receiptId, string userId, Action<RedeemAmazonIapRequest, RedeemAmazonIapResponse> successCallback, Action<RedeemAmazonIapRequest, RedeemAmazonIapError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Redeem Amazon Iap request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new RedeemAmazonIapRequest(key, receiptId, userId, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyRedeemAmazonIapSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyRedeemAmazonIapError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Validate a Receipt from a successful purchase on an Apple device and apply the
		/// rewards to the players account.
		/// </summary>
		///
		/// <param name="key">The key of the real money purchase that defines the rewards to be applied to the
		/// players account on successful verification. The real money purchase should
		/// specify an apple productId that matches the productId of the submitted Receipt.</param>
		/// <param name="receipt">Receipt data returned from the App Store as a result of a successful purchase.
		/// This should be base64 encoded.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void RedeemAppleIap(string key, string receipt, Action<RedeemAppleIapRequest, RedeemAppleIapResponse> successCallback, Action<RedeemAppleIapRequest, RedeemAppleIapError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Redeem Apple Iap request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new RedeemAppleIapRequest(key, receipt, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyRedeemAppleIapSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyRedeemAppleIapError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Validate a Receipt from a successful purchase on a Google device and apply the
		/// rewards to the players account.
		/// </summary>
		///
		/// <param name="key">The key of the real money purchase that defines the rewards to be applied to the
		/// players account on successful verification. The real money purchase should
		/// specify a Google productId that matches the productId of the submitted
		/// PurchaseData.</param>
		/// <param name="purchaseData">A JSON encoded string returned from a successful in app billing purchase. See the
		/// Google Documentation at
		/// 'http://developer.android.com/google/play/billing/billing_integrate.html#Purchase'
		/// on how to access this value from your app.</param>
		/// <param name="purchaseDataSignature">A signature of the PurchaseData returned from a successful in app billing
		/// purchase. See the Google Documentation at
		/// 'http://developer.android.com/google/play/billing/billing_integrate.html#Purchase'
		/// on how to access this value from your app.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void RedeemGoogleIap(string key, string purchaseData, string purchaseDataSignature, Action<RedeemGoogleIapRequest, RedeemGoogleIapResponse> successCallback, Action<RedeemGoogleIapRequest, RedeemGoogleIapError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Redeem Google Iap request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new RedeemGoogleIapRequest(key, purchaseData, purchaseDataSignature, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyRedeemGoogleIapSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyRedeemGoogleIapError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Get the Economy definitions for any Currency Conversion items.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetConversionDefinitions(GetConversionDefinitionsRequestDesc desc, Action<GetConversionDefinitionsRequest, GetConversionDefinitionsResponse> successCallback, Action<GetConversionDefinitionsRequest, GetConversionDefinitionsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Conversion Definitions request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetConversionDefinitionsRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetConversionDefinitionsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetConversionDefinitionsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Get the Economy definitions for any Currency items.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetCurrencyDefinitions(GetCurrencyDefinitionsRequestDesc desc, Action<GetCurrencyDefinitionsRequest, GetCurrencyDefinitionsResponse> successCallback, Action<GetCurrencyDefinitionsRequest, GetCurrencyDefinitionsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Currency Definitions request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetCurrencyDefinitionsRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetCurrencyDefinitionsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetCurrencyDefinitionsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Get the Economy definitions for any Inventory items.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetInventoryDefinitions(GetInventoryDefinitionsRequestDesc desc, Action<GetInventoryDefinitionsRequest, GetInventoryDefinitionsResponse> successCallback, Action<GetInventoryDefinitionsRequest, GetInventoryDefinitionsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Inventory Definitions request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetInventoryDefinitionsRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetInventoryDefinitionsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetInventoryDefinitionsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Get the Economy definitions for any Metadata items.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetMetadataDefinitions(GetMetadataDefinitionsRequestDesc desc, Action<GetMetadataDefinitionsRequest, GetMetadataDefinitionsResponse> successCallback, Action<GetMetadataDefinitionsRequest, GetMetadataDefinitionsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Metadata Definitions request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetMetadataDefinitionsRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetMetadataDefinitionsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetMetadataDefinitionsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Get the Economy definitions for any Real Money Purchase items.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetRealMoneyPurchaseDefinitions(GetRealMoneyPurchaseDefinitionsRequestDesc desc, Action<GetRealMoneyPurchaseDefinitionsRequest, GetRealMoneyPurchaseDefinitionsResponse> successCallback, Action<GetRealMoneyPurchaseDefinitionsRequest, GetRealMoneyPurchaseDefinitionsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Real Money Purchase Definitions request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetRealMoneyPurchaseDefinitionsRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetRealMoneyPurchaseDefinitionsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetRealMoneyPurchaseDefinitionsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Get the Economy definitions for any Virtual Purchase items.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetVirtualPurchaseDefinitions(GetVirtualPurchaseDefinitionsRequestDesc desc, Action<GetVirtualPurchaseDefinitionsRequest, GetVirtualPurchaseDefinitionsResponse> successCallback, Action<GetVirtualPurchaseDefinitionsRequest, GetVirtualPurchaseDefinitionsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Virtual Purchase Definitions request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetVirtualPurchaseDefinitionsRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetVirtualPurchaseDefinitionsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetVirtualPurchaseDefinitionsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Currency Balance request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetCurrencyBalanceSuccess(ServerResponse serverResponse, GetCurrencyBalanceRequest request, Action<GetCurrencyBalanceRequest, GetCurrencyBalanceResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetCurrencyBalance request succeeded.");
	
			GetCurrencyBalanceResponse outputResponse = new GetCurrencyBalanceResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Set Currency Balance request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifySetCurrencyBalanceSuccess(ServerResponse serverResponse, SetCurrencyBalanceRequest request, Action<SetCurrencyBalanceRequest, SetCurrencyBalanceResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("SetCurrencyBalance request succeeded.");
	
			SetCurrencyBalanceResponse outputResponse = new SetCurrencyBalanceResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Convert Currency request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyConvertCurrencySuccess(ServerResponse serverResponse, ConvertCurrencyRequest request, Action<ConvertCurrencyRequest, ConvertCurrencyResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("ConvertCurrency request succeeded.");
	
			ConvertCurrencyResponse outputResponse = new ConvertCurrencyResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Add Currency Balance request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyAddCurrencyBalanceSuccess(ServerResponse serverResponse, AddCurrencyBalanceRequest request, Action<AddCurrencyBalanceRequest, AddCurrencyBalanceResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("AddCurrencyBalance request succeeded.");
	
			AddCurrencyBalanceResponse outputResponse = new AddCurrencyBalanceResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Remove Currency Balance request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyRemoveCurrencyBalanceSuccess(ServerResponse serverResponse, RemoveCurrencyBalanceRequest request, Action<RemoveCurrencyBalanceRequest, RemoveCurrencyBalanceResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("RemoveCurrencyBalance request succeeded.");
	
			RemoveCurrencyBalanceResponse outputResponse = new RemoveCurrencyBalanceResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Inventory request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetInventorySuccess(ServerResponse serverResponse, Action<GetInventoryResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetInventory request succeeded.");
	
			GetInventoryResponse outputResponse = new GetInventoryResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Inventory For Keys request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetInventoryForKeysSuccess(ServerResponse serverResponse, GetInventoryForKeysRequest request, Action<GetInventoryForKeysRequest, GetInventoryForKeysResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetInventoryForKeys request succeeded.");
	
			GetInventoryForKeysResponse outputResponse = new GetInventoryForKeysResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Inventory For Item Ids request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetInventoryForItemIdsSuccess(ServerResponse serverResponse, GetInventoryForItemIdsRequest request, Action<GetInventoryForItemIdsRequest, GetInventoryForItemIdsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetInventoryForItemIds request succeeded.");
	
			GetInventoryForItemIdsResponse outputResponse = new GetInventoryForItemIdsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Add Inventory Item request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyAddInventoryItemSuccess(ServerResponse serverResponse, AddInventoryItemRequest request, Action<AddInventoryItemRequest, AddInventoryItemResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("AddInventoryItem request succeeded.");
	
			AddInventoryItemResponse outputResponse = new AddInventoryItemResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Update Inventory Item request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyUpdateInventoryItemSuccess(ServerResponse serverResponse, UpdateInventoryItemRequest request, Action<UpdateInventoryItemRequest, UpdateInventoryItemResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("UpdateInventoryItem request succeeded.");
	
			UpdateInventoryItemResponse outputResponse = new UpdateInventoryItemResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Remove Inventory Item request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyRemoveInventoryItemSuccess(ServerResponse serverResponse, RemoveInventoryItemRequest request, Action<RemoveInventoryItemRequest> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("RemoveInventoryItem request succeeded.");
	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Make Virtual Purchase request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyMakeVirtualPurchaseSuccess(ServerResponse serverResponse, MakeVirtualPurchaseRequest request, Action<MakeVirtualPurchaseRequest, MakeVirtualPurchaseResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("MakeVirtualPurchase request succeeded.");
	
			MakeVirtualPurchaseResponse outputResponse = new MakeVirtualPurchaseResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Redeem Amazon Iap request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyRedeemAmazonIapSuccess(ServerResponse serverResponse, RedeemAmazonIapRequest request, Action<RedeemAmazonIapRequest, RedeemAmazonIapResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("RedeemAmazonIap request succeeded.");
	
			RedeemAmazonIapResponse outputResponse = new RedeemAmazonIapResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Redeem Apple Iap request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyRedeemAppleIapSuccess(ServerResponse serverResponse, RedeemAppleIapRequest request, Action<RedeemAppleIapRequest, RedeemAppleIapResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("RedeemAppleIap request succeeded.");
	
			RedeemAppleIapResponse outputResponse = new RedeemAppleIapResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Redeem Google Iap request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyRedeemGoogleIapSuccess(ServerResponse serverResponse, RedeemGoogleIapRequest request, Action<RedeemGoogleIapRequest, RedeemGoogleIapResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("RedeemGoogleIap request succeeded.");
	
			RedeemGoogleIapResponse outputResponse = new RedeemGoogleIapResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Conversion Definitions request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetConversionDefinitionsSuccess(ServerResponse serverResponse, GetConversionDefinitionsRequest request, Action<GetConversionDefinitionsRequest, GetConversionDefinitionsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetConversionDefinitions request succeeded.");
	
			GetConversionDefinitionsResponse outputResponse = new GetConversionDefinitionsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Currency Definitions request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetCurrencyDefinitionsSuccess(ServerResponse serverResponse, GetCurrencyDefinitionsRequest request, Action<GetCurrencyDefinitionsRequest, GetCurrencyDefinitionsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetCurrencyDefinitions request succeeded.");
	
			GetCurrencyDefinitionsResponse outputResponse = new GetCurrencyDefinitionsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Inventory Definitions request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetInventoryDefinitionsSuccess(ServerResponse serverResponse, GetInventoryDefinitionsRequest request, Action<GetInventoryDefinitionsRequest, GetInventoryDefinitionsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetInventoryDefinitions request succeeded.");
	
			GetInventoryDefinitionsResponse outputResponse = new GetInventoryDefinitionsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Metadata Definitions request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetMetadataDefinitionsSuccess(ServerResponse serverResponse, GetMetadataDefinitionsRequest request, Action<GetMetadataDefinitionsRequest, GetMetadataDefinitionsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetMetadataDefinitions request succeeded.");
	
			GetMetadataDefinitionsResponse outputResponse = new GetMetadataDefinitionsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Real Money Purchase Definitions request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetRealMoneyPurchaseDefinitionsSuccess(ServerResponse serverResponse, GetRealMoneyPurchaseDefinitionsRequest request, Action<GetRealMoneyPurchaseDefinitionsRequest, GetRealMoneyPurchaseDefinitionsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetRealMoneyPurchaseDefinitions request succeeded.");
	
			GetRealMoneyPurchaseDefinitionsResponse outputResponse = new GetRealMoneyPurchaseDefinitionsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Virtual Purchase Definitions request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetVirtualPurchaseDefinitionsSuccess(ServerResponse serverResponse, GetVirtualPurchaseDefinitionsRequest request, Action<GetVirtualPurchaseDefinitionsRequest, GetVirtualPurchaseDefinitionsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetVirtualPurchaseDefinitions request succeeded.");
	
			GetVirtualPurchaseDefinitionsResponse outputResponse = new GetVirtualPurchaseDefinitionsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Currency Balance request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetCurrencyBalanceError(ServerResponse serverResponse, GetCurrencyBalanceRequest request, Action<GetCurrencyBalanceRequest, GetCurrencyBalanceError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Currency Balance request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Currency Balance request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Currency Balance request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetCurrencyBalanceError error = new GetCurrencyBalanceError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Set Currency Balance request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifySetCurrencyBalanceError(ServerResponse serverResponse, SetCurrencyBalanceRequest request, Action<SetCurrencyBalanceRequest, SetCurrencyBalanceError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Set Currency Balance request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Set Currency Balance request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Set Currency Balance request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			SetCurrencyBalanceError error = new SetCurrencyBalanceError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Convert Currency request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyConvertCurrencyError(ServerResponse serverResponse, ConvertCurrencyRequest request, Action<ConvertCurrencyRequest, ConvertCurrencyError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Convert Currency request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Convert Currency request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Convert Currency request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			ConvertCurrencyError error = new ConvertCurrencyError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Add Currency Balance request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyAddCurrencyBalanceError(ServerResponse serverResponse, AddCurrencyBalanceRequest request, Action<AddCurrencyBalanceRequest, AddCurrencyBalanceError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Add Currency Balance request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Add Currency Balance request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Add Currency Balance request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			AddCurrencyBalanceError error = new AddCurrencyBalanceError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Remove Currency Balance request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyRemoveCurrencyBalanceError(ServerResponse serverResponse, RemoveCurrencyBalanceRequest request, Action<RemoveCurrencyBalanceRequest, RemoveCurrencyBalanceError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Remove Currency Balance request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Remove Currency Balance request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Remove Currency Balance request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			RemoveCurrencyBalanceError error = new RemoveCurrencyBalanceError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Inventory request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetInventoryError(ServerResponse serverResponse, Action<GetInventoryError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Inventory request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Inventory request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Inventory request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetInventoryError error = new GetInventoryError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Inventory For Keys request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetInventoryForKeysError(ServerResponse serverResponse, GetInventoryForKeysRequest request, Action<GetInventoryForKeysRequest, GetInventoryForKeysError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Inventory For Keys request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Inventory For Keys request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Inventory For Keys request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetInventoryForKeysError error = new GetInventoryForKeysError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Inventory For Item Ids request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetInventoryForItemIdsError(ServerResponse serverResponse, GetInventoryForItemIdsRequest request, Action<GetInventoryForItemIdsRequest, GetInventoryForItemIdsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Inventory For Item Ids request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Inventory For Item Ids request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Inventory For Item Ids request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetInventoryForItemIdsError error = new GetInventoryForItemIdsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Add Inventory Item request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyAddInventoryItemError(ServerResponse serverResponse, AddInventoryItemRequest request, Action<AddInventoryItemRequest, AddInventoryItemError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Add Inventory Item request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Add Inventory Item request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Add Inventory Item request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			AddInventoryItemError error = new AddInventoryItemError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Update Inventory Item request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyUpdateInventoryItemError(ServerResponse serverResponse, UpdateInventoryItemRequest request, Action<UpdateInventoryItemRequest, UpdateInventoryItemError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Update Inventory Item request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Update Inventory Item request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Update Inventory Item request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			UpdateInventoryItemError error = new UpdateInventoryItemError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Remove Inventory Item request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyRemoveInventoryItemError(ServerResponse serverResponse, RemoveInventoryItemRequest request, Action<RemoveInventoryItemRequest, RemoveInventoryItemError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Remove Inventory Item request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Remove Inventory Item request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Remove Inventory Item request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			RemoveInventoryItemError error = new RemoveInventoryItemError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Make Virtual Purchase request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyMakeVirtualPurchaseError(ServerResponse serverResponse, MakeVirtualPurchaseRequest request, Action<MakeVirtualPurchaseRequest, MakeVirtualPurchaseError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Make Virtual Purchase request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Make Virtual Purchase request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Make Virtual Purchase request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			MakeVirtualPurchaseError error = new MakeVirtualPurchaseError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Redeem Amazon Iap request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyRedeemAmazonIapError(ServerResponse serverResponse, RedeemAmazonIapRequest request, Action<RedeemAmazonIapRequest, RedeemAmazonIapError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Redeem Amazon Iap request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Redeem Amazon Iap request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Redeem Amazon Iap request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			RedeemAmazonIapError error = new RedeemAmazonIapError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Redeem Apple Iap request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyRedeemAppleIapError(ServerResponse serverResponse, RedeemAppleIapRequest request, Action<RedeemAppleIapRequest, RedeemAppleIapError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Redeem Apple Iap request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Redeem Apple Iap request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Redeem Apple Iap request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			RedeemAppleIapError error = new RedeemAppleIapError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Redeem Google Iap request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyRedeemGoogleIapError(ServerResponse serverResponse, RedeemGoogleIapRequest request, Action<RedeemGoogleIapRequest, RedeemGoogleIapError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Redeem Google Iap request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Redeem Google Iap request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Redeem Google Iap request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			RedeemGoogleIapError error = new RedeemGoogleIapError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Conversion Definitions request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetConversionDefinitionsError(ServerResponse serverResponse, GetConversionDefinitionsRequest request, Action<GetConversionDefinitionsRequest, GetConversionDefinitionsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Conversion Definitions request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Conversion Definitions request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Conversion Definitions request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetConversionDefinitionsError error = new GetConversionDefinitionsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Currency Definitions request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetCurrencyDefinitionsError(ServerResponse serverResponse, GetCurrencyDefinitionsRequest request, Action<GetCurrencyDefinitionsRequest, GetCurrencyDefinitionsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Currency Definitions request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Currency Definitions request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Currency Definitions request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetCurrencyDefinitionsError error = new GetCurrencyDefinitionsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Inventory Definitions request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetInventoryDefinitionsError(ServerResponse serverResponse, GetInventoryDefinitionsRequest request, Action<GetInventoryDefinitionsRequest, GetInventoryDefinitionsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Inventory Definitions request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Inventory Definitions request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Inventory Definitions request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetInventoryDefinitionsError error = new GetInventoryDefinitionsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Metadata Definitions request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetMetadataDefinitionsError(ServerResponse serverResponse, GetMetadataDefinitionsRequest request, Action<GetMetadataDefinitionsRequest, GetMetadataDefinitionsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Metadata Definitions request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Metadata Definitions request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Metadata Definitions request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetMetadataDefinitionsError error = new GetMetadataDefinitionsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Real Money Purchase Definitions request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetRealMoneyPurchaseDefinitionsError(ServerResponse serverResponse, GetRealMoneyPurchaseDefinitionsRequest request, Action<GetRealMoneyPurchaseDefinitionsRequest, GetRealMoneyPurchaseDefinitionsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Real Money Purchase Definitions request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Real Money Purchase Definitions request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Real Money Purchase Definitions request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetRealMoneyPurchaseDefinitionsError error = new GetRealMoneyPurchaseDefinitionsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Virtual Purchase Definitions request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetVirtualPurchaseDefinitionsError(ServerResponse serverResponse, GetVirtualPurchaseDefinitionsRequest request, Action<GetVirtualPurchaseDefinitionsRequest, GetVirtualPurchaseDefinitionsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Virtual Purchase Definitions request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Virtual Purchase Definitions request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Virtual Purchase Definitions request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetVirtualPurchaseDefinitionsError error = new GetVirtualPurchaseDefinitionsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
	}
}
