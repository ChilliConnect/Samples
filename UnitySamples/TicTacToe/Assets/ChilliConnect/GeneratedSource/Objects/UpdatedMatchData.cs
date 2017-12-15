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
	/// <para>A container used to carry the updated Match Data.</para>
	///
	/// <para>This is immutable after construction and is therefore thread safe.</para>
	/// </summary>
	public sealed class UpdatedMatchData
	{
		/// <summary>
		/// StateData to replace the current value.
		/// </summary>
        public MultiTypeValue StateData { get; private set; }
	
		/// <summary>
		/// True if the Match's State should be updated to COMPLETED. Matches can only be
		/// completed if in the IN_PROGRESS State. Default false.
		/// </summary>
        public bool? Completed { get; private set; }
	
		/// <summary>
		/// Data that describes the outcome of the Match. Can only be set when Completed is
		/// set to true.
		/// </summary>
        public MultiTypeValue OutcomeData { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given description.
		/// </summary>
		///
		/// <param name="desc">The description.</param>
		public UpdatedMatchData(UpdatedMatchDataDesc desc)
		{
			ReleaseAssert.IsNotNull(desc, "A description object cannot be null.");
			
	
            StateData = desc.StateData;
            Completed = desc.Completed;
            OutcomeData = desc.OutcomeData;
		}
		
		/// <summary>
		/// Initialises a new instance from the given Json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the Json data.</param>
		public UpdatedMatchData(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// State Data
				if (entry.Key == "StateData")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is object, "Invalid serialised type.");
                        StateData = new MultiTypeValue((object)entry.Value);	
                    }
				}
		
				// Completed
				else if (entry.Key == "Completed")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is bool, "Invalid serialised type.");
                        Completed = (bool)entry.Value;
                    }
				}
		
				// Outcome Data
				else if (entry.Key == "OutcomeData")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is object, "Invalid serialised type.");
                        OutcomeData = new MultiTypeValue((object)entry.Value);	
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
			
			// State Data
            if (StateData != null)
			{
                dictionary.Add("StateData", StateData.Serialise());
            }
			
			// Completed
            if (Completed != null)
			{
				dictionary.Add("Completed", Completed);
            }
			
			// Outcome Data
            if (OutcomeData != null)
			{
                dictionary.Add("OutcomeData", OutcomeData.Serialise());
            }
			
			return dictionary;
		}
	}
}
