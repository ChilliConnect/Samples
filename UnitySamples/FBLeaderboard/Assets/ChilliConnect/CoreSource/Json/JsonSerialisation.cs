//
//  Created by Ian Copland on 2016-01-22
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
    /// <para>A convenience class which provides serialisation and deserialisation of 
    /// non-standard types to JSON format.</para>
    /// 
    /// <para>This is thread-safe.</para>
    /// </summary>
    public static class JsonSerialisation
    {
        /// <summary> 
        /// Serialises a date object to string.
        /// </summary>
        /// 
        /// <param name="date">The date object to be serialised.</param>
        /// 
        /// <returns>The serialised data as a string.</returns>
        public static string Serialise(DateTime date)
        {
            return DateFormatter.StringFromDate(date);
        }
       
        /// <summary> 
        /// Serialised a list to a Json compatible IList. A callback is provided for 
        /// serialising each element in the input list.
        /// </summary>
        /// 
        /// <param name="list">The list to be serialised.</param>
        /// <param name="elementCallback">The callback for serialising the elements in the list.</param>
        /// 
        /// <returns>The serialised list.</returns>
        public static IList<object> Serialise<TType>(IList<TType> list, Func<TType, object> elementCallback)
        {
            var output = new List<object>();
            
            foreach (var obj in list)
            {
                ReleaseAssert.IsNotNull(obj, "List elements should not be null.");
                
                var serialisedObj = elementCallback(obj);
                ReleaseAssert.IsTrue(serialisedObj is bool || serialisedObj is int || serialisedObj is long || serialisedObj is float || serialisedObj is string || 
                                     serialisedObj is IList<object> || serialisedObj is IDictionary<string, object>, "Serialised element must be a valid Json type.");
                
                output.Add(serialisedObj);
            }
            
            return output;
        }

        /// <summary> 
        /// Serialised a map to a Json compatible IDictionary. A callback is provided for 
        /// serialising each element in the input map.
        /// </summary>
        /// 
        /// <param name="map">The map to be serialised.</param>
        /// <param name="elementCallback">The callback for serialising the elements in the map.</param>
        /// 
        /// <returns>The serialised map.</returns>
        public static IDictionary<string, object> Serialise<TType>(IDictionary<string, TType> map, Func<TType, object> elementCallback)
        {
            var output = new Dictionary<string, object>();
            
            foreach (var entry in map)
            {
                ReleaseAssert.IsNotNull(entry.Key, "Map keys should not be null.");
                ReleaseAssert.IsNotNull(entry.Value, "Map elements should not be null.");
                
                var serialisedObj = elementCallback(entry.Value);
                ReleaseAssert.IsNotNull(serialisedObj, "Serialised list elements should not be null.");
                ReleaseAssert.IsTrue(serialisedObj is bool || serialisedObj is int || serialisedObj is long || serialisedObj is float || serialisedObj is string || 
                                     serialisedObj is IList<object> || serialisedObj is IDictionary<string, object>, "Serialised element must be a valid Json type.");
                
                output.Add(entry.Key, serialisedObj);
            }
            
            return output;
        }

        /// <summary> 
        /// Deserialises a date string to a DateTime object. If the string is
        /// not a valid date string then this will error.
        /// </summary>
        /// 
        /// <param name="dateString">The date string..</param>
        /// 
        /// <returns>The output Date.</returns>
        public static DateTime DeserialiseDate(string dateString)
        {
            return DateFormatter.DateFromString(dateString);
        }

        /// <summary> 
        /// Deserialised a list from a Json compatible IList. A callback is 
        /// provided for deserialising each element in the list.
        /// </summary>
        /// 
        /// <param name="list">The list to be deserialised.</param>
        /// <param name="elementCallback">The callback for deserialising the elements in the list.</param>
        /// 
        /// <returns>The deserialised list.</returns>
        public static ReadOnlyCollection<TType> DeserialiseList<TType>(IList<object> list, Func<object, TType> elementCallback)
        {
            var output = new List<TType>();
            
            foreach (var obj in list)
            {
                ReleaseAssert.IsNotNull(obj, "List elements should not be null.");
                ReleaseAssert.IsTrue(obj is bool || obj is int || obj is long || obj is float || obj is string || 
                                     obj is IList<object> || obj is IDictionary<string, object>, "Serialised element must be a valid Json type.");
                
                var deserialisedObj = elementCallback(obj);
                ReleaseAssert.IsNotNull(deserialisedObj, "Deserialised list elements should not be null.");
                
                output.Add(deserialisedObj);
            }
            
            return new ReadOnlyCollection<TType>(output);
        }

        /// <summary> 
        /// Deserialised a map from a Json compatible IDictionary. A callback is provided 
        /// for deserialising each element in the IDictionary.
        /// </summary>
        /// 
        /// <param name="map">The map to be deserialised.</param>
        /// <param name="elementCallback">The callback for deserialising the elements in the map.</param>
        /// 
        /// <returns>The deserialised map.</returns>
        public static ReadOnlyDictionary<string, TType> DeserialiseMap<TType>(IDictionary<string, object> map, Func<object, TType> elementCallback)
        {
            var output = new Dictionary<string, TType>();
            
            foreach (var entry in map)
            {
                ReleaseAssert.IsNotNull(entry.Key, "Map keys should not be null.");
                ReleaseAssert.IsNotNull(entry.Value, "Map elements should not be null.");
                ReleaseAssert.IsTrue(entry.Value is bool || entry.Value is int || entry.Value is long || entry.Value is float || entry.Value is string || 
                                     entry.Value is IList<object> || entry.Value is IDictionary<string, object>, "Serialised element must be a valid Json type.");
                
                var deserialisedObj = elementCallback(entry.Value);
                ReleaseAssert.IsNotNull(deserialisedObj, "Deserialised list elements should not be null.");
                
                output.Add(entry.Key, deserialisedObj);
            }
            
            return new ReadOnlyDictionary<string, TType>(output);
        }
    }
}