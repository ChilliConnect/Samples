//
//  Created by Ian Copland on 2016-02-01
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
using System.Collections;

namespace SdkCore
{
    /// <summary>
    /// <para>A utilities class for calculating object hash codes returned from 
    /// GetHashCode(). To generate a hash for each member of a class the previous
    /// hash needs to be passed into the next. For the initial member InitialValue 
    /// should be passed in as the previous hash.</para>
    /// 
    /// <para>This is thread-safe.</para>
    /// </summary>
    public static class HashCode
    {
        public const int InitialValue = 17;

        /// <summary>
        /// Adds a value to the hash.
        /// </summary>
        /// 
        /// <param name="currentHash">The current hash value before adding the new value.</param>
        /// <param name="value">The new value to add to thr hash.</param>
        /// 
        /// <returns>The new hash code.</returns>
        public static int Add<TType>(int currentHash, TType value) 
        {
            const int ArbitraryPrimeNumber = 23;

            //We want to allow the output value to wrap so it is placed in an unchecked block.
            unchecked
            {
                return currentHash * ArbitraryPrimeNumber + value.GetHashCode();
            }
        }
    }
}