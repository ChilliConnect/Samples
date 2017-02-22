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
	/// <para>Contains detailed information about the response from the AppStore verification
	/// service.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class AppStore
	{
		/// <summary>
		/// The status code sent back from the AppStore verification service as described in
		/// the Apple Documentation at
		/// 'https://developer.apple.com/library/ios/releasenotes/General/ValidateAppStoreReceipt/Chapters/ValidateRemotely.html'.
		/// </summary>
        public int ResponseCode { get; private set; }
	
		/// <summary>
		/// A textual description of the returned status code.
		/// </summary>
        public string ResponseMessage { get; private set; }
	
		/// <summary>
		/// The full response from the AppStore verification service as a JSON encoded
		/// string. See the Apple Documentation at
		/// 'https://developer.apple.com/library/ios/releasenotes/General/ValidateAppStoreReceipt/Chapters/ReceiptFields.html'
		/// for more information.
		/// </summary>
        public string ReceiptData { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given properties.
		/// </summary>
		///
		/// <param name="responseCode">The status code sent back from the AppStore verification service as described in
		/// the Apple Documentation at
		/// 'https://developer.apple.com/library/ios/releasenotes/General/ValidateAppStoreReceipt/Chapters/ValidateRemotely.html'.</param>
		/// <param name="responseMessage">A textual description of the returned status code.</param>
		/// <param name="receiptData">The full response from the AppStore verification service as a JSON encoded
		/// string. See the Apple Documentation at
		/// 'https://developer.apple.com/library/ios/releasenotes/General/ValidateAppStoreReceipt/Chapters/ReceiptFields.html'
		/// for more information.</param>
		public AppStore(int responseCode, string responseMessage, string receiptData)
		{
			ReleaseAssert.IsNotNull(responseMessage, "Response Message cannot be null.");
			ReleaseAssert.IsNotNull(receiptData, "Receipt Data cannot be null.");
	
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
            ReceiptData = receiptData;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public AppStore(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("ResponseCode"), "Json is missing required field 'ResponseCode'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("ResponseMessage"), "Json is missing required field 'ResponseMessage'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("ReceiptData"), "Json is missing required field 'ReceiptData'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Response Code
				if (entry.Key == "ResponseCode")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    ResponseCode = (int)(long)entry.Value;
				}
		
				// Response Message
				else if (entry.Key == "ResponseMessage")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    ResponseMessage = (string)entry.Value;
				}
		
				// Receipt Data
				else if (entry.Key == "ReceiptData")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    ReceiptData = (string)entry.Value;
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
			
			// Response Code
			dictionary.Add("ResponseCode", ResponseCode);
			
			// Response Message
			dictionary.Add("ResponseMessage", ResponseMessage);
			
			// Receipt Data
			dictionary.Add("ReceiptData", ReceiptData);
			
			return dictionary;
		}
	}
}
