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
    /// <para>An immutable data container describing the properties of a HTTP GET
    /// request, including the target web address and information on headers. This
    /// should be reated using a HttpGetRequestDesc.</para>
    /// 
    /// <para>This is immutable and therefore thread-safe.</para>
    /// </summary>
    public sealed class HttpGetRequest
    {
        /// <summary>
        /// Gets the URL that the request is targeting. Will never be null.
        /// </summary>
        public string Url { get; private set; }
        
        /// <summary>
        /// Gets the headers that will be sent with the GET request. Will never be 
        /// null.
        /// </summary>
        public IDictionary<string, string> Headers { get; private set; }

        /// <summary>
        /// Initializes a new HTTP GET request using the given description.
        /// </summary>
        /// 
        /// <param name="desc">The description of the HTTP GET request.</param>
        public HttpGetRequest(HttpGetRequestDesc desc)
        {
            ReleaseAssert.IsTrue(desc != null, "A HTTP GET request description should not be null.");
            ReleaseAssert.IsTrue(desc.Url != null, "The URL in a HTTP GET request must not be null.");
            ReleaseAssert.IsTrue(desc.Headers != null, "The headers in a HTTP GET request must not be null.");

            Url = desc.Url;
            Headers = desc.Headers;
        }
    }
}
