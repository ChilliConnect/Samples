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
	/// <para>The ChilliConnect Cloud Data module. Provides the means to store custom data
	/// against Player Accounts for retrieval.</para>
	///
	/// <para>This is thread-safe.</para>
	/// </summary>
	public sealed class CloudData
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
		public CloudData(Logging logging, TaskScheduler taskScheduler, ServerRequestSystem serverRequestSystem, DataStore dataStore)
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
		/// Sets the value of a specified Custom Data Key for the currently logged in player.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void SetPlayerData(SetPlayerDataRequestDesc desc, Action<SetPlayerDataRequest, SetPlayerDataResponse> successCallback, Action<SetPlayerDataRequest, SetPlayerDataError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Set Player Data request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new SetPlayerDataRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifySetPlayerDataSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifySetPlayerDataError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Returns the value of a set of specified Custom Data Keys for the currently logged
		/// in player.
		/// </summary>
		///
		/// <param name="keys">The Custom Data Keys for which to retrieve the values of.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetPlayerData(IList<string> keys, Action<GetPlayerDataRequest, GetPlayerDataResponse> successCallback, Action<GetPlayerDataRequest, GetPlayerDataError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Player Data request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetPlayerDataRequest(keys, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetPlayerDataSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetPlayerDataError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Returns the value of a specified Custom Data Key for a list of provided
		/// ChilliConnectIDs.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetPlayerDataForChilliConnectIds(GetPlayerDataForChilliConnectIdsRequestDesc desc, Action<GetPlayerDataForChilliConnectIdsRequest, GetPlayerDataForChilliConnectIdsResponse> successCallback, Action<GetPlayerDataForChilliConnectIdsRequest, GetPlayerDataForChilliConnectIdsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Player Data For Chilli Connect Ids request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetPlayerDataForChilliConnectIdsRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetPlayerDataForChilliConnectIdsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetPlayerDataForChilliConnectIdsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Returns the value of a specified Custom Data Key for all of a players Facebook
		/// Friends.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetPlayerDataForFacebookFriends(GetPlayerDataForFacebookFriendsRequestDesc desc, Action<GetPlayerDataForFacebookFriendsRequest, GetPlayerDataForFacebookFriendsResponse> successCallback, Action<GetPlayerDataForFacebookFriendsRequest, GetPlayerDataForFacebookFriendsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Player Data For Facebook Friends request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetPlayerDataForFacebookFriendsRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetPlayerDataForFacebookFriendsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetPlayerDataForFacebookFriendsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Deletes a specified Custom Data Key.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void DeletePlayerData(DeletePlayerDataRequestDesc desc, Action<DeletePlayerDataRequest> successCallback, Action<DeletePlayerDataRequest, DeletePlayerDataError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Delete Player Data request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new DeletePlayerDataRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyDeletePlayerDataSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyDeletePlayerDataError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Returns a list of Collection Objects identified by their ObjectID.
		/// </summary>
		///
		/// <param name="key">The Collection Key.</param>
		/// <param name="objectIds">The ObjectIDs for which to retrieve the objects of. Maximum 20.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetCollectionObjects(string key, IList<string> objectIds, Action<GetCollectionObjectsRequest, GetCollectionObjectsResponse> successCallback, Action<GetCollectionObjectsRequest, GetCollectionObjectsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Collection Objects request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetCollectionObjectsRequest(key, objectIds, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetCollectionObjectsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetCollectionObjectsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Adds an object to the specified collection.
		/// </summary>
		///
		/// <param name="key">The Collection Key.</param>
		/// <param name="value">The data to be saved. When serialised the maximum size is 400kb.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void AddCollectionObject(string key, MultiTypeValue value, Action<AddCollectionObjectRequest, AddCollectionObjectResponse> successCallback, Action<AddCollectionObjectRequest, AddCollectionObjectError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Add Collection Object request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new AddCollectionObjectRequest(key, value, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyAddCollectionObjectSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyAddCollectionObjectError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Updates an object in the specified collection.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void UpdateCollectionObject(UpdateCollectionObjectRequestDesc desc, Action<UpdateCollectionObjectRequest, UpdateCollectionObjectResponse> successCallback, Action<UpdateCollectionObjectRequest, UpdateCollectionObjectError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Update Collection Object request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new UpdateCollectionObjectRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyUpdateCollectionObjectSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyUpdateCollectionObjectError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Deletes an object in the specified collection.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void DeleteCollectionObject(DeleteCollectionObjectRequestDesc desc, Action<DeleteCollectionObjectRequest> successCallback, Action<DeleteCollectionObjectRequest, DeleteCollectionObjectError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Delete Collection Object request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new DeleteCollectionObjectRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyDeleteCollectionObjectSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyDeleteCollectionObjectError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Returns objects that satisfy the query for a specified collection.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void QueryCollection(QueryCollectionRequestDesc desc, Action<QueryCollectionRequest, QueryCollectionResponse> successCallback, Action<QueryCollectionRequest, QueryCollectionError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Query Collection request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new QueryCollectionRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyQueryCollectionSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyQueryCollectionError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Notifies the user that a Set Player Data request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifySetPlayerDataSuccess(ServerResponse serverResponse, SetPlayerDataRequest request, Action<SetPlayerDataRequest, SetPlayerDataResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("SetPlayerData request succeeded.");
	
			SetPlayerDataResponse outputResponse = new SetPlayerDataResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Player Data request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetPlayerDataSuccess(ServerResponse serverResponse, GetPlayerDataRequest request, Action<GetPlayerDataRequest, GetPlayerDataResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetPlayerData request succeeded.");
	
			GetPlayerDataResponse outputResponse = new GetPlayerDataResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Player Data For Chilli Connect Ids request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetPlayerDataForChilliConnectIdsSuccess(ServerResponse serverResponse, GetPlayerDataForChilliConnectIdsRequest request, Action<GetPlayerDataForChilliConnectIdsRequest, GetPlayerDataForChilliConnectIdsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetPlayerDataForChilliConnectIds request succeeded.");
	
			GetPlayerDataForChilliConnectIdsResponse outputResponse = new GetPlayerDataForChilliConnectIdsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Player Data For Facebook Friends request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetPlayerDataForFacebookFriendsSuccess(ServerResponse serverResponse, GetPlayerDataForFacebookFriendsRequest request, Action<GetPlayerDataForFacebookFriendsRequest, GetPlayerDataForFacebookFriendsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetPlayerDataForFacebookFriends request succeeded.");
	
			GetPlayerDataForFacebookFriendsResponse outputResponse = new GetPlayerDataForFacebookFriendsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Delete Player Data request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyDeletePlayerDataSuccess(ServerResponse serverResponse, DeletePlayerDataRequest request, Action<DeletePlayerDataRequest> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("DeletePlayerData request succeeded.");
	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Collection Objects request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetCollectionObjectsSuccess(ServerResponse serverResponse, GetCollectionObjectsRequest request, Action<GetCollectionObjectsRequest, GetCollectionObjectsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetCollectionObjects request succeeded.");
	
			GetCollectionObjectsResponse outputResponse = new GetCollectionObjectsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Add Collection Object request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyAddCollectionObjectSuccess(ServerResponse serverResponse, AddCollectionObjectRequest request, Action<AddCollectionObjectRequest, AddCollectionObjectResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("AddCollectionObject request succeeded.");
	
			AddCollectionObjectResponse outputResponse = new AddCollectionObjectResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Update Collection Object request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyUpdateCollectionObjectSuccess(ServerResponse serverResponse, UpdateCollectionObjectRequest request, Action<UpdateCollectionObjectRequest, UpdateCollectionObjectResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("UpdateCollectionObject request succeeded.");
	
			UpdateCollectionObjectResponse outputResponse = new UpdateCollectionObjectResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Delete Collection Object request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyDeleteCollectionObjectSuccess(ServerResponse serverResponse, DeleteCollectionObjectRequest request, Action<DeleteCollectionObjectRequest> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("DeleteCollectionObject request succeeded.");
	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Query Collection request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyQueryCollectionSuccess(ServerResponse serverResponse, QueryCollectionRequest request, Action<QueryCollectionRequest, QueryCollectionResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("QueryCollection request succeeded.");
	
			QueryCollectionResponse outputResponse = new QueryCollectionResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Set Player Data request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifySetPlayerDataError(ServerResponse serverResponse, SetPlayerDataRequest request, Action<SetPlayerDataRequest, SetPlayerDataError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Set Player Data request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Set Player Data request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Set Player Data request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			SetPlayerDataError error = new SetPlayerDataError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Player Data request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetPlayerDataError(ServerResponse serverResponse, GetPlayerDataRequest request, Action<GetPlayerDataRequest, GetPlayerDataError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Player Data request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Player Data request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Player Data request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetPlayerDataError error = new GetPlayerDataError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Player Data For Chilli Connect Ids request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetPlayerDataForChilliConnectIdsError(ServerResponse serverResponse, GetPlayerDataForChilliConnectIdsRequest request, Action<GetPlayerDataForChilliConnectIdsRequest, GetPlayerDataForChilliConnectIdsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Player Data For Chilli Connect Ids request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Player Data For Chilli Connect Ids request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Player Data For Chilli Connect Ids request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetPlayerDataForChilliConnectIdsError error = new GetPlayerDataForChilliConnectIdsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Player Data For Facebook Friends request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetPlayerDataForFacebookFriendsError(ServerResponse serverResponse, GetPlayerDataForFacebookFriendsRequest request, Action<GetPlayerDataForFacebookFriendsRequest, GetPlayerDataForFacebookFriendsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Player Data For Facebook Friends request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Player Data For Facebook Friends request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Player Data For Facebook Friends request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetPlayerDataForFacebookFriendsError error = new GetPlayerDataForFacebookFriendsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Delete Player Data request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyDeletePlayerDataError(ServerResponse serverResponse, DeletePlayerDataRequest request, Action<DeletePlayerDataRequest, DeletePlayerDataError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Delete Player Data request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Delete Player Data request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Delete Player Data request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			DeletePlayerDataError error = new DeletePlayerDataError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Collection Objects request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetCollectionObjectsError(ServerResponse serverResponse, GetCollectionObjectsRequest request, Action<GetCollectionObjectsRequest, GetCollectionObjectsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Collection Objects request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Collection Objects request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Collection Objects request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetCollectionObjectsError error = new GetCollectionObjectsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Add Collection Object request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyAddCollectionObjectError(ServerResponse serverResponse, AddCollectionObjectRequest request, Action<AddCollectionObjectRequest, AddCollectionObjectError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Add Collection Object request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Add Collection Object request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Add Collection Object request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			AddCollectionObjectError error = new AddCollectionObjectError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Update Collection Object request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyUpdateCollectionObjectError(ServerResponse serverResponse, UpdateCollectionObjectRequest request, Action<UpdateCollectionObjectRequest, UpdateCollectionObjectError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Update Collection Object request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Update Collection Object request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Update Collection Object request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			UpdateCollectionObjectError error = new UpdateCollectionObjectError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Delete Collection Object request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyDeleteCollectionObjectError(ServerResponse serverResponse, DeleteCollectionObjectRequest request, Action<DeleteCollectionObjectRequest, DeleteCollectionObjectError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Delete Collection Object request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Delete Collection Object request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Delete Collection Object request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			DeleteCollectionObjectError error = new DeleteCollectionObjectError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Query Collection request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyQueryCollectionError(ServerResponse serverResponse, QueryCollectionRequest request, Action<QueryCollectionRequest, QueryCollectionError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Query Collection request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Query Collection request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Query Collection request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			QueryCollectionError error = new QueryCollectionError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
	}
}
