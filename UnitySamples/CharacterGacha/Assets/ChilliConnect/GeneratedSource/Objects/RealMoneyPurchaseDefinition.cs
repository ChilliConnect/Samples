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
	/// <para>A container used to describe Economy Real Money Purchase objects.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class RealMoneyPurchaseDefinition
	{
		/// <summary>
		/// The key of the item.
		/// </summary>
        public string Key { get; private set; }
	
		/// <summary>
		/// The name of the item.
		/// </summary>
        public string Name { get; private set; }
	
		/// <summary>
		/// The tags of the item.
		/// </summary>
        public ReadOnlyCollection<string> Tags { get; private set; }
	
		/// <summary>
		/// The custom data of the item.
		/// </summary>
        public MultiTypeValue CustomData { get; private set; }
	
		/// <summary>
		/// The Amazon Product SKU.
		/// </summary>
        public string AmazonId { get; private set; }
	
		/// <summary>
		/// The Google Play Product ID.
		/// </summary>
        public string GoogleId { get; private set; }
	
		/// <summary>
		/// The iOS App Store Identifier.
		/// </summary>
        public string IosId { get; private set; }
	
		/// <summary>
		/// Rewards that will be allocated upon purchase.
		/// </summary>
        public PurchaseExchangeDefinition Rewards { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		public RealMoneyPurchaseDefinition(RealMoneyPurchaseDefinitionDesc desc)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.Key, "Key cannot be null.");
			ReleaseAssert.IsNotNull(desc.Name, "Name cannot be null.");
			ReleaseAssert.IsNotNull(desc.Tags, "Tags cannot be null.");
			ReleaseAssert.IsNotNull(desc.AmazonId, "AmazonId cannot be null.");
			ReleaseAssert.IsNotNull(desc.GoogleId, "GoogleId cannot be null.");
			ReleaseAssert.IsNotNull(desc.IosId, "IosId cannot be null.");
			ReleaseAssert.IsNotNull(desc.Rewards, "Rewards cannot be null.");
	
            Key = desc.Key;
            Name = desc.Name;
            Tags = Mutability.ToImmutable(desc.Tags);
            CustomData = desc.CustomData;
            AmazonId = desc.AmazonId;
            GoogleId = desc.GoogleId;
            IosId = desc.IosId;
            Rewards = desc.Rewards;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public RealMoneyPurchaseDefinition(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Key"), "Json is missing required field 'Key'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Name"), "Json is missing required field 'Name'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Tags"), "Json is missing required field 'Tags'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("AmazonID"), "Json is missing required field 'AmazonID'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("GoogleID"), "Json is missing required field 'GoogleID'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("IosID"), "Json is missing required field 'IosID'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Rewards"), "Json is missing required field 'Rewards'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Key
				if (entry.Key == "Key")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Key = (string)entry.Value;
				}
		
				// Name
				else if (entry.Key == "Name")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Name = (string)entry.Value;
				}
		
				// Tags
				else if (entry.Key == "Tags")
				{
                    ReleaseAssert.IsTrue(entry.Value is IList<object>, "Invalid serialised type.");
                    Tags = JsonSerialisation.DeserialiseList((IList<object>)entry.Value, (object element) =>
                    {
                        ReleaseAssert.IsTrue(element is string, "Invalid element type.");
                        return (string)element;
                    });
				}
		
				// Custom Data
				else if (entry.Key == "CustomData")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is object, "Invalid serialised type.");
                        CustomData = new MultiTypeValue((object)entry.Value);	
                    }
				}
		
				// Amazon Id
				else if (entry.Key == "AmazonID")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    AmazonId = (string)entry.Value;
				}
		
				// Google Id
				else if (entry.Key == "GoogleID")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    GoogleId = (string)entry.Value;
				}
		
				// Ios Id
				else if (entry.Key == "IosID")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    IosId = (string)entry.Value;
				}
		
				// Rewards
				else if (entry.Key == "Rewards")
				{
                    ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                    Rewards = new PurchaseExchangeDefinition((IDictionary<string, object>)entry.Value);	
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
			
			// Name
			dictionary.Add("Name", Name);
			
			// Tags
            var serialisedTags = JsonSerialisation.Serialise(Tags, (string element) =>
            {
                return element;
            });
            dictionary.Add("Tags", serialisedTags);
			
			// Custom Data
            if (CustomData != null)
			{
                dictionary.Add("CustomData", CustomData.Serialise());
            }
			
			// Amazon Id
			dictionary.Add("AmazonID", AmazonId);
			
			// Google Id
			dictionary.Add("GoogleID", GoogleId);
			
			// Ios Id
			dictionary.Add("IosID", IosId);
			
			// Rewards
            dictionary.Add("Rewards", Rewards.Serialise());
			
			return dictionary;
		}
	}
}
