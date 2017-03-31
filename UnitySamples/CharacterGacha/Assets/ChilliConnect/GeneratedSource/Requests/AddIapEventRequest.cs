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
	/// <para>A container for all information that will be sent to the server during a
 	/// Add Iap Event api call.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class AddIapEventRequest : IImmediateServerRequest
	{
		/// <summary>
		/// The url the request will be sent to.
		/// </summary>
		public string Url { get; private set; }
		
		/// <summary>
		/// The HTTP request method that should be used.
		/// </summary>
		public HttpRequestMethod HttpRequestMethod { get; private set; }
		
		/// <summary>
		/// MetricsAccessToken as returned from a call to SessionStart.
		/// </summary>
        public string MetricsAccessToken { get; private set; }
	
		/// <summary>
		/// Date that indicates the local, device time that the IAP was purchased. Format:
		/// ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime Date { get; private set; }
	
		/// <summary>
		/// A number representing the player's in game level.
		/// </summary>
        public int? UserGrade { get; private set; }
	
		/// <summary>
		/// A string indicating the test group the player belongs to.
		/// </summary>
        public string TestGroup { get; private set; }
	
		/// <summary>
		/// What offer, if any, the IAP was purchased under.
		/// </summary>
        public string Offer { get; private set; }
	
		/// <summary>
		/// A string identifying the item that the player purchased.
		/// </summary>
        public string Item { get; private set; }
	
		/// <summary>
		/// The amount of local currency paid by the player for the IAP.
		/// </summary>
        public float LocalCost { get; private set; }
	
		/// <summary>
		/// The local currency with which the player purchased the IAP. This must be a valid
		/// ISO-4217 currency code.
		/// </summary>
        public string LocalCurrency { get; private set; }

		/// <summary>
		/// Initialises a new instance of the request with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		/// <param name="metricsAccessToken">MetricsAccessToken as returned from a call to SessionStart.</param>
		public AddIapEventRequest(AddIapEventRequestDesc desc, string metricsAccessToken)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.Item, "Item cannot be null.");
			ReleaseAssert.IsNotNull(desc.LocalCurrency, "LocalCurrency cannot be null.");
	
			ReleaseAssert.IsNotNull(metricsAccessToken, "Metrics Access Token cannot be null.");
	
            UserGrade = desc.UserGrade;
            TestGroup = desc.TestGroup;
            Offer = desc.Offer;
            Item = desc.Item;
            LocalCost = desc.LocalCost;
            LocalCurrency = desc.LocalCurrency;
            MetricsAccessToken = metricsAccessToken;
			Date = DateTime.Now;
	
			Url = "https://metrics.chilliconnect.com/1.0/iaps/add";
			HttpRequestMethod = HttpRequestMethod.Post;
		}

		/// <summary>
		/// Serialises all header properties. The output will be a dictionary containing 
		/// the extra header key-value pairs in addition the standard headers sent with 
		/// all server requests. Will return an empty dictionary if there are no headers.
		/// </summary>
		///
		/// <returns>The header key-value pairs.</returns>
		public IDictionary<string, string> SerialiseHeaders()
		{
			var dictionary = new Dictionary<string, string>();
			
			// Metrics Access Token
			dictionary.Add("Metrics-Access-Token", MetricsAccessToken.ToString());
		
			return dictionary;
		}
		
		/// <summary>
		/// Serialises all body properties. The output will be a dictionary containing the 
		/// body of the request in a form that can easily be converted to Json. Will return
		/// an empty dictionary if there is no body.
		/// </summary>
		///
		/// <returns>The body Json in dictionary form.</returns>
		public IDictionary<string, object> SerialiseBody()
		{
            var dictionary = new Dictionary<string, object>();
			
			// Date
            dictionary.Add("Date", JsonSerialisation.Serialise(Date));
			
			// User Grade
            if (UserGrade != null)
			{
				dictionary.Add("UserGrade", UserGrade);
            }
			
			// Test Group
            if (TestGroup != null)
			{
				dictionary.Add("TestGroup", TestGroup);
            }
			
			// Offer
            if (Offer != null)
			{
				dictionary.Add("Offer", Offer);
            }
			
			// Item
			dictionary.Add("Item", Item);
			
			// Local Cost
			dictionary.Add("LocalCost", LocalCost);
			
			// Local Currency
			dictionary.Add("LocalCurrency", LocalCurrency);
	
			return dictionary;
		}
	}
}
