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
	/// <para>A container used to describe a Economy Currency Conversion Rule.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class ConversionRuleDefinition
	{
		/// <summary>
		/// The conversion from Currency.
		/// </summary>
        public CurrencyDefinition CurrencyFrom { get; private set; }
	
		/// <summary>
		/// The from amount.
		/// </summary>
        public int AmountFrom { get; private set; }
	
		/// <summary>
		/// The conversion to Currency.
		/// </summary>
        public CurrencyDefinition CurrencyTo { get; private set; }
	
		/// <summary>
		/// The to amount.
		/// </summary>
        public int AmountTo { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given properties.
		/// </summary>
		///
		/// <param name="currencyFrom">The conversion from Currency.</param>
		/// <param name="amountFrom">The from amount.</param>
		/// <param name="currencyTo">The conversion to Currency.</param>
		/// <param name="amountTo">The to amount.</param>
		public ConversionRuleDefinition(CurrencyDefinition currencyFrom, int amountFrom, CurrencyDefinition currencyTo, int amountTo)
		{
			ReleaseAssert.IsNotNull(currencyFrom, "Currency From cannot be null.");
			ReleaseAssert.IsNotNull(currencyTo, "Currency To cannot be null.");
	
            CurrencyFrom = currencyFrom;
            AmountFrom = amountFrom;
            CurrencyTo = currencyTo;
            AmountTo = amountTo;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public ConversionRuleDefinition(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("CurrencyFrom"), "Json is missing required field 'CurrencyFrom'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("AmountFrom"), "Json is missing required field 'AmountFrom'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("CurrencyTo"), "Json is missing required field 'CurrencyTo'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("AmountTo"), "Json is missing required field 'AmountTo'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Currency From
				if (entry.Key == "CurrencyFrom")
				{
                    ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                    CurrencyFrom = new CurrencyDefinition((IDictionary<string, object>)entry.Value);	
				}
		
				// Amount From
				else if (entry.Key == "AmountFrom")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    AmountFrom = (int)(long)entry.Value;
				}
		
				// Currency To
				else if (entry.Key == "CurrencyTo")
				{
                    ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                    CurrencyTo = new CurrencyDefinition((IDictionary<string, object>)entry.Value);	
				}
		
				// Amount To
				else if (entry.Key == "AmountTo")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    AmountTo = (int)(long)entry.Value;
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
			
			// Currency From
            dictionary.Add("CurrencyFrom", CurrencyFrom.Serialise());
			
			// Amount From
			dictionary.Add("AmountFrom", AmountFrom);
			
			// Currency To
            dictionary.Add("CurrencyTo", CurrencyTo.Serialise());
			
			// Amount To
			dictionary.Add("AmountTo", AmountTo);
			
			return dictionary;
		}
	}
}
