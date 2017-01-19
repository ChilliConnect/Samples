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
	}
}
