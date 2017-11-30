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
	/// A container for information on the response from a GetMessagesRequest.
	/// </summary>
	public sealed class GetMessagesResponse
	{
		/// <summary>
		/// A list of messages.
		/// </summary>
        public ReadOnlyCollection<Message> Messages { get; private set; }
	
		/// <summary>
		/// The current page.
		/// </summary>
        public int Page { get; private set; }
	
		/// <summary>
		/// The number of messages returned per page.
		/// </summary>
        public int PageSize { get; private set; }
	
		/// <summary>
		/// The total number of messages.
		/// </summary>
        public int Total { get; private set; }

		/// <summary>
		/// Initialises the response with the given json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the JSON data.</param>
		public GetMessagesResponse(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Messages"), "Json is missing required field 'Messages'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Page"), "Json is missing required field 'Page'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("PageSize"), "Json is missing required field 'PageSize'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Total"), "Json is missing required field 'Total'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Messages
				if (entry.Key == "Messages")
				{
                    ReleaseAssert.IsTrue(entry.Value is IList<object>, "Invalid serialised type.");
                    Messages = JsonSerialisation.DeserialiseList((IList<object>)entry.Value, (object element) =>
                    {
                        ReleaseAssert.IsTrue(element is IDictionary<string, object>, "Invalid element type.");
                        return new Message((IDictionary<string, object>)element);	
                    });
				}
		
				// Page
				else if (entry.Key == "Page")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    Page = (int)(long)entry.Value;
				}
		
				// Page Size
				else if (entry.Key == "PageSize")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    PageSize = (int)(long)entry.Value;
				}
		
				// Total
				else if (entry.Key == "Total")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    Total = (int)(long)entry.Value;
				}
			}
		}
	}
}
