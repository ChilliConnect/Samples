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
	/// </para>A mutable description of a AddScoreRequest.</para>
	///
	/// </para>This is not thread-safe and should typically only be used to create new 
	/// instances of AddScoreRequest.</para>
	/// </summary>
	public sealed class AddScoreRequestDesc
	{
		/// <summary>
		/// The score to post.
		/// </summary>
        public int Score { get; set; }
	
		/// <summary>
		/// The Key that identifies the leaderboard.
		/// </summary>
        public string Key { get; set; }
	
		/// <summary>
		/// Any arbitrary data to associated with the score When serialised the maximum size
		/// is 7kb. Note that Data will only be stored when the score is updated. For example
		/// a Leaderboard configured with Update Type Highest will only be written to with a
		/// score that is higher than the currently saved score.
		/// </summary>
        public MultiTypeValue Data { get; set; }

		/// <summary>
		/// Initialises a new instance of the description with the given required properties.
		/// </summary>
		///
		/// <param name="score">The score to post.</param>
		/// <param name="key">The Key that identifies the leaderboard.</param>
		public AddScoreRequestDesc(int score, string key)
		{
			ReleaseAssert.IsNotNull(key, "Key cannot be null.");
	
            Score = score;
            Key = key;
		}
	}
}
