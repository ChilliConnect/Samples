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
	/// <para>The ChillConnect Push Notifications module. Provides the means to send push
	/// messages to players using Amazon Device Messaging, Apple Push Notification
	/// Service and Google Cloud Messaging.</para>
	///
	/// <para>This is thread-safe.</para>
	/// </summary>
	public sealed class PushNotifications
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
		public PushNotifications(Logging logging, TaskScheduler taskScheduler, ServerRequestSystem serverRequestSystem, DataStore dataStore)
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
		/// Registers a Device Push Token for the currently logged in ChilliConnect player
		/// for a particular Push Notification Service. On success, returns an empty JSON
		/// object.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void RegisterToken(RegisterTokenRequestDesc desc, Action<RegisterTokenRequest> successCallback, Action<RegisterTokenRequest, RegisterTokenError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Register Token request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new RegisterTokenRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyRegisterTokenSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyRegisterTokenError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// UnRegister a Push Token previously registered. If the Push Token has not been
		/// registered with the currently logged in Player, the request is ignored.
		/// </summary>
		///
		/// <param name="service">The push notification service the device token belongs to. Must be one of APNS,
		/// GCM or ADM.</param>
		/// <param name="deviceToken">The Push Token. Note: for APNS (iOS) base64 encode the NSData object to form this
		/// string.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void UnregisterToken(string service, string deviceToken, Action<UnregisterTokenRequest> successCallback, Action<UnregisterTokenRequest, UnregisterTokenError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Unregister Token request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new UnregisterTokenRequest(service, deviceToken, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyUnregisterTokenSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyUnregisterTokenError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Set the Push Groups for the current ChilliConnect Player. Push Groups allow Mass
		/// Push Notifications to be targeted at a specific subset of Players. Setting a
		/// Players Push Groups will overwrite any previously set Push Groups.
		/// </summary>
		///
		/// <param name="groups">A list of Push Groups that the player belongs to, up to a maximum of 10.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void SetPushGroups(IList<string> groups, Action<SetPushGroupsRequest> successCallback, Action<SetPushGroupsRequest, SetPushGroupsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Set Push Groups request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new SetPushGroupsRequest(groups, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifySetPushGroupsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifySetPushGroupsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Notifies the user that a Register Token request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyRegisterTokenSuccess(ServerResponse serverResponse, RegisterTokenRequest request, Action<RegisterTokenRequest> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("RegisterToken request succeeded.");
	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Unregister Token request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyUnregisterTokenSuccess(ServerResponse serverResponse, UnregisterTokenRequest request, Action<UnregisterTokenRequest> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("UnregisterToken request succeeded.");
	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Set Push Groups request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifySetPushGroupsSuccess(ServerResponse serverResponse, SetPushGroupsRequest request, Action<SetPushGroupsRequest> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("SetPushGroups request succeeded.");
	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Register Token request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyRegisterTokenError(ServerResponse serverResponse, RegisterTokenRequest request, Action<RegisterTokenRequest, RegisterTokenError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Register Token request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Register Token request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Register Token request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			RegisterTokenError error = new RegisterTokenError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Unregister Token request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyUnregisterTokenError(ServerResponse serverResponse, UnregisterTokenRequest request, Action<UnregisterTokenRequest, UnregisterTokenError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Unregister Token request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Unregister Token request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Unregister Token request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			UnregisterTokenError error = new UnregisterTokenError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Set Push Groups request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifySetPushGroupsError(ServerResponse serverResponse, SetPushGroupsRequest request, Action<SetPushGroupsRequest, SetPushGroupsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Set Push Groups request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Set Push Groups request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Set Push Groups request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			SetPushGroupsError error = new SetPushGroupsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
	}
}
