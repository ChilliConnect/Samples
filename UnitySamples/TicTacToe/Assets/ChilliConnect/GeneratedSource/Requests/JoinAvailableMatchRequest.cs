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
 	/// Join Available Match api call.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class JoinAvailableMatchRequest : IImmediateServerRequest
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
		/// The MatchType Key.
		/// </summary>
        public string MatchTypeKey { get; private set; }
	
		/// <summary>
		/// A list of queries to find a Match to join. Queries will be executed in order, the
		/// first Match returned will be joined.
		/// </summary>
        public ReadOnlyCollection<string> Query { get; private set; }
	
		/// <summary>
		/// Key/value pairs that substitute placeholders in the query. E.g. {"name": "Geoff"}
		/// for query "Name = :name". See the guide at https://docs.chilliconnect.com
		/// </summary>
        public ReadOnlyDictionary<string, MultiTypeValue> Params { get; private set; }
	
		/// <summary>
		/// When true, if no Matches have been joined by the given queries, any Match will be
		/// joined. Default false.
		/// </summary>
        public bool? FallbackToAny { get; private set; }

		/// <summary>
		/// Initialises a new instance of the request with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		/// <param name="connectAccessToken">A valid session ConnectAccessToken obtained through one of the login endpoints.</param>
		public JoinAvailableMatchRequest(JoinAvailableMatchRequestDesc desc, string connectAccessToken)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.MatchTypeKey, "MatchTypeKey cannot be null.");
	
			ReleaseAssert.IsNotNull(connectAccessToken, "Connect Access Token cannot be null.");
	
            MatchTypeKey = desc.MatchTypeKey;
            if (desc.Query != null)
			{
                Query = Mutability.ToImmutable(desc.Query);
			}
            if (desc.Params != null)
			{
                Params = Mutability.ToImmutable(desc.Params);
			}
            FallbackToAny = desc.FallbackToAny;
            ConnectAccessToken = connectAccessToken;
	
			Url = "https://connect.chilliconnect.com/1.0/multiplayer/async/match/queryjoin";
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
			
			// Match Type Key
			dictionary.Add("MatchTypeKey", MatchTypeKey);
			
			// Query
            if (Query != null)
			{
                var serialisedQuery = JsonSerialisation.Serialise(Query, (string element) =>
                {
                    return element;
                });
                dictionary.Add("Query", serialisedQuery);
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
			
			// Fallback To Any
            if (FallbackToAny != null)
			{
				dictionary.Add("FallbackToAny", FallbackToAny);
            }
	
			return dictionary;
		}
	}
}
