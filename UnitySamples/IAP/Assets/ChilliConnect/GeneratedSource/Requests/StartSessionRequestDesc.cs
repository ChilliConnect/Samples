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
	/// </para>A mutable description of a StartSessionRequest.</para>
	///
	/// </para>This is not thread-safe and should typically only be used to create new 
	/// instances of StartSessionRequest.</para>
	/// </summary>
	public sealed class StartSessionRequestDesc
	{
		/// <summary>
		/// ID that uniquely identifies this player. This ID should not clash with any other
		/// player and should persist across Sessions.
		/// </summary>
        public string UserId { get; set; }
	
		/// <summary>
		/// The client version of your game.
		/// </summary>
        public string AppVersion { get; set; }
	
		/// <summary>
		/// Country of the player. Must be two letter country code ISO 3166-1 alpha-2. E.g.
		/// GB. If not provided, will be automatically populated.
		/// </summary>
        public string Country { get; set; }
	
		/// <summary>
		/// Type of device being used by the player. Accepted values: PHONE, TABLET, BROWSER,
		/// DESKTOP, OTHER.
		/// </summary>
        public string DeviceType { get; set; }
	
		/// <summary>
		/// Platform of the device being used by the player. Accepted values: ANDROID, IOS,
		/// KINDLE, WINDOWS, MACOS, LINUX, OTHER.
		/// </summary>
        public string Platform { get; set; }
	
		/// <summary>
		/// The player's currently assigned AB Test Key.
		/// </summary>
        public string TestKey { get; set; }
	
		/// <summary>
		/// The player's currently assigned AB Test Group Key. Only accepted with TestKey.
		/// </summary>
        public string TestGroupKey { get; set; }

		/// <summary>
		/// Initialises a new instance of the description with the given required properties.
		/// </summary>
		///
		/// <param name="userId">ID that uniquely identifies this player. This ID should not clash with any other
		/// player and should persist across Sessions.</param>
		public StartSessionRequestDesc(string userId)
		{
			ReleaseAssert.IsNotNull(userId, "User Id cannot be null.");
	
            UserId = userId;
		}
	}
}
