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
	/// <para>A container used to describe a message.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class Message
	{
		/// <summary>
		/// Identifier for the message.
		/// </summary>
        public string MessageId { get; private set; }
	
		/// <summary>
		/// Details of the player that sent the message.
		/// </summary>
        public MessageSender From { get; private set; }
	
		/// <summary>
		/// Date when the message was sent (UTC). Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime SentOn { get; private set; }
	
		/// <summary>
		/// Has the message been read.
		/// </summary>
        public bool Read { get; private set; }
	
		/// <summary>
		/// Date when the message was read (UTC). Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime ReadOn { get; private set; }
	
		/// <summary>
		/// Have the message rewards been redeemed.
		/// </summary>
        public bool? Redeemed { get; private set; }
	
		/// <summary>
		/// Date when the message rewards were redeemed (UTC). Format: ISO8601 e.g.
		/// 2016-01-12T11:08:23.
		/// </summary>
        public DateTime RedeemedOn { get; private set; }
	
		/// <summary>
		/// An array list of Tags for the message.
		/// </summary>
        public ReadOnlyCollection<string> Tags { get; private set; }
	
		/// <summary>
		/// Number of seconds until the message expires.
		/// </summary>
        public int? Expiry { get; private set; }
	
		/// <summary>
		/// A title or summary for the message.
		/// </summary>
        public string Title { get; private set; }
	
		/// <summary>
		/// The message body.
		/// </summary>
        public string Text { get; private set; }
	
		/// <summary>
		/// Custom data for the message.
		/// </summary>
        public MultiTypeValue Data { get; private set; }
	
		/// <summary>
		/// The rewards that may be redeemed by the recipient of the message.
		/// </summary>
        public MessageReward Rewards { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		public Message(MessageDesc desc)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.MessageId, "MessageId cannot be null.");
			ReleaseAssert.IsNotNull(desc.From, "From cannot be null.");
			ReleaseAssert.IsNotNull(desc.SentOn, "SentOn cannot be null.");
	
            MessageId = desc.MessageId;
            From = desc.From;
            SentOn = desc.SentOn;
            Read = desc.Read;
            ReadOn = desc.ReadOn;
            Redeemed = desc.Redeemed;
            RedeemedOn = desc.RedeemedOn;
            if (desc.Tags != null)
			{
                Tags = Mutability.ToImmutable(desc.Tags);
			}
            Expiry = desc.Expiry;
            Title = desc.Title;
            Text = desc.Text;
            Data = desc.Data;
            Rewards = desc.Rewards;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public Message(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("MessageID"), "Json is missing required field 'MessageID'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("From"), "Json is missing required field 'From'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("SentOn"), "Json is missing required field 'SentOn'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Read"), "Json is missing required field 'Read'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Message Id
				if (entry.Key == "MessageID")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    MessageId = (string)entry.Value;
				}
		
				// From
				else if (entry.Key == "From")
				{
                    ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                    From = new MessageSender((IDictionary<string, object>)entry.Value);	
				}
		
				// Sent On
				else if (entry.Key == "SentOn")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    SentOn = JsonSerialisation.DeserialiseDate((string)entry.Value);
				}
		
				// Read
				else if (entry.Key == "Read")
				{
                    ReleaseAssert.IsTrue(entry.Value is bool, "Invalid serialised type.");
                    Read = (bool)entry.Value;
				}
		
				// Read On
				else if (entry.Key == "ReadOn")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                        ReadOn = JsonSerialisation.DeserialiseDate((string)entry.Value);
                    }
				}
		
				// Redeemed
				else if (entry.Key == "Redeemed")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is bool, "Invalid serialised type.");
                        Redeemed = (bool)entry.Value;
                    }
				}
		
				// Redeemed On
				else if (entry.Key == "RedeemedOn")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                        RedeemedOn = JsonSerialisation.DeserialiseDate((string)entry.Value);
                    }
				}
		
				// Tags
				else if (entry.Key == "Tags")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is IList<object>, "Invalid serialised type.");
                        Tags = JsonSerialisation.DeserialiseList((IList<object>)entry.Value, (object element) =>
                        {
                            ReleaseAssert.IsTrue(element is string, "Invalid element type.");
                            return (string)element;
                        });
                    }
				}
		
				// Expiry
				else if (entry.Key == "Expiry")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                        Expiry = (int)(long)entry.Value;
                    }
				}
		
				// Title
				else if (entry.Key == "Title")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                        Title = (string)entry.Value;
                    }
				}
		
				// Text
				else if (entry.Key == "Text")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                        Text = (string)entry.Value;
                    }
				}
		
				// Data
				else if (entry.Key == "Data")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is object, "Invalid serialised type.");
                        Data = new MultiTypeValue((object)entry.Value);	
                    }
				}
		
				// Rewards
				else if (entry.Key == "Rewards")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                        Rewards = new MessageReward((IDictionary<string, object>)entry.Value);	
                    }
				}
	
				// An error has occurred.
				else
				{
#if DEBUG
					throw new ArgumentException("Input Json contains an invalid field.");
#endif
				}
			}
		}

		/// <summary>
		/// Serialises all properties. The output will be a dictionary containing the
		/// objects properties in a form that can easily be converted to Json. 
		/// </summary>
		///
		/// <returns>The serialised object in dictionary form.</returns>
		public IDictionary<string, object> Serialise()
		{
            var dictionary = new Dictionary<string, object>();
			
			// Message Id
			dictionary.Add("MessageID", MessageId);
			
			// From
            dictionary.Add("From", From.Serialise());
			
			// Sent On
            dictionary.Add("SentOn", JsonSerialisation.Serialise(SentOn));
			
			// Read
			dictionary.Add("Read", Read);
			
			// Read On
            if (ReadOn != null)
			{
                dictionary.Add("ReadOn", JsonSerialisation.Serialise(ReadOn));
            }
			
			// Redeemed
            if (Redeemed != null)
			{
				dictionary.Add("Redeemed", Redeemed);
            }
			
			// Redeemed On
            if (RedeemedOn != null)
			{
                dictionary.Add("RedeemedOn", JsonSerialisation.Serialise(RedeemedOn));
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
			
			// Rewards
            if (Rewards != null)
			{
                dictionary.Add("Rewards", Rewards.Serialise());
            }
			
			return dictionary;
		}
	}
}
