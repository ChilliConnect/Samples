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
	/// A container for information on the response from a GetDefinitionsPackageRequest.
	/// </summary>
	public sealed class GetDefinitionsPackageResponse
	{
		/// <summary>
		/// The Catalog Version.
		/// </summary>
        public string Version { get; private set; }
	
		/// <summary>
		/// The URL that the Package can be downloaded from.
		/// </summary>
        public string Url { get; private set; }
	
		/// <summary>
		/// The Package size in bytes.
		/// </summary>
        public int Size { get; private set; }
	
		/// <summary>
		/// The package SHA1 Checksum.
		/// </summary>
        public string Checksum { get; private set; }
	
		/// <summary>
		/// A list of files contained in the Package.
		/// </summary>
        public ReadOnlyCollection<CatalogPackageFile> Files { get; private set; }

		/// <summary>
		/// Initialises the response with the given json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the JSON data.</param>
		public GetDefinitionsPackageResponse(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Version"), "Json is missing required field 'Version'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Url"), "Json is missing required field 'Url'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Size"), "Json is missing required field 'Size'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Checksum"), "Json is missing required field 'Checksum'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Files"), "Json is missing required field 'Files'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Version
				if (entry.Key == "Version")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Version = (string)entry.Value;
				}
		
				// Url
				else if (entry.Key == "Url")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Url = (string)entry.Value;
				}
		
				// Size
				else if (entry.Key == "Size")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    Size = (int)(long)entry.Value;
				}
		
				// Checksum
				else if (entry.Key == "Checksum")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Checksum = (string)entry.Value;
				}
		
				// Files
				else if (entry.Key == "Files")
				{
                    ReleaseAssert.IsTrue(entry.Value is IList<object>, "Invalid serialised type.");
                    Files = JsonSerialisation.DeserialiseList((IList<object>)entry.Value, (object element) =>
                    {
                        ReleaseAssert.IsTrue(element is IDictionary<string, object>, "Invalid element type.");
                        return new CatalogPackageFile((IDictionary<string, object>)element);	
                    });
				}
			}
		}
	}
}
