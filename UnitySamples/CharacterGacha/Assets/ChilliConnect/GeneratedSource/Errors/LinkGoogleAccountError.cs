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
	/// LinkGoogleAccountRequest.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class LinkGoogleAccountError
	{
		/// <summary>
		/// An enum describing each of the possible error codes that can be returned from a
		/// CCLinkGoogleAccountRequest.
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
			/// Temporary Service Error. A temporary error is preventing the request from being
			/// processed.
			/// </summary>
			TemporaryServiceError = 1008,
	
			/// <summary>
			/// Supplied Auth Code Is Malformed. The Google verification service has rejected the
			/// supplied AuthCode. The supplied AuthCode is malformed, please check the
			/// formatting.
			/// </summary>
			SuppliedAuthCodeMalformed = 2020,
	
			/// <summary>
			/// Supplied Auth Code Could Not Be Authenticated. The Google verification service
			/// has rejected the supplied AuthCode. This could be due to configuration error,
			/// ensure all Google Developer Console values have been configured.
			/// </summary>
			SuppliedAuthCodeInvalid = 2021,
	
			/// <summary>
			/// Supplied Auth Code Has Already Been Redeemed. The Google verification service has
			/// rejected the supplied AuthCode. The supplied AuthCode has already been previously
			/// accepted by the service and requires a fresh code.
			/// </summary>
			SuppliedAuthCodeAlreadyRedeemed = 2019,
	
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
			/// No Google OAuth Configured For Project. Get the appropriate OAuth information
			/// from your google-services.json file or the Google Developer Console and configure
			/// your Game in the ChilliConnect Dashboard Api Settings area.
			/// </summary>
			NoOauthHasBeenConfiguredForApplication = 2018,
	
			/// <summary>
			/// Player Already Linked With A Google Account. The Google account is already linked
			/// with another player of this game.
			/// </summary>
			PlayerAlreadyLinkedToGoogleAccount = 2014,
	
			/// <summary>
			/// GoogleId Is Already Linked With Another Player. The supplied Google ID is already
			/// associated with another player.
			/// </summary>
			GoogleAccountLinked = 2015,
	
			/// <summary>
			/// Player Not Linked To Google. The current Player is not linked to a Google
			/// account.
			/// </summary>
			PlayerNotLinkedToGoogle = 2016,
	
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
		public LinkGoogleAccountError(ServerResponse serverResponse)
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
		public LinkGoogleAccountError(Error errorCode)
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
				case 1008:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 503, @"Invalid HTTP response code for error code.");
					return Error.TemporaryServiceError;		
				case 2020:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 409, @"Invalid HTTP response code for error code.");
					return Error.SuppliedAuthCodeMalformed;		
				case 2021:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 409, @"Invalid HTTP response code for error code.");
					return Error.SuppliedAuthCodeInvalid;		
				case 2019:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 409, @"Invalid HTTP response code for error code.");
					return Error.SuppliedAuthCodeAlreadyRedeemed;		
				case 1003:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 401, @"Invalid HTTP response code for error code.");
					return Error.ExpiredConnectAccessToken;		
				case 1004:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 401, @"Invalid HTTP response code for error code.");
					return Error.InvalidConnectAccessToken;		
				case 2018:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 409, @"Invalid HTTP response code for error code.");
					return Error.NoOauthHasBeenConfiguredForApplication;		
				case 2014:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 409, @"Invalid HTTP response code for error code.");
					return Error.PlayerAlreadyLinkedToGoogleAccount;		
				case 2015:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 409, @"Invalid HTTP response code for error code.");
					return Error.GoogleAccountLinked;		
				case 2016:
					ReleaseAssert.IsTrue(serverResponse.HttpResponseCode == 422, @"Invalid HTTP response code for error code.");
					return Error.PlayerNotLinkedToGoogle;		
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
				case Error.TemporaryServiceError:
					return "Temporary Service Error. A temporary error is preventing the request from being"
						+ " processed.";
				case Error.SuppliedAuthCodeMalformed:
					return "Supplied Auth Code Is Malformed. The Google verification service has rejected the"
						+ " supplied AuthCode. The supplied AuthCode is malformed, please check the"
						+ " formatting.";
				case Error.SuppliedAuthCodeInvalid:
					return "Supplied Auth Code Could Not Be Authenticated. The Google verification service"
						+ " has rejected the supplied AuthCode. This could be due to configuration error,"
						+ " ensure all Google Developer Console values have been configured.";
				case Error.SuppliedAuthCodeAlreadyRedeemed:
					return "Supplied Auth Code Has Already Been Redeemed. The Google verification service has"
						+ " rejected the supplied AuthCode. The supplied AuthCode has already been previously"
						+ " accepted by the service and requires a fresh code.";
				case Error.ExpiredConnectAccessToken:
					return "Expired Connect Access Token. The Connect Access Token used to authenticate with"
						+ " the server has expired and should be renewed.";
				case Error.InvalidConnectAccessToken:
					return "Invalid Connect Access Token. The Connect Access Token was not valid and cannot"
						+ " be used to authenticate requests.";
				case Error.NoOauthHasBeenConfiguredForApplication:
					return "No Google OAuth Configured For Project. Get the appropriate OAuth information"
						+ " from your google-services.json file or the Google Developer Console and configure"
						+ " your Game in the ChilliConnect Dashboard Api Settings area.";
				case Error.PlayerAlreadyLinkedToGoogleAccount:
					return "Player Already Linked With A Google Account. The Google account is already linked"
						+ " with another player of this game.";
				case Error.GoogleAccountLinked:
					return "GoogleId Is Already Linked With Another Player. The supplied Google ID is already"
						+ " associated with another player.";
				case Error.PlayerNotLinkedToGoogle:
					return "Player Not Linked To Google. The current Player is not linked to a Google"
						+ " account.";
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
