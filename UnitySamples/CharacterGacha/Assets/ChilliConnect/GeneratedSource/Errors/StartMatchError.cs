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
using System.Diagnostics;
using SdkCore;

namespace ChilliConnect
{
	/// <summary>
	/// <para>A container for information on any errors that occur during a 
	/// StartMatchRequest.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class StartMatchError
	{
		/// <summary>
		/// An enum describing each of the possible error codes that can be returned from a
		/// CCStartMatchRequest.
		/// </summary>
		public enum Error
		{
			/// <summary> 
			/// A connection could not be established.
			/// </summary>
			CouldNotConnect = -2,
	
			/// <summary> 
			/// An unexpected, fatal error has occured on the server. 
			/// </summary>
			UnexpectedError = 1,
			
	
			/// <summary>
			/// Invalid Request. One of more of the provided fields were not correctly formatted.
			/// The data property of the response body will contain specific error messages for
			/// each field.
			/// </summary>
			InvalidRequest = 1007,
	
			/// <summary>
			/// Rate Limit Reached. Too many requests. Player has been rate limited.
			/// </summary>
			RateLimitReached = 10002,
	
			/// <summary>
			/// Expired Connect Access Token. The Connect Access Token used to authenticate with
			/// the server has expired and should be renewed.
			/// </summary>
			ExpiredConnectAccessToken = 1003,
	
			/// <summary>
			/// Invalid Connect Access Token. The Connect Access Token was not valid and cannot
			/// be used to authenticate requests.
			/// </summary>
			InvalidConnectAccessToken = 1004,
	
			/// <summary>
			/// Temporary Service Error. A temporary error is preventing the request from being
			/// processed.
			/// </summary>
			TemporaryServiceError = 1008,
	
			/// <summary>
			/// Match Not Found. The supplied ID did not match any Match.
			/// </summary>
			MatchNotFound = 15001,
	
			/// <summary>
			/// Match Not Available. The Match specified is not available, it may be fully
			/// subscribed, or have timed out.
			/// </summary>
			MatchNotAvailable = 15009,
	
			/// <summary>
			/// Player Does Not Exist In Specified Match. The current user does not exist in the
			/// Match that is attempting to be started.
			/// </summary>
			PlayerNotExistMatch = 15006,
	
			/// <summary>
			/// Cannot Start Match, Not Enough Players. The Match being started does not have any
			/// Players.
			/// </summary>
			NotEnoughPlayersInMatch = 15005,
	
			/// <summary>
			/// Match Has Already Been Started. The Match specified is currently in the
			/// IN_PROGRESS state.
			/// </summary>
			MatchAlreadyStarted = 15014,
	
			/// <summary>
			/// List Of Players Does Not Match Provided Turn Order. The Turn Order list provided
			/// does not match the list of Players currently in the Match.
			/// </summary>
			PlayerListMisMatch = 15011,
	
			/// <summary>
			/// Turn Order Has Not Been Provided. The Sequential Match the has attempted to be
			/// started requires a Turn Order list to be provided.
			/// </summary>
			NoTurnOrderListProvided = 15013,
	
			/// <summary>
			/// Method Disabled. Public access to this method has been disabled on the
			/// ChilliConnect Dashboard.
			/// </summary>
			MethodDisabled = 1011
		}
		
		private const int SuccessHttpResponseCode = 200;
		private const int UnexpectedErrorHttpResponseCode = 500;
		
		/// <summary> 
		/// A code describing the error that has occurred.
		/// </summary>
		public Error ErrorCode { get; private set; }
		
		/// <summary> 
		/// A description of the error that as occurred.
		/// </summary>
		public string ErrorDescription { get; private set; }
        
        /// <summary> 
		/// A dictionary of additional, error specific information.
		/// </summary>
		public MultiTypeValue ErrorData { get; private set; }

		/// <summary> 
		/// Initialises a new instance from the given server response. The server response
		/// must describe an error otherwise this will throw an error.
		/// </summary>
		///
		/// <param name="serverResponse">The server response from which to initialise this error.
		/// The response must describe an error state.</param>
		public StartMatchError(ServerResponse serverResponse)
		{
			ReleaseAssert.IsNotNull(serverResponse, "A server response must be supplied.");
			ReleaseAssert.IsTrue(serverResponse.Result != HttpResult.Success || serverResponse.HttpResponseCode != SuccessHttpResponseCode, "Input server response must describe an error.");
			
			switch (serverResponse.Result)
			{
				case HttpResult.Success:
					if (serverResponse.HttpResponseCode == UnexpectedErrorHttpResponseCode)
					{
						ErrorCode = Error.UnexpectedError;
                        ErrorData = MultiTypeValue.Null;
					}
					else
					{
						ErrorCode = GetErrorCode(serverResponse);
                        ErrorData = GetErrorData(serverResponse.Body);
					}
					break;
				case HttpResult.CouldNotConnect:
					ErrorCode = Error.CouldNotConnect;
                    ErrorData = MultiTypeValue.Null;
					break;
				default:
					throw new ArgumentException("Invalid value for server response result.");
			}
			
			ErrorDescription = GetErrorDescription(ErrorCode);
		}
		
		/// <summary> 
		/// Initialises a new instance from the given error code.
		/// </summary>
		///
		/// <param name="errorCode">The error code.</param>
		public StartMatchError(Error errorCode)
		{
			ErrorCode = errorCode;
            ErrorData = MultiTypeValue.Null;
			ErrorDescription = GetErrorDescription(ErrorCode);
		}
		
		/// <summary>
		/// Parses the response body to get the response code.
		/// </summary>
		///
		/// <returns>The error code in the given response body.</returns>
		///
		/// <param name="serverResponse">The server response from which to get the error code. This
		/// must describe an successful response from the server which contains an error in the
		/// response body.</param>
		private static Error GetErrorCode(ServerResponse serverResponse) 
		{
			const string JsonKeyErrorCode = "Code";
			
			ReleaseAssert.IsNotNull(serverResponse, "A server response must be supplied.");
			ReleaseAssert.IsTrue(serverResponse.Result == HttpResult.Success, "The result must describe a successful server response.");
			ReleaseAssert.IsTrue(serverResponse.HttpResponseCode != SuccessHttpResponseCode && serverResponse.HttpResponseCode != UnexpectedErrorHttpResponseCode, 
				"Must not be a successful or unexpected HTTP response code.");
				
			object errorCodeObject = serverResponse.Body[JsonKeyErrorCode];
			ReleaseAssert.IsTrue(errorCodeObject is long, "'Code' must be a long.");
			
			long errorCode = (long)errorCodeObject;
			
			switch (errorCode)
			{
				case 1007:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 422, @"Invalid HTTP response code for error code.");
					return Error.InvalidRequest;		
				case 10002:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 429, @"Invalid HTTP response code for error code.");
					return Error.RateLimitReached;		
				case 1003:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 401, @"Invalid HTTP response code for error code.");
					return Error.ExpiredConnectAccessToken;		
				case 1004:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 401, @"Invalid HTTP response code for error code.");
					return Error.InvalidConnectAccessToken;		
				case 1008:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 503, @"Invalid HTTP response code for error code.");
					return Error.TemporaryServiceError;		
				case 15001:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 401, @"Invalid HTTP response code for error code.");
					return Error.MatchNotFound;		
				case 15009:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 422, @"Invalid HTTP response code for error code.");
					return Error.MatchNotAvailable;		
				case 15006:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 422, @"Invalid HTTP response code for error code.");
					return Error.PlayerNotExistMatch;		
				case 15005:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 422, @"Invalid HTTP response code for error code.");
					return Error.NotEnoughPlayersInMatch;		
				case 15014:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 422, @"Invalid HTTP response code for error code.");
					return Error.MatchAlreadyStarted;		
				case 15011:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 422, @"Invalid HTTP response code for error code.");
					return Error.PlayerListMisMatch;		
				case 15013:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 422, @"Invalid HTTP response code for error code.");
					return Error.NoTurnOrderListProvided;		
				case 1011:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 403, @"Invalid HTTP response code for error code.");
					return Error.MethodDisabled;		
				default:
					return Error.UnexpectedError;
			}
		}
        
        /// <summary>
        /// Extracts the error data json from the given response body.
        /// </summary>
        ///
        /// <returns>The additional error data.<returns/>
        ///
        /// <param name="responseBody">The response body containing the error data.</param>        
        private static MultiTypeValue GetErrorData(IDictionary<string, object> responseBody)
        {
            const string JsonKeyErrorData = "Data";
			
			ReleaseAssert.IsNotNull(responseBody, "The response body cannot be null.");
            
            if (!responseBody.ContainsKey(JsonKeyErrorData))
            {
                return MultiTypeValue.Null;
            }
            
            return new MultiTypeValue(responseBody[JsonKeyErrorData]);
        }
		
		/// <summary>
		/// Gets the error message for the given error code.
		/// </summary>
		///
		/// <returns>The error message.</returns>
		///		
		/// <param name="errorCode">The error code.</param>
		private static string GetErrorDescription(Error errorCode)
		{
			switch (errorCode) 
			{
				case Error.CouldNotConnect:
					return "A connection could not be established.";
				case Error.InvalidRequest:
					return "Invalid Request. One of more of the provided fields were not correctly formatted."
						+ " The data property of the response body will contain specific error messages for"
						+ " each field.";
				case Error.RateLimitReached:
					return "Rate Limit Reached. Too many requests. Player has been rate limited.";
				case Error.ExpiredConnectAccessToken:
					return "Expired Connect Access Token. The Connect Access Token used to authenticate with"
						+ " the server has expired and should be renewed.";
				case Error.InvalidConnectAccessToken:
					return "Invalid Connect Access Token. The Connect Access Token was not valid and cannot"
						+ " be used to authenticate requests.";
				case Error.TemporaryServiceError:
					return "Temporary Service Error. A temporary error is preventing the request from being"
						+ " processed.";
				case Error.MatchNotFound:
					return "Match Not Found. The supplied ID did not match any Match.";
				case Error.MatchNotAvailable:
					return "Match Not Available. The Match specified is not available, it may be fully"
						+ " subscribed, or have timed out.";
				case Error.PlayerNotExistMatch:
					return "Player Does Not Exist In Specified Match. The current user does not exist in the"
						+ " Match that is attempting to be started.";
				case Error.NotEnoughPlayersInMatch:
					return "Cannot Start Match, Not Enough Players. The Match being started does not have any"
						+ " Players.";
				case Error.MatchAlreadyStarted:
					return "Match Has Already Been Started. The Match specified is currently in the"
						+ " IN_PROGRESS state.";
				case Error.PlayerListMisMatch:
					return "List Of Players Does Not Match Provided Turn Order. The Turn Order list provided"
						+ " does not match the list of Players currently in the Match.";
				case Error.NoTurnOrderListProvided:
					return "Turn Order Has Not Been Provided. The Sequential Match the has attempted to be"
						+ " started requires a Turn Order list to be provided.";
				case Error.MethodDisabled:
					return "Method Disabled. Public access to this method has been disabled on the"
						+ " ChilliConnect Dashboard.";
				case Error.UnexpectedError:
				default:
					return "An unexpected server error occurred.";
			}
		}
	}
}
