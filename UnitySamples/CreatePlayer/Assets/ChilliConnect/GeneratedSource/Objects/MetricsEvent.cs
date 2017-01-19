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
	/// <para>A container for information on a single Metrics event.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class MetricsEvent
	{
		/// <summary>
		/// The type of custom event. This should map to a custom event type defined within
		/// the ChilliConnect dashboard.
		/// </summary>
        public string Type { get; private set; }
	
		/// <summary>
		/// Date that indicates the local, device time that the event occurred. Format:
		/// ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime Date { get; private set; }
	
		/// <summary>
		/// A number representing the player's in game level.
		/// </summary>
        public int? UserGrade { get; private set; }
	
		/// <summary>
		/// A string indicating the test group this player belongs to.
		/// </summary>
        public string TestGroup { get; private set; }
	
		/// <summary>
		/// Object containing Key-Value pairs that map on to the custom event parameter
		/// definitions for this event. All parameters are considered optional - however, any
		/// parameters submitted for a custom event must be defined otherwise the event will
		/// be considered invalid and not be processed.
		/// </summary>
        public ReadOnlyDictionary<string, string> Parameters { get; private set; }
	
		/// <summary>
		/// The number of times this event occurred. If not provided, this will be defaulted
		/// to 1.
		/// </summary>
        public int? Count { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		public MetricsEvent(MetricsEventDesc desc)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
			ReleaseAssert.IsNotNull(desc.Type, "Type cannot be null.");
			ReleaseAssert.IsNotNull(desc.Date, "Date cannot be null.");
	
            Type = desc.Type;
            Date = desc.Date;
            UserGrade = desc.UserGrade;
            TestGroup = desc.TestGroup;
            if (desc.Parameters != null)
			{
                Parameters = Mutability.ToImmutable(desc.Parameters);
			}
            Count = desc.Count;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public MetricsEvent(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Type"), "Json is missing required field 'Type'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Date"), "Json is missing required field 'Date'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Type
				if (entry.Key == "Type")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Type = (string)entry.Value;
				}
		
				// Date
				else if (entry.Key == "Date")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Date = JsonSerialisation.DeserialiseDate((string)entry.Value);
				}
		
				// User Grade
				else if (entry.Key == "UserGrade")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                        UserGrade = (int)(long)entry.Value;
                    }
				}
		
				// Test Group
				else if (entry.Key == "TestGroup")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                        TestGroup = (string)entry.Value;
                    }
				}
		
				// Parameters
				else if (entry.Key == "Params")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                        Parameters = JsonSerialisation.DeserialiseMap((IDictionary<string, object>)entry.Value, (object element) =>
                        {
                            ReleaseAssert.IsTrue(element is string, "Invalid element type.");
                            return (string)element;
                        });
                    }
				}
		
				// Count
				else if (entry.Key == "Count")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is long, "Invalid serialised type.");
                        Count = (int)(long)entry.Value;
                    }
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
			
			// Type
			dictionary.Add("Type", Type);
			
			// Date
            dictionary.Add("Date", JsonSerialisation.Serialise(Date));
			
			// User Grade
            if (UserGrade != null)
			{
				dictionary.Add("UserGrade", UserGrade);
            }
			
			// Test Group
            if (TestGroup != null)
			{
				dictionary.Add("TestGroup", TestGroup);
            }
			
			// Parameters
            if (Parameters != null)
			{
                var serialisedParameters = JsonSerialisation.Serialise(Parameters, (string element) =>
                {
                    return element;
                });
                dictionary.Add("Params", serialisedParameters);
            }
			
			// Count
            if (Count != null)
			{
				dictionary.Add("Count", Count);
            }
			
			return dictionary;
		}
	}
}
