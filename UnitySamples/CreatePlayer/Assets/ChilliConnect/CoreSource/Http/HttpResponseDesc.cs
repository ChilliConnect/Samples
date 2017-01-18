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
    /// <para>A data container for information on the result of an HTTP request. 
    /// This is typically used in the creation of an SCHttpResponseDesc instance.
    /// </para>
    /// 
    /// <para>This is not thread-safe and therefore should not be accessed from 
    /// multiple threads at the same time.</para>
    /// </summary>
    public sealed class HttpResponseDesc
    {
        /// <summary>
        /// Gets or sets the result of the HTTP request.
        /// </summary>
        public HttpResult Result { get; set; }
        
        /// <summary>
        /// Gets or sets the http response code of a http request. If a connection 
        /// could not be estabilished this should be 0.
        /// </summary>
        public int HttpResponseCode { get; set; }
        
        /// <summary>
        /// Gets or sets the key-value headers returned by the server. If no connection 
        /// could be established this should be an empty dictionary.
        /// </summary>
        public IDictionary<string, string> Headers { get; set; }
        
        /// <summary>
        /// Gets or sets the body of the response from the server. If no connection 
        /// could be established this should be a zero sized byte array.
        /// </summary>
        public byte[] Body { get; set; }
        
        /// <summary>
        /// Initialises a new instance of a HTTP response description with the given
        /// HTTP result.
        /// </summary>
        /// 
        /// <param name="httpResult">The HTTP result.</param>
        public HttpResponseDesc(HttpResult httpResult)
        {
            Result = httpResult;
            HttpResponseCode = 0;
            Headers = new Dictionary<string, string>();
            Body = new byte[0];
        }
    }
}
