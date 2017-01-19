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
 	/// Create Player api call.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class CreatePlayerRequest : IImmediateServerRequest
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
		/// The Game within which to create the new player.
		/// </summary>
        public string GameToken { get; private set; }
	
		/// <summary>
		/// The UserName of the new player account. If provided, this must be unique across
		/// all players within the game, contain only alpha, numeric, underscore or dash
		/// characters, and a minimum size of 3 characters, maximum 50.
		/// </summary>
        public string UserName { get; private set; }
	
		/// <summary>
		/// A non-unique DisplayName that can be used to identify the Player within the game.
		/// If provided it can contain only alpha, numeric, underscore or dash characters,
		/// and a minimum size of 3 characters, maximum 50.
		/// </summary>
        public string DisplayName { get; private set; }
	
		/// <summary>
		/// Email address to be associated with the new player account. If provided, this
		/// must be unique across all players within the game.
		/// </summary>
        public string Email { get; private set; }
	
		/// <summary>
		/// Password to be assigned to the new player account. If provided must be greater
		/// than 3 and less than 50 characters in length.
		/// </summary>
        public string Password { get; private set; }

		/// <summary>
		/// Initialises a new instance of the request with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		/// <param name="gameToken">The Game within which to create the new player.</param>
		public CreatePlayerRequest(CreatePlayerRequestDesc desc, string gameToken)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
	
			ReleaseAssert.IsNotNull(gameToken, "Game Token cannot be null.");
	
            UserName = desc.UserName;
            DisplayName = desc.DisplayName;
            Email = desc.Email;
            Password = desc.Password;
            GameToken = gameToken;
	
			Url = "https://connect.chilliconnect.com/1.0/player/create";
			HttpRequestMethod = HttpRequestMethod.Post;
		}

		/// <summary>
		/// Serialises all header properties. The output will be a dictionary containing 
		/// the extra header key-value pairs in addition the standard headers sent with 
		/// all server requests. Will return an empty dictionary if there are no headers.
		/// </summary>
		///
		/// <returns>The header hey-value pairs.</returns>
		public IDictionary<string, string> SerialiseHeaders()
		{
			var dictionary = new Dictionary<string, string>();
			
			// Game Token
			dictionary.Add("Game-Token", GameToken.ToString());
		
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
			
			// User Name
            if (UserName != null)
			{
				dictionary.Add("UserName", UserName);
            }
			
			// Display Name
            if (DisplayName != null)
			{
				dictionary.Add("DisplayName", DisplayName);
            }
			
			// Email
            if (Email != null)
			{
				dictionary.Add("Email", Email);
            }
			
			// Password
            if (Password != null)
			{
				dictionary.Add("Password", Password);
            }
	
			return dictionary;
		}
	}
}
