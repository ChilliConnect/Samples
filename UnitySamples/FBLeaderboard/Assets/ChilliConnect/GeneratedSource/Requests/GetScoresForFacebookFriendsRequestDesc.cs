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
	/// </para>A mutable description of a GetScoresForFacebookFriendsRequest.</para>
	///
	/// </para>This is not thread-safe and should typically only be used to create new 
	/// instances of GetScoresForFacebookFriendsRequest.</para>
	/// </summary>
	public sealed class GetScoresForFacebookFriendsRequestDesc
	{
		/// <summary>
		/// The Key that identifies the leaderboard.
		/// </summary>
        public string Key { get; set; }
	
		/// <summary>
		/// If true, include the currently logged in player's score in the results. If not
		/// provided, this will be defaulted to false.
		/// </summary>
        public bool? IncludeMe { get; set; }

		/// <summary>
		/// Initialises a new instance of the description with the given required properties.
		/// </summary>
		///
		/// <param name="key">The Key that identifies the leaderboard.</param>
		public GetScoresForFacebookFriendsRequestDesc(string key)
		{
			ReleaseAssert.IsNotNull(key, "Key cannot be null.");
	
            Key = key;
		}
	}
}
