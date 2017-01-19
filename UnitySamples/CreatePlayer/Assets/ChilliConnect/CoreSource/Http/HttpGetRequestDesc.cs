//
//  Created by Ian Copland on 2015-11-10
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
using System.Diagnostics;

namespace SdkCore
{
    /// <summary>
    /// <para>A data container class describing the properties of a HTTP GET 
    /// request, including the target web address and information on headers. 
    /// This is typically used for constructing new instances of HttpGetRequest.
    /// </para>
    /// 
    /// <para>This is not thread safe and should not be accessed from multiple 
    /// threads at  the same time.</para>
    /// </summary>
    public sealed class HttpGetRequestDesc
    {
        /// <summary>
        /// Gets or sets the URL that the request is targeting.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the headers that will be set with the GET request. If
        /// there are no headers this should be an empty dictionary.
        /// </summary>
        public IDictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Initialises a new instance of Http Get Request Desc with the given URL.
        /// </summary>
        /// 
        /// <param name="url">The URL for GET request.</param>
        public HttpGetRequestDesc(string url)
        {
            ReleaseAssert.IsTrue(url != null, "The URL in a HTTP GET request must not be null.");

            Url = url;
            Headers = new Dictionary<string, string>();
        }
    }
}
