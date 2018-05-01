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
	/// <para>The ChillConnect Catalog Management module. Provides the means to retrieve
	/// catalog definitions.</para>
	///
	/// <para>This is thread-safe.</para>
	/// </summary>
	public sealed class Catalog
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
		public Catalog(Logging logging, TaskScheduler taskScheduler, ServerRequestSystem serverRequestSystem, DataStore dataStore)
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
		/// Returns details about the Catalog for the currently logged in Player.
		/// </summary>
		///
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetCatalogVersion(Action<GetCatalogVersionResponse> successCallback, Action<GetCatalogVersionError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Catalog Version request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetCatalogVersionRequest(connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetCatalogVersionSuccess(serverResponse, successCallback);
				} 
				else 
				{
					NotifyGetCatalogVersionError(serverResponse, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Get a Catalog Definition Package along with details of the contained files and
		/// types.
		/// </summary>
		///
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetDefinitionsPackage(Action<GetDefinitionsPackageResponse> successCallback, Action<GetDefinitionsPackageError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Definitions Package request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetDefinitionsPackageRequest(connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetDefinitionsPackageSuccess(serverResponse, successCallback);
				} 
				else 
				{
					NotifyGetDefinitionsPackageError(serverResponse, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Get the Catalog definitions for any Currency Conversion items.
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
		/// Get the Catalog definitions for any Currency items.
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
		/// Get the Catalog definitions for any Inventory items.
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
		/// Get the Catalog definitions for any Metadata items.
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
		/// Get the Catalog definitions for any Real Money Purchase items.
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
		/// Get the Catalog definitions for any Virtual Purchase items.
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
		/// Get the Catalog definitions for any ZipPackage items.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetZipPackageDefinitions(GetZipPackageDefinitionsRequestDesc desc, Action<GetZipPackageDefinitionsRequest, GetZipPackageDefinitionsResponse> successCallback, Action<GetZipPackageDefinitionsRequest, GetZipPackageDefinitionsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Zip Package Definitions request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetZipPackageDefinitionsRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetZipPackageDefinitionsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetZipPackageDefinitionsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Get the custom Catalog definitions.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetCustomDefinitions(GetCustomDefinitionsRequestDesc desc, Action<GetCustomDefinitionsRequest, GetCustomDefinitionsResponse> successCallback, Action<GetCustomDefinitionsRequest, GetCustomDefinitionsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Custom Definitions request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetCustomDefinitionsRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetCustomDefinitionsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetCustomDefinitionsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Catalog Version request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetCatalogVersionSuccess(ServerResponse serverResponse, Action<GetCatalogVersionResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetCatalogVersion request succeeded.");
	
			GetCatalogVersionResponse outputResponse = new GetCatalogVersionResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Definitions Package request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetDefinitionsPackageSuccess(ServerResponse serverResponse, Action<GetDefinitionsPackageResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetDefinitionsPackage request succeeded.");
	
			GetDefinitionsPackageResponse outputResponse = new GetDefinitionsPackageResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(outputResponse);
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
		/// Notifies the user that a Get Zip Package Definitions request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetZipPackageDefinitionsSuccess(ServerResponse serverResponse, GetZipPackageDefinitionsRequest request, Action<GetZipPackageDefinitionsRequest, GetZipPackageDefinitionsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetZipPackageDefinitions request succeeded.");
	
			GetZipPackageDefinitionsResponse outputResponse = new GetZipPackageDefinitionsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Custom Definitions request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetCustomDefinitionsSuccess(ServerResponse serverResponse, GetCustomDefinitionsRequest request, Action<GetCustomDefinitionsRequest, GetCustomDefinitionsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetCustomDefinitions request succeeded.");
	
			GetCustomDefinitionsResponse outputResponse = new GetCustomDefinitionsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Catalog Version request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetCatalogVersionError(ServerResponse serverResponse, Action<GetCatalogVersionError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Catalog Version request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Catalog Version request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Catalog Version request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetCatalogVersionError error = new GetCatalogVersionError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Definitions Package request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetDefinitionsPackageError(ServerResponse serverResponse, Action<GetDefinitionsPackageError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Definitions Package request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Definitions Package request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Definitions Package request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetDefinitionsPackageError error = new GetDefinitionsPackageError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(error);
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
		
		/// <summary>
		/// Notifies the user that a Get Zip Package Definitions request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetZipPackageDefinitionsError(ServerResponse serverResponse, GetZipPackageDefinitionsRequest request, Action<GetZipPackageDefinitionsRequest, GetZipPackageDefinitionsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Zip Package Definitions request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Zip Package Definitions request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Zip Package Definitions request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetZipPackageDefinitionsError error = new GetZipPackageDefinitionsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Custom Definitions request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetCustomDefinitionsError(ServerResponse serverResponse, GetCustomDefinitionsRequest request, Action<GetCustomDefinitionsRequest, GetCustomDefinitionsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Custom Definitions request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Custom Definitions request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Custom Definitions request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetCustomDefinitionsError error = new GetCustomDefinitionsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
	}
}
