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
	/// <para>A container for all information that will be sent to the server during a
 	/// Validate Google Iap api call.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class ValidateGoogleIapRequest : IImmediateServerRequest
	{
		/// <summary>
		/// The url the request will be sent to.
		/// </summary>
		public string Url { get; private set; }
		
		/// <summary>
		/// The HTTP request method that should be used.
		/// </summary>
		public HttpRequestMethod HttpRequestMethod { get; private set; }
		
		/// <summary>
		/// A valid session ConnectAccessToken obtained through one of the login endpoints.
		/// </summary>
        public string ConnectAccessToken { get; private set; }
	
		/// <summary>
		/// A JSON encoded string returned from a successful in app billing purchase. See the
		/// Google Documentation at
		/// 'http://developer.android.com/google/play/billing/billing_integrate.html#Purchase'
		/// on how to access this value from your app.
		/// </summary>
        public string PurchaseData { get; private set; }
	
		/// <summary>
		/// A signature of the PurchaseData returned from a successful in app billing
		/// purchase. See the Google Documentation at
		/// 'http://developer.android.com/google/play/billing/billing_integrate.html#Purchase'
		/// on how to access this value from your app.
		/// </summary>
        public string PurchaseDataSignature { get; private set; }

		/// <summary>
		/// Initialises a new instance of the request with the given properties.
		/// </summary>
		///
		/// <param name="purchaseData">A JSON encoded string returned from a successful in app billing purchase. See the
		/// Google Documentation at
		/// 'http://developer.android.com/google/play/billing/billing_integrate.html#Purchase'
		/// on how to access this value from your app.</param>
		/// <param name="purchaseDataSignature">A signature of the PurchaseData returned from a successful in app billing
		/// purchase. See the Google Documentation at
		/// 'http://developer.android.com/google/play/billing/billing_integrate.html#Purchase'
		/// on how to access this value from your app.</param>
		/// <param name="connectAccessToken">A valid session ConnectAccessToken obtained through one of the login endpoints.</param>
		public ValidateGoogleIapRequest(string purchaseData, string purchaseDataSignature, string connectAccessToken)
		{
			ReleaseAssert.IsNotNull(purchaseData, "Purchase Data cannot be null.");
			ReleaseAssert.IsNotNull(purchaseDataSignature, "Purchase Data Signature cannot be null.");
	
			ReleaseAssert.IsNotNull(connectAccessToken, "Connect Access Token cannot be null.");
	
            PurchaseData = purchaseData;
            PurchaseDataSignature = purchaseDataSignature;
            ConnectAccessToken = connectAccessToken;
			
			Url = "https://connect.chilliconnect.com/1.0/iap/validate/google";
			HttpRequestMethod = HttpRequestMethod.Post;
		}

		/// <summary>
		/// Serialises all header properties. The output will be a dictionary containing 
		/// the extra header key-value pairs in addition the standard headers sent with 
		/// all server requests. Will return an empty dictionary if there are no headers.
		/// </summary>
		///
		/// <returns>The header hey-value pairs.</returns>
		public IDictionary<string, string> SerialiseHeaders()
		{
			var dictionary = new Dictionary<string, string>();
			
			// Connect Access Token
			dictionary.Add("Connect-Access-Token", ConnectAccessToken.ToString());
		
			return dictionary;
		}
		
		/// <summary>
		/// Serialises all body properties. The output will be a dictionary containing the 
		/// body of the request in a form that can easily be converted to Json. Will return
		/// an empty dictionary if there is no body.
		/// </summary>
		///
		/// <returns>The body Json in dictionary form.</returns>
		public IDictionary<string, object> SerialiseBody()
		{
            var dictionary = new Dictionary<string, object>();
			
			// Purchase Data
			dictionary.Add("PurchaseData", PurchaseData);
			
			// Purchase Data Signature
			dictionary.Add("PurchaseDataSignature", PurchaseDataSignature);
	
			return dictionary;
		}
	}
}
