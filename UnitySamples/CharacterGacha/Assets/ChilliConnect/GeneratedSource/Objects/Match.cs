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
	/// <para>A container used to describe a Match.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class Match
	{
		/// <summary>
		/// The ID of the Match.
		/// </summary>
        public string MatchId { get; private set; }
	
		/// <summary>
		/// The Match Type definition Key.
		/// </summary>
        public string MatchTypeKey { get; private set; }
	
		/// <summary>
		/// The State of the Match. Possible values: WAITING, READY, IN_PROGRESS, TIMEOUT,
		/// COMPLETED.
		/// </summary>
        public string State { get; private set; }
	
		/// <summary>
		/// The writelock of the Match.
		/// </summary>
        public string WriteLock { get; private set; }
	
		/// <summary>
		/// The Match's properties.
		/// </summary>
        public ReadOnlyDictionary<string, MultiTypeValue> Properties { get; private set; }
	
		/// <summary>
		/// The Match's current data.
		/// </summary>
        public MultiTypeValue StateData { get; private set; }
	
		/// <summary>
		/// The Match's outcome data. Optionally populated on Match completion.
		/// </summary>
        public MultiTypeValue OutcomeData { get; private set; }
	
		/// <summary>
		/// Amount of time before the Match will enter State TIMEOUT.
		/// </summary>
        public Timeout TurnTimeout { get; private set; }
	
		/// <summary>
		/// Amount of time a Match in WAITING State will wait before entering State TIMEOUT.
		/// </summary>
        public Timeout WaitingTimeout { get; private set; }
	
		/// <summary>
		/// The Match's turn type, ANY, SEQUENTIAL, PARALLEL.
		/// </summary>
        public string TurnType { get; private set; }
	
		/// <summary>
		/// The Match's Turn Order behaviour. Only when TurnType is SEQUENTIAL. Values:
		/// RANDOM, ORDERED, EXPLICIT.
		/// </summary>
        public string TurnOrderType { get; private set; }
	
		/// <summary>
		/// The maximum number of Players for the Match.
		/// </summary>
        public int? PlayerLimit { get; private set; }
	
		/// <summary>
		/// List of Players participating in the Match.
		/// </summary>
        public ReadOnlyCollection<Player> Players { get; private set; }
	
		/// <summary>
		/// True if the match will automatically go into State IN_PROGRESS when PlayerLimit
		/// is reached.
		/// </summary>
        public bool AutoStart { get; private set; }
	
		/// <summary>
		/// If true will omit the match from the QueryAvailableMatches and the
		/// JoinAvailableMatches call. Still available from other calls such as GetMatch.
		/// </summary>
        public bool IsPrivate { get; private set; }
	
		/// <summary>
		/// The current turn number. Only set if the match has started.
		/// </summary>
        public int? TurnNumber { get; private set; }
	
		/// <summary>
		/// The last completed Turn. Only set when match is in progress.
		/// </summary>
        public MatchTurn LastTurn { get; private set; }
	
		/// <summary>
		/// The next Turn. May be partially completed depending on the TurnType. Only set
		/// when match is in progress.
		/// </summary>
        public MatchTurn CurrentTurn { get; private set; }
	
		/// <summary>
		/// The Player that created the Match.
		/// </summary>
        public Player CreatedBy { get; private set; }
	
		/// <summary>
		/// The Date the Match was created. Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime DateCreated { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		public Match(MatchDesc desc)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.MatchId, "MatchId cannot be null.");
			ReleaseAssert.IsNotNull(desc.MatchTypeKey, "MatchTypeKey cannot be null.");
			ReleaseAssert.IsNotNull(desc.State, "State cannot be null.");
			ReleaseAssert.IsNotNull(desc.WriteLock, "WriteLock cannot be null.");
			ReleaseAssert.IsNotNull(desc.TurnType, "TurnType cannot be null.");
			ReleaseAssert.IsNotNull(desc.DateCreated, "DateCreated cannot be null.");
	
            MatchId = desc.MatchId;
            MatchTypeKey = desc.MatchTypeKey;
            State = desc.State;
            WriteLock = desc.WriteLock;
            if (desc.Properties != null)
			{
                Properties = Mutability.ToImmutable(desc.Properties);
			}
            StateData = desc.StateData;
            OutcomeData = desc.OutcomeData;
            TurnTimeout = desc.TurnTimeout;
            WaitingTimeout = desc.WaitingTimeout;
            TurnType = desc.TurnType;
            TurnOrderType = desc.TurnOrderType;
            PlayerLimit = desc.PlayerLimit;
            if (desc.Players != null)
			{
                Players = Mutability.ToImmutable(desc.Players);
			}
            AutoStart = desc.AutoStart;
            IsPrivate = desc.IsPrivate;
            TurnNumber = desc.TurnNumber;
            LastTurn = desc.LastTurn;
            CurrentTurn = desc.CurrentTurn;
            CreatedBy = desc.CreatedBy;
            DateCreated = desc.DateCreated;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public Match(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("MatchID"), "Json is missing required field 'MatchID'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("MatchTypeKey"), "Json is missing required field 'MatchTypeKey'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("State"), "Json is missing required field 'State'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("WriteLock"), "Json is missing required field 'WriteLock'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("TurnType"), "Json is missing required field 'TurnType'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("AutoStart"), "Json is missing required field 'AutoStart'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("IsPrivate"), "Json is missing required field 'IsPrivate'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("DateCreated"), "Json is missing required field 'DateCreated'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Match Id
				if (entry.Key == "MatchID")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    MatchId = (string)entry.Value;
				}
		
				// Match Type Key
				else if (entry.Key == "MatchTypeKey")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    MatchTypeKey = (string)entry.Value;
				}
		
				// State
				else if (entry.Key == "State")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    State = (string)entry.Value;
				}
		
				// Write Lock
				else if (entry.Key == "WriteLock")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    WriteLock = (string)entry.Value;
				}
		
				// Properties
				else if (entry.Key == "Properties")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                        Properties = JsonSerialisation.DeserialiseMap((IDictionary<string, object>)entry.Value, (object element) =>
                        {
                            ReleaseAssert.IsTrue(element is object, "Invalid element type.");
                            return new MultiTypeValue((object)element);	
                        });
                    }
				}
		
				// State Data
				else if (entry.Key == "StateData")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is object, "Invalid serialised type.");
                        StateData = new MultiTypeValue((object)entry.Value);	
                    }
				}
		
				// Outcome Data
				else if (entry.Key == "OutcomeData")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is object, "Invalid serialised type.");
                        OutcomeData = new MultiTypeValue((object)entry.Value);	
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
		
				// Turn Type
				else if (entry.Key == "TurnType")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    TurnType = (string)entry.Value;
				}
		
				// Turn Order Type
				else if (entry.Key == "TurnOrderType")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                        TurnOrderType = (string)entry.Value;
                    }
				}
		
				// Player Limit
				else if (entry.Key == "PlayerLimit")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                        PlayerLimit = (int)(long)entry.Value;
                    }
				}
		
				// Players
				else if (entry.Key == "Players")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is IList<object>, "Invalid serialised type.");
                        Players = JsonSerialisation.DeserialiseList((IList<object>)entry.Value, (object element) =>
                        {
                            ReleaseAssert.IsTrue(element is IDictionary<string, object>, "Invalid element type.");
                            return new Player((IDictionary<string, object>)element);	
                        });
                    }
				}
		
				// Auto Start
				else if (entry.Key == "AutoStart")
				{
                    ReleaseAssert.IsTrue(entry.Value is bool, "Invalid serialised type.");
                    AutoStart = (bool)entry.Value;
				}
		
				// Is Private
				else if (entry.Key == "IsPrivate")
				{
                    ReleaseAssert.IsTrue(entry.Value is bool, "Invalid serialised type.");
                    IsPrivate = (bool)entry.Value;
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
		
				// Last Turn
				else if (entry.Key == "LastTurn")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                        LastTurn = new MatchTurn((IDictionary<string, object>)entry.Value);	
                    }
				}
		
				// Current Turn
				else if (entry.Key == "CurrentTurn")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                        CurrentTurn = new MatchTurn((IDictionary<string, object>)entry.Value);	
                    }
				}
		
				// Created By
				else if (entry.Key == "CreatedBy")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                        CreatedBy = new Player((IDictionary<string, object>)entry.Value);	
                    }
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
			
			// Match Type Key
			dictionary.Add("MatchTypeKey", MatchTypeKey);
			
			// State
			dictionary.Add("State", State);
			
			// Write Lock
			dictionary.Add("WriteLock", WriteLock);
			
			// Properties
            if (Properties != null)
			{
                var serialisedProperties = JsonSerialisation.Serialise(Properties, (MultiTypeValue element) =>
                {
                    return element.Serialise();
                });
                dictionary.Add("Properties", serialisedProperties);
            }
			
			// State Data
            if (StateData != null)
			{
                dictionary.Add("StateData", StateData.Serialise());
            }
			
			// Outcome Data
            if (OutcomeData != null)
			{
                dictionary.Add("OutcomeData", OutcomeData.Serialise());
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
			
			// Turn Type
			dictionary.Add("TurnType", TurnType);
			
			// Turn Order Type
            if (TurnOrderType != null)
			{
				dictionary.Add("TurnOrderType", TurnOrderType);
            }
			
			// Player Limit
            if (PlayerLimit != null)
			{
				dictionary.Add("PlayerLimit", PlayerLimit);
            }
			
			// Players
            if (Players != null)
			{
                var serialisedPlayers = JsonSerialisation.Serialise(Players, (Player element) =>
                {
                    return element.Serialise();
                });
                dictionary.Add("Players", serialisedPlayers);
            }
			
			// Auto Start
			dictionary.Add("AutoStart", AutoStart);
			
			// Is Private
			dictionary.Add("IsPrivate", IsPrivate);
			
			// Turn Number
            if (TurnNumber != null)
			{
				dictionary.Add("TurnNumber", TurnNumber);
            }
			
			// Last Turn
            if (LastTurn != null)
			{
                dictionary.Add("LastTurn", LastTurn.Serialise());
            }
			
			// Current Turn
            if (CurrentTurn != null)
			{
                dictionary.Add("CurrentTurn", CurrentTurn.Serialise());
            }
			
			// Created By
            if (CreatedBy != null)
			{
                dictionary.Add("CreatedBy", CreatedBy.Serialise());
            }
			
			// Date Created
            dictionary.Add("DateCreated", JsonSerialisation.Serialise(DateCreated));
			
			return dictionary;
		}
	}
}
