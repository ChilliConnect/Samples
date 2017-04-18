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
	/// <para>A container for information on a single piece of Cloud Data retrieved for the
	/// current player.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class PlayerData
	{
		/// <summary>
		/// The requested Custom Data Key.
		/// </summary>
        public string Key { get; private set; }
	
		/// <summary>
		/// The value of the Custom Data Key.
		/// </summary>
        public MultiTypeValue Value { get; private set; }
	
		/// <summary>
		/// The current value of the WriteLock for this Key. To enable conflict checking, the
		/// returned WriteLock can be provided to the Add Data call on subsequent update
		/// attempts.
		/// </summary>
        public string WriteLock { get; private set; }
	
		/// <summary>
		/// The Date the Key was first created. Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime DateCreated { get; private set; }
	
		/// <summary>
		/// The Date the Key was last updated. Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime DateModified { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		public PlayerData(PlayerDataDesc desc)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.Key, "Key cannot be null.");
			ReleaseAssert.IsNotNull(desc.Value, "Value cannot be null.");
			ReleaseAssert.IsNotNull(desc.DateCreated, "DateCreated cannot be null.");
			ReleaseAssert.IsNotNull(desc.DateModified, "DateModified cannot be null.");
	
            Key = desc.Key;
            Value = desc.Value;
            WriteLock = desc.WriteLock;
            DateCreated = desc.DateCreated;
            DateModified = desc.DateModified;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public PlayerData(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Key"), "Json is missing required field 'Key'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Value"), "Json is missing required field 'Value'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("DateCreated"), "Json is missing required field 'DateCreated'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("DateModified"), "Json is missing required field 'DateModified'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Key
				if (entry.Key == "Key")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Key = (string)entry.Value;
				}
		
				// Value
				else if (entry.Key == "Value")
				{
                    ReleaseAssert.IsTrue(entry.Value is object, "Invalid serialised type.");
                    Value = new MultiTypeValue((object)entry.Value);	
				}
		
				// Write Lock
				else if (entry.Key == "WriteLock")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                        WriteLock = (string)entry.Value;
                    }
				}
		
				// Date Created
				else if (entry.Key == "DateCreated")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    DateCreated = JsonSerialisation.DeserialiseDate((string)entry.Value);
				}
		
				// Date Modified
				else if (entry.Key == "DateModified")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    DateModified = JsonSerialisation.DeserialiseDate((string)entry.Value);
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
			
			// Key
			dictionary.Add("Key", Key);
			
			// Value
            dictionary.Add("Value", Value.Serialise());
			
			// Write Lock
            if (WriteLock != null)
			{
				dictionary.Add("WriteLock", WriteLock);
            }
			
			// Date Created
            dictionary.Add("DateCreated", JsonSerialisation.Serialise(DateCreated));
			
			// Date Modified
            dictionary.Add("DateModified", JsonSerialisation.Serialise(DateModified));
			
			return dictionary;
		}
	}
}
