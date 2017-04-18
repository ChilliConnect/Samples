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
	/// <para>The ChilliConnect Player Accounts module. Provides the means to create new
	/// players, log in to existing accounts and modify account data.</para>
	///
	/// <para>This is thread-safe.</para>
	/// </summary>
	public sealed class PlayerAccounts
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
		public PlayerAccounts(Logging logging, TaskScheduler taskScheduler, ServerRequestSystem serverRequestSystem, DataStore dataStore)
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
		/// Creates a new, anonymous ChilliConnect player account for a specific game.
		/// UserName, DisplayName, Email and Password details can be provided but are not
		/// required. Will return a ChilliConnectID and ChilliConnectSecret that uniquely
		/// identifies the newly created player. These details can be used to login to the
		/// players account via the LogInUsingChilliConnect method.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void CreatePlayer(CreatePlayerRequestDesc desc, Action<CreatePlayerRequest, CreatePlayerResponse> successCallback, Action<CreatePlayerRequest, CreatePlayerError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Create Player request.");
			
            var gameToken = m_dataStore.GetString("AppToken");
			var request = new CreatePlayerRequest(desc, gameToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyCreatePlayerSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyCreatePlayerError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Login to the system using a ChilliConnectID and a ChilliConnectSecret. Returns an
		/// ConnectAccessToken that is tied to the player and should be used to authenticate
		/// on subsequent requests.
		/// </summary>
		///
		/// <param name="chilliConnectId">The player's ChilliConnectID.</param>
		/// <param name="chilliConnectSecret">The player's ChilliConnectSecret.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void LogInUsingChilliConnect(string chilliConnectId, string chilliConnectSecret, Action<LogInUsingChilliConnectRequest> successCallback, Action<LogInUsingChilliConnectRequest, LogInUsingChilliConnectError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Log In Using Chilli Connect request.");
			
            var gameToken = m_dataStore.GetString("AppToken");
			var request = new LogInUsingChilliConnectRequest(chilliConnectId, chilliConnectSecret, gameToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyLogInUsingChilliConnectSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyLogInUsingChilliConnectError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Login to the sytem using an Email and Password. Returns an ConnectAccessToken
		/// that is tied to the player and should be used to authenticate on subsequent
		/// requests. Also returns the ChilliConnectID and ChilliConnectSecret of the logged
		/// in player that can be used to generate new ConnectAccessTokens via the
		/// LogInUsingChilliConnect method without requiring the player to explicitly
		/// reauthenticate.
		/// </summary>
		///
		/// <param name="email">The player's Email.</param>
		/// <param name="password">The player's Password.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void LogInUsingEmail(string email, string password, Action<LogInUsingEmailRequest, LogInUsingEmailResponse> successCallback, Action<LogInUsingEmailRequest, LogInUsingEmailError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Log In Using Email request.");
			
            var gameToken = m_dataStore.GetString("AppToken");
			var request = new LogInUsingEmailRequest(email, password, gameToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyLogInUsingEmailSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyLogInUsingEmailError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Login to the sytem using a FacebookAccessToken. Returns an ConnectAccessToken
		/// that is tied to the player and should be used to authenticate on subsequent
		/// requests. Also returns the ChilliConnectID and ChilliConnectSecret of the logged
		/// in player that can be used to generate new ConnectAccessTokens via the
		/// LogInUsingChilliConnect method without requiring the player to explicitly
		/// reauthenticate.
		/// </summary>
		///
		/// <param name="facebookAccessToken">Access Token provided from the Facebook API.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void LogInUsingFacebook(string facebookAccessToken, Action<LogInUsingFacebookRequest, LogInUsingFacebookResponse> successCallback, Action<LogInUsingFacebookRequest, LogInUsingFacebookError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Log In Using Facebook request.");
			
            var gameToken = m_dataStore.GetString("AppToken");
			var request = new LogInUsingFacebookRequest(facebookAccessToken, gameToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyLogInUsingFacebookSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyLogInUsingFacebookError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Login to the sytem using an UserName and Password. Returns an ConnectAccessToken
		/// that is tied to the player and should be used to authenticate on subsequent
		/// requests. Also returns the ChilliConnectID and ChilliConnectSecret of the logged
		/// in player that can be used to generate new ConnectAccessTokens via the
		/// LogInUsingChilliConnect method without requiring the player to explicitly
		/// reauthenticate.
		/// </summary>
		///
		/// <param name="userName">The player's Username.</param>
		/// <param name="password">The player's Password.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void LogInUsingUserName(string userName, string password, Action<LogInUsingUserNameRequest, LogInUsingUserNameResponse> successCallback, Action<LogInUsingUserNameRequest, LogInUsingUserNameError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Log In Using User Name request.");
			
            var gameToken = m_dataStore.GetString("AppToken");
			var request = new LogInUsingUserNameRequest(userName, password, gameToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyLogInUsingUserNameSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyLogInUsingUserNameError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Updates the details of the currently logged in Player.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void SetPlayerDetails(SetPlayerDetailsRequestDesc desc, Action<SetPlayerDetailsRequest, SetPlayerDetailsResponse> successCallback, Action<SetPlayerDetailsRequest, SetPlayerDetailsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Set Player Details request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new SetPlayerDetailsRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifySetPlayerDetailsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifySetPlayerDetailsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Returns the details of the currently logged in Player.
		/// </summary>
		///
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetPlayerDetails(Action<GetPlayerDetailsResponse> successCallback, Action<GetPlayerDetailsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Player Details request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetPlayerDetailsRequest(connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetPlayerDetailsSuccess(serverResponse, successCallback);
				} 
				else 
				{
					NotifyGetPlayerDetailsError(serverResponse, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Associate a player account with a Facebook account. Each player can only be
		/// associated with a single Facebook account and a Facebook account can only be
		/// associated with a single player per game. If the player is already associated
		/// with a Facebook account an error will be returned, unless the Replace flag is
		/// provided, in which case the association will be updated. If the Facebook account
		/// is already associated with another player within this game, an error will be
		/// returned along with the ChilliConnectID and ChilliConnectSecret for the
		/// associated player within the data parameter of the response body. If the Update
		/// flag is provided, the existing association will be removed and Facebook account
		/// associated with the current ChilliConnect account.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void LinkFacebookAccount(LinkFacebookAccountRequestDesc desc, Action<LinkFacebookAccountRequest, LinkFacebookAccountResponse> successCallback, Action<LinkFacebookAccountRequest, LinkFacebookAccountError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Link Facebook Account request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new LinkFacebookAccountRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyLinkFacebookAccountSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyLinkFacebookAccountError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Retrieve a boolean indicating if a player's Facebook Access Token is Valid or
		/// not.
		/// </summary>
		///
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void VerifyFacebookToken(Action<VerifyFacebookTokenResponse> successCallback, Action<VerifyFacebookTokenError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Verify Facebook Token request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new VerifyFacebookTokenRequest(connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyVerifyFacebookTokenSuccess(serverResponse, successCallback);
				} 
				else 
				{
					NotifyVerifyFacebookTokenError(serverResponse, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Find the ChilliConnectID's of players associated with provided FacebookID's.
		/// Returns an array of objects for each FacebookID that was found providing the
		/// FacebookName, ChilliConnectID, UserName and DisplayName of the associated player.
		/// </summary>
		///
		/// <param name="facebookIds">An array of FacebookIDs to look up. Maximum 10.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void LookupFacebookPlayers(IList<string> facebookIds, Action<LookupFacebookPlayersRequest, LookupFacebookPlayersResponse> successCallback, Action<LookupFacebookPlayersRequest, LookupFacebookPlayersError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Lookup Facebook Players request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new LookupFacebookPlayersRequest(facebookIds, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyLookupFacebookPlayersSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyLookupFacebookPlayersError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Find the ChilliConnectID's of players associated with provided UserName's.
		/// Returns an array of objects for each UserName that was found providing the
		/// ChilliConnectID, UserName and DisplayName of the associated player.
		/// </summary>
		///
		/// <param name="userNames">An array of UserNames to look up. Maximum 10.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void LookupUserNames(IList<string> userNames, Action<LookupUserNamesRequest, LookupUserNamesResponse> successCallback, Action<LookupUserNamesRequest, LookupUserNamesError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Lookup User Names request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new LookupUserNamesRequest(userNames, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyLookupUserNamesSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyLookupUserNamesError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Get back a players ChilliConnect registered Facebook friends along with their
		/// current Facebook profile pictures.
		/// </summary>
		///
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetFacebookFriends(Action<GetFacebookFriendsResponse> successCallback, Action<GetFacebookFriendsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Facebook Friends request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetFacebookFriendsRequest(connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetFacebookFriendsSuccess(serverResponse, successCallback);
				} 
				else 
				{
					NotifyGetFacebookFriendsError(serverResponse, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Remove an associate between a player and a Facebook account previously created
		/// via the LinkFacebookAccount method.
		/// </summary>
		///
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void UnlinkFacebookAccount(Action<UnlinkFacebookAccountResponse> successCallback, Action<UnlinkFacebookAccountError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Unlink Facebook Account request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new UnlinkFacebookAccountRequest(connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyUnlinkFacebookAccountSuccess(serverResponse, successCallback);
				} 
				else 
				{
					NotifyUnlinkFacebookAccountError(serverResponse, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Notifies the user that a Create Player request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyCreatePlayerSuccess(ServerResponse serverResponse, CreatePlayerRequest request, Action<CreatePlayerRequest, CreatePlayerResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("CreatePlayer request succeeded.");
	
			CreatePlayerResponse outputResponse = new CreatePlayerResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Log In Using Chilli Connect request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyLogInUsingChilliConnectSuccess(ServerResponse serverResponse, LogInUsingChilliConnectRequest request, Action<LogInUsingChilliConnectRequest> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("LogInUsingChilliConnect request succeeded.");
	
            var connectAccessToken = serverResponse.Body["ConnectAccessToken"] as string;
            ReleaseAssert.IsNotNull(connectAccessToken, "Data Store property cannot be null.");
            m_dataStore.Set("UserAccessToken", connectAccessToken);
        
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Log In Using Email request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyLogInUsingEmailSuccess(ServerResponse serverResponse, LogInUsingEmailRequest request, Action<LogInUsingEmailRequest, LogInUsingEmailResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("LogInUsingEmail request succeeded.");
	
            var connectAccessToken = serverResponse.Body["ConnectAccessToken"] as string;
            ReleaseAssert.IsNotNull(connectAccessToken, "Data Store property cannot be null.");
            m_dataStore.Set("UserAccessToken", connectAccessToken);
        
			LogInUsingEmailResponse outputResponse = new LogInUsingEmailResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Log In Using Facebook request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyLogInUsingFacebookSuccess(ServerResponse serverResponse, LogInUsingFacebookRequest request, Action<LogInUsingFacebookRequest, LogInUsingFacebookResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("LogInUsingFacebook request succeeded.");
	
            var connectAccessToken = serverResponse.Body["ConnectAccessToken"] as string;
            ReleaseAssert.IsNotNull(connectAccessToken, "Data Store property cannot be null.");
            m_dataStore.Set("UserAccessToken", connectAccessToken);
        
			LogInUsingFacebookResponse outputResponse = new LogInUsingFacebookResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Log In Using User Name request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyLogInUsingUserNameSuccess(ServerResponse serverResponse, LogInUsingUserNameRequest request, Action<LogInUsingUserNameRequest, LogInUsingUserNameResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("LogInUsingUserName request succeeded.");
	
            var connectAccessToken = serverResponse.Body["ConnectAccessToken"] as string;
            ReleaseAssert.IsNotNull(connectAccessToken, "Data Store property cannot be null.");
            m_dataStore.Set("UserAccessToken", connectAccessToken);
        
			LogInUsingUserNameResponse outputResponse = new LogInUsingUserNameResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Set Player Details request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifySetPlayerDetailsSuccess(ServerResponse serverResponse, SetPlayerDetailsRequest request, Action<SetPlayerDetailsRequest, SetPlayerDetailsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("SetPlayerDetails request succeeded.");
	
			SetPlayerDetailsResponse outputResponse = new SetPlayerDetailsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Player Details request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetPlayerDetailsSuccess(ServerResponse serverResponse, Action<GetPlayerDetailsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetPlayerDetails request succeeded.");
	
			GetPlayerDetailsResponse outputResponse = new GetPlayerDetailsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Link Facebook Account request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyLinkFacebookAccountSuccess(ServerResponse serverResponse, LinkFacebookAccountRequest request, Action<LinkFacebookAccountRequest, LinkFacebookAccountResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("LinkFacebookAccount request succeeded.");
	
			LinkFacebookAccountResponse outputResponse = new LinkFacebookAccountResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Verify Facebook Token request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyVerifyFacebookTokenSuccess(ServerResponse serverResponse, Action<VerifyFacebookTokenResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("VerifyFacebookToken request succeeded.");
	
			VerifyFacebookTokenResponse outputResponse = new VerifyFacebookTokenResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Lookup Facebook Players request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyLookupFacebookPlayersSuccess(ServerResponse serverResponse, LookupFacebookPlayersRequest request, Action<LookupFacebookPlayersRequest, LookupFacebookPlayersResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("LookupFacebookPlayers request succeeded.");
	
			LookupFacebookPlayersResponse outputResponse = new LookupFacebookPlayersResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Lookup User Names request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyLookupUserNamesSuccess(ServerResponse serverResponse, LookupUserNamesRequest request, Action<LookupUserNamesRequest, LookupUserNamesResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("LookupUserNames request succeeded.");
	
			LookupUserNamesResponse outputResponse = new LookupUserNamesResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Facebook Friends request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetFacebookFriendsSuccess(ServerResponse serverResponse, Action<GetFacebookFriendsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetFacebookFriends request succeeded.");
	
			GetFacebookFriendsResponse outputResponse = new GetFacebookFriendsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Unlink Facebook Account request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyUnlinkFacebookAccountSuccess(ServerResponse serverResponse, Action<UnlinkFacebookAccountResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("UnlinkFacebookAccount request succeeded.");
	
			UnlinkFacebookAccountResponse outputResponse = new UnlinkFacebookAccountResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Create Player request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyCreatePlayerError(ServerResponse serverResponse, CreatePlayerRequest request, Action<CreatePlayerRequest, CreatePlayerError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Create Player request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Create Player request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Create Player request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			CreatePlayerError error = new CreatePlayerError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Log In Using Chilli Connect request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyLogInUsingChilliConnectError(ServerResponse serverResponse, LogInUsingChilliConnectRequest request, Action<LogInUsingChilliConnectRequest, LogInUsingChilliConnectError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Log In Using Chilli Connect request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Log In Using Chilli Connect request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Log In Using Chilli Connect request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			LogInUsingChilliConnectError error = new LogInUsingChilliConnectError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Log In Using Email request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyLogInUsingEmailError(ServerResponse serverResponse, LogInUsingEmailRequest request, Action<LogInUsingEmailRequest, LogInUsingEmailError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Log In Using Email request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Log In Using Email request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Log In Using Email request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			LogInUsingEmailError error = new LogInUsingEmailError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Log In Using Facebook request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyLogInUsingFacebookError(ServerResponse serverResponse, LogInUsingFacebookRequest request, Action<LogInUsingFacebookRequest, LogInUsingFacebookError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Log In Using Facebook request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Log In Using Facebook request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Log In Using Facebook request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			LogInUsingFacebookError error = new LogInUsingFacebookError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Log In Using User Name request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyLogInUsingUserNameError(ServerResponse serverResponse, LogInUsingUserNameRequest request, Action<LogInUsingUserNameRequest, LogInUsingUserNameError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Log In Using User Name request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Log In Using User Name request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Log In Using User Name request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			LogInUsingUserNameError error = new LogInUsingUserNameError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Set Player Details request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifySetPlayerDetailsError(ServerResponse serverResponse, SetPlayerDetailsRequest request, Action<SetPlayerDetailsRequest, SetPlayerDetailsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Set Player Details request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Set Player Details request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Set Player Details request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			SetPlayerDetailsError error = new SetPlayerDetailsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Player Details request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetPlayerDetailsError(ServerResponse serverResponse, Action<GetPlayerDetailsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Player Details request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Player Details request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Player Details request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetPlayerDetailsError error = new GetPlayerDetailsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Link Facebook Account request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyLinkFacebookAccountError(ServerResponse serverResponse, LinkFacebookAccountRequest request, Action<LinkFacebookAccountRequest, LinkFacebookAccountError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Link Facebook Account request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Link Facebook Account request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Link Facebook Account request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			LinkFacebookAccountError error = new LinkFacebookAccountError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Verify Facebook Token request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyVerifyFacebookTokenError(ServerResponse serverResponse, Action<VerifyFacebookTokenError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Verify Facebook Token request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Verify Facebook Token request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Verify Facebook Token request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			VerifyFacebookTokenError error = new VerifyFacebookTokenError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Lookup Facebook Players request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyLookupFacebookPlayersError(ServerResponse serverResponse, LookupFacebookPlayersRequest request, Action<LookupFacebookPlayersRequest, LookupFacebookPlayersError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Lookup Facebook Players request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Lookup Facebook Players request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Lookup Facebook Players request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			LookupFacebookPlayersError error = new LookupFacebookPlayersError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Lookup User Names request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyLookupUserNamesError(ServerResponse serverResponse, LookupUserNamesRequest request, Action<LookupUserNamesRequest, LookupUserNamesError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Lookup User Names request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Lookup User Names request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Lookup User Names request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			LookupUserNamesError error = new LookupUserNamesError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Facebook Friends request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetFacebookFriendsError(ServerResponse serverResponse, Action<GetFacebookFriendsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Facebook Friends request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Facebook Friends request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Facebook Friends request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetFacebookFriendsError error = new GetFacebookFriendsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Unlink Facebook Account request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyUnlinkFacebookAccountError(ServerResponse serverResponse, Action<UnlinkFacebookAccountError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Unlink Facebook Account request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Unlink Facebook Account request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Unlink Facebook Account request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			UnlinkFacebookAccountError error = new UnlinkFacebookAccountError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(error);
			});	
		}
	}
}
