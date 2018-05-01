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
	/// <para>The ChilliConnect Async Multiplayer module.</para>
	///
	/// <para>This is thread-safe.</para>
	/// </summary>
	public sealed class AsyncMultiplayer
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
		public AsyncMultiplayer(Logging logging, TaskScheduler taskScheduler, ServerRequestSystem serverRequestSystem, DataStore dataStore)
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
		/// Creates a new match.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void CreateMatch(CreateMatchRequestDesc desc, Action<CreateMatchRequest, CreateMatchResponse> successCallback, Action<CreateMatchRequest, CreateMatchError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Create Match request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new CreateMatchRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyCreateMatchSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyCreateMatchError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Search for available (Status: WAITING) matches, and will join the first Match.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void JoinAvailableMatch(JoinAvailableMatchRequestDesc desc, Action<JoinAvailableMatchRequest, JoinAvailableMatchResponse> successCallback, Action<JoinAvailableMatchRequest, JoinAvailableMatchError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Join Available Match request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new JoinAvailableMatchRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyJoinAvailableMatchSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyJoinAvailableMatchError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Search for available (Status: WAITING) matches.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void QueryAvailableMatches(QueryAvailableMatchesRequestDesc desc, Action<QueryAvailableMatchesRequest, QueryAvailableMatchesResponse> successCallback, Action<QueryAvailableMatchesRequest, QueryAvailableMatchesError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Query Available Matches request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new QueryAvailableMatchesRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyQueryAvailableMatchesSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyQueryAvailableMatchesError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Join a Match identified by ID.
		/// </summary>
		///
		/// <param name="matchId">The ID of the Match to join.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void JoinMatch(string matchId, Action<JoinMatchRequest, JoinMatchResponse> successCallback, Action<JoinMatchRequest, JoinMatchError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Join Match request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new JoinMatchRequest(matchId, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyJoinMatchSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyJoinMatchError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Start a Match. A match must have at least one player in order to be started.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void StartMatch(StartMatchRequestDesc desc, Action<StartMatchRequest, StartMatchResponse> successCallback, Action<StartMatchRequest, StartMatchError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Start Match request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new StartMatchRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyStartMatchSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyStartMatchError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Take a Turn in a Match.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void SubmitTurn(SubmitTurnRequestDesc desc, Action<SubmitTurnRequest, SubmitTurnResponse> successCallback, Action<SubmitTurnRequest, SubmitTurnError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Submit Turn request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new SubmitTurnRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifySubmitTurnSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifySubmitTurnError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Retrieve a Match by it's ID.
		/// </summary>
		///
		/// <param name="matchId">The ID of the Match.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetMatch(string matchId, Action<GetMatchRequest, GetMatchResponse> successCallback, Action<GetMatchRequest, GetMatchError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Match request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetMatchRequest(matchId, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetMatchSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetMatchError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Get Turns for a Match.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetMatchTurns(GetMatchTurnsRequestDesc desc, Action<GetMatchTurnsRequest, GetMatchTurnsResponse> successCallback, Action<GetMatchTurnsRequest, GetMatchTurnsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Match Turns request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetMatchTurnsRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetMatchTurnsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetMatchTurnsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Get matches a Player has participated in.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetPlayerMatches(GetPlayerMatchesRequestDesc desc, Action<GetPlayerMatchesRequest, GetPlayerMatchesResponse> successCallback, Action<GetPlayerMatchesRequest, GetPlayerMatchesError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Player Matches request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetPlayerMatchesRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetPlayerMatchesSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetPlayerMatchesError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Update a Match without taking a Turn. Note: Matches updated by taking a Turn
		/// result in replayable Turn history, updates using this method do not. Matches in
		/// States WAITING and IN_PROGRESS are able to be updated using this method.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void UpdateMatch(UpdateMatchRequestDesc desc, Action<UpdateMatchRequest, UpdateMatchResponse> successCallback, Action<UpdateMatchRequest, UpdateMatchError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Update Match request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new UpdateMatchRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyUpdateMatchSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyUpdateMatchError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Get the game MatchType definitions.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetMatchTypeDefinitions(GetMatchTypeDefinitionsRequestDesc desc, Action<GetMatchTypeDefinitionsRequest, GetMatchTypeDefinitionsResponse> successCallback, Action<GetMatchTypeDefinitionsRequest, GetMatchTypeDefinitionsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Match Type Definitions request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetMatchTypeDefinitionsRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetMatchTypeDefinitionsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetMatchTypeDefinitionsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Notifies the user that a Create Match request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyCreateMatchSuccess(ServerResponse serverResponse, CreateMatchRequest request, Action<CreateMatchRequest, CreateMatchResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("CreateMatch request succeeded.");
	
			CreateMatchResponse outputResponse = new CreateMatchResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Join Available Match request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyJoinAvailableMatchSuccess(ServerResponse serverResponse, JoinAvailableMatchRequest request, Action<JoinAvailableMatchRequest, JoinAvailableMatchResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("JoinAvailableMatch request succeeded.");
	
			JoinAvailableMatchResponse outputResponse = new JoinAvailableMatchResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Query Available Matches request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyQueryAvailableMatchesSuccess(ServerResponse serverResponse, QueryAvailableMatchesRequest request, Action<QueryAvailableMatchesRequest, QueryAvailableMatchesResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("QueryAvailableMatches request succeeded.");
	
			QueryAvailableMatchesResponse outputResponse = new QueryAvailableMatchesResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Join Match request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyJoinMatchSuccess(ServerResponse serverResponse, JoinMatchRequest request, Action<JoinMatchRequest, JoinMatchResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("JoinMatch request succeeded.");
	
			JoinMatchResponse outputResponse = new JoinMatchResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Start Match request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyStartMatchSuccess(ServerResponse serverResponse, StartMatchRequest request, Action<StartMatchRequest, StartMatchResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("StartMatch request succeeded.");
	
			StartMatchResponse outputResponse = new StartMatchResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Submit Turn request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifySubmitTurnSuccess(ServerResponse serverResponse, SubmitTurnRequest request, Action<SubmitTurnRequest, SubmitTurnResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("SubmitTurn request succeeded.");
	
			SubmitTurnResponse outputResponse = new SubmitTurnResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Match request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetMatchSuccess(ServerResponse serverResponse, GetMatchRequest request, Action<GetMatchRequest, GetMatchResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetMatch request succeeded.");
	
			GetMatchResponse outputResponse = new GetMatchResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Match Turns request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetMatchTurnsSuccess(ServerResponse serverResponse, GetMatchTurnsRequest request, Action<GetMatchTurnsRequest, GetMatchTurnsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetMatchTurns request succeeded.");
	
			GetMatchTurnsResponse outputResponse = new GetMatchTurnsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Player Matches request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetPlayerMatchesSuccess(ServerResponse serverResponse, GetPlayerMatchesRequest request, Action<GetPlayerMatchesRequest, GetPlayerMatchesResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetPlayerMatches request succeeded.");
	
			GetPlayerMatchesResponse outputResponse = new GetPlayerMatchesResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Update Match request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyUpdateMatchSuccess(ServerResponse serverResponse, UpdateMatchRequest request, Action<UpdateMatchRequest, UpdateMatchResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("UpdateMatch request succeeded.");
	
			UpdateMatchResponse outputResponse = new UpdateMatchResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Match Type Definitions request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetMatchTypeDefinitionsSuccess(ServerResponse serverResponse, GetMatchTypeDefinitionsRequest request, Action<GetMatchTypeDefinitionsRequest, GetMatchTypeDefinitionsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetMatchTypeDefinitions request succeeded.");
	
			GetMatchTypeDefinitionsResponse outputResponse = new GetMatchTypeDefinitionsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Create Match request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyCreateMatchError(ServerResponse serverResponse, CreateMatchRequest request, Action<CreateMatchRequest, CreateMatchError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Create Match request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Create Match request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Create Match request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			CreateMatchError error = new CreateMatchError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Join Available Match request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyJoinAvailableMatchError(ServerResponse serverResponse, JoinAvailableMatchRequest request, Action<JoinAvailableMatchRequest, JoinAvailableMatchError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Join Available Match request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Join Available Match request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Join Available Match request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			JoinAvailableMatchError error = new JoinAvailableMatchError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Query Available Matches request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyQueryAvailableMatchesError(ServerResponse serverResponse, QueryAvailableMatchesRequest request, Action<QueryAvailableMatchesRequest, QueryAvailableMatchesError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Query Available Matches request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Query Available Matches request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Query Available Matches request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			QueryAvailableMatchesError error = new QueryAvailableMatchesError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Join Match request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyJoinMatchError(ServerResponse serverResponse, JoinMatchRequest request, Action<JoinMatchRequest, JoinMatchError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Join Match request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Join Match request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Join Match request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			JoinMatchError error = new JoinMatchError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Start Match request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyStartMatchError(ServerResponse serverResponse, StartMatchRequest request, Action<StartMatchRequest, StartMatchError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Start Match request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Start Match request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Start Match request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			StartMatchError error = new StartMatchError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Submit Turn request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifySubmitTurnError(ServerResponse serverResponse, SubmitTurnRequest request, Action<SubmitTurnRequest, SubmitTurnError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Submit Turn request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Submit Turn request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Submit Turn request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			SubmitTurnError error = new SubmitTurnError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Match request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetMatchError(ServerResponse serverResponse, GetMatchRequest request, Action<GetMatchRequest, GetMatchError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Match request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Match request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Match request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetMatchError error = new GetMatchError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Match Turns request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetMatchTurnsError(ServerResponse serverResponse, GetMatchTurnsRequest request, Action<GetMatchTurnsRequest, GetMatchTurnsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Match Turns request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Match Turns request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Match Turns request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetMatchTurnsError error = new GetMatchTurnsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Player Matches request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetPlayerMatchesError(ServerResponse serverResponse, GetPlayerMatchesRequest request, Action<GetPlayerMatchesRequest, GetPlayerMatchesError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Player Matches request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Player Matches request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Player Matches request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetPlayerMatchesError error = new GetPlayerMatchesError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Update Match request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyUpdateMatchError(ServerResponse serverResponse, UpdateMatchRequest request, Action<UpdateMatchRequest, UpdateMatchError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Update Match request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Update Match request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Update Match request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			UpdateMatchError error = new UpdateMatchError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Match Type Definitions request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetMatchTypeDefinitionsError(ServerResponse serverResponse, GetMatchTypeDefinitionsRequest request, Action<GetMatchTypeDefinitionsRequest, GetMatchTypeDefinitionsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Match Type Definitions request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Match Type Definitions request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Match Type Definitions request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetMatchTypeDefinitionsError error = new GetMatchTypeDefinitionsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
	}
}
