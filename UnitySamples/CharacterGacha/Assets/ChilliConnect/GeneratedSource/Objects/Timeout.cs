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
	/// <para>A container used to describe a Timeout.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class Timeout
	{
		/// <summary>
		/// Number of seconds until expiry.
		/// </summary>
        public int SecondsRemaining { get; private set; }
	
		/// <summary>
		/// Date of expiry (UTC). Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime Expires { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given properties.
		/// </summary>
		///
		/// <param name="secondsRemaining">Number of seconds until expiry.</param>
		/// <param name="expires">Date of expiry (UTC). Format: ISO8601 e.g. 2016-01-12T11:08:23.</param>
		public Timeout(int secondsRemaining, DateTime expires)
		{
			ReleaseAssert.IsNotNull(expires, "Expires cannot be null.");
	
            SecondsRemaining = secondsRemaining;
            Expires = expires;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public Timeout(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("SecondsRemaining"), "Json is missing required field 'SecondsRemaining'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Expires"), "Json is missing required field 'Expires'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Seconds Remaining
				if (entry.Key == "SecondsRemaining")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    SecondsRemaining = (int)(long)entry.Value;
				}
		
				// Expires
				else if (entry.Key == "Expires")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Expires = JsonSerialisation.DeserialiseDate((string)entry.Value);
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
			
			// Seconds Remaining
			dictionary.Add("SecondsRemaining", SecondsRemaining);
			
			// Expires
            dictionary.Add("Expires", JsonSerialisation.Serialise(Expires));
			
			return dictionary;
		}
	}
}
