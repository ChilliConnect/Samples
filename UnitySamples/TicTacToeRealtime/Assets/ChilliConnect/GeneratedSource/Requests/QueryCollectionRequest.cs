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
 	/// Query Collection api call.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class QueryCollectionRequest : IImmediateServerRequest
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
		/// A valid session ConnectAccessToken obtained through one of the login endpoints.
		/// </summary>
        public string ConnectAccessToken { get; private set; }
	
		/// <summary>
		/// The Collection Key.
		/// </summary>
        public string Key { get; private set; }
	
		/// <summary>
		/// A query to define which Objects to return. If not given all Objects in a
		/// Collection will be returned. See the Querying a Collection guide at
		/// https://docs.chilliconnect.com
		/// </summary>
        public string Query { get; private set; }
	
		/// <summary>
		/// Key/value pairs that substitute placeholders in the query. E.g. {"name": "Geoff"}
		/// for query "Value.Name = :name". See the Querying a Collection guide at
		/// https://docs.chilliconnect.com
		/// </summary>
        public ReadOnlyDictionary<string, MultiTypeValue> Params { get; private set; }
	
		/// <summary>
		/// An ordered list that define the response sort order. If not given Objects will be
		/// ordered by DateCreated ascending.
		/// </summary>
        public ReadOnlyCollection<SortField> SortFields { get; private set; }
	
		/// <summary>
		/// Results are paged. Maximum 100.
		/// </summary>
        public int? Page { get; private set; }

		/// <summary>
		/// Initialises a new instance of the request with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		/// <param name="connectAccessToken">A valid session ConnectAccessToken obtained through one of the login endpoints.</param>
		public QueryCollectionRequest(QueryCollectionRequestDesc desc, string connectAccessToken)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.Key, "Key cannot be null.");
	
			ReleaseAssert.IsNotNull(connectAccessToken, "Connect Access Token cannot be null.");
	
            Key = desc.Key;
            Query = desc.Query;
            if (desc.Params != null)
			{
                Params = Mutability.ToImmutable(desc.Params);
			}
            if (desc.SortFields != null)
			{
                SortFields = Mutability.ToImmutable(desc.SortFields);
			}
            Page = desc.Page;
            ConnectAccessToken = connectAccessToken;
	
			Url = "https://connect.chilliconnect.com/1.0/data/collection/query";
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
			
			// Connect Access Token
			dictionary.Add("Connect-Access-Token", ConnectAccessToken.ToString());
		
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
			
			// Key
			dictionary.Add("Key", Key);
			
			// Query
            if (Query != null)
			{
				dictionary.Add("Query", Query);
            }
			
			// Params
            if (Params != null)
			{
                var serialisedParams = JsonSerialisation.Serialise(Params, (MultiTypeValue element) =>
                {
                    return element.Serialise();
                });
                dictionary.Add("Params", serialisedParams);
            }
			
			// Sort Fields
            if (SortFields != null)
			{
                var serialisedSortFields = JsonSerialisation.Serialise(SortFields, (SortField element) =>
                {
                    return element.Serialise();
                });
                dictionary.Add("SortFields", serialisedSortFields);
            }
			
			// Page
            if (Page != null)
			{
				dictionary.Add("Page", Page);
            }
	
			return dictionary;
		}
	}
}
