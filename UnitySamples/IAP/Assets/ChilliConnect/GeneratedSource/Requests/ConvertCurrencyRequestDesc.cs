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
	/// </para>A mutable description of a ConvertCurrencyRequest.</para>
	///
	/// </para>This is not thread-safe and should typically only be used to create new 
	/// instances of ConvertCurrencyRequest.</para>
	/// </summary>
	public sealed class ConvertCurrencyRequestDesc
	{
		/// <summary>
		/// The Key of the currency that should be converted from.
		/// </summary>
        public string FromKey { get; set; }
	
		/// <summary>
		/// The Key of the currency that the From currency should be converted to.
		/// </summary>
        public string ToKey { get; set; }
	
		/// <summary>
		/// Amount of the From currency to convert.
		/// </summary>
        public int Amount { get; set; }
	
		/// <summary>
		/// Key of the Currency Conversion that should be used to perform the conversion.
		/// </summary>
        public string ConversionKey { get; set; }
	
		/// <summary>
		/// To enable conflict checking on the currency balance being converted from, provide
		/// the previous WriteLock value, or "1" for the initial write. If this value does
		/// not match the data store a CurrencyBalanceWriteConflict will be issued. If you
		/// don't wish to use conflict checking don't provide this parameter - data will be
		/// written with no checking.
		/// </summary>
        public string FromWriteLock { get; set; }
	
		/// <summary>
		/// To enable conflict checking on the currency balance being converted to, provide
		/// the previous WriteLock value, or "1" for the initial write. If this value does
		/// not match the data store a CurrencyBalanceWriteConflict will be issued. If you
		/// don't wish to use conflict checking don't provide this parameter - data will be
		/// written with no checking.
		/// </summary>
        public string ToWriteLock { get; set; }

		/// <summary>
		/// Initialises a new instance of the description with the given required properties.
		/// </summary>
		///
		/// <param name="fromKey">The Key of the currency that should be converted from.</param>
		/// <param name="toKey">The Key of the currency that the From currency should be converted to.</param>
		/// <param name="amount">Amount of the From currency to convert.</param>
		/// <param name="conversionKey">Key of the Currency Conversion that should be used to perform the conversion.</param>
		public ConvertCurrencyRequestDesc(string fromKey, string toKey, int amount, string conversionKey)
		{
			ReleaseAssert.IsNotNull(fromKey, "From Key cannot be null.");
			ReleaseAssert.IsNotNull(toKey, "To Key cannot be null.");
			ReleaseAssert.IsNotNull(conversionKey, "Conversion Key cannot be null.");
	
            FromKey = fromKey;
            ToKey = toKey;
            Amount = amount;
            ConversionKey = conversionKey;
		}
	}
}
