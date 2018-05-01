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
	/// <para>A mutable description of a MatchTypeDefinition.</para>
	///
	/// <para>This is not thread-safe and should typically only be used to create new 
	/// instances of MatchTypeDefinition.</para>
	/// </summary>
	public sealed class MatchTypeDefinitionDesc
	{
		/// <summary>
		/// The key of the MatchType.
		/// </summary>
        public string Key { get; set; }
	
		/// <summary>
		/// The name of the MatchType.
		/// </summary>
        public string Name { get; set; }
	
		/// <summary>
		/// The allowed TurnTypes.
		/// </summary>
        public IList<string> TurnTypes { get; set; }
	
		/// <summary>
		/// The MatchType's property definitions.
		/// </summary>
        public IList<MatchTypePropertyDefinition> Properties { get; set; }
	
		/// <summary>
		/// Amount of time a Match in WAITING State will wait before entering State TIMEOUT.
		/// </summary>
        public int DefaultWaitingTimeout { get; set; }
	
		/// <summary>
		/// Amount of time before the Match will enter State TIMEOUT.
		/// </summary>
        public int DefaultTurnTimeout { get; set; }
	
		/// <summary>
		/// The custom data of the MatchType.
		/// </summary>
        public MultiTypeValue CustomData { get; set; }

		/// <summary>
		/// Initialises a new instance with the given required properties.
		/// </summary>
		///
		/// <param name="key">The key of the MatchType.</param>
		/// <param name="name">The name of the MatchType.</param>
		/// <param name="turnTypes">The allowed TurnTypes.</param>
		/// <param name="defaultWaitingTimeout">Amount of time a Match in WAITING State will wait before entering State TIMEOUT.</param>
		/// <param name="defaultTurnTimeout">Amount of time before the Match will enter State TIMEOUT.</param>
		public MatchTypeDefinitionDesc(string key, string name, IList<string> turnTypes, int defaultWaitingTimeout, int defaultTurnTimeout)
		{
			ReleaseAssert.IsNotNull(key, "Key cannot be null.");
			ReleaseAssert.IsNotNull(name, "Name cannot be null.");
			ReleaseAssert.IsNotNull(turnTypes, "Turn Types cannot be null.");
	
            Key = key;
            Name = name;
            TurnTypes = Mutability.ToImmutable(turnTypes);
            DefaultWaitingTimeout = defaultWaitingTimeout;
            DefaultTurnTimeout = defaultTurnTimeout;
		}
	}
}
