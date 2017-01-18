//
//  Created by Ian Copland on 2016-01-04
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
using UnityEngine.Assertions;

namespace SdkCore
{
    /// <summary>
    /// <para>Assert is useful for testing the assumptions in code, while ensuring these 
    /// assumptions are not checked during a release build. In a framework it can be useful 
    /// to check assumptions for things that the API user can affect, however this can be 
    /// problematic as the API user is using the release version of the framework.</para>
    /// 
    /// <para>ReleaseAssert provides an alternate to Assert which can be used in this case 
    /// as it will not be omitted from release builds. Failure will still throw an exception 
    /// but only includes information relevant to the user, i.e line numbers are omitted.
    /// </para>
    /// 
    /// <para>This is not intended to be a replacement for Assert. It should be used in cases 
    /// where the assertion will be affected by API user input, any asserts checking internal 
    /// logic should still use Assert.</para>
    /// 
    /// <para>This is stateless and is therefore thread-safe.</para>
    /// </summary>
    public static class ReleaseAssert 
    {
        /// <summary>
        /// Confirms that the given condition is true, otherwise an AssertionException is thrown.
        /// </summary>
        /// 
        /// <param name="condition">The condition that will be checked.</param>
        /// <param name="message">The message that will be printed if the assertion fails.</param>
        public static void IsTrue(bool condition, string message)
        {
            if (!condition)
            {
                throw new AssertionException(message, "");
            }
        }
        
        /// <summary>
        /// Confirms that the given condition is false, otherwise an AssertionException is thrown.
        /// </summary>
        /// 
        /// <param name="condition">The condition that will be checked.</param>
        /// <param name="message">The message that will be printed if the assertion fails.</param>
        public static void IsFalse(bool condition, string message)
        {
            if (condition)
            {
                throw new AssertionException(message, "");
            }
        }
        
        /// <summary>
        /// Confirms that the given object is not null, otherwise an AssertionException is thrown.
        /// </summary>
        /// 
        /// <param name="condition">The object that will be checked.</param>
        /// <param name="message">The message that will be printed if the assertion fails.</param>
        public static void IsNotNull(object obj, string message)
        {
            if (obj == null)
            {
                throw new AssertionException(message, "");
            }
        }
        
        /// <summary>
        /// Confirms that the given object is null, otherwise an AssertionException is thrown.
        /// </summary>
        /// 
        /// <param name="condition">The object that will be checked.</param>
        /// <param name="message">The message that will be printed if the assertion fails.</param>
        public static void IsNull(object obj, string message)
        {
            if (obj != null)
            {
                throw new AssertionException(message, "");
            }
        }
    }
}
