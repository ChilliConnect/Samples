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
	/// <para>A mutable description of a Message.</para>
	///
	/// <para>This is not thread-safe and should typically only be used to create new 
	/// instances of Message.</para>
	/// </summary>
	public sealed class MessageDesc
	{
		/// <summary>
		/// Identifier for the message.
		/// </summary>
        public string MessageId { get; set; }
	
		/// <summary>
		/// Details of the player that sent the message.
		/// </summary>
        public MessageSender From { get; set; }
	
		/// <summary>
		/// Date when the message was sent (UTC). Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime SentOn { get; set; }
	
		/// <summary>
		/// Has the message been read.
		/// </summary>
        public bool Read { get; set; }
	
		/// <summary>
		/// Date when the message was read (UTC). Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime ReadOn { get; set; }
	
		/// <summary>
		/// Have the message rewards been redeemed.
		/// </summary>
        public bool? Redeemed { get; set; }
	
		/// <summary>
		/// Date when the message rewards were redeemed (UTC). Format: ISO8601 e.g.
		/// 2016-01-12T11:08:23.
		/// </summary>
        public DateTime RedeemedOn { get; set; }
	
		/// <summary>
		/// An array list of Tags for the message.
		/// </summary>
        public IList<string> Tags { get; set; }
	
		/// <summary>
		/// Number of seconds until the message expires.
		/// </summary>
        public int? Expiry { get; set; }
	
		/// <summary>
		/// A title or summary for the message.
		/// </summary>
        public string Title { get; set; }
	
		/// <summary>
		/// The message body.
		/// </summary>
        public string Text { get; set; }
	
		/// <summary>
		/// Custom data for the message.
		/// </summary>
        public MultiTypeValue Data { get; set; }
	
		/// <summary>
		/// The rewards that may be redeemed by the recipient of the message.
		/// </summary>
        public MessageReward Rewards { get; set; }

		/// <summary>
		/// Initialises a new instance with the given required properties.
		/// </summary>
		///
		/// <param name="messageId">Identifier for the message.</param>
		/// <param name="from">Details of the player that sent the message.</param>
		/// <param name="sentOn">Date when the message was sent (UTC). Format: ISO8601 e.g. 2016-01-12T11:08:23.</param>
		/// <param name="read">Has the message been read.</param>
		public MessageDesc(string messageId, MessageSender from, DateTime sentOn, bool read)
		{
			ReleaseAssert.IsNotNull(messageId, "Message Id cannot be null.");
			ReleaseAssert.IsNotNull(from, "From cannot be null.");
			ReleaseAssert.IsNotNull(sentOn, "Sent On cannot be null.");
	
            MessageId = messageId;
            From = from;
            SentOn = sentOn;
            Read = read;
		}
	}
}
