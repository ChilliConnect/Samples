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
	/// </para>A mutable description of a LogInUsingMobileDeviceIdRequest.</para>
	///
	/// </para>This is not thread-safe and should typically only be used to create new 
	/// instances of LogInUsingMobileDeviceIdRequest.</para>
	/// </summary>
	public sealed class LogInUsingMobileDeviceIdRequestDesc
	{
		/// <summary>
		/// DeviceId is provided by iOS system on calling the Social system.
		/// </summary>
        public string DeviceId { get; set; }
	
		/// <summary>
		/// Platform of the device being used by the player. Accepted values: ANDROID, IOS,
		/// KINDLE.
		/// </summary>
        public string Platform { get; set; }
	
		/// <summary>
		/// Flag which will tell ChilliConnect to create a player if there is not one
		/// associated with this DeviceId and Platform.
		/// </summary>
        public bool? CreatePlayer { get; set; }
	
		/// <summary>
		/// Model of device being used by the player. E.g. SamsungABC123.
		/// </summary>
        public string DeviceModel { get; set; }
	
		/// <summary>
		/// Type of device being used by the player. Accepted values: PHONE, TABLET, BROWSER,
		/// DESKTOP, OTHER.
		/// </summary>
        public string DeviceType { get; set; }
	
		/// <summary>
		/// The client version of your game.
		/// </summary>
        public string AppVersion { get; set; }

		/// <summary>
		/// Initialises a new instance of the description with the given required properties.
		/// </summary>
		///
		/// <param name="deviceId">DeviceId is provided by iOS system on calling the Social system.</param>
		/// <param name="platform">Platform of the device being used by the player. Accepted values: ANDROID, IOS,
		/// KINDLE.</param>
		public LogInUsingMobileDeviceIdRequestDesc(string deviceId, string platform)
		{
			ReleaseAssert.IsNotNull(deviceId, "Device Id cannot be null.");
			ReleaseAssert.IsNotNull(platform, "Platform cannot be null.");
	
            DeviceId = deviceId;
            Platform = platform;
		}
	}
}
