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
	/// A container for information on the response from a AddScoreRequest.
	/// </summary>
	public sealed class AddScoreResponse
	{
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
		/// Initialises the response with the given json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the JSON data.</param>
		public AddScoreResponse(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Score"), "Json is missing required field 'Score'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("GlobalRank"), "Json is missing required field 'GlobalRank'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("GlobalTotal"), "Json is missing required field 'GlobalTotal'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Data
				if (entry.Key == "Data")
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
	}
}
