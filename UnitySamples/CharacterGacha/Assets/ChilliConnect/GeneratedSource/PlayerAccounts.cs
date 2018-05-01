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
		/// It is no longer recommended to create new players through this endpoint. It is
		/// better to create new players by using any of the other Login endpoints that have
		/// a CreatePlayer property. Creates a new, anonymous ChilliConnect player account
		/// for a specific game. UserName, DisplayName, Email and Password details can be
		/// provided but are not required. Will return a ChilliConnectID and
		/// ChilliConnectSecret that uniquely identifies the newly created player. These
		/// details can be used to login to the players account via the
		/// LogInUsingChilliConnect method.
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
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void LogInUsingChilliConnect(LogInUsingChilliConnectRequestDesc desc, Action<LogInUsingChilliConnectRequest, LogInUsingChilliConnectResponse> successCallback, Action<LogInUsingChilliConnectRequest, LogInUsingChilliConnectError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Log In Using Chilli Connect request.");
			
            var gameToken = m_dataStore.GetString("AppToken");
			var request = new LogInUsingChilliConnectRequest(desc, gameToken);
	
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
		/// Login to the system using an Email and Password. Returns an ConnectAccessToken
		/// that is tied to the player and should be used to authenticate on subsequent
		/// requests. Also returns the ChilliConnectID of the logged in player.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void LogInUsingEmail(LogInUsingEmailRequestDesc desc, Action<LogInUsingEmailRequest, LogInUsingEmailResponse> successCallback, Action<LogInUsingEmailRequest, LogInUsingEmailError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Log In Using Email request.");
			
            var gameToken = m_dataStore.GetString("AppToken");
			var request = new LogInUsingEmailRequest(desc, gameToken);
	
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
		/// Login to the system using a FacebookAccessToken. Returns an ConnectAccessToken
		/// that is tied to the player and should be used to authenticate on subsequent
		/// requests. Also returns the ChilliConnectID of the logged in player.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void LogInUsingFacebook(LogInUsingFacebookRequestDesc desc, Action<LogInUsingFacebookRequest, LogInUsingFacebookResponse> successCallback, Action<LogInUsingFacebookRequest, LogInUsingFacebookError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Log In Using Facebook request.");
			
            var gameToken = m_dataStore.GetString("AppToken");
			var request = new LogInUsingFacebookRequest(desc, gameToken);
	
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
		/// Login to the system using a Google Auth Code. Returns an ConnectAccessToken that
		/// is tied to the player and should be used to authenticate on subsequent requests.
		/// Also returns the ChilliConnectID of the logged in player.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void LogInUsingGoogle(LogInUsingGoogleRequestDesc desc, Action<LogInUsingGoogleRequest, LogInUsingGoogleResponse> successCallback, Action<LogInUsingGoogleRequest, LogInUsingGoogleError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Log In Using Google request.");
			
            var gameToken = m_dataStore.GetString("AppToken");
			var request = new LogInUsingGoogleRequest(desc, gameToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyLogInUsingGoogleSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyLogInUsingGoogleError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Login to the system using a GameCenterID. Returns an ConnectAccessToken that is
		/// tied to the player and should be used to authenticate on subsequent requests.
		/// Also returns the ChilliConnectID of the logged in player.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void LogInUsingGameCenter(LogInUsingGameCenterRequestDesc desc, Action<LogInUsingGameCenterRequest, LogInUsingGameCenterResponse> successCallback, Action<LogInUsingGameCenterRequest, LogInUsingGameCenterError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Log In Using Game Center request.");
			
            var gameToken = m_dataStore.GetString("AppToken");
			var request = new LogInUsingGameCenterRequest(desc, gameToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyLogInUsingGameCenterSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyLogInUsingGameCenterError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Login to the system using a Mobile DeviceId and Platform. Returns an
		/// ConnectAccessToken that is tied to the player and should be used to authenticate
		/// on subsequent requests. Also returns the ChilliConnectID of the logged in player.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void LogInUsingMobileDeviceId(LogInUsingMobileDeviceIdRequestDesc desc, Action<LogInUsingMobileDeviceIdRequest, LogInUsingMobileDeviceIdResponse> successCallback, Action<LogInUsingMobileDeviceIdRequest, LogInUsingMobileDeviceIdError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Log In Using Mobile Device Id request.");
			
            var gameToken = m_dataStore.GetString("AppToken");
			var request = new LogInUsingMobileDeviceIdRequest(desc, gameToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyLogInUsingMobileDeviceIdSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyLogInUsingMobileDeviceIdError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Login to the system using an UserName and Password. Returns an ConnectAccessToken
		/// that is tied to the player and should be used to authenticate on subsequent
		/// requests. Also returns the ChilliConnectID of the logged in player.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void LogInUsingUserName(LogInUsingUserNameRequestDesc desc, Action<LogInUsingUserNameRequest, LogInUsingUserNameResponse> successCallback, Action<LogInUsingUserNameRequest, LogInUsingUserNameError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Log In Using User Name request.");
			
            var gameToken = m_dataStore.GetString("AppToken");
			var request = new LogInUsingUserNameRequest(desc, gameToken);
	
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
		/// Associate a player account with a GameCenterID. Each player can only be
		/// associated with a single GameCenterID and a GameCenterID can only be associated
		/// with a single player per game. If the player is already associated with a
		/// GameCenterID an error will be returned, unless the Replace flag is provided, in
		/// which case the association will be updated. If the GameCenterID is already
		/// associated with another player within this game, an error will be returned along
		/// with the ChilliConnectID for the associated player within the data parameter of
		/// the response body. If the Update flag is provided, the existing association will
		/// be removed and GameCenterID associated with the current ChilliConnect account.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void LinkGameCenterAccount(LinkGameCenterAccountRequestDesc desc, Action<LinkGameCenterAccountRequest, LinkGameCenterAccountResponse> successCallback, Action<LinkGameCenterAccountRequest, LinkGameCenterAccountError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Link Game Center Account request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new LinkGameCenterAccountRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyLinkGameCenterAccountSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyLinkGameCenterAccountError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Remove an associate between a player and a GameCenter account previously created
		/// via the LinkGameCenterAccount method.
		/// </summary>
		///
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void UnlinkGameCenterAccount(Action<UnlinkGameCenterAccountResponse> successCallback, Action<UnlinkGameCenterAccountError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Unlink Game Center Account request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new UnlinkGameCenterAccountRequest(connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyUnlinkGameCenterAccountSuccess(serverResponse, successCallback);
				} 
				else 
				{
					NotifyUnlinkGameCenterAccountError(serverResponse, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Associate a unique Mobile DeviceId with a player account. Each player can have
		/// multiple devices of the same platform linked with their account, so long as the
		/// combination of DeviceID and Platform are unique. If the player is already
		/// associated with a DeviceID and Platform, an error will be returned. If the
		/// DeviceID and Platform are already associated with another player within this
		/// game, an error will be returned along with the ChilliConnectID for the associated
		/// player within the data parameter of the response body. If the Update flag is
		/// provided, the existing association will be removed and the DeviceID and Platform
		/// will be associated with the current ChilliConnect account.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void LinkMobileDeviceId(LinkMobileDeviceIdRequestDesc desc, Action<LinkMobileDeviceIdRequest, LinkMobileDeviceIdResponse> successCallback, Action<LinkMobileDeviceIdRequest, LinkMobileDeviceIdError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Link Mobile Device Id request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new LinkMobileDeviceIdRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyLinkMobileDeviceIdSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyLinkMobileDeviceIdError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Remove an association between a player and a DeviceId previously created via the
		/// LinkMobileDeviceId method.
		/// </summary>
		///
		/// <param name="deviceId">DeviceId generated on the Players current device.</param>
		/// <param name="platform">Platform of the Players current Device. Must only be ANDROID, IOS, or KINDLE.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void UnlinkMobileDeviceId(string deviceId, string platform, Action<UnlinkMobileDeviceIdRequest, UnlinkMobileDeviceIdResponse> successCallback, Action<UnlinkMobileDeviceIdRequest, UnlinkMobileDeviceIdError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Unlink Mobile Device Id request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new UnlinkMobileDeviceIdRequest(deviceId, platform, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyUnlinkMobileDeviceIdSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyUnlinkMobileDeviceIdError(serverResponse, request, errorCallback);
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
		/// returned along with the FacebookID and FacebookName for the associated player
		/// within the data parameter of the response body. If the Update flag is provided,
		/// the existing association will be removed and Facebook account associated with the
		/// current ChilliConnect account.
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
		/// Associate a player account with a Google account. Each player can only be
		/// associated with a single Google account and a Google account can only be
		/// associated with a single player per game. If the player is already associated
		/// with a Google account an error will be returned, unless the Replace flag is
		/// provided, in which case the association will be updated. If the Google account is
		/// already associated with another player within this game, an error will be
		/// returned along with the ChilliConnectID for the associated player within the data
		/// parameter of the response body. If the Update flag is provided, the existing
		/// association will be removed and Google account associated with the current
		/// ChilliConnect account.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void LinkGoogleAccount(LinkGoogleAccountRequestDesc desc, Action<LinkGoogleAccountRequest, LinkGoogleAccountResponse> successCallback, Action<LinkGoogleAccountRequest, LinkGoogleAccountError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Link Google Account request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new LinkGoogleAccountRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyLinkGoogleAccountSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyLinkGoogleAccountError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Remove an associate between a player and a Google account previously created via
		/// the LinkGoogleAccount method.
		/// </summary>
		///
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void UnlinkGoogleAccount(Action<UnlinkGoogleAccountResponse> successCallback, Action<UnlinkGoogleAccountError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Unlink Google Account request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new UnlinkGoogleAccountRequest(connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyUnlinkGoogleAccountSuccess(serverResponse, successCallback);
				} 
				else 
				{
					NotifyUnlinkGoogleAccountError(serverResponse, errorCallback);
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
		private void NotifyLogInUsingChilliConnectSuccess(ServerResponse serverResponse, LogInUsingChilliConnectRequest request, Action<LogInUsingChilliConnectRequest, LogInUsingChilliConnectResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("LogInUsingChilliConnect request succeeded.");
	
            var connectAccessToken = serverResponse.Body["ConnectAccessToken"] as string;
            if (connectAccessToken != null) {
            	m_dataStore.Set("UserAccessToken", connectAccessToken);
        	}
        
            var metricsAccessToken = serverResponse.Body["MetricsAccessToken"] as string;
            if (metricsAccessToken != null) {
            	m_dataStore.Set("MetricsAccessToken", metricsAccessToken);
        	}
        
			LogInUsingChilliConnectResponse outputResponse = new LogInUsingChilliConnectResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
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
            if (connectAccessToken != null) {
            	m_dataStore.Set("UserAccessToken", connectAccessToken);
        	}
        
            var metricsAccessToken = serverResponse.Body["MetricsAccessToken"] as string;
            if (metricsAccessToken != null) {
            	m_dataStore.Set("MetricsAccessToken", metricsAccessToken);
        	}
        
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
            if (connectAccessToken != null) {
            	m_dataStore.Set("UserAccessToken", connectAccessToken);
        	}
        
            var metricsAccessToken = serverResponse.Body["MetricsAccessToken"] as string;
            if (metricsAccessToken != null) {
            	m_dataStore.Set("MetricsAccessToken", metricsAccessToken);
        	}
        
			LogInUsingFacebookResponse outputResponse = new LogInUsingFacebookResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Log In Using Google request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyLogInUsingGoogleSuccess(ServerResponse serverResponse, LogInUsingGoogleRequest request, Action<LogInUsingGoogleRequest, LogInUsingGoogleResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("LogInUsingGoogle request succeeded.");
	
            var connectAccessToken = serverResponse.Body["ConnectAccessToken"] as string;
            if (connectAccessToken != null) {
            	m_dataStore.Set("UserAccessToken", connectAccessToken);
        	}
        
            var metricsAccessToken = serverResponse.Body["MetricsAccessToken"] as string;
            if (metricsAccessToken != null) {
            	m_dataStore.Set("MetricsAccessToken", metricsAccessToken);
        	}
        
			LogInUsingGoogleResponse outputResponse = new LogInUsingGoogleResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Log In Using Game Center request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyLogInUsingGameCenterSuccess(ServerResponse serverResponse, LogInUsingGameCenterRequest request, Action<LogInUsingGameCenterRequest, LogInUsingGameCenterResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("LogInUsingGameCenter request succeeded.");
	
            var connectAccessToken = serverResponse.Body["ConnectAccessToken"] as string;
            if (connectAccessToken != null) {
            	m_dataStore.Set("UserAccessToken", connectAccessToken);
        	}
        
            var metricsAccessToken = serverResponse.Body["MetricsAccessToken"] as string;
            if (metricsAccessToken != null) {
            	m_dataStore.Set("MetricsAccessToken", metricsAccessToken);
        	}
        
			LogInUsingGameCenterResponse outputResponse = new LogInUsingGameCenterResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Log In Using Mobile Device Id request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyLogInUsingMobileDeviceIdSuccess(ServerResponse serverResponse, LogInUsingMobileDeviceIdRequest request, Action<LogInUsingMobileDeviceIdRequest, LogInUsingMobileDeviceIdResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("LogInUsingMobileDeviceId request succeeded.");
	
            var connectAccessToken = serverResponse.Body["ConnectAccessToken"] as string;
            if (connectAccessToken != null) {
            	m_dataStore.Set("UserAccessToken", connectAccessToken);
        	}
        
            var metricsAccessToken = serverResponse.Body["MetricsAccessToken"] as string;
            if (metricsAccessToken != null) {
            	m_dataStore.Set("MetricsAccessToken", metricsAccessToken);
        	}
        
			LogInUsingMobileDeviceIdResponse outputResponse = new LogInUsingMobileDeviceIdResponse(serverResponse.Body);
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
            if (connectAccessToken != null) {
            	m_dataStore.Set("UserAccessToken", connectAccessToken);
        	}
        
            var metricsAccessToken = serverResponse.Body["MetricsAccessToken"] as string;
            if (metricsAccessToken != null) {
            	m_dataStore.Set("MetricsAccessToken", metricsAccessToken);
        	}
        
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
		/// Notifies the user that a Link Game Center Account request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyLinkGameCenterAccountSuccess(ServerResponse serverResponse, LinkGameCenterAccountRequest request, Action<LinkGameCenterAccountRequest, LinkGameCenterAccountResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("LinkGameCenterAccount request succeeded.");
	
			LinkGameCenterAccountResponse outputResponse = new LinkGameCenterAccountResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Unlink Game Center Account request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyUnlinkGameCenterAccountSuccess(ServerResponse serverResponse, Action<UnlinkGameCenterAccountResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("UnlinkGameCenterAccount request succeeded.");
	
			UnlinkGameCenterAccountResponse outputResponse = new UnlinkGameCenterAccountResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Link Mobile Device Id request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyLinkMobileDeviceIdSuccess(ServerResponse serverResponse, LinkMobileDeviceIdRequest request, Action<LinkMobileDeviceIdRequest, LinkMobileDeviceIdResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("LinkMobileDeviceId request succeeded.");
	
			LinkMobileDeviceIdResponse outputResponse = new LinkMobileDeviceIdResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Unlink Mobile Device Id request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyUnlinkMobileDeviceIdSuccess(ServerResponse serverResponse, UnlinkMobileDeviceIdRequest request, Action<UnlinkMobileDeviceIdRequest, UnlinkMobileDeviceIdResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("UnlinkMobileDeviceId request succeeded.");
	
			UnlinkMobileDeviceIdResponse outputResponse = new UnlinkMobileDeviceIdResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
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
		/// Notifies the user that a Link Google Account request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyLinkGoogleAccountSuccess(ServerResponse serverResponse, LinkGoogleAccountRequest request, Action<LinkGoogleAccountRequest, LinkGoogleAccountResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("LinkGoogleAccount request succeeded.");
	
			LinkGoogleAccountResponse outputResponse = new LinkGoogleAccountResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Unlink Google Account request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyUnlinkGoogleAccountSuccess(ServerResponse serverResponse, Action<UnlinkGoogleAccountResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("UnlinkGoogleAccount request succeeded.");
	
			UnlinkGoogleAccountResponse outputResponse = new UnlinkGoogleAccountResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(outputResponse);
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
		/// Notifies the user that a Log In Using Google request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyLogInUsingGoogleError(ServerResponse serverResponse, LogInUsingGoogleRequest request, Action<LogInUsingGoogleRequest, LogInUsingGoogleError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Log In Using Google request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Log In Using Google request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Log In Using Google request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			LogInUsingGoogleError error = new LogInUsingGoogleError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Log In Using Game Center request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyLogInUsingGameCenterError(ServerResponse serverResponse, LogInUsingGameCenterRequest request, Action<LogInUsingGameCenterRequest, LogInUsingGameCenterError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Log In Using Game Center request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Log In Using Game Center request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Log In Using Game Center request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			LogInUsingGameCenterError error = new LogInUsingGameCenterError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Log In Using Mobile Device Id request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyLogInUsingMobileDeviceIdError(ServerResponse serverResponse, LogInUsingMobileDeviceIdRequest request, Action<LogInUsingMobileDeviceIdRequest, LogInUsingMobileDeviceIdError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Log In Using Mobile Device Id request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Log In Using Mobile Device Id request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Log In Using Mobile Device Id request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			LogInUsingMobileDeviceIdError error = new LogInUsingMobileDeviceIdError(serverResponse);	
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
		/// Notifies the user that a Link Game Center Account request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyLinkGameCenterAccountError(ServerResponse serverResponse, LinkGameCenterAccountRequest request, Action<LinkGameCenterAccountRequest, LinkGameCenterAccountError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Link Game Center Account request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Link Game Center Account request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Link Game Center Account request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			LinkGameCenterAccountError error = new LinkGameCenterAccountError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Unlink Game Center Account request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyUnlinkGameCenterAccountError(ServerResponse serverResponse, Action<UnlinkGameCenterAccountError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Unlink Game Center Account request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Unlink Game Center Account request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Unlink Game Center Account request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			UnlinkGameCenterAccountError error = new UnlinkGameCenterAccountError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Link Mobile Device Id request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyLinkMobileDeviceIdError(ServerResponse serverResponse, LinkMobileDeviceIdRequest request, Action<LinkMobileDeviceIdRequest, LinkMobileDeviceIdError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Link Mobile Device Id request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Link Mobile Device Id request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Link Mobile Device Id request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			LinkMobileDeviceIdError error = new LinkMobileDeviceIdError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Unlink Mobile Device Id request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyUnlinkMobileDeviceIdError(ServerResponse serverResponse, UnlinkMobileDeviceIdRequest request, Action<UnlinkMobileDeviceIdRequest, UnlinkMobileDeviceIdError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Unlink Mobile Device Id request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Unlink Mobile Device Id request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Unlink Mobile Device Id request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			UnlinkMobileDeviceIdError error = new UnlinkMobileDeviceIdError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
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
		
		/// <summary>
		/// Notifies the user that a Link Google Account request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyLinkGoogleAccountError(ServerResponse serverResponse, LinkGoogleAccountRequest request, Action<LinkGoogleAccountRequest, LinkGoogleAccountError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Link Google Account request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Link Google Account request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Link Google Account request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			LinkGoogleAccountError error = new LinkGoogleAccountError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Unlink Google Account request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyUnlinkGoogleAccountError(ServerResponse serverResponse, Action<UnlinkGoogleAccountError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Unlink Google Account request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Unlink Google Account request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Unlink Google Account request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			UnlinkGoogleAccountError error = new UnlinkGoogleAccountError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(error);
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
	}
}
