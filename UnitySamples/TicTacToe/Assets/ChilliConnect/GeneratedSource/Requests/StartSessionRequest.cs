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
 	/// Start Session api call.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class StartSessionRequest : IImmediateServerRequest
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
		/// The GameToken.
		/// </summary>
        public string GameToken { get; private set; }
	
		/// <summary>
		/// ID that uniquely identifies this player. This ID should not clash with any other
		/// player and should persist across Sessions.
		/// </summary>
        public string UserId { get; private set; }
	
		/// <summary>
		/// The local device time that the session started. Format: ISO8601 e.g.
		/// 2016-01-12T11:08:23.
		/// </summary>
        public DateTime Date { get; private set; }
	
		/// <summary>
		/// The client version of your game.
		/// </summary>
        public string AppVersion { get; private set; }
	
		/// <summary>
		/// Country of the player. Must be two letter country code ISO 3166-1 alpha-2. E.g.
		/// GB. If not provided, will be automatically populated.
		/// </summary>
        public string Country { get; private set; }
	
		/// <summary>
		/// Type of device being used by the player. Accepted values: PHONE, TABLET, BROWSER,
		/// DESKTOP, OTHER.
		/// </summary>
        public string DeviceType { get; private set; }
	
		/// <summary>
		/// Platform of the device being used by the player. Accepted values: ANDROID, IOS,
		/// KINDLE, WINDOWS, MACOS, LINUX, OTHER.
		/// </summary>
        public string Platform { get; private set; }
	
		/// <summary>
		/// The player's currently assigned AB Test Key.
		/// </summary>
        public string TestKey { get; private set; }
	
		/// <summary>
		/// The player's currently assigned AB Test Group Key. Only accepted with TestKey.
		/// </summary>
        public string TestGroupKey { get; private set; }

		/// <summary>
		/// Initialises a new instance of the request with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		/// <param name="gameToken">The GameToken.</param>
		public StartSessionRequest(StartSessionRequestDesc desc, string gameToken)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.UserId, "UserId cannot be null.");
	
			ReleaseAssert.IsNotNull(gameToken, "Game Token cannot be null.");
	
            UserId = desc.UserId;
            AppVersion = desc.AppVersion;
            Country = desc.Country;
            if (desc.DeviceType == null)
			{
                DeviceType = DeviceTypeDefaultProvider.GetDefault();
            } else {
            	DeviceType = desc.DeviceType;
            }
            if (desc.Platform == null)
			{
                Platform = PlatformDefaultProvider.GetDefault();
            } else {
            	Platform = desc.Platform;
            }
            TestKey = desc.TestKey;
            TestGroupKey = desc.TestGroupKey;
            GameToken = gameToken;
			Date = DateTime.Now;
	
			Url = "https://metrics.chilliconnect.com/1.0/session/start";
			HttpRequestMethod = HttpRequestMethod.Post;
		}

		/// <summary>
		/// Serialises all header properties. The output will be a dictionary containing 
		/// the extra header key-value pairs in addition the standard headers sent with 
		/// all server requests. Will return an empty dictionary if there are no headers.
		/// </summary>
		///
		/// <returns>The header key-value pairs.</returns>
		public IDictionary<string, string> SerialiseHeaders()
		{
			var dictionary = new Dictionary<string, string>();
			
			// Game Token
			dictionary.Add("Game-Token", GameToken.ToString());
		
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
			
			// User Id
			dictionary.Add("UserID", UserId);
			
			// Date
            dictionary.Add("Date", JsonSerialisation.Serialise(Date));
			
			// App Version
            if (AppVersion != null)
			{
				dictionary.Add("AppVersion", AppVersion);
            }
			
			// Country
            if (Country != null)
			{
				dictionary.Add("Country", Country);
            }
			
			// Device Type
            if (DeviceType != null)
			{
				dictionary.Add("DeviceType", DeviceType);
            }
			
			// Platform
            if (Platform != null)
			{
				dictionary.Add("Platform", Platform);
            }
			
			// Test Key
            if (TestKey != null)
			{
				dictionary.Add("TestKey", TestKey);
            }
			
			// Test Group Key
            if (TestGroupKey != null)
			{
				dictionary.Add("TestGroupKey", TestGroupKey);
            }
	
			return dictionary;
		}
	}
}
