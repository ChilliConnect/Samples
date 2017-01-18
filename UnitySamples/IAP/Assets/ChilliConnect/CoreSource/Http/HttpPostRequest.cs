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
    /// <para>A basic data container class describing the properties of a HTTP POST
    /// request, including the target web address, information on headers and the 
    /// body data. This hould be created using a HttpPostRequestDesc.</para>
    /// 
    /// <para>This is immutable and therefore thread-safe.</para>
    /// </summary>
    public sealed class HttpPostRequest
    {
        /// <summary>
        /// Gets the URL that the request is targetting. Will never be null.
        /// </summary>
        public string Url { get; private set; }
        
        /// <summary>
        /// Gets the headers that will be set with the GET request. Will never be 
        /// null.
        /// </summary>
        public IDictionary<string, string> Headers { get; private set; }

        /// <summary>
        /// Gets the body of the post request. Will never be null.
        /// </summary>
        public byte[] Body { get; private set; }

        /// <summary>
        /// Gets the content type of the request. If this is null then the default 
        /// content type should be used.
        /// </summary>
        public string ContentType { get; private set; }
        
        /// <summary>
        /// Initialises the HTTP POST request with the given description.
        /// </summary>
        /// 
        /// <param name="desc">The description of the HTTP POST request.</param>
        public HttpPostRequest(HttpPostRequestDesc desc)
        {
            ReleaseAssert.IsTrue(desc != null, "The description of a HTTP POST request must not be null.");
            ReleaseAssert.IsTrue(desc.Url != null, "The URL in a HTTP GET request must not be null.");
            ReleaseAssert.IsTrue(desc.Headers != null, "The headers of a HTTP POST request must not be null.");
            ReleaseAssert.IsTrue(desc.Body != null, "The body of a HTTP POST request must not be null.");

            Url = desc.Url;
            Body = desc.Body;
            Headers = desc.Headers;
            ContentType = desc.ContentType;
        }
    }
}
