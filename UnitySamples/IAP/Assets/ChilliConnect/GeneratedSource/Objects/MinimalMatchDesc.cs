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
	/// <para>A mutable description of a MinimalMatch.</para>
	///
	/// <para>This is not thread-safe and should typically only be used to create new 
	/// instances of MinimalMatch.</para>
	/// </summary>
	public sealed class MinimalMatchDesc
	{
		/// <summary>
		/// The ID of the Match.
		/// </summary>
        public string MatchId { get; set; }
	
		/// <summary>
		/// The State of the Match. Possible values: WAITING, READY, IN_PROGRESS, TIMEOUT,
		/// COMPLETED.
		/// </summary>
        public string State { get; set; }
	
		/// <summary>
		/// The Match's turn type, ANY, SEQUENTIAL, PARALLEL.
		/// </summary>
        public string TurnType { get; set; }
	
		/// <summary>
		/// The current turn number. Only set if the match has started.
		/// </summary>
        public int? TurnNumber { get; set; }
	
		/// <summary>
		/// Amount of time before the Match will enter State TIMEOUT.
		/// </summary>
        public Timeout TurnTimeout { get; set; }
	
		/// <summary>
		/// Amount of time a Match in WAITING State will wait before entering State TIMEOUT.
		/// </summary>
        public Timeout WaitingTimeout { get; set; }
	
		/// <summary>
		/// List of Players participating in the Match.
		/// </summary>
        public IList<Player> Players { get; set; }
	
		/// <summary>
		/// A boolean value to indicate if the Player can currently submit a turn to this
		/// Match.
		/// </summary>
        public bool CanSubmitTurn { get; set; }
	
		/// <summary>
		/// The Date the Match was created. Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime DateCreated { get; set; }

		/// <summary>
		/// Initialises a new instance with the given required properties.
		/// </summary>
		///
		/// <param name="matchId">The ID of the Match.</param>
		/// <param name="state">The State of the Match. Possible values: WAITING, READY, IN_PROGRESS, TIMEOUT,
		/// COMPLETED.</param>
		/// <param name="turnType">The Match's turn type, ANY, SEQUENTIAL, PARALLEL.</param>
		/// <param name="canSubmitTurn">A boolean value to indicate if the Player can currently submit a turn to this
		/// Match.</param>
		/// <param name="dateCreated">The Date the Match was created. Format: ISO8601 e.g. 2016-01-12T11:08:23.</param>
		public MinimalMatchDesc(string matchId, string state, string turnType, bool canSubmitTurn, DateTime dateCreated)
		{
			ReleaseAssert.IsNotNull(matchId, "Match Id cannot be null.");
			ReleaseAssert.IsNotNull(state, "State cannot be null.");
			ReleaseAssert.IsNotNull(turnType, "Turn Type cannot be null.");
			ReleaseAssert.IsNotNull(dateCreated, "Date Created cannot be null.");
	
            MatchId = matchId;
            State = state;
            TurnType = turnType;
            CanSubmitTurn = canSubmitTurn;
            DateCreated = dateCreated;
		}
	}
}
