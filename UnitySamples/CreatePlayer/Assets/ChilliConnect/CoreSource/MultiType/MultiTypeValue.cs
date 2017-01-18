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
using System;

namespace SdkCore
{
    /// <summary>
    /// <para>A container for a single value which can be one of several different
    /// types. This is required to allow some basic storage of heterogeneous types,
    /// and generic Json serialisation of those types.</para>
    /// 
    /// <para>The the different types that can be represented are: Null, Number, String,
    /// List and Dictionary. A number can contain bool, int, long, float or double. These
    /// number types are interchangable, i.e AsFloat() will work on a MultiTypeValue which 
    /// represents a long. Note that these conversions are lossy, for example conversion
    /// from float to int or from long to int may result in truncation.
    /// </para>
    /// 
    /// <para>This is immutable and therefore thread-safe.</para>
    /// </summary>
    public sealed class MultiTypeValue
    {
        private readonly Type m_type;
        private readonly object m_value;

        /// <summary>
        /// A constant which represents null.
        /// </summary>
        public static readonly MultiTypeValue Null = new MultiTypeValue();

        /// <summary>
        /// An enum describing the various possible types that can be contained in a
        /// MultiTypeValue.
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// The contained value is of type null.
            /// </summary>
            Null,

            /// <summary>
            /// The contained value is a number. This can be of type bool, Int, Long
            /// Float or Double. These types are interchangable and AsBool(), AsInt(), 
            /// AsLong(), AsFloat(), or AsDouble() will work for any MultiTypeValue of 
            /// type Number.
            /// </summary>
            Number,

            /// <summary>
            /// The contained value is of type String.
            /// </summary>
            String,

            /// <summary>
            /// The contained value is of type MultiTypeList.
            /// </summary>
            List,

            /// <summary>
            /// The contained value is of type MultiTypeDictionary.
            /// </summary>
            Dictionary
        }

        /// <summary>
        /// Creates a new MultiTypeValue instance containing the given value.
        /// </summary>
        /// 
        /// <param name="value">The value that this should contain.</param>
        public MultiTypeValue(bool value)
        {
            m_type = Type.Number;
            m_value = value;
        }

        /// <summary>
        /// Creates a new MultiTypeValue instance containing the given value.
        /// </summary>
        /// 
        /// <param name="value">The value that this should contain.</param>
        public MultiTypeValue(int value)
        {
            m_type = Type.Number;
            m_value = value;
        }

        /// <summary>
        /// Creates a new MultiTypeValue instance containing the given value.
        /// </summary>
        /// 
        /// <param name="value">The value that this should contain.</param>
        public MultiTypeValue(long value)
        {
            m_type = Type.Number;
            m_value = value;
        }

        /// <summary>
        /// Creates a new MultiTypeValue instance containing the given value.
        /// </summary>
        /// 
        /// <param name="value">The value that this should contain.</param>
        public MultiTypeValue(float value)
        {
            m_type = Type.Number;
            m_value = value;
        }

        /// <summary>
        /// Creates a new MultiTypeValue instance containing the given value.
        /// </summary>
        /// 
        /// <param name="value">The value that this should contain.</param>
        public MultiTypeValue(double value)
        {
            m_type = Type.Number;
            m_value = value;
        }

        /// <summary>
        /// Creates a new MultiTypeValue instance containing the given value.
        /// </summary>
        /// 
        /// <param name="value">The value that this should contain.</param>
        public MultiTypeValue(string value)
        {
            m_type = Type.String;
            m_value = value;
        }

        /// <summary>
        /// Creates a new MultiTypeValue instance containing the given value.
        /// </summary>
        /// 
        /// <param name="value">The value that this should contain.</param>
        public MultiTypeValue(MultiTypeList value)
        {
            m_type = Type.List;
            m_value = value;
        }

        /// <summary>
        /// Creates a new MultiTypeValue instance containing the given value.
        /// </summary>
        /// 
        /// <param name="value">The value that this should contain.</param>
        public MultiTypeValue(MultiTypeDictionary value)
        {
            m_type = Type.Dictionary;
            m_value = value;
        }

        /// <summary>
        /// Creates a new MultiTypeValue instance from the given json value. This must
        /// be a valid Json type: bool, int, long, float, string, IList<object> or 
        /// IDictionary<string, object>.
        /// </summary>
        public MultiTypeValue(object jsonValue)
        {
            if (jsonValue == null)
            {
                m_type = Type.Null;
            }
            else if (jsonValue is bool || jsonValue is int || jsonValue is long || jsonValue is float || jsonValue is double)
            {
                m_type = Type.Number;
                m_value = jsonValue;
            } 
            else if (jsonValue is string)
            {
                m_type = Type.String;
                m_value = jsonValue;
            } 
            else if (jsonValue is IList<object>)
            {
                m_type = Type.List;
                m_value = new MultiTypeList((IList<object>)jsonValue);
            } 
            else if (jsonValue is IDictionary<string, object>)
            {
                m_type = Type.Dictionary;
                m_value = new MultiTypeDictionary((IDictionary<string, object>)jsonValue);
            } 
            else
            {
                throw new ArgumentException("Supplied parameter is not of a valid Json type.");
            }
        }
       
        /// <summary>
        /// Returns the type this describes.
        /// </summary>
        /// 
        /// <returns>The type.</returns>
        public Type GetContainedType()
        {
            return m_type;
        }

        /// <summary>
        /// Evaluates whether or not this describes null.
        /// </summary>
        /// 
        /// <returns>Whether or not this describes null.</returns>
        public bool IsNull()
        {
            return (m_type == Type.Null);
        }

        /// <summary>
        /// Evaluates whether or not this describes a number value. A number may contain
        /// a bool, int, long, float or double.
        /// </summary>
        /// 
        /// <returns>Whether or not this describes a number value.</returns>
        public bool IsNumber()
        {
            return (m_type == Type.Number);
        }

        /// <summary>
        /// Evaluates whether or not this describes a string value.
        /// </summary>
        /// 
        /// <returns>Whether or not this describes a string value.</returns>
        public bool IsString()
        {
            return (m_type == Type.String);
        }

        /// <summary>
        /// Evaluates whether or not this describes a list value.
        /// </summary>
        /// 
        /// <returns>Whether or not this describes a list value.</returns>
        public bool IsList()
        {
            return (m_type == Type.List);
        }

        /// <summary>
        /// Evaluates whether or not this describes a dictionary value.
        /// </summary>
        /// 
        /// <returns>Whether or not this describes a dictionary value.</returns>
        public bool IsDictionary()
        {
            return (m_type == Type.Dictionary);
        }

        /// <summary>
        /// Returns the value this describes as a bool. Numeric values will be converted
        /// to a boolean representation: 0 is false, anything else is true. If the value
        /// is not a number an error will occur.
        /// </summary>
        /// 
        /// <returns>The value.</returns>
        public bool AsBool()
        {
            ReleaseAssert.IsTrue(IsNumber(), "Value is not a number.");

            if (m_value is bool)
            {
                return (bool)m_value;
            } 
            else if (m_value is int)
            {
                return (int)m_value == 0 ? false : true;
            } 
            else if (m_value is long)
            {
                return (long)m_value == 0L ? false : true;
            } 
            else if (m_value is float)
            {
                return (float)m_value == 0.0f ? false : true;
            } 
            else if (m_value is double)
            {
                return (double)m_value == 0.0d ? false : true;
            } 
            else
            {
                throw new InvalidOperationException("MultiTypeValue is in an invalid state.");
            }
        }

        /// <summary>
        /// Returns the value this describes as a int. Numberic values will be converted
        /// to an int represation. Note that this may be lossy and result in truncation.
        /// </summary>
        /// 
        /// <returns>The value.</returns>
        public int AsInt()
        {
            ReleaseAssert.IsTrue(IsNumber(), "Value is not a number.");
            
            if (m_value is bool)
            {
                return (bool)m_value ? 1 : 0;
            } 
            else if (m_value is int)
            {
                return (int)m_value;
            } 
            else if (m_value is long)
            {
                return (int)(long)m_value;
            } 
            else if (m_value is float)
            {
                return (int)(float)m_value;
            } 
            else if (m_value is double)
            {
                return (int)(double)m_value;
            } 
            else
            {
                throw new InvalidOperationException("MultiTypeValue is in an invalid state.");
            }
        }

        /// <summary>
        /// Returns the value this describes as a long. Numberic values will be converted
        /// to a long represation. Note that this may be lossy and result in truncation.
        /// </summary>
        /// 
        /// <returns>The value.</returns>
        public long AsLong()
        {
            ReleaseAssert.IsTrue(IsNumber(), "Value is not a number.");
            
            if (m_value is bool)
            {
                return (bool)m_value ? 1L : 0L;
            } 
            else if (m_value is int)
            {
                return (long)(int)m_value;
            } 
            else if (m_value is long)
            {
                return (long)m_value;
            } 
            else if (m_value is float)
            {
                return (long)(float)m_value;
            } 
            else if (m_value is double)
            {
                return (long)(double)m_value;
            } 
            else
            {
                throw new InvalidOperationException("MultiTypeValue is in an invalid state.");
            }
        }

        /// <summary>
        /// Returns the value this describes as a float. Numberic values will be converted
        /// to a float represation. Note that this may be lossy and result in truncation.
        /// </summary>
        /// <returns>The value.</returns>
        public float AsFloat()
        {
            ReleaseAssert.IsTrue(IsNumber(), "Value is not a number.");
            
            if (m_value is bool)
            {
                return (bool)m_value ? 1.0f : 1.0f;
            } 
            else if (m_value is int)
            {
                return (float)(int)m_value;
            } 
            else if (m_value is long)
            {
                return (float)(long)m_value;
            } 
            else if (m_value is float)
            {
                return (float)m_value;
            } 
            else if (m_value is double)
            {
                return (float)(double)m_value;
            } 
            else
            {
                throw new InvalidOperationException("MultiTypeValue is in an invalid state.");
            }
        }

        /// <summary>
        /// Returns the value this describes as a double. Numberic values will be converted
        /// to a double represation. Note that this may be lossy and result in truncation.
        /// </summary>
        /// <returns>The value.</returns>
        public double AsDouble()
        {
            ReleaseAssert.IsTrue(IsNumber(), "Value is not a number.");
            
            if (m_value is bool)
            {
                return (bool)m_value ? 1.0d : 1.0d;
            } 
            else if (m_value is int)
            {
                return (double)(int)m_value;
            } 
            else if (m_value is long)
            {
                return (double)(long)m_value;
            } 
            else if (m_value is float)
            {
                return (double)(float)m_value;
            } 
            else if (m_value is double)
            {
                return (double)m_value;
            } 
            else
            {
                throw new InvalidOperationException("MultiTypeValue is in an invalid state.");
            }
        }

        /// <summary>
        /// Returns the value this describes if it is a string. If it is of a different
        /// type, an error will occur.
        /// </summary>
        /// 
        /// <returns>The value.</returns>
        public string AsString()
        {
            ReleaseAssert.IsTrue(IsString(), "Value is not a string.");
            
            return (string)m_value;
        }

        /// <summary>
        /// Returns the value this describes if it is a list. If it is of a different
        /// type, an error will occur.
        /// </summary>
        /// 
        /// <returns>The value.</returns>
        public MultiTypeList AsList()
        {
            ReleaseAssert.IsTrue(IsList(), "Value is not a list.");
            
            return (MultiTypeList)m_value;
        }

        /// <summary>
        /// Returns the value this describes if it is a dictionary. If it is of a different
        /// type, an error will occur.
        /// </summary>
        /// 
        /// <returns>The value.</returns>
        public MultiTypeDictionary AsDictionary()
        {
            ReleaseAssert.IsTrue(IsDictionary(), "Value is not a dictionary.");
            
            return (MultiTypeDictionary)m_value;
        }

        /// <summary>
        /// Serialises the object this describes to a json compliant type. This will be one of: 
        /// bool, int, long, float, string, IList<object> or IDictionary<string, object>. Values
        /// stored in a list or a dictionary will aslo be one of the json compliant types.
        /// </summary>
        /// 
        /// <returns>The json compliant object.</returns>
        public object Serialise()
        {
            if (m_type == Type.List)
            {
                return ((MultiTypeList)m_value).Serialise();
            } else if (m_type == Type.Dictionary)
            {
                return ((MultiTypeDictionary)m_value).Serialise();
            } else
            {
                return m_value;
            }
        }

        /// <summary>
        /// Evaluates whether or not this and the given MultiTypeValue are equal in value.
        /// </summary>
        /// 
        /// <param name="value">The value to compare.</c>
        /// 
        /// <returns>Whether or not they are equal.</returns>
        public bool Equals(MultiTypeValue value)
        {
            if ((object)value == null)
            {
                return false;
            }

            if (object.ReferenceEquals(this, value))
            {
                return true;
            }

            if (m_type != value.m_type)
            {
                return false;
            }

            switch (m_type)
            {
                case Type.Null:
                {
                    return true;
                }
                case Type.Number:
                {
                    if (m_value is bool || value.m_value is bool)
                    {
                        return (AsBool() == value.AsBool());
                    }
                    else if (m_value is int || value.m_value is int)
                    {
                        return (AsInt() == value.AsInt());
                    }
                    else if (m_value is long || value.m_value is long)
                    {
                        return (AsLong() == value.AsLong());
                    }
                    else if (m_value is float || value.m_value is float)
                    {
                        return (AsFloat() == value.AsFloat());
                    }
                    else if (m_value is double || m_value is double)
                    {
                        return (AsDouble() == value.AsDouble());
                    }
                    else
                    {
                        throw new InvalidOperationException("MultiTypeValue is in an invalid state.");
                    }
                }
                default:
                {
                    return m_value.Equals(value.m_value);
                }
            }
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

            if (object.ReferenceEquals(this, value))
            {
                return true;
            }

            var multiTypeValue = value as MultiTypeValue;
            if (multiTypeValue == null)
            {
                return false;
            }

            return Equals(multiTypeValue);
        }

        /// <summary>
        /// Returns the hash code of the underlying type.
        /// </summary>
        /// 
        /// <returns>The hash code of the underlying type.</returns>
        public override int GetHashCode()
        {
            int hashCode = HashCode.InitialValue;

            hashCode = HashCode.Add(hashCode, m_value);
            
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
        public static bool operator ==(MultiTypeValue a, MultiTypeValue b)
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
        /// This allows implicit casts from a bool to a MultiTypeValue. This means that 
        /// any API which will accept a MultiTypeValue will also accept a bool without 
        /// manual conversion.
        /// </summary>
        /// 
        /// <param name="value">The bool to convert.</c>
        /// 
        /// <returns>A MultiTypeValue which represents the given value.</returns>
        public static implicit operator MultiTypeValue(bool value)
        {
            return new MultiTypeValue(value);
        }

        /// <summary>
        /// This allows implicit casts from an int to a MultiTypeValue. This means that 
        /// any API which will accept a MultiTypeValue will also accept an int without 
        /// manual conversion.
        /// </summary>
        /// 
        /// <param name="value">The int to convert.</c>
        /// 
        /// <returns>A MultiTypeValue which represents the given value.</returns>
        public static implicit operator MultiTypeValue(int value)
        {
            return new MultiTypeValue(value);
        }

        /// <summary>
        /// This allows implicit casts from a long to a MultiTypeValue. This means that 
        /// any API which will accept a MultiTypeValue will also accept a long without 
        /// manual conversion.
        /// </summary>
        /// 
        /// <param name="value">The long to convert.</c>
        /// 
        /// <returns>A MultiTypeValue which represents the given value.</returns>
        public static implicit operator MultiTypeValue(long value)
        {
            return new MultiTypeValue(value);
        }

        /// <summary>
        /// This allows implicit casts from a float to a MultiTypeValue. This means that 
        /// any API which will accept a MultiTypeValue will also accept a float without 
        /// manual conversion.
        /// </summary>
        /// 
        /// <param name="value">The float to convert.</c>
        /// 
        /// <returns>A MultiTypeValue which represents the given value.</returns>
        public static implicit operator MultiTypeValue(float value)
        {
            return new MultiTypeValue(value);
        }

        /// <summary>
        /// This allows implicit casts from a double to a MultiTypeValue. This means that 
        /// any API which will accept a MultiTypeValue will also accept a double without 
        /// manual conversion.
        /// </summary>
        /// 
        /// <param name="value">The double to convert.</c>
        /// 
        /// <returns>A MultiTypeValue which represents the given value.</returns>
        public static implicit operator MultiTypeValue(double value)
        {
            return new MultiTypeValue(value);
        }

        /// <summary>
        /// This allows implicit casts from a string to a MultiTypeValue. This means that 
        /// any API which will accept a MultiTypeValue will also accept a string without 
        /// manual conversion.
        /// </summary>
        /// 
        /// <param name="value">The string to convert.</c>
        /// 
        /// <returns>A MultiTypeValue which represents the given value.</returns>
        public static implicit operator MultiTypeValue(string value)
        {
            return new MultiTypeValue(value);
        }

        /// <summary>
        /// This allows implicit casts from a MultiTypeList to a MultiTypeValue. This means that 
        /// any API which will accept a MultiTypeValue will also accept a MultiTypeList without 
        /// manual conversion.
        /// </summary>
        /// 
        /// <param name="MultiTypeList">The MultiTypeList to convert.</c>
        /// 
        /// <returns>A MultiTypeValue which represents the given value.</returns>
        public static implicit operator MultiTypeValue(MultiTypeList value)
        {
            return new MultiTypeValue(value);
        }

        /// <summary>
        /// This allows implicit casts from a MultiTypeDictionary to a MultiTypeValue. This means that 
        /// any API which will accept a MultiTypeValue will also accept a MultiTypeDictionary without 
        /// manual conversion.
        /// </summary>
        /// 
        /// <param name="value">The MultiTypeDictionary to convert.</c>
        /// 
        /// <returns>A MultiTypeValue which represents the given value.</returns>
        public static implicit operator MultiTypeValue(MultiTypeDictionary value)
        {
            return new MultiTypeValue(value);
        }

        /// <summary>
        /// Overloaded inequality operator.
        /// </summary>
        /// 
        /// <param name="a">The first value.</c>
        /// <param name="b">The second value.</c>
        /// 
        /// <returns>Whether or not the two values are inequal.</returns>
        public static bool operator !=(MultiTypeValue a, MultiTypeValue b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Creates a new MultiTypeValue which represents Null. Note that this should not be
        /// used directly; if null is required use the MultiTypeValue.Null instead.
        /// </summary>
        private MultiTypeValue()
        {
            m_type = Type.Null;
        }
    }
}
