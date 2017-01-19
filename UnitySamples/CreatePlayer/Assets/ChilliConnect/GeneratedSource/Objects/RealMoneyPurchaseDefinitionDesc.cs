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
	/// <para>A mutable description of a RealMoneyPurchaseDefinition.</para>
	///
	/// <para>This is not thread-safe and should typically only be used to create new 
	/// instances of RealMoneyPurchaseDefinition.</para>
	/// </summary>
	public sealed class RealMoneyPurchaseDefinitionDesc
	{
		/// <summary>
		/// The key of the item.
		/// </summary>
        public string Key { get; set; }
	
		/// <summary>
		/// The name of the item.
		/// </summary>
        public string Name { get; set; }
	
		/// <summary>
		/// The tags of the item.
		/// </summary>
        public IList<string> Tags { get; set; }
	
		/// <summary>
		/// The custom data of the item.
		/// </summary>
        public MultiTypeValue CustomData { get; set; }
	
		/// <summary>
		/// The Amazon Product SKU.
		/// </summary>
        public string AmazonId { get; set; }
	
		/// <summary>
		/// The Google Play Product ID.
		/// </summary>
        public string GoogleId { get; set; }
	
		/// <summary>
		/// The iOS App Store Identifier.
		/// </summary>
        public string IosId { get; set; }
	
		/// <summary>
		/// Rewards that will be allocated upon purchase.
		/// </summary>
        public PurchaseExchangeDefinition Rewards { get; set; }

		/// <summary>
		/// Initialises a new instance with the given required properties.
		/// </summary>
		///
		/// <param name="key">The key of the item.</param>
		/// <param name="name">The name of the item.</param>
		/// <param name="tags">The tags of the item.</param>
		/// <param name="amazonId">The Amazon Product SKU.</param>
		/// <param name="googleId">The Google Play Product ID.</param>
		/// <param name="iosId">The iOS App Store Identifier.</param>
		/// <param name="rewards">Rewards that will be allocated upon purchase.</param>
		public RealMoneyPurchaseDefinitionDesc(string key, string name, IList<string> tags, string amazonId, string googleId, string iosId, PurchaseExchangeDefinition rewards)
		{
			ReleaseAssert.IsNotNull(key, "Key cannot be null.");
			ReleaseAssert.IsNotNull(name, "Name cannot be null.");
			ReleaseAssert.IsNotNull(tags, "Tags cannot be null.");
			ReleaseAssert.IsNotNull(amazonId, "Amazon Id cannot be null.");
			ReleaseAssert.IsNotNull(googleId, "Google Id cannot be null.");
			ReleaseAssert.IsNotNull(iosId, "Ios Id cannot be null.");
			ReleaseAssert.IsNotNull(rewards, "Rewards cannot be null.");
	
            Key = key;
            Name = name;
            Tags = Mutability.ToImmutable(tags);
            AmazonId = amazonId;
            GoogleId = googleId;
            IosId = iosId;
            Rewards = rewards;
		}
	}
}
