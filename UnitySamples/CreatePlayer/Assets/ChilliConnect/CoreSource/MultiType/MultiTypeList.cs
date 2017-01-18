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
    /// <para>An immutable list which can contain values of heterogeneous types.
    /// The list can be serialised to a json compliant IList.</para>
    /// 
    /// <para>Typically this is generated via a MultiTypeListBuilder; alternatively it can
    /// be generated from a json compliant IList.</para>
    /// 
    /// <para>This is thread-safe.</para>
    /// </summary>
    public sealed class MultiTypeList : IEnumerable<MultiTypeValue>
    {
        private readonly IList<MultiTypeValue> m_list;

        /// <summary>
        /// Gets the length of the list.
        /// </summary>
        public int Count 
        {
            get
            {
                return m_list.Count;
            }
        }

        /// <summary>
        /// Get the MultiTypeValue at the requested index in the list. An error will occur if the
        /// index is outside the bounds of the list.
        /// </summary>
        /// 
        /// <param name="index">The index.</param>
        public MultiTypeValue this[int index]
        {
            get
            {
                ReleaseAssert.IsTrue(index >= 0 && index < Count, "Index out of bounds!");
                return m_list[index];
            }
        }

        /// <summary>
        /// Initialises an empty MultiTypeList.
        /// </summary>
        public MultiTypeList()
        {
            m_list = new List<MultiTypeValue>();
        }

        /// <summary>
        /// Initialises a new MultiTypeList from the given list of MultiTypeValues. Typically
        /// this isn't called directly, instead this is generated via a MultiTypeListBuilder.
        /// </summary>
        /// 
        /// <param name="values">The values the MultiTypeList should contain.</param>
        public MultiTypeList(IList<MultiTypeValue> values)
        {
            m_list = new List<MultiTypeValue>(values);
        }

        /// <summary>
        /// Initialises a new MultiTypeList from the given Json Array. The list must contain
        /// only Json compliant types: bool, int, long, float, string, IList<object> or
        /// IDictionary<string, object>.
        /// </summary>
        /// 
        /// <param name="jsonArray">The Json Array.</param>
        public MultiTypeList(IList<object> jsonArray)
        {
            m_list = new List<MultiTypeValue>();
            foreach (var jsonValue in jsonArray)
            {
                m_list.Add(new MultiTypeValue(jsonValue));
            }
        }

        /// <summary>
        /// Returns whether or not this contains the given value.
        /// </summary>
        /// 
        /// <param name="value">The value to look up.</param>
        public bool Contains(MultiTypeValue value)
        {
            return m_list.Contains(value);
        }

        /// <summary>
        /// Returns whether or not this contains the given value.
        /// </summary>
        /// 
        /// <param name="value">The value to look up.</param>
        public bool Contains(bool value)
        {
            return Contains(new MultiTypeValue(value));
        }

        /// <summary>
        /// Returns whether or not this contains the given value.
        /// </summary>
        /// 
        /// <param name="value">The value to look up.</param>
        public bool Contains(int value)
        {
            return Contains(new MultiTypeValue(value));
        }

        /// <summary>
        /// Returns whether or not this contains the given value.
        /// </summary>
        /// 
        /// <param name="value">The value to look up.</param>
        public bool Contains(long value)
        {
            return Contains(new MultiTypeValue(value));
        }

        /// <summary>
        /// Returns whether or not this contains the given value.
        /// </summary>
        /// 
        /// <param name="value">The value to look up.</param>
        public bool Contains(float value)
        {
            return Contains(new MultiTypeValue(value));
        }

        /// <summary>
        /// Returns whether or not this contains the given value.
        /// </summary>
        /// 
        /// <param name="value">The value to look up.</param>
        public bool Contains(string value)
        {
            return Contains(new MultiTypeValue(value));
        }

        /// <summary>
        /// Returns whether or not this contains the given value.
        /// </summary>
        /// 
        /// <param name="value">The value to look up.</param>
        public bool Contains(MultiTypeList value)
        {
            return Contains(new MultiTypeValue(value));
        }

        /// <summary>
        /// Returns whether or not this contains the given value.
        /// </summary>
        /// 
        /// <param name="value">The value to look up.</param>
        public bool Contains(MultiTypeDictionary value)
        {
            return Contains(new MultiTypeValue(value));
        }

        /// <summary>
        /// Returns the bool value at the given index. Numeric values will be converted
        /// to a bool representation: 0 is false, anything else is true. If the value
        /// is not a number or the index is out of bounds an error will occur.
        /// </summary>
        /// 
        /// <param name="index">The index</param>
        public bool GetBool(int index)
        {
            var multiTypeValue = this[index];
            ReleaseAssert.IsNotNull(multiTypeValue, "Value is null.");
            return multiTypeValue.AsBool();
        }

        /// <summary>
        /// Returns the int value at the given index. Numeric values will be converted
        /// to an int representation. Note that this may be lossy and result in 
        /// truncation. If the value is not a number or the index is out of bounds an 
        /// error will occur.
        /// </summary>
        /// 
        /// <param name="index">The index</param>
        public int GetInt(int index)
        {
            var multiTypeValue = this[index];
            ReleaseAssert.IsNotNull(multiTypeValue, "Value is null.");
            return multiTypeValue.AsInt();
        }

        /// <summary>
        /// Returns the long value at the given index. Numeric values will be converted
        /// to a long representation. Note that this may be lossy and result in 
        /// truncation. If the value is not a number or the index is out of bounds an 
        /// error will occur.
        /// </summary>
        /// 
        /// <param name="index">The index</param>
        public long GetLong(int index)
        {
            var multiTypeValue = this[index];
            ReleaseAssert.IsNotNull(multiTypeValue, "Value is null.");
            return multiTypeValue.AsLong();
        }

        /// <summary>
        /// Returns the float value at the given index. Numeric values will be converted
        /// to a float representation. Note that this may be lossy and result in 
        /// truncation. If the value is not a number or the index is out of bounds an 
        /// error will occur.
        /// </summary>
        /// 
        /// <param name="index">The index</param>
        public float GetFloat(int index)
        {
            var multiTypeValue = this[index];
            ReleaseAssert.IsNotNull(multiTypeValue, "Value is null.");
            return multiTypeValue.AsFloat();
        }

        /// <summary>
        /// Returns the double value at the given index. Numeric values will be converted
        /// to a double representation. Note that this may be lossy and result in 
        /// truncation. If the value is not a number or the index is out of bounds an 
        /// error will occur.
        /// </summary>
        /// 
        /// <param name="index">The index</param>
        public double GetDouble(int index)
        {
            var multiTypeValue = this[index];
            ReleaseAssert.IsNotNull(multiTypeValue, "Value is null.");
            return multiTypeValue.AsDouble();
        }

        /// <summary>
        /// Returns the string value at the given index. If the value at the index
        /// is not a string type, or the index is out of bounds, an error will occur.
        /// </summary>
        /// 
        /// <param name="index">The index</param>
        public string GetString(int index)
        {
            var multiTypeValue = this[index];
            ReleaseAssert.IsNotNull(multiTypeValue, "Value is null.");
            return multiTypeValue.AsString();
        }

        /// <summary>
        /// Returns the MultiTypeList at the given index. If the value at the index
        /// is not a MultiTypeList, or the index is out of bounds, an error will occur.
        /// </summary>
        /// 
        /// <param name="index">The index</param>
        public MultiTypeList GetList(int index)
        {
            var multiTypeValue = this[index];
            ReleaseAssert.IsNotNull(multiTypeValue, "Value is null.");
            return multiTypeValue.AsList();
        }

        /// <summary>
        /// Returns the MultiTypeDictionary at the given index. If the value at the index
        /// is not a MultiTypeDictionary, or the index is out of bounds, an error will occur.
        /// </summary>
        /// 
        /// <param name="index">The index</param>
        public MultiTypeDictionary GetDictionary(int index)
        {
            var multiTypeValue = this[index];
            ReleaseAssert.IsNotNull(multiTypeValue, "Value is null.");
            return multiTypeValue.AsDictionary();
        }

        /// <summary>
        /// Returns an enumerator for iterating over the MultiTypeValue elements.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<MultiTypeValue> GetEnumerator()
        {
            return m_list.GetEnumerator();
        }

        /// <summary>
        /// Serialises the list this describes to a json compliant IList<object>. List values will 
        /// also be of json compliant types: bool, int, long, float, string,  IList<object> or 
        /// IDictionary<string, object>. 
        /// </summary>
        /// 
        /// <returns>The serialised json compliant list.</returns>
        public IList<object> Serialise()
        {
            IList<object> output = new List<object>();

            foreach (var value in m_list)
            {
                output.Add(value.Serialise());
            }

            return output;
        }

        /// <summary>
        /// Evaluates whether or not this and the given MultiTypeList are equal in value.
        /// </summary>
        /// 
        /// <param name="value">The value to compare.</c>
        /// 
        /// <returns>Whether or not they are equal.</returns>
        public bool Equals(MultiTypeList value)
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

            for (int i = 0; i < Count; ++i)
            {
                if (this[i] != value[i])
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
            
            var multiTypeList = value as MultiTypeList;
            if (multiTypeList == null)
            {
                return false;
            }
            
            return Equals(multiTypeList);
        }
        
        /// <summary>
        /// Returns the hash code of the underlying list.
        /// </summary>
        /// 
        /// <returns>The hash code of the underlying list.</returns>
        public override int GetHashCode()
        {
            int hashCode = HashCode.InitialValue;

            foreach (var value in this)
            {
                hashCode = HashCode.Add(hashCode, value);
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
        public static bool operator ==(MultiTypeList a, MultiTypeList b)
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
        public static bool operator !=(MultiTypeList a, MultiTypeList b)
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
