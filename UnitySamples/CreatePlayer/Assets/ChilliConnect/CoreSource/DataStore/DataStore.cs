//
//  Created by Ian Copland on 2016-01-04
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
using System;
using System.Collections;
using System.Collections.Generic;

namespace SdkCore
{
    /// <summary>
    /// <para>Provides the means to store values with an associated key that will persist
    /// accross the current session.</para>
    /// 
    /// <para>This is thread-safe.</para>
    /// </summary>
    public sealed class DataStore
    {
        Dictionary<string, object> m_dataStore = new Dictionary<string, object>();
        private object m_lock = new object();

        /// <summary> 
        /// Reads the value from the data store with the given key. If the value doesn't
        /// exist, or it is of the wrong type then an error will occur.
        /// </summary>
        /// 
        /// <param name="key">The key to look up.</param>
        /// 
        /// <returns>The boolean value.</returns>
        public bool GetBool(string key)
        {
            ReleaseAssert.IsNotNull(key, "Cannot provide a null key.");

            object value = null;

            lock (m_lock)
            {
                m_dataStore.TryGetValue(key, out value);
            }
            
            ReleaseAssert.IsNotNull(value, "Value for key '" + key + "' is missing from data store.");
            ReleaseAssert.IsTrue(value is bool, "Value is incorrect type.");
            return (bool)value;
        }

        /// <summary> 
        /// Reads the value from the data store with the given key. If the value doesn't
        /// exist, or it is of the wrong type then an error will occur.
        /// </summary>
        /// 
        /// <param name="key">The key to look up.</param>
        /// 
        /// <returns>The integer value.</returns>
        public int GetInt(string key)
        {
            ReleaseAssert.IsNotNull(key, "Cannot provide a null key.");
            
            object value = null;
            
            lock (m_lock)
            {
                m_dataStore.TryGetValue(key, out value);
            }
            
            ReleaseAssert.IsNotNull(value, "Value for key '" + key + "' is missing from data store.");
            ReleaseAssert.IsTrue(value is int, "Value is incorrect type.");
            return (int)value;
        }

        /// <summary> 
        /// Reads the value from the data store with the given key. If the value doesn't
        /// exist, or it is of the wrong type then an error will occur.
        /// </summary>
        /// 
        /// <param name="key">The key to look up.</param>
        /// 
        /// <returns>The long value.</returns>
        public long GetLong(string key)
        {
            ReleaseAssert.IsNotNull(key, "Cannot provide a null key.");
            
            object value = null;
            
            lock (m_lock)
            {
                m_dataStore.TryGetValue(key, out value);
            }
            
            ReleaseAssert.IsNotNull(value, "Value for key '" + key + "' is missing from data store.");
            ReleaseAssert.IsTrue(value is long, "Value is incorrect type.");
            return (long)value;
        }

        /// <summary> 
        /// Reads the value from the data store with the given key. If the value doesn't
        /// exist, or it is of the wrong type then an error will occur.
        /// </summary>
        /// 
        /// <param name="key">The key to look up.</param>
        /// 
        /// <returns>The float value.</returns>
        public float GetFloat(string key)
        {
            ReleaseAssert.IsNotNull(key, "Cannot provide a null key.");

            object value = null;
            
            lock (m_lock)
            {
                m_dataStore.TryGetValue(key, out value);
            }
            
            ReleaseAssert.IsNotNull(value, "Value for key '" + key + "' is missing from data store.");
            ReleaseAssert.IsTrue(value is float, "Value is incorrect type.");
            return (float)value;
        }

        /// <summary> 
        /// Reads the value from the data store with the given key. If the value doesn't
        /// exist, or it is of the wrong type then an error will occur.
        /// </summary>
        /// 
        /// <param name="key">The key to look up.</param>
        /// 
        /// <returns>The string value.</returns>
        public string GetString(string key)
        {
            ReleaseAssert.IsNotNull(key, "Cannot provide a null key.");
            
            object value = null;
            
            lock (m_lock)
            {
                m_dataStore.TryGetValue(key, out value);
            }
            
            ReleaseAssert.IsNotNull(value, "Value for key '" + key + "' is missing from data store.");
            ReleaseAssert.IsTrue(value is string, "Value is incorrect type.");
            return (string)value;
        }

        /// <summary> 
        /// Reads the value from the data store with the given key. If the value doesn't
        /// exist, or it is of the wrong type then an error will occur.
        /// </summary>
        /// 
        /// <param name="key">The key to look up.</param>
        /// 
        /// <returns>The Json Object value.</returns>
        public ReadOnlyDictionary<string, object> GetJsonObject(string key)
        {
            ReleaseAssert.IsNotNull(key, "Cannot provide a null key.");
            
            object value = null;
            
            lock (m_lock)
            {
                m_dataStore.TryGetValue(key, out value);
            }
            
            ReleaseAssert.IsNotNull(value, "Value for key '" + key + "' is missing from data store.");
            ReleaseAssert.IsTrue(value is ReadOnlyDictionary<string, object>, "Value is incorrect type.");
            return (ReadOnlyDictionary<string, object>)value;
        }

        /// <summary> 
        /// Reads the value from the data store with the given key. If the value doesn't
        /// exist, or it is of the wrong type then an error will occur.
        /// </summary>
        /// 
        /// <param name="key">The key to look up.</param>
        /// 
        /// <returns>The date value.</returns>
        public DateTime GetDate(string key)
        {
            ReleaseAssert.IsNotNull(key, "Cannot provide a null key.");
            
            object value = null;
            
            lock (m_lock)
            {
                m_dataStore.TryGetValue(key, out value);
            }
            
            ReleaseAssert.IsNotNull(value, "Value for key '" + key + "' is missing from data store.");
            ReleaseAssert.IsTrue(value is DateTime, "Value is incorrect type.");
            return (DateTime)value;
        }

        /// <summary> 
        /// Stores a new value in the data store. If data already exists for the given 
        /// key, it will be overwritten.
        /// </summary>
        /// 
        /// <param name="key">The key to store the value as.</param>
        /// <param name="value">The value which should be stored.</param>
        public void Set(string key, bool value)
        {
            ReleaseAssert.IsNotNull(key, "Cannot provide a null key.");

            lock (m_lock)
            {
                m_dataStore[key] = value;
            }
        }

        /// <summary> 
        /// Stores a new value in the data store. If data already exists for the given 
        /// key, it will be overwritten.
        /// </summary>
        /// 
        /// <param name="key">The key to store the value as.</param>
        /// <param name="value">The value which should be stored.</param>
        public void Set(string key, int value)
        {
            ReleaseAssert.IsNotNull(key, "Cannot provide a null key.");
            
            lock (m_lock)
            {
                m_dataStore[key] = value;
            }
        }

        /// <summary> 
        /// Stores a new value in the data store. If data already exists for the given 
        /// key, it will be overwritten.
        /// </summary>
        /// 
        /// <param name="key">The key to store the value as.</param>
        /// <param name="value">The value which should be stored.</param>
        public void Set(string key, long value)
        {
            ReleaseAssert.IsNotNull(key, "Cannot provide a null key.");
            
            lock (m_lock)
            {
                m_dataStore[key] = value;
            }
        }

        /// <summary> 
        /// Stores a new value in the data store. If data already exists for the given 
        /// key, it will be overwritten.
        /// </summary>
        /// 
        /// <param name="key">The key to store the value as.</param>
        /// <param name="value">The value which should be stored.</param>
        public void Set(string key, float value)
        {
            ReleaseAssert.IsNotNull(key, "Cannot provide a null key.");
            
            lock (m_lock)
            {
                m_dataStore[key] = value;
            }
        }

        /// <summary> 
        /// Stores a new value in the data store. If data already exists for the given 
        /// key, it will be overwritten.
        /// </summary>
        /// 
        /// <param name="key">The key to store the value as.</param>
        /// <param name="value">The value which should be stored.</param>
        public void Set(string key, string value)
        {
            ReleaseAssert.IsNotNull(key, "Cannot provide a null key.");
            ReleaseAssert.IsNotNull(key, "Cannot provide a null value.");
            
            lock (m_lock)
            {
                m_dataStore[key] = value;
            }
        }

        /// <summary> 
        /// Stores a new value in the data store. If data already exists for the given 
        /// key, it will be overwritten.
        /// </summary>
        /// 
        /// <param name="key">The key to store the value as.</param>
        /// <param name="value">The value which should be stored.</param>
        public void Set(string key, DateTime value)
        {
            ReleaseAssert.IsNotNull(key, "Cannot provide a null key.");
            ReleaseAssert.IsNotNull(key, "Cannot provide a null value.");
            
            lock (m_lock)
            {
                m_dataStore[key] = value;
            }
        }

        /// <summary> 
        /// Stores a new value in the data store. If data already exists for the given 
        /// key, it will be overwritten.
        /// </summary>
        /// 
        /// <param name="key">The key to store the value as.</param>
        /// <param name="value">The value which should be stored.</param>
        public void Set(string key, IDictionary<string, object> value)
        {
            ReleaseAssert.IsNotNull(key, "Cannot provide a null key.");
            ReleaseAssert.IsNotNull(key, "Cannot provide a null value.");
            
            lock (m_lock)
            {
                m_dataStore[key] = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>(value));
            }
        }
    }
}