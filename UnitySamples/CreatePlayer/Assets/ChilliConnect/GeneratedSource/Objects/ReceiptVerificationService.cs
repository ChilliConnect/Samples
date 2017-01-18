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
	/// <para>Contains detailed information about the response from the Amazon Receipt
	/// Verification Service.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class ReceiptVerificationService
	{
		/// <summary>
		/// The HTTP status returned from the Receipt Verification Service as described in
		/// the Amazon Documentation at
		/// 'https://developer.amazon.com/public/apis/earn/in-app-purchasing/docs-v2/verifying-receipts-in-iap-2.0#RVS
		/// Response Syntax'.
		/// </summary>
        public int HttpCode { get; private set; }
	
		/// <summary>
		/// A textual description of the returned HTTP status code.
		/// </summary>
        public string HttpCodeDescription { get; private set; }
	
		/// <summary>
		/// The full response from the Receipt Verification Service verification service as a
		/// JSON encoded string. See the Amazon Documentation at
		/// 'https://developer.amazon.com/public/apis/earn/in-app-purchasing/docs-v2/verifying-receipts-in-iap-2.0#RVS
		/// Response Syntax' for more information.
		/// </summary>
        public string ResponseJson { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given properties.
		/// </summary>
		///
		/// <param name="httpCode">The HTTP status returned from the Receipt Verification Service as described in
		/// the Amazon Documentation at
		/// 'https://developer.amazon.com/public/apis/earn/in-app-purchasing/docs-v2/verifying-receipts-in-iap-2.0#RVS
		/// Response Syntax'.</param>
		/// <param name="httpCodeDescription">A textual description of the returned HTTP status code.</param>
		/// <param name="responseJson">The full response from the Receipt Verification Service verification service as a
		/// JSON encoded string. See the Amazon Documentation at
		/// 'https://developer.amazon.com/public/apis/earn/in-app-purchasing/docs-v2/verifying-receipts-in-iap-2.0#RVS
		/// Response Syntax' for more information.</param>
		public ReceiptVerificationService(int httpCode, string httpCodeDescription, string responseJson)
		{
			ReleaseAssert.IsNotNull(httpCodeDescription, "Http Code Description cannot be null.");
			ReleaseAssert.IsNotNull(responseJson, "Response Json cannot be null.");
	
            HttpCode = httpCode;
            HttpCodeDescription = httpCodeDescription;
            ResponseJson = responseJson;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public ReceiptVerificationService(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("HttpCode"), "Json is missing required field 'HttpCode'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("HttpCodeDescription"), "Json is missing required field 'HttpCodeDescription'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("ResponseJson"), "Json is missing required field 'ResponseJson'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Http Code
				if (entry.Key == "HttpCode")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    HttpCode = (int)(long)entry.Value;
				}
		
				// Http Code Description
				else if (entry.Key == "HttpCodeDescription")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    HttpCodeDescription = (string)entry.Value;
				}
		
				// Response Json
				else if (entry.Key == "ResponseJson")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    ResponseJson = (string)entry.Value;
				}
	
				// An error has occurred.
				else
				{
#if DEBUG
					throw new ArgumentException("Input Json contains an invalid field.");
#endif
				}
			}
		}

		/// <summary>
		/// Serialises all properties. The output will be a dictionary containing the
		/// objects properties in a form that can easily be converted to Json. 
		/// </summary>
		///
		/// <returns>The serialised object in dictionary form.</returns>
		public IDictionary<string, object> Serialise()
		{
            var dictionary = new Dictionary<string, object>();
			
			// Http Code
			dictionary.Add("HttpCode", HttpCode);
			
			// Http Code Description
			dictionary.Add("HttpCodeDescription", HttpCodeDescription);
			
			// Response Json
			dictionary.Add("ResponseJson", ResponseJson);
			
			return dictionary;
		}
	}
}
