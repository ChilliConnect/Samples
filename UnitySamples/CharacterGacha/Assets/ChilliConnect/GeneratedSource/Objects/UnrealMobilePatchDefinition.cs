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
	/// <para>A container used to describe Catalog UnrealMobilePatch objects.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class UnrealMobilePatchDefinition
	{
		/// <summary>
		/// The key of the item.
		/// </summary>
        public string Key { get; private set; }
	
		/// <summary>
		/// The name of the item.
		/// </summary>
        public string Name { get; private set; }
	
		/// <summary>
		/// The tags of the item.
		/// </summary>
        public ReadOnlyCollection<string> Tags { get; private set; }
	
		/// <summary>
		/// The custom data of the item.
		/// </summary>
        public MultiTypeValue CustomData { get; private set; }
	
		/// <summary>
		/// A map of UnrealMobilePatch Patches keyed by the patch label. Note: Patches are
		/// keyed by label to enable future multi-patch support. Currently a single Patch is
		/// available under the label 'Default'.
		/// </summary>
        public ReadOnlyDictionary<string, UnrealMobilePatchDefinitionPatch> Patches { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		public UnrealMobilePatchDefinition(UnrealMobilePatchDefinitionDesc desc)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.Key, "Key cannot be null.");
			ReleaseAssert.IsNotNull(desc.Name, "Name cannot be null.");
			ReleaseAssert.IsNotNull(desc.Tags, "Tags cannot be null.");
			ReleaseAssert.IsNotNull(desc.Patches, "Patches cannot be null.");
	
            Key = desc.Key;
            Name = desc.Name;
            Tags = Mutability.ToImmutable(desc.Tags);
            CustomData = desc.CustomData;
            Patches = Mutability.ToImmutable(desc.Patches);
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public UnrealMobilePatchDefinition(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Key"), "Json is missing required field 'Key'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Name"), "Json is missing required field 'Name'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Tags"), "Json is missing required field 'Tags'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Patches"), "Json is missing required field 'Patches'");
	
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
		
				// Tags
				else if (entry.Key == "Tags")
				{
                    ReleaseAssert.IsTrue(entry.Value is IList<object>, "Invalid serialised type.");
                    Tags = JsonSerialisation.DeserialiseList((IList<object>)entry.Value, (object element) =>
                    {
                        ReleaseAssert.IsTrue(element is string, "Invalid element type.");
                        return (string)element;
                    });
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
		
				// Patches
				else if (entry.Key == "Patches")
				{
                    ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                    Patches = JsonSerialisation.DeserialiseMap((IDictionary<string, object>)entry.Value, (object element) =>
                    {
                        ReleaseAssert.IsTrue(element is IDictionary<string, object>, "Invalid element type.");
                        return new UnrealMobilePatchDefinitionPatch((IDictionary<string, object>)element);	
                    });
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
			
			// Tags
            var serialisedTags = JsonSerialisation.Serialise(Tags, (string element) =>
            {
                return element;
            });
            dictionary.Add("Tags", serialisedTags);
			
			// Custom Data
            if (CustomData != null)
			{
                dictionary.Add("CustomData", CustomData.Serialise());
            }
			
			// Patches
            var serialisedPatches = JsonSerialisation.Serialise(Patches, (UnrealMobilePatchDefinitionPatch element) =>
            {
                return element.Serialise();
            });
            dictionary.Add("Patches", serialisedPatches);
			
			return dictionary;
		}
	}
}
