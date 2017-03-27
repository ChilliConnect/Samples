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
	/// <para>The ChilliConnect Messaging module. Enables sending of messages, and gifting
	/// economy items to players.</para>
	///
	/// <para>This is thread-safe.</para>
	/// </summary>
	public sealed class Messaging
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
		public Messaging(Logging logging, TaskScheduler taskScheduler, ServerRequestSystem serverRequestSystem, DataStore dataStore)
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
		/// Sends a message to a player. This call allows the From parameter to be set -
		/// enabling sending from any Player, or not set - enabling sending a system message.
		/// To send a message from the currently logged-in Player see SendMessageFromPlayer.
		/// A message can generate, or transfer from the sender, Currency and Inventory
		/// items. Direct access to this method from the SDKs is disabled by default and must
		/// be enabled from the ChilliConnect dashboard. Note: At least one of; Title, Text,
		/// Data, or Rewards is required.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void SendMessage(SendMessageRequestDesc desc, Action<SendMessageRequest, SendMessageResponse> successCallback, Action<SendMessageRequest, SendMessageError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Send Message request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new SendMessageRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifySendMessageSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifySendMessageError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Sends a message to a player from the currently logged-in Player. A message can
		/// transfer Currency and Inventory items between players. Direct access to this
		/// method from the SDKs is disabled by default and must be enabled from the
		/// ChilliConnect dashboard. Note: At least one of; Title, Text, Data, or Rewards is
		/// required.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void SendMessageFromPlayer(SendMessageFromPlayerRequestDesc desc, Action<SendMessageFromPlayerRequest, SendMessageFromPlayerResponse> successCallback, Action<SendMessageFromPlayerRequest, SendMessageFromPlayerError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Send Message From Player request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new SendMessageFromPlayerRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifySendMessageFromPlayerSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifySendMessageFromPlayerError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Gets a message for a Player.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetMessage(GetMessageRequestDesc desc, Action<GetMessageRequest, GetMessageResponse> successCallback, Action<GetMessageRequest, GetMessageError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Message request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetMessageRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetMessageSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetMessageError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Lists the messages for Player.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void GetMessages(GetMessagesRequestDesc desc, Action<GetMessagesRequest, GetMessagesResponse> successCallback, Action<GetMessagesRequest, GetMessagesError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Get Messages request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new GetMessagesRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyGetMessagesSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyGetMessagesError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Redeem the rewards contained in a message. Direct access to this method from the
		/// SDKs is disabled by default and must be enabled from the ChilliConnect dashboard.
		/// </summary>
		///
		/// <param name="desc">The request description.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void RedeemMessageRewards(RedeemMessageRewardsRequestDesc desc, Action<RedeemMessageRewardsRequest, RedeemMessageRewardsResponse> successCallback, Action<RedeemMessageRewardsRequest, RedeemMessageRewardsError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Redeem Message Rewards request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new RedeemMessageRewardsRequest(desc, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyRedeemMessageRewardsSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyRedeemMessageRewardsError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Marks a Player's message as read. Direct access to this method from the SDKs is
		/// disabled by default and must be enabled from the ChilliConnect dashboard.
		/// </summary>
		///
		/// <param name="messageId">An identifier for the message.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void MarkMessageAsRead(string messageId, Action<MarkMessageAsReadRequest> successCallback, Action<MarkMessageAsReadRequest, MarkMessageAsReadError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Mark Message As Read request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new MarkMessageAsReadRequest(messageId, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyMarkMessageAsReadSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyMarkMessageAsReadError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Deletes a message. Direct access to this method from the SDKs is disabled by
		/// default and must be enabled from the ChilliConnect dashboard.
		/// </summary>
		///
		/// <param name="messageId">An identifier for the message.</param>
		/// <param name="successCallback">The delegate which is called if the request was successful.</param>
		/// <param name="errorCallback">The delegate which is called if the request was unsuccessful. Provides 
		/// a container with information on what went wrong.</param>
		public void DeleteMessage(string messageId, Action<DeleteMessageRequest> successCallback, Action<DeleteMessageRequest, DeleteMessageError> errorCallback)
		{
			m_logging.LogVerboseMessage("Sending Delete Message request.");
			
            var connectAccessToken = m_dataStore.GetString("UserAccessToken");
			var request = new DeleteMessageRequest(messageId, connectAccessToken);
	
			m_serverRequestSystem.SendImmediateRequest(request, (IImmediateServerRequest sentRequest, ServerResponse serverResponse) =>
			{
				ReleaseAssert.IsTrue(request == sentRequest, "Received request is not the same as the one sent!");
				
				if (serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode)
				{
					NotifyDeleteMessageSuccess(serverResponse, request, successCallback);
				} 
				else 
				{
					NotifyDeleteMessageError(serverResponse, request, errorCallback);
				}
			});
		}
		
		/// <summary>
		/// Notifies the user that a Send Message request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifySendMessageSuccess(ServerResponse serverResponse, SendMessageRequest request, Action<SendMessageRequest, SendMessageResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("SendMessage request succeeded.");
	
			SendMessageResponse outputResponse = new SendMessageResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Send Message From Player request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifySendMessageFromPlayerSuccess(ServerResponse serverResponse, SendMessageFromPlayerRequest request, Action<SendMessageFromPlayerRequest, SendMessageFromPlayerResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("SendMessageFromPlayer request succeeded.");
	
			SendMessageFromPlayerResponse outputResponse = new SendMessageFromPlayerResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Message request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetMessageSuccess(ServerResponse serverResponse, GetMessageRequest request, Action<GetMessageRequest, GetMessageResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetMessage request succeeded.");
	
			GetMessageResponse outputResponse = new GetMessageResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Get Messages request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyGetMessagesSuccess(ServerResponse serverResponse, GetMessagesRequest request, Action<GetMessagesRequest, GetMessagesResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("GetMessages request succeeded.");
	
			GetMessagesResponse outputResponse = new GetMessagesResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Redeem Message Rewards request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyRedeemMessageRewardsSuccess(ServerResponse serverResponse, RedeemMessageRewardsRequest request, Action<RedeemMessageRewardsRequest, RedeemMessageRewardsResponse> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("RedeemMessageRewards request succeeded.");
	
			RedeemMessageRewardsResponse outputResponse = new RedeemMessageRewardsResponse(serverResponse.Body);
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request, outputResponse);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Mark Message As Read request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyMarkMessageAsReadSuccess(ServerResponse serverResponse, MarkMessageAsReadRequest request, Action<MarkMessageAsReadRequest> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("MarkMessageAsRead request succeeded.");
	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Delete Message request was successful.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// successful responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The success callback.</param>
		private void NotifyDeleteMessageSuccess(ServerResponse serverResponse, DeleteMessageRequest request, Action<DeleteMessageRequest> successCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success && serverResponse.HttpResponseCode == SuccessHttpResponseCode, "Input server request must describe a success.");
			
			m_logging.LogVerboseMessage("DeleteMessage request succeeded.");
	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				successCallback(request);
			});
		}
		
		/// <summary>
		/// Notifies the user that a Send Message request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifySendMessageError(ServerResponse serverResponse, SendMessageRequest request, Action<SendMessageRequest, SendMessageError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Send Message request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Send Message request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Send Message request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			SendMessageError error = new SendMessageError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Send Message From Player request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifySendMessageFromPlayerError(ServerResponse serverResponse, SendMessageFromPlayerRequest request, Action<SendMessageFromPlayerRequest, SendMessageFromPlayerError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Send Message From Player request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Send Message From Player request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Send Message From Player request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			SendMessageFromPlayerError error = new SendMessageFromPlayerError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Message request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetMessageError(ServerResponse serverResponse, GetMessageRequest request, Action<GetMessageRequest, GetMessageError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Message request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Message request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Message request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetMessageError error = new GetMessageError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Get Messages request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyGetMessagesError(ServerResponse serverResponse, GetMessagesRequest request, Action<GetMessagesRequest, GetMessagesError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Get Messages request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Get Messages request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Get Messages request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			GetMessagesError error = new GetMessagesError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Redeem Message Rewards request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyRedeemMessageRewardsError(ServerResponse serverResponse, RedeemMessageRewardsRequest request, Action<RedeemMessageRewardsRequest, RedeemMessageRewardsError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Redeem Message Rewards request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Redeem Message Rewards request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Redeem Message Rewards request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			RedeemMessageRewardsError error = new RedeemMessageRewardsError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Mark Message As Read request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyMarkMessageAsReadError(ServerResponse serverResponse, MarkMessageAsReadRequest request, Action<MarkMessageAsReadRequest, MarkMessageAsReadError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Mark Message As Read request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Mark Message As Read request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Mark Message As Read request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			MarkMessageAsReadError error = new MarkMessageAsReadError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
		
		/// <summary>
		/// Notifies the user that a Delete Message request has failed.
		/// </summary>
		///
		/// <param name="serverResponse">A container for information on the response from the server. Only 
		/// failed responses can be passed into this method.</param>
		/// <param name="request"> The request that was sent to the server.</param>
		/// <param name="callback">The error callback.</param>
		private void NotifyDeleteMessageError(ServerResponse serverResponse, DeleteMessageRequest request, Action<DeleteMessageRequest, DeleteMessageError> errorCallback)
		{
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server request must describe an error.");
			
			switch (serverResponse.Result) 
			{
				case HttpResult.Success:
					m_logging.LogVerboseMessage("Delete Message request failed with http response code: " + serverResponse.HttpResponseCode);
					break;
				case HttpResult.CouldNotConnect:
					m_logging.LogVerboseMessage("Delete Message request failed becuase a connection could be established.");
					break;
				default:
					m_logging.LogVerboseMessage("Delete Message request failed for an unknown reason.");
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			DeleteMessageError error = new DeleteMessageError(serverResponse);	
			m_taskScheduler.ScheduleMainThreadTask(() =>
			{
				errorCallback(request, error);
			});	
		}
	}
}
