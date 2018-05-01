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
	/// <para>A mutable description of a MatchTurn.</para>
	///
	/// <para>This is not thread-safe and should typically only be used to create new 
	/// instances of MatchTurn.</para>
	/// </summary>
	public sealed class MatchTurnDesc
	{
		/// <summary>
		/// The Turn number.
		/// </summary>
        public int TurnNumber { get; set; }
	
		/// <summary>
		/// The State of the Turn. Possible values: IN_PROGRESS, TIMEOUT, COMPLETED.
		/// </summary>
        public string State { get; set; }
	
		/// <summary>
		/// A list of ChillIConnectIDs that the Match Turn is waiting for before it can
		/// become State COMPLETED. For TurnType PARALLEL this is a list of Players,
		/// SEQUENTIAL the next Player, ANY it is empty.
		/// </summary>
        public IList<Player> PlayersWaitingFor { get; set; }
	
		/// <summary>
		/// List of PlayerTurns.
		/// </summary>
        public IList<PlayerTurn> PlayerTurns { get; set; }
	
		/// <summary>
		/// The State Data before the Turn was completed.
		/// </summary>
        public MultiTypeValue PreStateData { get; set; }
	
		/// <summary>
		/// The State Data after the Turn was completed.
		/// </summary>
        public MultiTypeValue PostStateData { get; set; }
	
		/// <summary>
		/// The Date the Match Turn was started. Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime DateStarted { get; set; }
	
		/// <summary>
		/// The Date the Match Turn was completed. Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime DateCompleted { get; set; }

		/// <summary>
		/// Initialises a new instance with the given required properties.
		/// </summary>
		///
		/// <param name="turnNumber">The Turn number.</param>
		/// <param name="state">The State of the Turn. Possible values: IN_PROGRESS, TIMEOUT, COMPLETED.</param>
		/// <param name="dateStarted">The Date the Match Turn was started. Format: ISO8601 e.g. 2016-01-12T11:08:23.</param>
		public MatchTurnDesc(int turnNumber, string state, DateTime dateStarted)
		{
			ReleaseAssert.IsNotNull(state, "State cannot be null.");
			ReleaseAssert.IsNotNull(dateStarted, "Date Started cannot be null.");
	
            TurnNumber = turnNumber;
            State = state;
            DateStarted = dateStarted;
		}
	}
}
