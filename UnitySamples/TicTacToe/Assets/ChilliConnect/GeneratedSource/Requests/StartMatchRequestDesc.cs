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
	/// </para>A mutable description of a StartMatchRequest.</para>
	///
	/// </para>This is not thread-safe and should typically only be used to create new 
	/// instances of StartMatchRequest.</para>
	/// </summary>
	public sealed class StartMatchRequestDesc
	{
		/// <summary>
		/// The ID of the Match to start.
		/// </summary>
        public string MatchId { get; set; }
	
		/// <summary>
		/// The new State Data to set for Match.
		/// </summary>
        public MultiTypeValue StateData { get; set; }
	
		/// <summary>
		/// Ordered list of Player ChilliConnectIDs that form the turn order for the match.
		/// Only when Match TurnOrderType is set to EXPLICIT.
		/// </summary>
        public IList<string> TurnOrder { get; set; }

		/// <summary>
		/// Initialises a new instance of the description with the given required properties.
		/// </summary>
		///
		/// <param name="matchId">The ID of the Match to start.</param>
		public StartMatchRequestDesc(string matchId)
		{
			ReleaseAssert.IsNotNull(matchId, "Match Id cannot be null.");
	
            MatchId = matchId;
		}
	}
}
