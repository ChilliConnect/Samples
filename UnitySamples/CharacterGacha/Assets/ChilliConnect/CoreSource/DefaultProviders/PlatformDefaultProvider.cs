//
//  Created by Richard Sanderson on 2017-06-06
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
	/// <para>A class that identifies the platform to one of the types accepted by ChilliConnect.</para>
	/// </summary>
	public sealed class PlatformDefaultProvider
	{
		/// <summary>
		/// An enum describing the various possible platforms that are accepted by ChilliConnect.
		/// </summary>
		public enum Type
		{
			/// <summary>
			/// Android platform.
			/// </summary>
			Android,

			/// <summary>
			/// iOS platform.
			/// </summary>
			Ios,

			/// <summary>
			/// Kindle platform.
			/// </summary>
			Kindle,

			/// <summary>
			/// Windows platform.
			/// </summary>
			Windows,

			/// <summary>
			/// Desktop device type.
			/// </summary>
			Macos,

			/// <summary>
			/// Desktop device type.
			/// </summary>
			Linux,

			/// <summary>
			/// Any other type, or unable to be detected.
			/// </summary>
			Other
		}

		/// <summary>
		/// Detects and returns the platform of the device being run on.
		/// </summary>
		/// 
		/// <returns>A platform identifier string value accepted by ChilliConnect.</returns>
		public static string GetDefault()
		{
			return GetApiValue(Identify ());
		}

		/// <summary>
		/// Detects and returns the platform of the device being run on.
		/// </summary>
		/// 
		/// <returns>A platform type.</returns>
		private static Type Identify()
		{
			
			Type platform = Type.Other;

			switch(Application.platform) {
				case RuntimePlatform.Android:
					platform = Type.Android;
					break;
				case RuntimePlatform.IPhonePlayer:
					platform = Type.Ios;
					break;
				case RuntimePlatform.LinuxPlayer:
					platform = Type.Linux;
					break;
				case RuntimePlatform.OSXPlayer:
					platform = Type.Macos;
					break;
				case RuntimePlatform.WindowsPlayer:
					platform = Type.Windows;
					break;
			}

			return platform;
		}

		private static string GetApiValue(Type platform)
		{
			switch (platform) {
			case Type.Android:
				return "ANDROID";
			case Type.Ios:
				return "IOS";
			case Type.Kindle:
				return "KINDLE";
			case Type.Windows:
				return "WINDOWS";
			case Type.Macos:
				return "MACOS";
			case Type.Linux:
				return "LINUX";
			}

			return "OTHER";
		}
	}
}
