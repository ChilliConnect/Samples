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
	/// <para>A mutable description of a CollectionDataObject.</para>
	///
	/// <para>This is not thread-safe and should typically only be used to create new 
	/// instances of CollectionDataObject.</para>
	/// </summary>
	public sealed class CollectionDataObjectDesc
	{
		/// <summary>
		/// The ID of the Object.
		/// </summary>
        public string ObjectId { get; set; }
	
		/// <summary>
		/// The Player that created the Object.
		/// </summary>
        public Player CreatedBy { get; set; }
	
		/// <summary>
		/// The Date the Object was created. Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime DateCreated { get; set; }
	
		/// <summary>
		/// The last Player that last modified the Object.
		/// </summary>
        public Player ModifiedBy { get; set; }
	
		/// <summary>
		/// The Date the Object was last updated. Format: ISO8601 e.g. 2016-01-12T11:08:23.
		/// </summary>
        public DateTime DateModified { get; set; }
	
		/// <summary>
		/// The Object's data.
		/// </summary>
        public MultiTypeValue Value { get; set; }
	
		/// <summary>
		/// The current value of the WriteLock. To enable conflict checking, the returned
		/// WriteLock can be provided to the update and delete calls on subsequent update
		/// attempts.
		/// </summary>
        public string WriteLock { get; set; }

		/// <summary>
		/// Initialises a new instance with the given required properties.
		/// </summary>
		///
		/// <param name="objectId">The ID of the Object.</param>
		/// <param name="createdBy">The Player that created the Object.</param>
		/// <param name="dateCreated">The Date the Object was created. Format: ISO8601 e.g. 2016-01-12T11:08:23.</param>
		/// <param name="value">The Object's data.</param>
		public CollectionDataObjectDesc(string objectId, Player createdBy, DateTime dateCreated, MultiTypeValue value)
		{
			ReleaseAssert.IsNotNull(objectId, "Object Id cannot be null.");
			ReleaseAssert.IsNotNull(createdBy, "Created By cannot be null.");
			ReleaseAssert.IsNotNull(dateCreated, "Date Created cannot be null.");
			ReleaseAssert.IsNotNull(value, "Value cannot be null.");
	
            ObjectId = objectId;
            CreatedBy = createdBy;
            DateCreated = dateCreated;
            Value = value;
		}
	}
}
