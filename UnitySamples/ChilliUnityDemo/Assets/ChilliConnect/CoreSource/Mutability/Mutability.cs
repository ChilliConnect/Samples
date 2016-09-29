//
//  Created by Ian Copland on 2016-01-26
//
//  The MIT License (MIT)
//
//  Copyright (c) 2016 Tag Games Limited
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

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SdkCore
{
    /// <summary>
    /// <para>A convenience class which provides methods for converting between
    /// mutable and immutable variations of data types.</para>
    /// 
    /// <para>This is thread-safe.</para>
    /// </summary>
    public static class Mutability
    {
        /// <summary>
        /// Converts the given mutable list to an immutable equivalent. The original list
        /// will be copied and wrapped in a read-only view of the data. This will not create
        /// a deep copy of the list, so the element type should also be immutable. 
        /// </summary>
        /// 
        /// <param name="list">The mutable list that should be converted.</param>
        /// 
        /// <returns>An immutable copy of the given list.</returns>
        /// 
        /// <typeparam name="TType">The type of list element.</typeparam>
        public static ReadOnlyCollection<TElementType> ToImmutable<TElementType>(IList<TElementType> list)
        {
            return new ReadOnlyCollection<TElementType>(new List<TElementType>(list));
        }

        /// <summary>
        /// Converts the given mutable dictionary to an immutable equivalent. The original 
        /// dictionary will be copied and wrapped in a read-only view of the data. This will 
        /// not create a deep copy of the dictionary, so the value type should also be 
        /// immutable.
        /// </summary>
        /// 
        /// <param name="list">The mutable dictionary that should be converted.</param>
        /// 
        /// <returns>An immutable copy of the given dictionary.</returns>
        /// 
        /// <typeparam name="TType">The value type.</typeparam>
        public static ReadOnlyDictionary<string, TElementType> ToImmutable<TElementType>(IDictionary<string, TElementType> dictionary)
        {
            return new ReadOnlyDictionary<string, TElementType>(new Dictionary<string, TElementType>(dictionary));
        }
    }
}