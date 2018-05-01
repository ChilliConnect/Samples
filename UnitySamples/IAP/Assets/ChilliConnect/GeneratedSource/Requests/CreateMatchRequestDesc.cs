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
	/// </para>A mutable description of a CreateMatchRequest.</para>
	///
	/// </para>This is not thread-safe and should typically only be used to create new 
	/// instances of CreateMatchRequest.</para>
	/// </summary>
	public sealed class CreateMatchRequestDesc
	{
		/// <summary>
		/// A Match Type definition Key.
		/// </summary>
        public string MatchTypeKey { get; set; }
	
		/// <summary>
		/// Describes the turn behaviour ChilliConnect will apply to the match. Accepted
		/// values defined by the MatchType.
		/// </summary>
        public string TurnType { get; set; }
	
		/// <summary>
		/// Maximum number of players that can join the match. Max 10.
		/// </summary>
        public int PlayerLimit { get; set; }
	
		/// <summary>
		/// A set of Key-Value pairs that are used to search for matches. Keys and
		/// Value-types are defined in the MatchType.
		/// </summary>
        public IDictionary<string, MultiTypeValue> Properties { get; set; }
	
		/// <summary>
		/// Initial state data.
		/// </summary>
        public MultiTypeValue StateData { get; set; }
	
		/// <summary>
		/// Number of minutes that each Player has to complete their turn before the Match
		/// will go into a TIMEOUT state. Defaults to the MatchType value. Max 10,080 (7
		/// days).
		/// </summary>
        public int? TurnTimeout { get; set; }
	
		/// <summary>
		/// Number of minutes that the match can remain in the WAITING state before going
		/// into the TIMEOUT state. Defaults to the MatchType value. Max 10,080 (7 days).
		/// </summary>
        public int? WaitingTimeout { get; set; }
	
		/// <summary>
		/// Only when TurnType is SEQUENTIAL. Accepted values: RANDOM, ORDERED, EXPLICIT.
		/// Defaults to ORDERED. See documentation.
		/// </summary>
        public string TurnOrderType { get; set; }
	
		/// <summary>
		/// List of Player ChilliConnectIDs to be added as participants of the match. Note:
		/// the currently logged in Player, if there one, is automatically added as a match
		/// participant.
		/// </summary>
        public IList<string> Players { get; set; }
	
		/// <summary>
		/// Defaults to true. When true, a match will be started when PlayerLimit is reached.
		/// </summary>
        public bool? AutoStart { get; set; }
	
		/// <summary>
		/// True if the match should only be available by it's MatchID. Default; false.
		/// </summary>
        public bool? Private { get; set; }

		/// <summary>
		/// Initialises a new instance of the description with the given required properties.
		/// </summary>
		///
		/// <param name="matchTypeKey">A Match Type definition Key.</param>
		/// <param name="turnType">Describes the turn behaviour ChilliConnect will apply to the match. Accepted
		/// values defined by the MatchType.</param>
		/// <param name="playerLimit">Maximum number of players that can join the match. Max 10.</param>
		public CreateMatchRequestDesc(string matchTypeKey, string turnType, int playerLimit)
		{
			ReleaseAssert.IsNotNull(matchTypeKey, "Match Type Key cannot be null.");
			ReleaseAssert.IsNotNull(turnType, "Turn Type cannot be null.");
	
            MatchTypeKey = matchTypeKey;
            TurnType = turnType;
            PlayerLimit = playerLimit;
		}
	}
}
