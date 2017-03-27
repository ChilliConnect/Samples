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
	/// A container for information on the response from a LogInUsingEmailRequest.
	/// </summary>
	public sealed class LogInUsingEmailResponse
	{
		/// <summary>
		/// The player's ChilliConnectID.
		/// </summary>
        public string ChilliConnectId { get; private set; }
	
		/// <summary>
		/// The player's ChilliConnectSecret.
		/// </summary>
        public string ChilliConnectSecret { get; private set; }

		/// <summary>
		/// Initialises the response with the given json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the JSON data.</param>
		public LogInUsingEmailResponse(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("ChilliConnectID"), "Json is missing required field 'ChilliConnectID'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("ChilliConnectSecret"), "Json is missing required field 'ChilliConnectSecret'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Chilli Connect Id
				if (entry.Key == "ChilliConnectID")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    ChilliConnectId = (string)entry.Value;
				}
		
				// Chilli Connect Secret
				else if (entry.Key == "ChilliConnectSecret")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    ChilliConnectSecret = (string)entry.Value;
				}
	
				// Connect Access Token
				else if (entry.Key == "ConnectAccessToken")
				{
					//Do nothing
				}
			}
		}
	}
}
