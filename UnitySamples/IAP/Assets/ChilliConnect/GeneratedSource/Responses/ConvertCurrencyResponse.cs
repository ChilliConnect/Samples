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
	/// A container for information on the response from a ConvertCurrencyRequest.
	/// </summary>
	public sealed class ConvertCurrencyResponse
	{
		/// <summary>
		/// The amount of the From Currency that was converted. The Amount converted will be
		/// rounded down to the nearest multiple defined in the Currency Conversion rule so
		/// could be less than the Amount submitted. For example, if the Currency Conversion
		/// defines a rule that states 10 of CurrencyOne can be converted to 1 of
		/// CurrencyTwo, and a conversion request is submitted with an Amount of 24, only 20
		/// of CurrencyOne will be converted to 2 of CurrencyTwo.
		/// </summary>
        public int AmountConverted { get; private set; }
	
		/// <summary>
		/// The final balance of the currency converted to.
		/// </summary>
        public CurrencyBalance ToBalance { get; private set; }
	
		/// <summary>
		/// The final balance of the currency converted to.
		/// </summary>
        public CurrencyBalance FromBalance { get; private set; }

		/// <summary>
		/// Initialises the response with the given json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the JSON data.</param>
		public ConvertCurrencyResponse(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("AmountConverted"), "Json is missing required field 'AmountConverted'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("ToBalance"), "Json is missing required field 'ToBalance'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("FromBalance"), "Json is missing required field 'FromBalance'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Amount Converted
				if (entry.Key == "AmountConverted")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    AmountConverted = (int)(long)entry.Value;
				}
		
				// To Balance
				else if (entry.Key == "ToBalance")
				{
                    ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                    ToBalance = new CurrencyBalance((IDictionary<string, object>)entry.Value);	
				}
		
				// From Balance
				else if (entry.Key == "FromBalance")
				{
                    ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                    FromBalance = new CurrencyBalance((IDictionary<string, object>)entry.Value);	
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
	}
}
