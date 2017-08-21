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
	/// <para>Bring back a list of requested DLC packages along with their contained files.
	/// When a request provides multiple Tags, only packages that have all Tags will be
	/// returned.</para>
	///
	/// <para>This is thread-safe.</para>
	/// </summary>
	public sealed class Dlc
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
		public Dlc(Logging logging, TaskScheduler taskScheduler, ServerRequestSystem serverRequestSystem, DataStore dataStore)
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
		/// Bring back a list of requested DLC packages along with their contained files.
		/// </summary>
		///
		/// <param name="tags">An array list of Tags for the player to search for.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetDlcUsingTags(IList<string> tags, Action<GetDlcUsingTagsRequest, GetDlcUsingTagsResponse> successCallback, Action<GetDlcUsingTagsRequest, GetDlcUsingTagsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Dlc Using Tags request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetDlcUsingTagsRequest(tags, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetDlcUsingTagsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetDlcUsingTagsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Dlc Using Tags request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetDlcUsingTagsSuccess(ServerResponse serverResponse, GetDlcUsingTagsRequest request, Action<GetDlcUsingTagsRequest, GetDlcUsingTagsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetDlcUsingTags request succeeded.");
	
			GetDlcUsingTagsResponse outputResponse = new GetDlcUsingTagsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Dlc Using Tags request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetDlcUsingTagsError(ServerResponse serverResponse, GetDlcUsingTagsRequest request, Action<GetDlcUsingTagsRequest, GetDlcUsingTagsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Dlc Using Tags request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Dlc Using Tags request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Dlc Using Tags request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetDlcUsingTagsError error = new GetDlcUsingTagsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
	}
}
