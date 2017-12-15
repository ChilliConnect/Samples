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
	/// </para>A mutable description of a QueryAvailableMatchesRequest.</para>
	///
	/// </para>This is not thread-safe and should typically only be used to create new 
	/// instances of QueryAvailableMatchesRequest.</para>
	/// </summary>
	public sealed class QueryAvailableMatchesRequestDesc
	{
		/// <summary>
		/// The MatchType Key.
		/// </summary>
        public string MatchTypeKey { get; set; }
	
		/// <summary>
		/// A query to define which Matches to return. If not given all Matches will be
		/// returned. See the guide at https://docs.chilliconnect.com
		/// </summary>
        public string Query { get; set; }
	
		/// <summary>
		/// Key/value pairs that substitute placeholders in the query. E.g. {"name": "Geoff"}
		/// for query "Name = :name". See the guide at https://docs.chilliconnect.com
		/// </summary>
        public IDictionary<string, MultiTypeValue> Params { get; set; }
	
		/// <summary>
		/// An ordered list that define the response sort order. If not given response will
		/// be ordered by DateCreated ascending.
		/// </summary>
        public IList<SortField> SortFields { get; set; }
	
		/// <summary>
		/// Results are paged.
		/// </summary>
        public int? Page { get; set; }

		/// <summary>
		/// Initialises a new instance of the description with the given required properties.
		/// </summary>
		///
		/// <param name="matchTypeKey">The MatchType Key.</param>
		public QueryAvailableMatchesRequestDesc(string matchTypeKey)
		{
			ReleaseAssert.IsNotNull(matchTypeKey, "Match Type Key cannot be null.");
	
            MatchTypeKey = matchTypeKey;
		}
	}
}
