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
	/// </para>A mutable description of a SendMessageFromPlayerRequest.</para>
	///
	/// </para>This is not thread-safe and should typically only be used to create new 
	/// instances of SendMessageFromPlayerRequest.</para>
	/// </summary>
	public sealed class SendMessageFromPlayerRequestDesc
	{
		/// <summary>
		/// ChilliConnectID of the Player to send the message to.
		/// </summary>
        public string To { get; set; }
	
		/// <summary>
		/// A title or summary for the message.
		/// </summary>
        public string Title { get; set; }
	
		/// <summary>
		/// The message body to send.
		/// </summary>
        public string Text { get; set; }
	
		/// <summary>
		/// Custom data to be sent with the message.
		/// </summary>
        public MultiTypeValue Data { get; set; }
	
		/// <summary>
		/// An array list of Tags.
		/// </summary>
        public IList<string> Tags { get; set; }
	
		/// <summary>
		/// Number of seconds until the message expires. Default: 7776000 (90 days). Maximum:
		/// 7776000 (90 days).
		/// </summary>
        public int? Expiry { get; set; }
	
		/// <summary>
		/// Items that are to be transferred from the sender to the recipient. Items are
		/// deducted from the sender's account upon sending.
		/// </summary>
        public MessageTransfer Transfer { get; set; }

		/// <summary>
		/// Initialises a new instance of the description with the given required properties.
		/// </summary>
		///
		/// <param name="to">ChilliConnectID of the Player to send the message to.</param>
		public SendMessageFromPlayerRequestDesc(string to)
		{
			ReleaseAssert.IsNotNull(to, "To cannot be null.");
	
            To = to;
		}
	}
}
