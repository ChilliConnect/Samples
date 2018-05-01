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
 	/// Create Match api call.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class CreateMatchRequest : IImmediateServerRequest
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
		/// A Match Type definition Key.
		/// </summary>
        public string MatchTypeKey { get; private set; }
	
		/// <summary>
		/// Describes the turn behaviour ChilliConnect will apply to the match. Accepted
		/// values defined by the MatchType.
		/// </summary>
        public string TurnType { get; private set; }
	
		/// <summary>
		/// Maximum number of players that can join the match. Max 10.
		/// </summary>
        public int PlayerLimit { get; private set; }
	
		/// <summary>
		/// A set of Key-Value pairs that are used to search for matches. Keys and
		/// Value-types are defined in the MatchType.
		/// </summary>
        public ReadOnlyDictionary<string, MultiTypeValue> Properties { get; private set; }
	
		/// <summary>
		/// Initial state data.
		/// </summary>
        public MultiTypeValue StateData { get; private set; }
	
		/// <summary>
		/// Number of minutes that each Player has to complete their turn before the Match
		/// will go into a TIMEOUT state. Defaults to the MatchType value. Max 10,080 (7
		/// days).
		/// </summary>
        public int? TurnTimeout { get; private set; }
	
		/// <summary>
		/// Number of minutes that the match can remain in the WAITING state before going
		/// into the TIMEOUT state. Defaults to the MatchType value. Max 10,080 (7 days).
		/// </summary>
        public int? WaitingTimeout { get; private set; }
	
		/// <summary>
		/// Only when TurnType is SEQUENTIAL. Accepted values: RANDOM, ORDERED, EXPLICIT.
		/// Defaults to ORDERED. See documentation.
		/// </summary>
        public string TurnOrderType { get; private set; }
	
		/// <summary>
		/// List of Player ChilliConnectIDs to be added as participants of the match. Note:
		/// the currently logged in Player, if there one, is automatically added as a match
		/// participant.
		/// </summary>
        public ReadOnlyCollection<string> Players { get; private set; }
	
		/// <summary>
		/// Defaults to true. When true, a match will be started when PlayerLimit is reached.
		/// </summary>
        public bool? AutoStart { get; private set; }
	
		/// <summary>
		/// True if the match should only be available by it's MatchID. Default; false.
		/// </summary>
        public bool? Private { get; private set; }

		/// <summary>
		/// Initialises a new instance of the request with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		/// <param name="connectAccessToken">A valid session ConnectAccessToken obtained through one of the login endpoints.</param>
		public CreateMatchRequest(CreateMatchRequestDesc desc, string connectAccessToken)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.MatchTypeKey, "MatchTypeKey cannot be null.");
			ReleaseAssert.IsNotNull(desc.TurnType, "TurnType cannot be null.");
	
			ReleaseAssert.IsNotNull(connectAccessToken, "Connect Access Token cannot be null.");
	
            MatchTypeKey = desc.MatchTypeKey;
            TurnType = desc.TurnType;
            PlayerLimit = desc.PlayerLimit;
            if (desc.Properties != null)
			{
                Properties = Mutability.ToImmutable(desc.Properties);
			}
            StateData = desc.StateData;
            TurnTimeout = desc.TurnTimeout;
            WaitingTimeout = desc.WaitingTimeout;
            TurnOrderType = desc.TurnOrderType;
            if (desc.Players != null)
			{
                Players = Mutability.ToImmutable(desc.Players);
			}
            AutoStart = desc.AutoStart;
            Private = desc.Private;
            ConnectAccessToken = connectAccessToken;
	
			Url = "https://connect.chilliconnect.com/1.0/multiplayer/async/match/create";
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
			
			// Turn Type
			dictionary.Add("TurnType", TurnType);
			
			// Player Limit
			dictionary.Add("PlayerLimit", PlayerLimit);
			
			// Properties
            if (Properties != null)
			{
                var serialisedProperties = JsonSerialisation.Serialise(Properties, (MultiTypeValue element) =>
                {
                    return element.Serialise();
                });
                dictionary.Add("Properties", serialisedProperties);
            }
			
			// State Data
            if (StateData != null)
			{
                dictionary.Add("StateData", StateData.Serialise());
            }
			
			// Turn Timeout
            if (TurnTimeout != null)
			{
				dictionary.Add("TurnTimeout", TurnTimeout);
            }
			
			// Waiting Timeout
            if (WaitingTimeout != null)
			{
				dictionary.Add("WaitingTimeout", WaitingTimeout);
            }
			
			// Turn Order Type
            if (TurnOrderType != null)
			{
				dictionary.Add("TurnOrderType", TurnOrderType);
            }
			
			// Players
            if (Players != null)
			{
                var serialisedPlayers = JsonSerialisation.Serialise(Players, (string element) =>
                {
                    return element;
                });
                dictionary.Add("Players", serialisedPlayers);
            }
			
			// Auto Start
            if (AutoStart != null)
			{
				dictionary.Add("AutoStart", AutoStart);
            }
			
			// Private
            if (Private != null)
			{
				dictionary.Add("Private", Private);
            }
	
			return dictionary;
		}
	}
}
