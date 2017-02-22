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
	/// <para>A container that can be used to describe items and currencies contained in a
	/// message.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class MessageRewardRedeemed
	{
		/// <summary>
		/// A list of Currencies.
		/// </summary>
        public ReadOnlyCollection<MessageRewardRedeemedCurrency> Currencies { get; private set; }
	
		/// <summary>
		/// A list of Inventory Items.
		/// </summary>
        public ReadOnlyCollection<MessageRewardRedeemedInventory> Items { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given properties.
		/// </summary>
		///
		/// <param name="currencies">A list of Currencies.</param>
		/// <param name="items">A list of Inventory Items.</param>
		public MessageRewardRedeemed(IList<MessageRewardRedeemedCurrency> currencies, IList<MessageRewardRedeemedInventory> items)
		{
			ReleaseAssert.IsNotNull(currencies, "Currencies cannot be null.");
			ReleaseAssert.IsNotNull(items, "Items cannot be null.");
	
            Currencies = Mutability.ToImmutable(currencies);
            Items = Mutability.ToImmutable(items);
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public MessageRewardRedeemed(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Currencies"), "Json is missing required field 'Currencies'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Items"), "Json is missing required field 'Items'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Currencies
				if (entry.Key == "Currencies")
				{
                    ReleaseAssert.IsTrue(entry.Value is IList<object>, "Invalid serialised type.");
                    Currencies = JsonSerialisation.DeserialiseList((IList<object>)entry.Value, (object element) =>
                    {
                        ReleaseAssert.IsTrue(element is IDictionary<string, object>, "Invalid element type.");
                        return new MessageRewardRedeemedCurrency((IDictionary<string, object>)element);	
                    });
				}
		
				// Items
				else if (entry.Key == "Items")
				{
                    ReleaseAssert.IsTrue(entry.Value is IList<object>, "Invalid serialised type.");
                    Items = JsonSerialisation.DeserialiseList((IList<object>)entry.Value, (object element) =>
                    {
                        ReleaseAssert.IsTrue(element is IDictionary<string, object>, "Invalid element type.");
                        return new MessageRewardRedeemedInventory((IDictionary<string, object>)element);	
                    });
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
			
			// Currencies
            var serialisedCurrencies = JsonSerialisation.Serialise(Currencies, (MessageRewardRedeemedCurrency element) =>
            {
                return element.Serialise();
            });
            dictionary.Add("Currencies", serialisedCurrencies);
			
			// Items
            var serialisedItems = JsonSerialisation.Serialise(Items, (MessageRewardRedeemedInventory element) =>
            {
                return element.Serialise();
            });
            dictionary.Add("Items", serialisedItems);
			
			return dictionary;
		}
	}
}
