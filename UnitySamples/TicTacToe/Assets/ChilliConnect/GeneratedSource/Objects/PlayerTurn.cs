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
	/// <para>A container used to describe a Player's Turn in a Match.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class PlayerTurn
	{
		/// <summary>
		/// The Turn data.
		/// </summary>
        public MultiTypeValue TurnData { get; private set; }
	
		/// <summary>
		/// The Player that created the Object.
		/// </summary>
        public Player Player { get; private set; }
	
		/// <summary>
		/// The Date the Player Turn was taken. Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime DateSubmitted { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		public PlayerTurn(PlayerTurnDesc desc)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.DateSubmitted, "DateSubmitted cannot be null.");
	
            TurnData = desc.TurnData;
            Player = desc.Player;
            DateSubmitted = desc.DateSubmitted;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public PlayerTurn(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("DateSubmitted"), "Json is missing required field 'DateSubmitted'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Turn Data
				if (entry.Key == "TurnData")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is object, "Invalid serialised type.");
                        TurnData = new MultiTypeValue((object)entry.Value);	
                    }
				}
		
				// Player
				else if (entry.Key == "Player")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                        Player = new Player((IDictionary<string, object>)entry.Value);	
                    }
				}
		
				// Date Submitted
				else if (entry.Key == "DateSubmitted")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    DateSubmitted = JsonSerialisation.DeserialiseDate((string)entry.Value);
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
			
			// Turn Data
            if (TurnData != null)
			{
                dictionary.Add("TurnData", TurnData.Serialise());
            }
			
			// Player
            if (Player != null)
			{
                dictionary.Add("Player", Player.Serialise());
            }
			
			// Date Submitted
            dictionary.Add("DateSubmitted", JsonSerialisation.Serialise(DateSubmitted));
			
			return dictionary;
		}
	}
}
