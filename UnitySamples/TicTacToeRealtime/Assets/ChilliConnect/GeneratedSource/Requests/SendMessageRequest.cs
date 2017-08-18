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
 	/// Send Message api call.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class SendMessageRequest : IImmediateServerRequest
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
		/// ChilliConnectID of the Player to send the message to.
		/// </summary>
        public string To { get; private set; }
	
		/// <summary>
		/// ChilliConnectID of the Player to send the message from.
		/// </summary>
        public string From { get; private set; }
	
		/// <summary>
		/// A title or summary for the message.
		/// </summary>
        public string Title { get; private set; }
	
		/// <summary>
		/// The message body to send.
		/// </summary>
        public string Text { get; private set; }
	
		/// <summary>
		/// Custom data to be sent with the message.
		/// </summary>
        public MultiTypeValue Data { get; private set; }
	
		/// <summary>
		/// An array list of Tags.
		/// </summary>
        public ReadOnlyCollection<string> Tags { get; private set; }
	
		/// <summary>
		/// Number of seconds until the message expires. Default: 7776000 (90 days). Maximum:
		/// 7776000 (90 days).
		/// </summary>
        public int? Expiry { get; private set; }
	
		/// <summary>
		/// Items that are going to be generated and sent to the recipient.
		/// </summary>
        public MessageGifts Gifts { get; private set; }
	
		/// <summary>
		/// Items that are to be transferred from the sender to the recipient. Items are
		/// deducted from the sender's account upon sending. Note: It is invalid to populate
		/// this parameter if there is no sender (From is not given).
		/// </summary>
        public MessageTransfer Transfer { get; private set; }

		/// <summary>
		/// Initialises a new instance of the request with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		/// <param name="connectAccessToken">A valid session ConnectAccessToken obtained through one of the login endpoints.</param>
		public SendMessageRequest(SendMessageRequestDesc desc, string connectAccessToken)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.To, "To cannot be null.");
	
			ReleaseAssert.IsNotNull(connectAccessToken, "Connect Access Token cannot be null.");
	
            To = desc.To;
            From = desc.From;
            Title = desc.Title;
            Text = desc.Text;
            Data = desc.Data;
            if (desc.Tags != null)
			{
                Tags = Mutability.ToImmutable(desc.Tags);
			}
            Expiry = desc.Expiry;
            Gifts = desc.Gifts;
            Transfer = desc.Transfer;
            ConnectAccessToken = connectAccessToken;
	
			Url = "https://connect.chilliconnect.com/1.0/message/send";
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
			
			// To
			dictionary.Add("To", To);
			
			// From
            if (From != null)
			{
				dictionary.Add("From", From);
            }
			
			// Title
            if (Title != null)
			{
				dictionary.Add("Title", Title);
            }
			
			// Text
            if (Text != null)
			{
				dictionary.Add("Text", Text);
            }
			
			// Data
            if (Data != null)
			{
                dictionary.Add("Data", Data.Serialise());
            }
			
			// Tags
            if (Tags != null)
			{
                var serialisedTags = JsonSerialisation.Serialise(Tags, (string element) =>
                {
                    return element;
                });
                dictionary.Add("Tags", serialisedTags);
            }
			
			// Expiry
            if (Expiry != null)
			{
				dictionary.Add("Expiry", Expiry);
            }
			
			// Gifts
            if (Gifts != null)
			{
                dictionary.Add("Gifts", Gifts.Serialise());
            }
			
			// Transfer
            if (Transfer != null)
			{
                dictionary.Add("Transfer", Transfer.Serialise());
            }
	
			return dictionary;
		}
	}
}
