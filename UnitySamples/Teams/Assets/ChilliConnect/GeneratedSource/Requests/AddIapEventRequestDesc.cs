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
	/// </para>A mutable description of a AddIapEventRequest.</para>
	///
	/// </para>This is not thread-safe and should typically only be used to create new 
	/// instances of AddIapEventRequest.</para>
	/// </summary>
	public sealed class AddIapEventRequestDesc
	{
		/// <summary>
		/// A number representing the player's in game level.
		/// </summary>
        public int? UserGrade { get; set; }
	
		/// <summary>
		/// A string indicating the test group the player belongs to.
		/// </summary>
        public string TestGroup { get; set; }
	
		/// <summary>
		/// What offer, if any, the IAP was purchased under.
		/// </summary>
        public string Offer { get; set; }
	
		/// <summary>
		/// A string identifying the item that the player purchased.
		/// </summary>
        public string Item { get; set; }
	
		/// <summary>
		/// The amount of local currency paid by the player for the IAP.
		/// </summary>
        public float LocalCost { get; set; }
	
		/// <summary>
		/// The local currency with which the player purchased the IAP. This must be a valid
		/// ISO-4217 currency code.
		/// </summary>
        public string LocalCurrency { get; set; }

		/// <summary>
		/// Initialises a new instance of the description with the given required properties.
		/// </summary>
		///
		/// <param name="item">A string identifying the item that the player purchased.</param>
		/// <param name="localCost">The amount of local currency paid by the player for the IAP.</param>
		/// <param name="localCurrency">The local currency with which the player purchased the IAP. This must be a valid
		/// ISO-4217 currency code.</param>
		public AddIapEventRequestDesc(string item, float localCost, string localCurrency)
		{
			ReleaseAssert.IsNotNull(item, "Item cannot be null.");
			ReleaseAssert.IsNotNull(localCurrency, "Local Currency cannot be null.");
	
            Item = item;
            LocalCost = localCost;
            LocalCurrency = localCurrency;
		}
	}
}
