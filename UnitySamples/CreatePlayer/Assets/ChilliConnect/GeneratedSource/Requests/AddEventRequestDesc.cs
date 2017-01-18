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
	/// </para>A mutable description of a AddEventRequest.</para>
	///
	/// </para>This is not thread-safe and should typically only be used to create new 
	/// instances of AddEventRequest.</para>
	/// </summary>
	public sealed class AddEventRequestDesc
	{
		/// <summary>
		/// The type of custom event. This should map to a custom event type defined within
		/// the ChilliConnect dashboard.
		/// </summary>
        public string Type { get; set; }
	
		/// <summary>
		/// A number representing the player's in game level.
		/// </summary>
        public int? UserGrade { get; set; }
	
		/// <summary>
		/// A string indicating the test group the player belongs to.
		/// </summary>
        public string TestGroup { get; set; }
	
		/// <summary>
		/// Object containing Key-Value pairs that map on to the custom event parameter
		/// definitions for this event. All parameters are considered optional - however, any
		/// parameters submitted for a custom event must be defined otherwise the event will
		/// be considered invalid and not be processed.
		/// </summary>
        public IDictionary<string, string> Parameters { get; set; }
	
		/// <summary>
		/// The number of times this event occurred. If not provided, this will be defaulted
		/// to 1.
		/// </summary>
        public int? Count { get; set; }

		/// <summary>
		/// Initialises a new instance of the description with the given required properties.
		/// </summary>
		///
		/// <param name="type">The type of custom event. This should map to a custom event type defined within
		/// the ChilliConnect dashboard.</param>
		public AddEventRequestDesc(string type)
		{
			ReleaseAssert.IsNotNull(type, "Type cannot be null.");
	
            Type = type;
		}
	}
}
