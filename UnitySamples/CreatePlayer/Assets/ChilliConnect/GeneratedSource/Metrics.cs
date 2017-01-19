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
	/// <para>The ChillConnect Metrics module. This provides the means to log metrics events
	/// with the server.</para>
	///
	/// <para>This is thread-safe.</para>
	/// </summary>
	public sealed class Metrics
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
		public Metrics(Logging logging, TaskScheduler taskScheduler, ServerRequestSystem serverRequestSystem, DataStore dataStore)
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
		/// Generates a universally unique identifier (UUID) that can be persisted locally on
		/// a device and used to identify a player on subsequent calls to StartSession.
		/// </summary>
		///
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GenerateUuid(Action<GenerateUuidResponse> successCallback, Action<GenerateUuidError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Generate Uuid request.");
			
            var gameToken = m_dataStore.GetString("AppToken");
			var request = new GenerateUuidRequest(gameToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGenerateUuidSuccess(serverResponse, successCallback);
				} 
				else 
				{
					NotifyGenerateUuidError(serverResponse, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Registers the start of a new session with the metrics platforms. Sessions are
		/// used when calculating DAU, WAU, MAU and Retention Metrics. On successfully
		/// starting a session, a Metrics-Access-Token value will be returned. This should
		/// then be used on subsequent calls to register custom events within the session, as
		/// well as closing the session.
		/// </summary>
		///
		/// <param name="userId">ID that uniquely identifies this player. This ID should not clash with any other
		/// player and should persist across Sessions.</param>
		/// <param name="appVersion">The version of your game from which the Session was started.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void StartSession(string userId, string appVersion, Action<StartSessionRequest> successCallback, Action<StartSessionRequest, StartSessionError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Start Session request.");
			
            var gameToken = m_dataStore.GetString("AppToken");
			var request = new StartSessionRequest(userId, appVersion, gameToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyStartSessionSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyStartSessionError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Records a custom metrics event that occured within the context of a session. The
		/// behaviour of this method is identical to AddEvents method, with the exception
		/// that the request format accepts a single Event json object rather than an array.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void AddEvent(AddEventRequestDesc desc, Action<AddEventRequest> successCallback, Action<AddEventRequest, AddEventError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Add Event request.");
			
            var metricsAccessToken = m_dataStore.GetString("MetricsAccessToken");
			var request = new AddEventRequest(desc, metricsAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyAddEventSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyAddEventError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Records one or more custom metrics event that occurred within the context of a
		/// session. The posted body to this method should be a json encoded array of
		/// individual custom events. Events are validated against the custom event
		/// definitions created within the ChilliConnect dashboard. If any events are
		/// invalid, the request will not be processed and an InvalidRequest response
		/// returned. The data property of the response will contain a JSON structure that
		/// indicates the number of events successfully processed as well as the number
		/// failed in addition to specific error messages for each failed event as well as
		/// that events index within the original upload. If the provided events are valid,
		/// an empty json object will be returned.
		/// </summary>
		///
		/// <param name="events">An array of events.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void AddEvents(IList<MetricsEvent> events, Action<AddEventsRequest> successCallback, Action<AddEventsRequest, AddEventsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Add Events request.");
			
            var metricsAccessToken = m_dataStore.GetString("MetricsAccessToken");
			var request = new AddEventsRequest(events, metricsAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyAddEventsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyAddEventsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Records a successfully completed IAP transaction.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void AddIapEvent(AddIapEventRequestDesc desc, Action<AddIapEventRequest> successCallback, Action<AddIapEventRequest, AddIapEventError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Add Iap Event request.");
			
            var metricsAccessToken = m_dataStore.GetString("MetricsAccessToken");
			var request = new AddIapEventRequest(desc, metricsAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyAddIapEventSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyAddIapEventError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Closes a session previously opened with a call to StartSession. On successful
		/// close an empty response with a HTTP code of 200 is returned. No request body is
		/// expected.
		/// </summary>
		///
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void EndSession(Action successCallback, Action<EndSessionError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending End Session request.");
			
            var metricsAccessToken = m_dataStore.GetString("MetricsAccessToken");
			var request = new EndSessionRequest(metricsAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyEndSessionSuccess(serverResponse, successCallback);
				} 
				else 
				{
					NotifyEndSessionError(serverResponse, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Notifies the user that a Generate Uuid request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGenerateUuidSuccess(ServerResponse serverResponse, Action<GenerateUuidResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GenerateUuid request succeeded.");
	
			GenerateUuidResponse outputResponse = new GenerateUuidResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Start Session request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyStartSessionSuccess(ServerResponse serverResponse, StartSessionRequest request, Action<StartSessionRequest> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("StartSession request succeeded.");
	
            var metricsAccessToken = serverResponse.Body["MetricsAccessToken"] as string;
            ReleaseAssert.IsNotNull(metricsAccessToken, "Data Store property cannot be null.");
            m_dataStore.Set("MetricsAccessToken", metricsAccessToken);
        
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Add Event request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyAddEventSuccess(ServerResponse serverResponse, AddEventRequest request, Action<AddEventRequest> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("AddEvent request succeeded.");
	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Add Events request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyAddEventsSuccess(ServerResponse serverResponse, AddEventsRequest request, Action<AddEventsRequest> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("AddEvents request succeeded.");
	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Add Iap Event request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyAddIapEventSuccess(ServerResponse serverResponse, AddIapEventRequest request, Action<AddIapEventRequest> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("AddIapEvent request succeeded.");
	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request);
			});
		}
		
		/// <summary>
		/// Notifies the user that a End Session request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyEndSessionSuccess(ServerResponse serverResponse, Action successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("EndSession request succeeded.");
	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback();
			});
		}
		
		/// <summary>
		/// Notifies the user that a Generate Uuid request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGenerateUuidError(ServerResponse serverResponse, Action<GenerateUuidError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Generate Uuid request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Generate Uuid request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Generate Uuid request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GenerateUuidError error = new GenerateUuidError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Start Session request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyStartSessionError(ServerResponse serverResponse, StartSessionRequest request, Action<StartSessionRequest, StartSessionError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Start Session request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Start Session request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Start Session request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			StartSessionError error = new StartSessionError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Add Event request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyAddEventError(ServerResponse serverResponse, AddEventRequest request, Action<AddEventRequest, AddEventError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Add Event request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Add Event request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Add Event request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			AddEventError error = new AddEventError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Add Events request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyAddEventsError(ServerResponse serverResponse, AddEventsRequest request, Action<AddEventsRequest, AddEventsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Add Events request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Add Events request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Add Events request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			AddEventsError error = new AddEventsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Add Iap Event request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyAddIapEventError(ServerResponse serverResponse, AddIapEventRequest request, Action<AddIapEventRequest, AddIapEventError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Add Iap Event request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Add Iap Event request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Add Iap Event request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			AddIapEventError error = new AddIapEventError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a End Session request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyEndSessionError(ServerResponse serverResponse, Action<EndSessionError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("End Session request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("End Session request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("End Session request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			EndSessionError error = new EndSessionError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(error);
			});	
		}
	}
}
