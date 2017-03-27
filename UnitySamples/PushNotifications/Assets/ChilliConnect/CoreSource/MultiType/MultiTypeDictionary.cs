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
using System.Collections.ObjectModel;

namespace SdkCore
{
    /// <summary>
    /// <para>An immutable dictionary which can contain values of heterogeneous types.
    /// The key is always a string. The dictionary can be serialised to a json 
    /// compliant IDictionary<string, object>.</para>
    /// 
    /// <para>Typically this is generated via a MultiTypeDictionaryBuilder; alternatively it 
    /// can be generated from a json compliant IDictionary<string, object>.</para>
    /// 
    /// <para>This is thread-safe.</para>
    /// </summary>
    public sealed class MultiTypeDictionary : IEnumerable<KeyValuePair<string, MultiTypeValue>>
    {
        IDictionary<string, MultiTypeValue> m_dictionary;

        /// <summary>
        /// Gets the length of the dictionary.
        /// </summary>
        public int Count
        {
            get
            {
                return m_dictionary.Count;
            }
        }
        
        /// <summary>
        /// Get the MultiTypeValue for the given key. If the key doesn't an error will occur.
        /// </summary>
        /// 
        /// <param name="key">The key.</param>
        public MultiTypeValue this[string key]
        {
            get 
            {
                ReleaseAssert.IsTrue(m_dictionary.ContainsKey(key), "Dictionary does not contain key '" + key + "'.");
                return m_dictionary[key];
            }
        }

        /// <summary>
        /// Initialises an empty MultiTypeDictionary.
        /// </summary>
        public MultiTypeDictionary()
        {
            m_dictionary = new Dictionary<string, MultiTypeValue>();
        }
        
        /// <summary>
        /// Initialises a new MultiTypeDictionary from the given IDictionary of MultiTypeValues. 
        /// Typically this isn't called directly, instead this is generated via a 
        /// MultiTypeDictionaryBuilder.
        /// </summary>
        /// 
        /// <param name="values">The values the MultiTypeList should contain.</param>
        public MultiTypeDictionary(IDictionary<string, MultiTypeValue> values)
        {
            m_dictionary = new Dictionary<string, MultiTypeValue>(values);
        }
        
        /// <summary>
        /// Initialises a new MultiTypeDictionary from the given Json Object. This must contain
        /// only Json compliant types: bool, int, long, float, string, IList<object> or
        /// IDictionary<string, object>.
        /// </summary>
        /// 
        /// <param name="jsonObject">The Json Object.</param>
        public MultiTypeDictionary(IDictionary<string, object> jsonObject)
        {
            m_dictionary = new Dictionary<string, MultiTypeValue>();

            foreach (var entry in jsonObject)
            {
                m_dictionary.Add(entry.Key, new MultiTypeValue(entry.Value));
            }
        }
        
        /// <summary>
        /// Returns whether or not this contains the given key.
        /// </summary>
        /// 
        /// <param name="key">The key to look up.</param>
        public bool ContainsKey(string key)
        {
            return m_dictionary.ContainsKey(key);
        }
        
        /// <summary>
        /// Returns the bool value for the given key. Numeric values will be converted
        /// to a bool representation: 0 is false, anything else is true. If the value
        /// is not a number or the key doesn't exist an error will occur.
        /// </summary>
        /// 
        /// <param name="key">The key.</param>
        public bool GetBool(string key)
        {
            var multiTypeValue = this[key];
            return multiTypeValue.AsBool();
        }
        
        /// <summary>
        /// Returns the int value for the given key. Numeric values will be converted
        /// to an int representation. Note that this may be lossy and result in 
        /// truncation. If the value is not a number or the key doesn't exist an error 
        /// will occur.
        /// </summary>
        /// 
        /// <param name="key">The key.</param>
        public int GetInt(string key)
        {
            var multiTypeValue = this[key];
            return multiTypeValue.AsInt();
        }
        
        /// <summary>
        /// Returns the long value for the given key. Numeric values will be converted
        /// to a long representation. Note that this may be lossy and result in 
        /// truncation. If the value is not a number or the key doesn't exist an error 
        /// will occur.
        /// </summary>
        /// 
        /// <param name="key">The key.</param>
        public long GetLong(string key)
        {
            var multiTypeValue = this[key];
            return multiTypeValue.AsLong();
        }
        
        /// <summary>
        /// Returns the float value for the given key. Numeric values will be converted
        /// to a float representation. Note that this may be lossy and result in 
        /// truncation. If the value is not a number or the key doesn't exist an error 
        /// will occur.
        /// </summary>
        /// 
        /// <param name="key">The key.</param>
        public float GetFloat(string key)
        {
            var multiTypeValue = this[key];
            return multiTypeValue.AsFloat();
        }

        /// <summary>
        /// Returns the double value for the given key. Numeric values will be converted
        /// to a double representation. Note that this may be lossy and result in 
        /// truncation. If the value is not a number or the key doesn't exist an error 
        /// will occur.
        /// </summary>
        /// 
        /// <param name="key">The key.</param>
        public double GetDouble(string key)
        {
            var multiTypeValue = this[key];
            return multiTypeValue.AsDouble();
        }
        
        /// <summary>
        /// Returns the string value for the given key. If the key does not exist or the value
        /// is not of the correct type, then an error will occur.
        /// </summary>
        /// 
        /// <param name="key">The key.</param>
        public string GetString(string key)
        {
            var multiTypeValue = this[key];
            return multiTypeValue.AsString();
        }
        
        /// <summary>
        /// Returns the MultiTypeList for the given key. If the key does not exist or the value
        /// is not of the correct type, then an error will occur.
        /// </summary>
        /// 
        /// <param name="key">The key.</param>
        public MultiTypeList GetList(string key)
        {
            var multiTypeValue = this[key];
            return multiTypeValue.AsList();
        }
        
        /// <summary>
        /// Returns the MultiTypeDictionary for the given key. If the key does not exist or the value
        /// is not of the correct type, then an error will occur.
        /// </summary>
        /// 
        /// <param name="key">The key.</param>
        public MultiTypeDictionary GetDictionary(string key)
        {
            var multiTypeValue = this[key];
            return multiTypeValue.AsDictionary();
        }
        
        /// <summary>
        /// Returns an enumerator for iterating over the Key-Value pairs in the Dictionary.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<KeyValuePair<string, MultiTypeValue>> GetEnumerator()
        {
            return m_dictionary.GetEnumerator();
        }
        
        /// <summary>
        /// Serialises the dictionary this describes to a json compliant IDictionary<string, object>. 
        /// Dictionary values will also be of json compliant types: bool, int, long, float, string, 
        /// IList<object> or IDictionary<string, object>. 
        /// </summary>
        /// 
        /// <returns>The serialised json compliant list.</returns>
        public IDictionary<string, object> Serialise()
        {
            IDictionary<string, object> output = new Dictionary<string, object>();

            foreach (var entry in m_dictionary)
            {
                output.Add(entry.Key, entry.Value.Serialise());
            }

            return output;
        }
        
        /// <summary>
        /// Evaluates whether or not this and the given MultiTypeDictionary are equal in value.
        /// </summary>
        /// 
        /// <param name="value">The value to compare.</c>
        /// 
        /// <returns>Whether or not they are equal.</returns>
        public bool Equals(MultiTypeDictionary value)
        {
            if ((object)value == null)
            {
                return false;
            }
            
            if (object.ReferenceEquals(this, value))
            {
                return true;
            }
            
            if (Count != value.Count)
            {
                return false;
            }
            
            foreach (var entry in this)
            {
                if (!value.ContainsKey(entry.Key))
                {
                    return false;
                }

                if (value[entry.Key] != entry.Value)
                {
                    return false;
                }
            }
            
            return true;
        }
        
        /// <summary>
        /// Evaluates whether or not this and the given object are equal in value. 
        /// </summary>
        /// 
        /// <param name="value">The value to compare.</c>
        /// 
        /// <returns>Whether or not they are equal.</returns>
        public override bool Equals(object value)
        {
            if ((object)value == null)
            {
                return false;
            }
            
            if (Object.ReferenceEquals(this, value))
            {
                return true;
            }
            
            var multiTypeDictionary = value as MultiTypeDictionary;
            if (multiTypeDictionary == null)
            {
                return false;
            }
            
            return Equals(multiTypeDictionary);
        }        

        /// <summary>
        /// Returns the hash code of the underlying dictionary.
        /// </summary>
        /// 
        /// <returns>The hash code of the underlying dictionary.</returns>
        public override int GetHashCode()
        {
            int hashCode = HashCode.InitialValue;
            
            foreach (var entry in this)
            {
                hashCode = HashCode.Add(hashCode, entry.Key);
                hashCode = HashCode.Add(hashCode, entry.Value);
            }
            
            return hashCode;
        }

        /// <summary>
        /// Overloaded equality operator.
        /// </summary>
        /// 
        /// <param name="a">The first value.</c>
        /// <param name="b">The second value.</c>
        /// 
        /// <returns>Whether or not the two values are equal.</returns>
        public static bool operator ==(MultiTypeDictionary a, MultiTypeDictionary b)
        {
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }
            
            if ((object)a == null)
            {
                return false;
            }
            
            return a.Equals(b);
        }
        
        /// <summary>
        /// Overloaded inequality operator.
        /// </summary>
        /// 
        /// <param name="a">The first value.</c>
        /// <param name="b">The second value.</c>
        /// 
        /// <returns>Whether or not the two values are inequal.</returns>
        public static bool operator !=(MultiTypeDictionary a, MultiTypeDictionary b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Returns an enumerator for iterating over the dictionary. This is not exposed
        /// through this interface.
        /// </summary>
        /// 
        /// <returns>The enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
