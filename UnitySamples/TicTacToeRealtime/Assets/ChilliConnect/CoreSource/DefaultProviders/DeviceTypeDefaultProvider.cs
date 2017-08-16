//
//  Created by Richard Sanderson on 2017-06-02
//
//  The MIT License (MIT)
//
//  Copyright (c) 2017 ChilliConnect Limited
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
using UnityEngine;
using System;

namespace SdkCore
{
	/// <summary>
	/// <para>A class that identifies the device type to one of the types accepted by ChilliConnect.</para>
	/// </summary>
	public sealed class DeviceTypeDefaultProvider
	{
		private const float TabletScreenSizeThreshold = 6f;

		/// <summary>
		/// An enum describing the various possible device types that are accepted by ChilliConnect.
		/// </summary>
		public enum Type
		{
			/// <summary>
			/// Phone device type.
			/// </summary>
			Phone,

			/// <summary>
			/// Tablet device type.
			/// </summary>
			Tablet,

			/// <summary>
			/// Browser device type.
			/// </summary>
			Browser,

			/// <summary>
			/// Desktop device type.
			/// </summary>
			Desktop,

			/// <summary>
			/// Any other type, or unable to be detected.
			/// </summary>
			Other
		}

		/// <summary>
		/// Detects and returns the Type of the device being run on.
		/// </summary>
		/// 
		/// <returns>A device type identifier string value accepted by ChilliConnect.</returns>
		public static string GetDefault()
		{
			return GetApiValue (Identify());
		}

		/// <summary>
		/// Detects and returns the Type of the device being run on.
		/// </summary>
		/// 
		/// <returns>A device type.</returns>
		private static Type Identify()
		{
			Type deviceType = Type.Other;

			if (SystemInfo.deviceType == DeviceType.Handheld) {
				deviceType = IsTablet() ? Type.Tablet : Type.Phone;
			} else if (SystemInfo.deviceType == DeviceType.Desktop) {
				deviceType = Type.Desktop;
			}

			if (Application.platform == RuntimePlatform.WebGLPlayer) {
				deviceType = Type.Browser;
			}

			return deviceType;
		}

		private static bool IsTablet()
		{
			return DeviceDiagonalSizeInInches() >= TabletScreenSizeThreshold;
		}

		private static float DeviceDiagonalSizeInInches ()
		{
			float screenWidth = Screen.width / Screen.dpi;
			float screenHeight = Screen.height / Screen.dpi;
			return Mathf.Sqrt (Mathf.Pow (screenWidth, 2) + Mathf.Pow (screenHeight, 2));
		}

		private static string GetApiValue(Type deviceType)
		{
			switch (deviceType) {
				case Type.Phone:
					return "PHONE";
				case Type.Tablet:
					return "TABLET";
				case Type.Browser:
					return "BROWSER";
				case Type.Desktop:
					return "DESKTOP";
			}

			return "OTHER";
		}
	}
}
