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
	/// <para>A mutable description of a TestDetails.</para>
	///
	/// <para>This is not thread-safe and should typically only be used to create new 
	/// instances of TestDetails.</para>
	/// </summary>
	public sealed class TestDetailsDesc
	{
		/// <summary>
		/// The name of the Test.
		/// </summary>
        public string Name { get; set; }
	
		/// <summary>
		/// The key of the Test.
		/// </summary>
        public string Key { get; set; }
	
		/// <summary>
		/// The Date the Test began. Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime StartDate { get; set; }
	
		/// <summary>
		/// The Date the Test ended. Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime EndDate { get; set; }
	
		/// <summary>
		/// The custom data of the Test.
		/// </summary>
        public MultiTypeValue CustomData { get; set; }

		/// <summary>
		/// Initialises a new instance with the given required properties.
		/// </summary>
		///
		/// <param name="name">The name of the Test.</param>
		/// <param name="key">The key of the Test.</param>
		/// <param name="startDate">The Date the Test began. Format: ISO8601 e.g. 2016-01-12T11:08:23.</param>
		public TestDetailsDesc(string name, string key, DateTime startDate)
		{
			ReleaseAssert.IsNotNull(name, "Name cannot be null.");
			ReleaseAssert.IsNotNull(key, "Key cannot be null.");
			ReleaseAssert.IsNotNull(startDate, "Start Date cannot be null.");
	
            Name = name;
            Key = key;
            StartDate = startDate;
		}
	}
}
