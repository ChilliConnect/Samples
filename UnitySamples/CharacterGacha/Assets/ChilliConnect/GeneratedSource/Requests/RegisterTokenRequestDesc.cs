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
	/// </para>A mutable description of a RegisterTokenRequest.</para>
	///
	/// </para>This is not thread-safe and should typically only be used to create new 
	/// instances of RegisterTokenRequest.</para>
	/// </summary>
	public sealed class RegisterTokenRequestDesc
	{
		/// <summary>
		/// The push notification service the device token belongs to. Must be one of APNS,
		/// GCM or ADM.
		/// </summary>
        public string Service { get; set; }
	
		/// <summary>
		/// The Push Token. Note: for APNS (iOS) base64 encode the NSData object to form this
		/// string.
		/// </summary>
        public string DeviceToken { get; set; }
	
		/// <summary>
		/// If true, will clear any previously stored Push Tokens for this Player and
		/// Service. Defaults to false.
		/// </summary>
        public bool? Overwrite { get; set; }

		/// <summary>
		/// Initialises a new instance of the description with the given required properties.
		/// </summary>
		///
		/// <param name="service">The push notification service the device token belongs to. Must be one of APNS,
		/// GCM or ADM.</param>
		/// <param name="deviceToken">The Push Token. Note: for APNS (iOS) base64 encode the NSData object to form this
		/// string.</param>
		public RegisterTokenRequestDesc(string service, string deviceToken)
		{
			ReleaseAssert.IsNotNull(service, "Service cannot be null.");
			ReleaseAssert.IsNotNull(deviceToken, "Device Token cannot be null.");
	
            Service = service;
            DeviceToken = deviceToken;
		}
	}
}
