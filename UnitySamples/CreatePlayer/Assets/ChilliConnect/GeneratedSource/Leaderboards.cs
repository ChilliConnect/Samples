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
	/// <para>The ChillConnect Leaderboards module. Provides the means to add to and query from
	/// leaderboards.</para>
	///
	/// <para>This is thread-safe.</para>
	/// </summary>
	public sealed class Leaderboards
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
		public Leaderboards(Logging logging, TaskScheduler taskScheduler, ServerRequestSystem serverRequestSystem, DataStore dataStore)
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
		/// Adds a score to a leaderboard for the currently logged in player.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void AddScore(AddScoreRequestDesc desc, Action<AddScoreRequest, AddScoreResponse> successCallback, Action<AddScoreRequest, AddScoreError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Add Score request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new AddScoreRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyAddScoreSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyAddScoreError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Retrieve the currently logged in player's score and rank for a given leaderboard.
		/// </summary>
		///
		/// <param name="key">The Key that identifies the leaderboard.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetPlayerScore(string key, Action<GetPlayerScoreRequest, GetPlayerScoreResponse> successCallback, Action<GetPlayerScoreRequest, GetPlayerScoreError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Player Score request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetPlayerScoreRequest(key, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetPlayerScoreSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetPlayerScoreError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Retrieve paged scores for a given leaderboard around the currently logged in
		/// player. The response is the same as the GetScores method, but with additional
		/// fields populated at the top level of the response to indicate the player's
		/// position in the global leaderboard and also their index in the returned Scores
		/// array.
		/// </summary>
		///
		/// <param name="key">The Key that identifies the leaderboard.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetScoresAroundPlayer(string key, Action<GetScoresAroundPlayerRequest, GetScoresAroundPlayerResponse> successCallback, Action<GetScoresAroundPlayerRequest, GetScoresAroundPlayerError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Scores Around Player request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetScoresAroundPlayerRequest(key, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetScoresAroundPlayerSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetScoresAroundPlayerError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Retrieve scores and ranks for a provided list of ChilliConnectIDs. For each
		/// player that has a score in the provided leaderboard, their global rank will be
		/// returned along with their local ranking within the returned scores.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetScoresForChilliConnectIds(GetScoresForChilliConnectIdsRequestDesc desc, Action<GetScoresForChilliConnectIdsRequest, GetScoresForChilliConnectIdsResponse> successCallback, Action<GetScoresForChilliConnectIdsRequest, GetScoresForChilliConnectIdsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Scores For Chilli Connect Ids request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetScoresForChilliConnectIdsRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetScoresForChilliConnectIdsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetScoresForChilliConnectIdsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Retrieve scores for each of the currently logged in player's facebook friends.
		/// Friends are retrieved from Facebook using the AccessToken provided on the players
		/// last succesful login to Facebook. Returns an array of objects for each player
		/// with a score posted on the provided leaderboard.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetScoresForFacebookFriends(GetScoresForFacebookFriendsRequestDesc desc, Action<GetScoresForFacebookFriendsRequest, GetScoresForFacebookFriendsResponse> successCallback, Action<GetScoresForFacebookFriendsRequest, GetScoresForFacebookFriendsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Scores For Facebook Friends request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetScoresForFacebookFriendsRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetScoresForFacebookFriendsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetScoresForFacebookFriendsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Retrieve paged scores for a given leaderboard.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetScores(GetScoresRequestDesc desc, Action<GetScoresRequest, GetScoresResponse> successCallback, Action<GetScoresRequest, GetScoresError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Scores request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetScoresRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetScoresSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetScoresError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Notifies the user that a Add Score request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyAddScoreSuccess(ServerResponse serverResponse, AddScoreRequest request, Action<AddScoreRequest, AddScoreResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("AddScore request succeeded.");
	
			AddScoreResponse outputResponse = new AddScoreResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Player Score request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetPlayerScoreSuccess(ServerResponse serverResponse, GetPlayerScoreRequest request, Action<GetPlayerScoreRequest, GetPlayerScoreResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetPlayerScore request succeeded.");
	
			GetPlayerScoreResponse outputResponse = new GetPlayerScoreResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Scores Around Player request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetScoresAroundPlayerSuccess(ServerResponse serverResponse, GetScoresAroundPlayerRequest request, Action<GetScoresAroundPlayerRequest, GetScoresAroundPlayerResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetScoresAroundPlayer request succeeded.");
	
			GetScoresAroundPlayerResponse outputResponse = new GetScoresAroundPlayerResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Scores For Chilli Connect Ids request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetScoresForChilliConnectIdsSuccess(ServerResponse serverResponse, GetScoresForChilliConnectIdsRequest request, Action<GetScoresForChilliConnectIdsRequest, GetScoresForChilliConnectIdsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetScoresForChilliConnectIds request succeeded.");
	
			GetScoresForChilliConnectIdsResponse outputResponse = new GetScoresForChilliConnectIdsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Scores For Facebook Friends request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetScoresForFacebookFriendsSuccess(ServerResponse serverResponse, GetScoresForFacebookFriendsRequest request, Action<GetScoresForFacebookFriendsRequest, GetScoresForFacebookFriendsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetScoresForFacebookFriends request succeeded.");
	
			GetScoresForFacebookFriendsResponse outputResponse = new GetScoresForFacebookFriendsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Scores request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetScoresSuccess(ServerResponse serverResponse, GetScoresRequest request, Action<GetScoresRequest, GetScoresResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetScores request succeeded.");
	
			GetScoresResponse outputResponse = new GetScoresResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Add Score request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyAddScoreError(ServerResponse serverResponse, AddScoreRequest request, Action<AddScoreRequest, AddScoreError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Add Score request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Add Score request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Add Score request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			AddScoreError error = new AddScoreError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Player Score request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetPlayerScoreError(ServerResponse serverResponse, GetPlayerScoreRequest request, Action<GetPlayerScoreRequest, GetPlayerScoreError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Player Score request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Player Score request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Player Score request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetPlayerScoreError error = new GetPlayerScoreError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Scores Around Player request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetScoresAroundPlayerError(ServerResponse serverResponse, GetScoresAroundPlayerRequest request, Action<GetScoresAroundPlayerRequest, GetScoresAroundPlayerError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Scores Around Player request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Scores Around Player request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Scores Around Player request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetScoresAroundPlayerError error = new GetScoresAroundPlayerError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Scores For Chilli Connect Ids request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetScoresForChilliConnectIdsError(ServerResponse serverResponse, GetScoresForChilliConnectIdsRequest request, Action<GetScoresForChilliConnectIdsRequest, GetScoresForChilliConnectIdsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Scores For Chilli Connect Ids request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Scores For Chilli Connect Ids request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Scores For Chilli Connect Ids request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetScoresForChilliConnectIdsError error = new GetScoresForChilliConnectIdsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Scores For Facebook Friends request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetScoresForFacebookFriendsError(ServerResponse serverResponse, GetScoresForFacebookFriendsRequest request, Action<GetScoresForFacebookFriendsRequest, GetScoresForFacebookFriendsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Scores For Facebook Friends request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Scores For Facebook Friends request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Scores For Facebook Friends request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetScoresForFacebookFriendsError error = new GetScoresForFacebookFriendsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Scores request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetScoresError(ServerResponse serverResponse, GetScoresRequest request, Action<GetScoresRequest, GetScoresError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Scores request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Scores request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Scores request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetScoresError error = new GetScoresError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
	}
}
