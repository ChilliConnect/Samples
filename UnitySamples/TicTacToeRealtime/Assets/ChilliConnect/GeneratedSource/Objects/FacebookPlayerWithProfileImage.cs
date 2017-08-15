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
	/// <para>A container for information on a player. This includes the ChilliConnectID of the
	/// account, as well as information such as the UserName, DisplayName, FacebookID,
	/// FacebookName and FacebookProfileImage.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class FacebookPlayerWithProfileImage
	{
		/// <summary>
		/// The player's ChilliConnectID.
		/// </summary>
        public string ChilliConnectId { get; private set; }
	
		/// <summary>
		/// The player's UserName.
		/// </summary>
        public string UserName { get; private set; }
	
		/// <summary>
		/// The player's DisplayName.
		/// </summary>
        public string DisplayName { get; private set; }
	
		/// <summary>
		/// The player's FacebookID.
		/// </summary>
        public string FacebookId { get; private set; }
	
		/// <summary>
		/// The player's Facebook Name.
		/// </summary>
        public string FacebookName { get; private set; }
	
		/// <summary>
		/// The player's Facebook profile image URL.
		/// </summary>
        public string FacebookProfileImage { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		public FacebookPlayerWithProfileImage(FacebookPlayerWithProfileImageDesc desc)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.ChilliConnectId, "ChilliConnectId cannot be null.");
			ReleaseAssert.IsNotNull(desc.FacebookId, "FacebookId cannot be null.");
			ReleaseAssert.IsNotNull(desc.FacebookName, "FacebookName cannot be null.");
			ReleaseAssert.IsNotNull(desc.FacebookProfileImage, "FacebookProfileImage cannot be null.");
	
            ChilliConnectId = desc.ChilliConnectId;
            UserName = desc.UserName;
            DisplayName = desc.DisplayName;
            FacebookId = desc.FacebookId;
            FacebookName = desc.FacebookName;
            FacebookProfileImage = desc.FacebookProfileImage;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public FacebookPlayerWithProfileImage(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("ChilliConnectID"), "Json is missing required field 'ChilliConnectID'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("FacebookID"), "Json is missing required field 'FacebookID'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("FacebookName"), "Json is missing required field 'FacebookName'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("FacebookProfileImage"), "Json is missing required field 'FacebookProfileImage'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Chilli Connect Id
				if (entry.Key == "ChilliConnectID")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    ChilliConnectId = (string)entry.Value;
				}
		
				// User Name
				else if (entry.Key == "UserName")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                        UserName = (string)entry.Value;
                    }
				}
		
				// Display Name
				else if (entry.Key == "DisplayName")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                        DisplayName = (string)entry.Value;
                    }
				}
		
				// Facebook Id
				else if (entry.Key == "FacebookID")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    FacebookId = (string)entry.Value;
				}
		
				// Facebook Name
				else if (entry.Key == "FacebookName")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    FacebookName = (string)entry.Value;
				}
		
				// Facebook Profile Image
				else if (entry.Key == "FacebookProfileImage")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    FacebookProfileImage = (string)entry.Value;
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
			
			// Chilli Connect Id
			dictionary.Add("ChilliConnectID", ChilliConnectId);
			
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
			
			// Facebook Id
			dictionary.Add("FacebookID", FacebookId);
			
			// Facebook Name
			dictionary.Add("FacebookName", FacebookName);
			
			// Facebook Profile Image
			dictionary.Add("FacebookProfileImage", FacebookProfileImage);
			
			return dictionary;
		}
	}
}
