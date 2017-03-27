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
    /// <para>A data container class describing the properties of a HTTP POST 
    /// request, including the target web address, information on headers and body
    /// data. his is typically used to construct new instances of HttpPostRequest.
    /// </para>
    /// 
    /// <para>This is not thread safe and should not be accessed from multiple 
    /// threads at the same time.</para>
    /// </summary>
    public sealed class HttpPostRequestDesc
    {
        /// <summary>
        /// Gets or sets the URL that the request is targeting.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the headers that will be set with the POST request. If there
        /// are no headers then this should be an empty dictionary.
        /// </summary>
        public IDictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Gets or sets the body of the POST request. 
        /// </summary>
        public byte[] Body { get; set; }

        /// <summary>
        /// Gets or sets the content type of the request. If this is null 
        /// then the default content type will be used.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Initialises a new instance of Http POST Request Desc with the given URL 
        /// and body data.
        /// </summary>
        /// 
        /// <param name="url">The URL for POST request.</param>
        /// <param name="body">The body data. This must not be null.</param>
        public HttpPostRequestDesc(string url, byte[] body)
        {
            ReleaseAssert.IsTrue(url != null, "The URL of a HTTP POST request must not be null.");
            ReleaseAssert.IsTrue(body != null, "The body of a HTTP POST request must not be null.");

            Url = url;
            Body = body;
            Headers = new Dictionary<string, string>();
        }
    }
}
