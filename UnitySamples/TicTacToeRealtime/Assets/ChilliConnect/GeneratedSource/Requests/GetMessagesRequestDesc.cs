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
	/// </para>A mutable description of a GetMessagesRequest.</para>
	///
	/// </para>This is not thread-safe and should typically only be used to create new 
	/// instances of GetMessagesRequest.</para>
	/// </summary>
	public sealed class GetMessagesRequestDesc
	{
		/// <summary>
		/// The Page requested. Paging is 1-indexed. If not provided, this will be defaulted
		/// to 1.
		/// </summary>
        public int? Page { get; set; }
	
		/// <summary>
		/// Only messages received now minus Since seconds will be returned. Default: 86400
		/// (24 hours).
		/// </summary>
        public int? Since { get; set; }
	
		/// <summary>
		/// Only unread messages will be returned. Default: true.
		/// </summary>
        public bool? UnreadOnly { get; set; }
	
		/// <summary>
		/// Only messages with the specified Tags will be returned.
		/// </summary>
        public IList<string> Tags { get; set; }
	
		/// <summary>
		/// Return full message bodies with the response. Default: false. If false; Text,
		/// Data, and Rewards will not be returned.
		/// </summary>
        public bool? FullMessages { get; set; }
	
		/// <summary>
		/// Mark messages as read once returned by this call. Default: false.
		/// </summary>
        public bool? MarkAsRead { get; set; }

		/// <summary>
		/// Initialises a new instance of the description with the given required properties.
		/// </summary>
		///
		public GetMessagesRequestDesc()
		{
		}
	}
}
