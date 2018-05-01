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
	/// <para>A container for information on a Catalog UnrealMobilePatch Patch.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class UnrealMobilePatchDefinitionPatch
	{
		/// <summary>
		/// The URL to the CloudDir of the Patch.
		/// </summary>
        public string CloudUrl { get; private set; }
	
		/// <summary>
		/// The URL to the Manifest file of the Patch.
		/// </summary>
        public string ManifestUrl { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given properties.
		/// </summary>
		///
		/// <param name="cloudUrl">The URL to the CloudDir of the Patch.</param>
		/// <param name="manifestUrl">The URL to the Manifest file of the Patch.</param>
		public UnrealMobilePatchDefinitionPatch(string cloudUrl, string manifestUrl)
		{
			ReleaseAssert.IsNotNull(cloudUrl, "Cloud Url cannot be null.");
			ReleaseAssert.IsNotNull(manifestUrl, "Manifest Url cannot be null.");
	
            CloudUrl = cloudUrl;
            ManifestUrl = manifestUrl;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public UnrealMobilePatchDefinitionPatch(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("CloudUrl"), "Json is missing required field 'CloudUrl'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("ManifestUrl"), "Json is missing required field 'ManifestUrl'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Cloud Url
				if (entry.Key == "CloudUrl")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    CloudUrl = (string)entry.Value;
				}
		
				// Manifest Url
				else if (entry.Key == "ManifestUrl")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    ManifestUrl = (string)entry.Value;
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
			
			// Cloud Url
			dictionary.Add("CloudUrl", CloudUrl);
			
			// Manifest Url
			dictionary.Add("ManifestUrl", ManifestUrl);
			
			return dictionary;
		}
	}
}
