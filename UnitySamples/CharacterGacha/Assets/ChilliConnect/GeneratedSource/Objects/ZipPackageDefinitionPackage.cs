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
	/// <para>A container for information on a Catalog ZipPackage Package.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class ZipPackageDefinitionPackage
	{
		/// <summary>
		/// The URL that the Package can be downloaded from.
		/// </summary>
        public string Url { get; private set; }
	
		/// <summary>
		/// The package SHA1 Checksum.
		/// </summary>
        public string Checksum { get; private set; }
	
		/// <summary>
		/// The Package size in bytes.
		/// </summary>
        public int Size { get; private set; }
	
		/// <summary>
		/// A list of files contained in the Package.
		/// </summary>
        public ReadOnlyCollection<ZipPackageDefinitionPackageFile> Files { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given properties.
		/// </summary>
		///
		/// <param name="url">The URL that the Package can be downloaded from.</param>
		/// <param name="checksum">The package SHA1 Checksum.</param>
		/// <param name="size">The Package size in bytes.</param>
		/// <param name="files">A list of files contained in the Package.</param>
		public ZipPackageDefinitionPackage(string url, string checksum, int size, IList<ZipPackageDefinitionPackageFile> files)
		{
			ReleaseAssert.IsNotNull(url, "Url cannot be null.");
			ReleaseAssert.IsNotNull(checksum, "Checksum cannot be null.");
			ReleaseAssert.IsNotNull(files, "Files cannot be null.");
	
            Url = url;
            Checksum = checksum;
            Size = size;
            Files = Mutability.ToImmutable(files);
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public ZipPackageDefinitionPackage(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Url"), "Json is missing required field 'Url'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Checksum"), "Json is missing required field 'Checksum'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Size"), "Json is missing required field 'Size'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Files"), "Json is missing required field 'Files'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Url
				if (entry.Key == "Url")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Url = (string)entry.Value;
				}
		
				// Checksum
				else if (entry.Key == "Checksum")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Checksum = (string)entry.Value;
				}
		
				// Size
				else if (entry.Key == "Size")
				{
                    ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                    Size = (int)(long)entry.Value;
				}
		
				// Files
				else if (entry.Key == "Files")
				{
                    ReleaseAssert.IsTrue(entry.Value is IList<object>, "Invalid serialised type.");
                    Files = JsonSerialisation.DeserialiseList((IList<object>)entry.Value, (object element) =>
                    {
                        ReleaseAssert.IsTrue(element is IDictionary<string, object>, "Invalid element type.");
                        return new ZipPackageDefinitionPackageFile((IDictionary<string, object>)element);	
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
			
			// Url
			dictionary.Add("Url", Url);
			
			// Checksum
			dictionary.Add("Checksum", Checksum);
			
			// Size
			dictionary.Add("Size", Size);
			
			// Files
            var serialisedFiles = JsonSerialisation.Serialise(Files, (ZipPackageDefinitionPackageFile element) =>
            {
                return element.Serialise();
            });
            dictionary.Add("Files", serialisedFiles);
			
			return dictionary;
		}
	}
}
