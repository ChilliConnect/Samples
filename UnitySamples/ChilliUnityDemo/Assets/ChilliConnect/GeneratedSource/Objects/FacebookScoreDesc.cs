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
	/// <para>A mutable description of a FacebookScore.</para>
	///
	/// <para>This is not thread-safe and should typically only be used to create new 
	/// instances of FacebookScore.</para>
	/// </summary>
	public sealed class FacebookScoreDesc
	{
		/// <summary>
		/// The ChilliConnectID of the player.
		/// </summary>
        public string ChilliConnectId { get; set; }
	
		/// <summary>
		/// The player's UserName.
		/// </summary>
        public string UserName { get; set; }
	
		/// <summary>
		/// The player's DisplayName.
		/// </summary>
        public string DisplayName { get; set; }
	
		/// <summary>
		/// The player's FacebookName.
		/// </summary>
        public string FacebookName { get; set; }
	
		/// <summary>
		/// Date that indicates when the score was recorded (UTC). Format: ISO8601 e.g.
		/// 2016-01-12T11:08:23.
		/// </summary>
        public DateTime Date { get; set; }
	
		/// <summary>
		/// Any data associated with the score.
		/// </summary>
        public MultiTypeValue Data { get; set; }
	
		/// <summary>
		/// The player's score.
		/// </summary>
        public int Score { get; set; }
	
		/// <summary>
		/// The player's rank within the global leaderboard.
		/// </summary>
        public int GlobalRank { get; set; }
	
		/// <summary>
		/// The total number of scores within the the global leaderboard.
		/// </summary>
        public int GlobalTotal { get; set; }
	
		/// <summary>
		/// The player's rank within the results returned.
		/// </summary>
        public int LocalRank { get; set; }

		/// <summary>
		/// Initialises a new instance with the given required properties.
		/// </summary>
		///
		/// <param name="chilliConnectId">The ChilliConnectID of the player.</param>
		/// <param name="facebookName">The player's FacebookName.</param>
		/// <param name="date">Date that indicates when the score was recorded (UTC). Format: ISO8601 e.g.
		/// 2016-01-12T11:08:23.</param>
		/// <param name="score">The player's score.</param>
		/// <param name="globalRank">The player's rank within the global leaderboard.</param>
		/// <param name="globalTotal">The total number of scores within the the global leaderboard.</param>
		/// <param name="localRank">The player's rank within the results returned.</param>
		public FacebookScoreDesc(string chilliConnectId, string facebookName, DateTime date, int score, int globalRank, int globalTotal, int localRank)
		{
			ReleaseAssert.IsNotNull(chilliConnectId, "Chilli Connect Id cannot be null.");
			ReleaseAssert.IsNotNull(facebookName, "Facebook Name cannot be null.");
			ReleaseAssert.IsNotNull(date, "Date cannot be null.");
	
            ChilliConnectId = chilliConnectId;
            FacebookName = facebookName;
            Date = date;
            Score = score;
            GlobalRank = globalRank;
            GlobalTotal = globalTotal;
            LocalRank = localRank;
		}
	}
}
