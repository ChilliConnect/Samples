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

namespace SdkCore
{
    /// <summary>
    /// <para>A convenience class which provides a means to convert between an DateTime
    /// object and ISO-8601 format date strings.</para>
    /// 
    /// <para>This is thread-safe.</para>
    /// </summary>
    public static class DateFormatter
    {
        /// <summary> 
        /// Creates a new instance of DateTime from a string. The string must represent 
        /// an IS0-8601 date. If the string is not a valid date then this will error.
        /// </summary>
        /// 
        /// <param name="dateString">The string respresentation of a IS0-8601 date.</param>
        /// 
        /// <returns>The date that the given string respresents.</returns>
        public static DateTime DateFromString(string dateString)
        {
            return DateTime.Parse(dateString, null, System.Globalization.DateTimeStyles.RoundtripKind);
        }

        /// <summary> 
        /// Converts the given DateTime to an IS0-8601 date date string.
        /// </summary>
        /// 
        /// <param name="dateString">The date to convert.</param>
        /// 
        /// <returns>The string representation of the date in IS0-8601 format.</returns>
        public static string StringFromDate(DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:ss");
        }
    }
}