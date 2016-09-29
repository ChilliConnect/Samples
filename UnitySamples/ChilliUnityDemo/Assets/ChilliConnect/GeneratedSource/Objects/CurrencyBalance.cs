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
	/// <para>A container for Balance information for a currency.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class CurrencyBalance
	{
		/// <summary>
		/// The currency name.
		/// </summary>
        public string Name { get; private set; }
	
		/// <summary>
		/// The currency key.
		/// </summary>
        public string Key { get; private set; }
	
		/// <summary>
		/// The player's balance.
		/// </summary>
        public int Balance { get; private set; }
	
		/// <summary>
		/// The current value of the WriteLock for this Currency Key. To enable conflict
		/// checking, the returned WriteLock can be provided to the Set Currency Balance,
		/// Withdraw Currency, Deposit Currency and Convert Currency calls on subsequent
		/// update attempts.
		/// </summary>
        public string WriteLock { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		public CurrencyBalance(CurrencyBalanceDesc desc)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.Name, "Name cannot be null.");
			ReleaseAssert.IsNotNull(desc.Key, "Key cannot be null.");
	
            Name = desc.Name;
            Key = desc.Key;
            Balance = desc.Balance;
            WriteLock = desc.WriteLock;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public CurrencyBalance(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Name"), "Json is missing required field 'Name'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Key"), "Json is missing required field 'Key'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Balance"), "Json is missing required field 'Balance'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Name
				if (entry.Key == "Name")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Name = (string)entry.Value;
				}
		
				// Key
				else if (entry.Key == "Key")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Key = (string)entry.Value;
				}
		
				// Balance
				else if (entry.Key == "Balance")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    Balance = (int)(long)entry.Value;
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
			
			// Name
			dictionary.Add("Name", Name);
			
			// Key
			dictionary.Add("Key", Key);
			
			// Balance
			dictionary.Add("Balance", Balance);
			
			// Write Lock
            if (WriteLock != null)
			{
				dictionary.Add("WriteLock", WriteLock);
            }
			
			return dictionary;
		}
	}
}
