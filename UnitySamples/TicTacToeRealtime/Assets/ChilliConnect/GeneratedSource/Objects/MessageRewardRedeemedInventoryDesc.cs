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
	/// <para>A mutable description of a MessageRewardRedeemedInventory.</para>
	///
	/// <para>This is not thread-safe and should typically only be used to create new 
	/// instances of MessageRewardRedeemedInventory.</para>
	/// </summary>
	public sealed class MessageRewardRedeemedInventoryDesc
	{
		/// <summary>
		/// An instance identifier for the item added to the player's inventory.
		/// </summary>
        public string ItemId { get; set; }
	
		/// <summary>
		/// The name of the item.
		/// </summary>
        public string Name { get; set; }
	
		/// <summary>
		/// The key of the item.
		/// </summary>
        public string Key { get; set; }
	
		/// <summary>
		/// The data associated with this item instance.
		/// </summary>
        public MultiTypeValue InstanceData { get; set; }

		/// <summary>
		/// Initialises a new instance with the given required properties.
		/// </summary>
		///
		/// <param name="itemId">An instance identifier for the item added to the player's inventory.</param>
		/// <param name="name">The name of the item.</param>
		/// <param name="key">The key of the item.</param>
		public MessageRewardRedeemedInventoryDesc(string itemId, string name, string key)
		{
			ReleaseAssert.IsNotNull(itemId, "Item Id cannot be null.");
			ReleaseAssert.IsNotNull(name, "Name cannot be null.");
			ReleaseAssert.IsNotNull(key, "Key cannot be null.");
	
            ItemId = itemId;
            Name = name;
            Key = key;
		}
	}
}
