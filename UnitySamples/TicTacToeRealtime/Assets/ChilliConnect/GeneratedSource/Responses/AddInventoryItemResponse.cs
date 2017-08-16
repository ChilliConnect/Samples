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
	/// A container for information on the response from a AddInventoryItemRequest.
	/// </summary>
	public sealed class AddInventoryItemResponse
	{
		/// <summary>
		/// An instance identifier for the item added to the player's inventory.
		/// </summary>
        public string ItemId { get; private set; }
	
		/// <summary>
		/// Identifier for this write of this item in the player's inventory.
		/// </summary>
        public string WriteLock { get; private set; }

		/// <summary>
		/// Initialises the response with the given json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the JSON data.</param>
		public AddInventoryItemResponse(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("ItemID"), "Json is missing required field 'ItemID'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("WriteLock"), "Json is missing required field 'WriteLock'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Item Id
				if (entry.Key == "ItemID")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    ItemId = (string)entry.Value;
				}
		
				// Write Lock
				else if (entry.Key == "WriteLock")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    WriteLock = (string)entry.Value;
				}
			}
		}
	}
}
