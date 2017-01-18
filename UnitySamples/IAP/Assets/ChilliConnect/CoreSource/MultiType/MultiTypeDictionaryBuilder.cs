//
//  Created by Ian Copland on 2016-01-28
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
using System.Collections.Generic;

namespace SdkCore
{
    /// <summary>
    /// <para>Provides the means to construct a new MultiTypeDictionary instance. 
    /// Heterogeneous ypes can be added to the dictionary description; the constructed
    /// MultiTypeDictionary will then contain these objects. As this is purely for 
    /// construction of a MultiTypeDictionary, other typical dictionary functionality is 
    /// omitted.</para>
    /// 
    /// <para>This is mutable and therefore not thread-safe. It should never be accessed
    /// from multiple threads at the same time.</para>
    /// </summary>
    public sealed class MultiTypeDictionaryBuilder
    {
        IDictionary<string, MultiTypeValue> m_dictionary = new Dictionary<string, MultiTypeValue>();

        /// <summary>
        /// Adds a new value to the dictionary description.
        /// </summary>
        /// 
        /// <param name="key">The key this value should be stored under.</param>
        /// <param name="value">The value which should be added.</param>
        public MultiTypeDictionaryBuilder Add(string key, MultiTypeValue value)
        {
            ReleaseAssert.IsFalse(m_dictionary.ContainsKey(key), "Dictionary already contains the key '" + key + "'");
            m_dictionary.Add(key, value);
            return this;
        }

        /// <summary>
        /// Adds a new value to the dictionary description.
        /// </summary>
        /// 
        /// <param name="key">The key this value should be stored under.</param>
        /// <param name="value">The value which should be added.</param>
        public MultiTypeDictionaryBuilder Add(string key, bool value)
        {
            return Add(key, new MultiTypeValue(value));
        }

        /// <summary>
        /// Adds a new value to the dictionary description.
        /// </summary>
        /// 
        /// <param name="key">The key this value should be stored under.</param>
        /// <param name="value">The value which should be added.</param>
        public MultiTypeDictionaryBuilder Add(string key, int value)
        {
            return Add(key, new MultiTypeValue(value));
        } 

        /// <summary>
        /// Adds a new value to the dictionary description.
        /// </summary>
        /// 
        /// <param name="key">The key this value should be stored under.</param>
        /// <param name="value">The value which should be added.</param>
        public MultiTypeDictionaryBuilder Add(string key, long value)
        {
            return Add(key, new MultiTypeValue(value));
        } 

        /// <summary>
        /// Adds a new value to the dictionary description.
        /// </summary>
        /// 
        /// <param name="key">The key this value should be stored under.</param>
        /// <param name="value">The value which should be added.</param>
        public MultiTypeDictionaryBuilder Add(string key, float value)
        {
            return Add(key, new MultiTypeValue(value));
        } 

        /// <summary>
        /// Adds a new value to the dictionary description.
        /// </summary>
        /// 
        /// <param name="key">The key this value should be stored under.</param>
        /// <param name="value">The value which should be added.</param>
        public MultiTypeDictionaryBuilder Add(string key, double value)
        {
            return Add(key, new MultiTypeValue(value));
        } 

        /// <summary>
        /// Adds a new value to the dictionary description.
        /// </summary>
        /// 
        /// <param name="key">The key this value should be stored under.</param>
        /// <param name="value">The value which should be added.</param>
        public MultiTypeDictionaryBuilder Add(string key, string value)
        {
            return Add(key, new MultiTypeValue(value));
        } 

        /// <summary>
        /// Adds a new value to the dictionary description.
        /// </summary>
        /// 
        /// <param name="key">The key this value should be stored under.</param>
        /// <param name="value">The value which should be added.</param>
        public MultiTypeDictionaryBuilder Add(string key, MultiTypeList value)
        {
            return Add(key, new MultiTypeValue(value));
        } 

        /// <summary>
        /// Adds a new value to the dictionary description.
        /// </summary>
        /// 
        /// <param name="key">The key this value should be stored under.</param>
        /// <param name="value">The value which should be added.</param>
        public MultiTypeDictionaryBuilder Add(string key, MultiTypeDictionary value)
        {
            return Add(key, new MultiTypeValue(value));
        } 

        /// <summary>
        /// Generates a new immutable instance of the described dictionary. 
        /// </summary>
        /// 
        /// <returns>The new MultiTypeDictionary.</returns>
        public MultiTypeDictionary Build()
        {
            return new MultiTypeDictionary(m_dictionary);
        } 
    }
}
