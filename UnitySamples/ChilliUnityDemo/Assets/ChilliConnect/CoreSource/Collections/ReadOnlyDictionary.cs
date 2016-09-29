//
//  Created by Ian Copland on 2015-11-25
//
//  The MIT License (MIT)
//
//  Copyright (c) 2015 Tag Games Limited
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
using System.Collections.ObjectModel;
using System;

namespace SdkCore
{
    /// <summary>
    /// Provides the means to expose a read-only view of a dictionary. The
    /// underlying dictionary may still change but it cannot be changed through
    /// this API. This is the equivalent to ReadOnlyCollection for a Dictionary
    /// and is directly comparable to ReadOnlyDictionary available in later
    /// versions of .NET.
    /// </summary>
    public sealed class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private const string ReadOnlyErrorMessage = "A ReadOnlyDictionary cannot be modified.";
        private readonly IDictionary<TKey, TValue> m_dictionary;

        /// <summary>
        /// Gets whether or not the dictionary is read only. For a ReadOnlyDictionary
        /// this will always return true.
        /// </summary>
        public bool IsReadOnly { get; private set; } 

        /// <summary>
        /// Gets the number of key-value pairs in the dictionary.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets a read only collection containing all of the keys in the dictionary.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                return m_dictionary.Keys;
            }
        }

        /// <summary>
        /// Gets a read only collection containing all of the values in the dictionary.
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                return m_dictionary.Values;
            }
        }

        /// <summary>
        /// Gets the value for the given key from the dictionary.
        /// </summary>
        public TValue this [TKey key]
        {
            get
            {
                return m_dictionary[key];
            }
        }

        /// <summary>
        /// Creates a new, empty readonly dictionary. As this has no backing 
        /// dictionary, this is completely immutable.
        /// </summary>
        public ReadOnlyDictionary()
        {
            IsReadOnly = true;
            m_dictionary = new Dictionary<TKey, TValue>();
            Count = m_dictionary.Count;
        }

        /// <summary>
        /// Initialise the readonly dictionary using the given dictionary as the
        /// backing dictionary.
        /// </summary>
        /// 
        /// <param name="dictionary">The directionary this should provide a readonly
        /// view into.</param>
        public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
        {
            IsReadOnly = true;
            m_dictionary = dictionary;
            Count = m_dictionary.Count;
        }

        /// <summary>
        /// Evaluates whether the dictionary contains the given key.
        /// </summary>
        /// 
        /// <returns>Whether or not the dictionary contains the key.</returns>
        /// 
        /// <param name="key">The key to check for.</param>
        public bool ContainsKey(TKey key)
        {
            return m_dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Evaluates whether the dictionary contains the given key-value pair.
        /// </summary>
        /// 
        /// <returns>Whether or not the dictionary contains the key-value pair.</returns>
        /// 
        /// <param name="keyValuePair">The key-value pair to check for</param>
        public bool Contains(KeyValuePair<TKey, TValue> keyValuePair)
        {
            return m_dictionary.Contains(keyValuePair);
        }

        /// <summary>
        /// Attempts to get the value for the given key from the dictionary. If the key
        /// exists, true will be returned and the out value will be set for the value for
        /// the key. Otherwise false will be returned.
        /// </summary>
        /// 
        /// <returns>Whether or not the requested key exists.</returns>
        /// 
        /// <param name="key">The key to look for.</param>
        /// <param name="value">The output value.</param>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return m_dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Copies the key value pairs in this dictionary to the given array, starting at
        /// the given index.
        /// </summary>
        /// 
        /// <param name="array">The target array.</param>
        /// <param name="index">The index into the array to start the copy.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            m_dictionary.CopyTo(array, index);
        }

        /// <summary>
        /// Creates a new read/write copy of this dictionary. Note that this will only
        /// take a shallow copy.
        /// </summary>
        /// 
        /// <returns>The read/write copy of this dictionary.</returns>
        public Dictionary<TKey, TValue> ToDictionary()
        {
            return new Dictionary<TKey, TValue>(m_dictionary);
        }

        /// <summary>
        /// Returns the enumerator. 
        /// </summary>
        /// 
        /// <returns>The enumerator.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return m_dictionary.GetEnumerator();
        }

        /// <summary>
        /// <para>Gets the value of the requested key if it exists. If attempting to 
        /// set the value, InvalidOperationException is thrown.</para>
        /// 
        /// <para>This is not exposed though the ReadOnlyDictionary API.</para>
        /// </summary>
        /// 
        /// <param name="key">The key to look up.</param>
        TValue IDictionary<TKey, TValue>.this [TKey key]
        {
            get
            {
                return m_dictionary [key];
            }
            set
            {
                throw new InvalidOperationException(ReadOnlyErrorMessage);
            }
        }

        /// <summary>
        /// <para>Throws an InvalidOperationException if attempting to Add to the 
        /// dictionary.</para>
        /// 
        /// <para>This is not exposed though the ReadOnlyDictionary API.</para>
        /// </summary>
        /// 
        /// <param name="key">The key to add.</param>
        /// <param name="value">The value to add.</param>
        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            throw new InvalidOperationException(ReadOnlyErrorMessage);
        }

        /// <summary>
        /// <para>Throws an InvalidOperationException if attempting to Add to the 
        /// dictionary.</para>
        /// 
        /// <para>This is not exposed though the ReadOnlyDictionary API.</para>
        /// </summary>
        /// 
        /// <param name="key">The key-value pair to add.</param>
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            throw new InvalidOperationException(ReadOnlyErrorMessage);
        }

        /// <summary>
        /// <para>Throws an InvalidOperationException if trying to remove from the 
        /// dictionary.</para>
        /// 
        /// <para>This is not exposed though the ReadOnlyDictionary API.</para>
        /// </summary>
        /// 
        /// <param name="key">The key to remove.</param>
        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            throw new InvalidOperationException(ReadOnlyErrorMessage);
        }

        /// <summary>
        /// <para>Throws an InvalidOperationException if trying to remove from the 
        /// dictionary.</para>
        /// 
        /// <para>This is not exposed though the ReadOnlyDictionary API.</para>
        /// </summary>
        /// 
        /// <param name="key">The key-value pair to remove.</param>
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
        {
            throw new InvalidOperationException(ReadOnlyErrorMessage);
        }

        /// <summary>
        /// <para>Throws an InvalidOperationException if trying to clear the dictionary.
        /// </para>
        /// 
        /// <para>This is not exposed though the ReadOnlyDictionary API.</para>
        /// </summary>
        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            throw new InvalidOperationException(ReadOnlyErrorMessage);
        }

        /// <summary>
        /// <para>Gets the enumerator.</para>
        /// 
        /// <para>This is not exposed though the ReadOnlyDictionary API.</para>
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
