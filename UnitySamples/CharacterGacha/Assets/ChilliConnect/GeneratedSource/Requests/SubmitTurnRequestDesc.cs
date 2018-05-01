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
	/// </para>A mutable description of a SubmitTurnRequest.</para>
	///
	/// </para>This is not thread-safe and should typically only be used to create new 
	/// instances of SubmitTurnRequest.</para>
	/// </summary>
	public sealed class SubmitTurnRequestDesc
	{
		/// <summary>
		/// The ID of the Match to take a Turn of.
		/// </summary>
        public string MatchId { get; set; }
	
		/// <summary>
		/// Updated StateData with the Player's Turn taken.
		/// </summary>
        public MultiTypeValue StateData { get; set; }
	
		/// <summary>
		/// Data that contains changes made by the Player during this turn.
		/// </summary>
        public MultiTypeValue TurnData { get; set; }
	
		/// <summary>
		/// The current value of the WriteLock for this Match. To enable conflict checking.
		/// </summary>
        public string WriteLock { get; set; }
	
		/// <summary>
		/// True if the Match's State should be updated to COMPLETED. Default false.
		/// </summary>
        public bool? Completed { get; set; }
	
		/// <summary>
		/// Data that describes the outcome of the Match. Can only be set when Completed is
		/// set to true.
		/// </summary>
        public MultiTypeValue OutcomeData { get; set; }

		/// <summary>
		/// Initialises a new instance of the description with the given required properties.
		/// </summary>
		///
		/// <param name="matchId">The ID of the Match to take a Turn of.</param>
		public SubmitTurnRequestDesc(string matchId)
		{
			ReleaseAssert.IsNotNull(matchId, "Match Id cannot be null.");
	
            MatchId = matchId;
		}
	}
}
