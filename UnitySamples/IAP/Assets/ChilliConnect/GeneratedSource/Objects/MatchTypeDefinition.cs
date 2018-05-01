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
	/// <para>A container used to describe Multiplayer MatchTypes.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class MatchTypeDefinition
	{
		/// <summary>
		/// The key of the MatchType.
		/// </summary>
        public string Key { get; private set; }
	
		/// <summary>
		/// The name of the MatchType.
		/// </summary>
        public string Name { get; private set; }
	
		/// <summary>
		/// The allowed TurnTypes.
		/// </summary>
        public ReadOnlyCollection<string> TurnTypes { get; private set; }
	
		/// <summary>
		/// The MatchType's property definitions.
		/// </summary>
        public ReadOnlyCollection<MatchTypePropertyDefinition> Properties { get; private set; }
	
		/// <summary>
		/// Amount of time a Match in WAITING State will wait before entering State TIMEOUT.
		/// </summary>
        public int DefaultWaitingTimeout { get; private set; }
	
		/// <summary>
		/// Amount of time before the Match will enter State TIMEOUT.
		/// </summary>
        public int DefaultTurnTimeout { get; private set; }
	
		/// <summary>
		/// The custom data of the MatchType.
		/// </summary>
        public MultiTypeValue CustomData { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		public MatchTypeDefinition(MatchTypeDefinitionDesc desc)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.Key, "Key cannot be null.");
			ReleaseAssert.IsNotNull(desc.Name, "Name cannot be null.");
			ReleaseAssert.IsNotNull(desc.TurnTypes, "TurnTypes cannot be null.");
	
            Key = desc.Key;
            Name = desc.Name;
            TurnTypes = Mutability.ToImmutable(desc.TurnTypes);
            if (desc.Properties != null)
			{
                Properties = Mutability.ToImmutable(desc.Properties);
			}
            DefaultWaitingTimeout = desc.DefaultWaitingTimeout;
            DefaultTurnTimeout = desc.DefaultTurnTimeout;
            CustomData = desc.CustomData;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public MatchTypeDefinition(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Key"), "Json is missing required field 'Key'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Name"), "Json is missing required field 'Name'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("TurnTypes"), "Json is missing required field 'TurnTypes'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("DefaultWaitingTimeout"), "Json is missing required field 'DefaultWaitingTimeout'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("DefaultTurnTimeout"), "Json is missing required field 'DefaultTurnTimeout'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Key
				if (entry.Key == "Key")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Key = (string)entry.Value;
				}
		
				// Name
				else if (entry.Key == "Name")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Name = (string)entry.Value;
				}
		
				// Turn Types
				else if (entry.Key == "TurnTypes")
				{
                    ReleaseAssert.IsTrue(entry.Value is IList<object>, "Invalid serialised type.");
                    TurnTypes = JsonSerialisation.DeserialiseList((IList<object>)entry.Value, (object element) =>
                    {
                        ReleaseAssert.IsTrue(element is string, "Invalid element type.");
                        return (string)element;
                    });
				}
		
				// Properties
				else if (entry.Key == "Properties")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is IList<object>, "Invalid serialised type.");
                        Properties = JsonSerialisation.DeserialiseList((IList<object>)entry.Value, (object element) =>
                        {
                            ReleaseAssert.IsTrue(element is IDictionary<string, object>, "Invalid element type.");
                            return new MatchTypePropertyDefinition((IDictionary<string, object>)element);	
                        });
                    }
				}
		
				// Default Waiting Timeout
				else if (entry.Key == "DefaultWaitingTimeout")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    DefaultWaitingTimeout = (int)(long)entry.Value;
				}
		
				// Default Turn Timeout
				else if (entry.Key == "DefaultTurnTimeout")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    DefaultTurnTimeout = (int)(long)entry.Value;
				}
		
				// Custom Data
				else if (entry.Key == "CustomData")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is object, "Invalid serialised type.");
                        CustomData = new MultiTypeValue((object)entry.Value);	
                    }
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
			
			// Key
			dictionary.Add("Key", Key);
			
			// Name
			dictionary.Add("Name", Name);
			
			// Turn Types
            var serialisedTurnTypes = JsonSerialisation.Serialise(TurnTypes, (string element) =>
            {
                return element;
            });
            dictionary.Add("TurnTypes", serialisedTurnTypes);
			
			// Properties
            if (Properties != null)
			{
                var serialisedProperties = JsonSerialisation.Serialise(Properties, (MatchTypePropertyDefinition element) =>
                {
                    return element.Serialise();
                });
                dictionary.Add("Properties", serialisedProperties);
            }
			
			// Default Waiting Timeout
			dictionary.Add("DefaultWaitingTimeout", DefaultWaitingTimeout);
			
			// Default Turn Timeout
			dictionary.Add("DefaultTurnTimeout", DefaultTurnTimeout);
			
			// Custom Data
            if (CustomData != null)
			{
                dictionary.Add("CustomData", CustomData.Serialise());
            }
			
			return dictionary;
		}
	}
}
