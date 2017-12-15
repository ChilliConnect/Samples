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
	/// <para>A container used to describe a Match in minimal format.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class MinimalMatch
	{
		/// <summary>
		/// The ID of the Match.
		/// </summary>
        public string MatchId { get; private set; }
	
		/// <summary>
		/// The State of the Match. Possible values: WAITING, READY, IN_PROGRESS, TIMEOUT,
		/// COMPLETED.
		/// </summary>
        public string State { get; private set; }
	
		/// <summary>
		/// The Match's turn type, ANY, SEQUENTIAL, PARALLEL.
		/// </summary>
        public string TurnType { get; private set; }
	
		/// <summary>
		/// The current turn number. Only set if the match has started.
		/// </summary>
        public int? TurnNumber { get; private set; }
	
		/// <summary>
		/// Amount of time before the Match will enter State TIMEOUT.
		/// </summary>
        public Timeout TurnTimeout { get; private set; }
	
		/// <summary>
		/// Amount of time a Match in WAITING State will wait before entering State TIMEOUT.
		/// </summary>
        public Timeout WaitingTimeout { get; private set; }
	
		/// <summary>
		/// A boolean value to indicate if the Player can currently submit a turn to this
		/// Match.
		/// </summary>
        public bool CanSubmitTurn { get; private set; }
	
		/// <summary>
		/// The Date the Match was created. Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime DateCreated { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		public MinimalMatch(MinimalMatchDesc desc)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.MatchId, "MatchId cannot be null.");
			ReleaseAssert.IsNotNull(desc.State, "State cannot be null.");
			ReleaseAssert.IsNotNull(desc.TurnType, "TurnType cannot be null.");
			ReleaseAssert.IsNotNull(desc.DateCreated, "DateCreated cannot be null.");
	
            MatchId = desc.MatchId;
            State = desc.State;
            TurnType = desc.TurnType;
            TurnNumber = desc.TurnNumber;
            TurnTimeout = desc.TurnTimeout;
            WaitingTimeout = desc.WaitingTimeout;
            CanSubmitTurn = desc.CanSubmitTurn;
            DateCreated = desc.DateCreated;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public MinimalMatch(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("MatchID"), "Json is missing required field 'MatchID'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("State"), "Json is missing required field 'State'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("TurnType"), "Json is missing required field 'TurnType'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("CanSubmitTurn"), "Json is missing required field 'CanSubmitTurn'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("DateCreated"), "Json is missing required field 'DateCreated'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Match Id
				if (entry.Key == "MatchID")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    MatchId = (string)entry.Value;
				}
		
				// State
				else if (entry.Key == "State")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    State = (string)entry.Value;
				}
		
				// Turn Type
				else if (entry.Key == "TurnType")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    TurnType = (string)entry.Value;
				}
		
				// Turn Number
				else if (entry.Key == "TurnNumber")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                        TurnNumber = (int)(long)entry.Value;
                    }
				}
		
				// Turn Timeout
				else if (entry.Key == "TurnTimeout")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                        TurnTimeout = new Timeout((IDictionary<string, object>)entry.Value);	
                    }
				}
		
				// Waiting Timeout
				else if (entry.Key == "WaitingTimeout")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                        WaitingTimeout = new Timeout((IDictionary<string, object>)entry.Value);	
                    }
				}
		
				// Can Submit Turn
				else if (entry.Key == "CanSubmitTurn")
				{
                    ReleaseAssert.IsTrue(entry.Value is bool, "Invalid serialised type.");
                    CanSubmitTurn = (bool)entry.Value;
				}
		
				// Date Created
				else if (entry.Key == "DateCreated")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    DateCreated = JsonSerialisation.DeserialiseDate((string)entry.Value);
				}
			}
		}

		/// <summary>
		/// Serialises all properties. The output will be a dictionary containing the
		/// objects properties in a form that can easily be converted to Json. 
		/// </summary>
		///
		/// <returns>The serialised object in dictionary form.</returns>
		public IDictionary<string, object> Serialise()
		{
            var dictionary = new Dictionary<string, object>();
			
			// Match Id
			dictionary.Add("MatchID", MatchId);
			
			// State
			dictionary.Add("State", State);
			
			// Turn Type
			dictionary.Add("TurnType", TurnType);
			
			// Turn Number
            if (TurnNumber != null)
			{
				dictionary.Add("TurnNumber", TurnNumber);
            }
			
			// Turn Timeout
            if (TurnTimeout != null)
			{
                dictionary.Add("TurnTimeout", TurnTimeout.Serialise());
            }
			
			// Waiting Timeout
            if (WaitingTimeout != null)
			{
                dictionary.Add("WaitingTimeout", WaitingTimeout.Serialise());
            }
			
			// Can Submit Turn
			dictionary.Add("CanSubmitTurn", CanSubmitTurn);
			
			// Date Created
            dictionary.Add("DateCreated", JsonSerialisation.Serialise(DateCreated));
			
			return dictionary;
		}
	}
}
