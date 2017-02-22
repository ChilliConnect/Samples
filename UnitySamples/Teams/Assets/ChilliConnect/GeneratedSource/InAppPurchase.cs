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
	/// <para>The ChillConnect In-App Purchase Validation module. Provides the means to
	/// validate in-app purchases using Amazon Receipt Validation Service, Apple AppStore
	/// and Google Play Store.</para>
	///
	/// <para>This is thread-safe.</para>
	/// </summary>
	public sealed class InAppPurchase
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
		public InAppPurchase(Logging logging, TaskScheduler taskScheduler, ServerRequestSystem serverRequestSystem, DataStore dataStore)
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
		/// Validate a Receipt from a successful purchase on an Amazon device.
		/// </summary>
		///
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
		public void ValidateAmazonIap(string receiptId, string userId, Action<ValidateAmazonIapRequest, ValidateAmazonIapResponse> successCallback, Action<ValidateAmazonIapRequest, ValidateAmazonIapError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Validate Amazon Iap request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new ValidateAmazonIapRequest(receiptId, userId, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyValidateAmazonIapSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyValidateAmazonIapError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Validate a Receipt from a successful purchase on an Apple device.
		/// </summary>
		///
		/// <param name="receipt">Receipt data returned from the App Store as a result of a successful purchase.
		/// This should be <code>base64</code> encoded.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void ValidateAppleIap(string receipt, Action<ValidateAppleIapRequest, ValidateAppleIapResponse> successCallback, Action<ValidateAppleIapRequest, ValidateAppleIapError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Validate Apple Iap request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new ValidateAppleIapRequest(receipt, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyValidateAppleIapSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyValidateAppleIapError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Validate a Receipt from a successful purchase on a Google device.
		/// </summary>
		///
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
		public void ValidateGoogleIap(string purchaseData, string purchaseDataSignature, Action<ValidateGoogleIapRequest, ValidateGoogleIapResponse> successCallback, Action<ValidateGoogleIapRequest, ValidateGoogleIapError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Validate Google Iap request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new ValidateGoogleIapRequest(purchaseData, purchaseDataSignature, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyValidateGoogleIapSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyValidateGoogleIapError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Notifies the user that a Validate Amazon Iap request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyValidateAmazonIapSuccess(ServerResponse serverResponse, ValidateAmazonIapRequest request, Action<ValidateAmazonIapRequest, ValidateAmazonIapResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("ValidateAmazonIap request succeeded.");
	
			ValidateAmazonIapResponse outputResponse = new ValidateAmazonIapResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Validate Apple Iap request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyValidateAppleIapSuccess(ServerResponse serverResponse, ValidateAppleIapRequest request, Action<ValidateAppleIapRequest, ValidateAppleIapResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("ValidateAppleIap request succeeded.");
	
			ValidateAppleIapResponse outputResponse = new ValidateAppleIapResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Validate Google Iap request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyValidateGoogleIapSuccess(ServerResponse serverResponse, ValidateGoogleIapRequest request, Action<ValidateGoogleIapRequest, ValidateGoogleIapResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("ValidateGoogleIap request succeeded.");
	
			ValidateGoogleIapResponse outputResponse = new ValidateGoogleIapResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Validate Amazon Iap request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyValidateAmazonIapError(ServerResponse serverResponse, ValidateAmazonIapRequest request, Action<ValidateAmazonIapRequest, ValidateAmazonIapError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Validate Amazon Iap request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Validate Amazon Iap request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Validate Amazon Iap request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			ValidateAmazonIapError error = new ValidateAmazonIapError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Validate Apple Iap request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyValidateAppleIapError(ServerResponse serverResponse, ValidateAppleIapRequest request, Action<ValidateAppleIapRequest, ValidateAppleIapError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Validate Apple Iap request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Validate Apple Iap request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Validate Apple Iap request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			ValidateAppleIapError error = new ValidateAppleIapError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Validate Google Iap request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyValidateGoogleIapError(ServerResponse serverResponse, ValidateGoogleIapRequest request, Action<ValidateGoogleIapRequest, ValidateGoogleIapError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Validate Google Iap request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Validate Google Iap request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Validate Google Iap request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			ValidateGoogleIapError error = new ValidateGoogleIapError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
	}
}
