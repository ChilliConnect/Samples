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
	/// <para>A container used to describe a Turn in a Match.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class MatchTurn
	{
		/// <summary>
		/// The Turn number.
		/// </summary>
        public int TurnNumber { get; private set; }
	
		/// <summary>
		/// The State of the Turn. Possible values: IN_PROGRESS, TIMEOUT, COMPLETED.
		/// </summary>
        public string State { get; private set; }
	
		/// <summary>
		/// A list of ChillIConnectIDs that the Match Turn is waiting for before it can
		/// become State COMPLETED. For TurnType PARALLEL this is a list of Players,
		/// SEQUENTIAL the next Player, ANY it is empty.
		/// </summary>
        public ReadOnlyCollection<Player> PlayersWaitingFor { get; private set; }
	
		/// <summary>
		/// List of PlayerTurns.
		/// </summary>
        public ReadOnlyCollection<PlayerTurn> PlayerTurns { get; private set; }
	
		/// <summary>
		/// The State Data before the Turn was completed.
		/// </summary>
        public MultiTypeValue PreStateData { get; private set; }
	
		/// <summary>
		/// The State Data after the Turn was completed.
		/// </summary>
        public MultiTypeValue PostStateData { get; private set; }
	
		/// <summary>
		/// The Date the Match Turn was started. Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime DateStarted { get; private set; }
	
		/// <summary>
		/// The Date the Match Turn was completed. Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime DateCompleted { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		public MatchTurn(MatchTurnDesc desc)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.State, "State cannot be null.");
			ReleaseAssert.IsNotNull(desc.DateStarted, "DateStarted cannot be null.");
	
            TurnNumber = desc.TurnNumber;
            State = desc.State;
            if (desc.PlayersWaitingFor != null)
			{
                PlayersWaitingFor = Mutability.ToImmutable(desc.PlayersWaitingFor);
			}
            if (desc.PlayerTurns != null)
			{
                PlayerTurns = Mutability.ToImmutable(desc.PlayerTurns);
			}
            PreStateData = desc.PreStateData;
            PostStateData = desc.PostStateData;
            DateStarted = desc.DateStarted;
            DateCompleted = desc.DateCompleted;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public MatchTurn(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("TurnNumber"), "Json is missing required field 'TurnNumber'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("State"), "Json is missing required field 'State'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("DateStarted"), "Json is missing required field 'DateStarted'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Turn Number
				if (entry.Key == "TurnNumber")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    TurnNumber = (int)(long)entry.Value;
				}
		
				// State
				else if (entry.Key == "State")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    State = (string)entry.Value;
				}
		
				// Players Waiting For
				else if (entry.Key == "PlayersWaitingFor")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is IList<object>, "Invalid serialised type.");
                        PlayersWaitingFor = JsonSerialisation.DeserialiseList((IList<object>)entry.Value, (object element) =>
                        {
                            ReleaseAssert.IsTrue(element is IDictionary<string, object>, "Invalid element type.");
                            return new Player((IDictionary<string, object>)element);	
                        });
                    }
				}
		
				// Player Turns
				else if (entry.Key == "PlayerTurns")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is IList<object>, "Invalid serialised type.");
                        PlayerTurns = JsonSerialisation.DeserialiseList((IList<object>)entry.Value, (object element) =>
                        {
                            ReleaseAssert.IsTrue(element is IDictionary<string, object>, "Invalid element type.");
                            return new PlayerTurn((IDictionary<string, object>)element);	
                        });
                    }
				}
		
				// Pre State Data
				else if (entry.Key == "PreStateData")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is object, "Invalid serialised type.");
                        PreStateData = new MultiTypeValue((object)entry.Value);	
                    }
				}
		
				// Post State Data
				else if (entry.Key == "PostStateData")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is object, "Invalid serialised type.");
                        PostStateData = new MultiTypeValue((object)entry.Value);	
                    }
				}
		
				// Date Started
				else if (entry.Key == "DateStarted")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    DateStarted = JsonSerialisation.DeserialiseDate((string)entry.Value);
				}
		
				// Date Completed
				else if (entry.Key == "DateCompleted")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                        DateCompleted = JsonSerialisation.DeserialiseDate((string)entry.Value);
                    }
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
			
			// Turn Number
			dictionary.Add("TurnNumber", TurnNumber);
			
			// State
			dictionary.Add("State", State);
			
			// Players Waiting For
            if (PlayersWaitingFor != null)
			{
                var serialisedPlayersWaitingFor = JsonSerialisation.Serialise(PlayersWaitingFor, (Player element) =>
                {
                    return element.Serialise();
                });
                dictionary.Add("PlayersWaitingFor", serialisedPlayersWaitingFor);
            }
			
			// Player Turns
            if (PlayerTurns != null)
			{
                var serialisedPlayerTurns = JsonSerialisation.Serialise(PlayerTurns, (PlayerTurn element) =>
                {
                    return element.Serialise();
                });
                dictionary.Add("PlayerTurns", serialisedPlayerTurns);
            }
			
			// Pre State Data
            if (PreStateData != null)
			{
                dictionary.Add("PreStateData", PreStateData.Serialise());
            }
			
			// Post State Data
            if (PostStateData != null)
			{
                dictionary.Add("PostStateData", PostStateData.Serialise());
            }
			
			// Date Started
            dictionary.Add("DateStarted", JsonSerialisation.Serialise(DateStarted));
			
			// Date Completed
            if (DateCompleted != null)
			{
                dictionary.Add("DateCompleted", JsonSerialisation.Serialise(DateCompleted));
            }
			
			return dictionary;
		}
	}
}
