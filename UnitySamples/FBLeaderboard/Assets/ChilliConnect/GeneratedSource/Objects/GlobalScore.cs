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
	/// <para>A container for information on a single score entry with information on the
	/// global ranking.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class GlobalScore
	{
		/// <summary>
		/// The ChilliConnectID of the player.
		/// </summary>
        public string ChilliConnectId { get; private set; }
	
		/// <summary>
		/// The player's UserName.
		/// </summary>
        public string UserName { get; private set; }
	
		/// <summary>
		/// The player's DisplayName.
		/// </summary>
        public string DisplayName { get; private set; }
	
		/// <summary>
		/// Date that indicates when the score was recorded (UTC). Format: ISO8601 e.g.
		/// 2016-01-12T11:08:23.
		/// </summary>
        public DateTime Date { get; private set; }
	
		/// <summary>
		/// Any data associated with the score.
		/// </summary>
        public MultiTypeValue Data { get; private set; }
	
		/// <summary>
		/// The player's score.
		/// </summary>
        public int Score { get; private set; }
	
		/// <summary>
		/// The player's rank within the global leaderboard.
		/// </summary>
        public int GlobalRank { get; private set; }
	
		/// <summary>
		/// The total number of scores within the the global leaderboard.
		/// </summary>
        public int GlobalTotal { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		public GlobalScore(GlobalScoreDesc desc)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.ChilliConnectId, "ChilliConnectId cannot be null.");
			ReleaseAssert.IsNotNull(desc.Date, "Date cannot be null.");
	
            ChilliConnectId = desc.ChilliConnectId;
            UserName = desc.UserName;
            DisplayName = desc.DisplayName;
            Date = desc.Date;
            Data = desc.Data;
            Score = desc.Score;
            GlobalRank = desc.GlobalRank;
            GlobalTotal = desc.GlobalTotal;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public GlobalScore(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("ChilliConnectID"), "Json is missing required field 'ChilliConnectID'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Date"), "Json is missing required field 'Date'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Score"), "Json is missing required field 'Score'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("GlobalRank"), "Json is missing required field 'GlobalRank'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("GlobalTotal"), "Json is missing required field 'GlobalTotal'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Chilli Connect Id
				if (entry.Key == "ChilliConnectID")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    ChilliConnectId = (string)entry.Value;
				}
		
				// User Name
				else if (entry.Key == "UserName")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                        UserName = (string)entry.Value;
                    }
				}
		
				// Display Name
				else if (entry.Key == "DisplayName")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                        DisplayName = (string)entry.Value;
                    }
				}
		
				// Date
				else if (entry.Key == "Date")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Date = JsonSerialisation.DeserialiseDate((string)entry.Value);
				}
		
				// Data
				else if (entry.Key == "Data")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is object, "Invalid serialised type.");
                        Data = new MultiTypeValue((object)entry.Value);	
                    }
				}
		
				// Score
				else if (entry.Key == "Score")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    Score = (int)(long)entry.Value;
				}
		
				// Global Rank
				else if (entry.Key == "GlobalRank")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    GlobalRank = (int)(long)entry.Value;
				}
		
				// Global Total
				else if (entry.Key == "GlobalTotal")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    GlobalTotal = (int)(long)entry.Value;
				}
	
				// An error has occurred.
				else
				{
#if DEBUG
					throw new ArgumentException("Input Json contains an invalid field.");
#endif
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
			
			// Chilli Connect Id
			dictionary.Add("ChilliConnectID", ChilliConnectId);
			
			// User Name
            if (UserName != null)
			{
				dictionary.Add("UserName", UserName);
            }
			
			// Display Name
            if (DisplayName != null)
			{
				dictionary.Add("DisplayName", DisplayName);
            }
			
			// Date
            dictionary.Add("Date", JsonSerialisation.Serialise(Date));
			
			// Data
            if (Data != null)
			{
                dictionary.Add("Data", Data.Serialise());
            }
			
			// Score
			dictionary.Add("Score", Score);
			
			// Global Rank
			dictionary.Add("GlobalRank", GlobalRank);
			
			// Global Total
			dictionary.Add("GlobalTotal", GlobalTotal);
			
			return dictionary;
		}
	}
}
