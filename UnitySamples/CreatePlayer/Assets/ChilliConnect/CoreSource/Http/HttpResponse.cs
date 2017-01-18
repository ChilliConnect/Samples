//
//  Created by Ian Copland on 2015-11-11
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
    /// <para>An immutable data container for information on the result of an HTTP 
    /// request. This should be created using a HttpResponseDesc.</para> 
    /// 
    /// <para>This is immutable after construction and is therefore thread-safe.
    /// </para>
    /// </summary>
    public sealed class HttpResponse
    {
        /// <summary>
        /// Gets the result of the HTTP request.
        /// </summary>
        public HttpResult Result { get; private set; }
        
        /// <summary>
        /// Gets the http response code of a http request. If a connection could 
		/// not be established this will be 0.
        /// </summary>
        public int HttpResponseCode { get; private set; }
        
        /// <summary>
        /// Gets the key-value headers returned by the server. If no connection 
        /// could be established this will be an empty dictionary.
        /// </summary>
        public IDictionary<string, string> Headers { get; private set; }
        
        /// <summary>
        /// Gets the body of the response from the server. If no connection could 
        /// be established this will be a zero sized byte array.
        /// </summary>
        public byte[] Body { get; private set; }
        
        /// <summary>
        /// Initialises a new instance of a HTTP response with the given description.
        /// </summary>
        /// 
        /// <param name="desc">The HTTP response description.</param>
        public HttpResponse(HttpResponseDesc desc)
        {
            ReleaseAssert.IsTrue(desc != null, "The description of a HTTP response must not be null.");
            ReleaseAssert.IsTrue(desc.Headers != null, "The headers of a HTTP response must not be null.");
            ReleaseAssert.IsTrue(desc.Body != null, "The body of a HTTP response must not be null.");

            Result = desc.Result;
            HttpResponseCode = desc.HttpResponseCode;
            Headers = desc.Headers;
            Body = desc.Body;
        }
    }
}
