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
	/// <para>A container for information on a DLC package file.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class DlcPackageFile
	{
		/// <summary>
		/// The file name.
		/// </summary>
        public string Name { get; private set; }
	
		/// <summary>
		/// The file location within the package.
		/// </summary>
        public string Location { get; private set; }
	
		/// <summary>
		/// The file size in bytes.
		/// </summary>
        public int Size { get; private set; }
	
		/// <summary>
		/// The file SHA1 checksum.
		/// </summary>
        public string Checksum { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given properties.
		/// </summary>
		///
		/// <param name="name">The file name.</param>
		/// <param name="location">The file location within the package.</param>
		/// <param name="size">The file size in bytes.</param>
		/// <param name="checksum">The file SHA1 checksum.</param>
		public DlcPackageFile(string name, string location, int size, string checksum)
		{
			ReleaseAssert.IsNotNull(name, "Name cannot be null.");
			ReleaseAssert.IsNotNull(location, "Location cannot be null.");
			ReleaseAssert.IsNotNull(checksum, "Checksum cannot be null.");
	
            Name = name;
            Location = location;
            Size = size;
            Checksum = checksum;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public DlcPackageFile(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Name"), "Json is missing required field 'Name'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Location"), "Json is missing required field 'Location'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Size"), "Json is missing required field 'Size'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Checksum"), "Json is missing required field 'Checksum'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Name
				if (entry.Key == "Name")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Name = (string)entry.Value;
				}
		
				// Location
				else if (entry.Key == "Location")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Location = (string)entry.Value;
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
			
			// Name
			dictionary.Add("Name", Name);
			
			// Location
			dictionary.Add("Location", Location);
			
			// Size
			dictionary.Add("Size", Size);
			
			// Checksum
			dictionary.Add("Checksum", Checksum);
			
			return dictionary;
		}
	}
}
