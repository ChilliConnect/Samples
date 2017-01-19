//
//  Created by Ian Copland on 2015-11-26
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
using System.Text;
using SdkCore.MiniJSON;
using System.Collections.Generic;
using System.Diagnostics;

namespace SdkCore
{
    /// <summary>
    /// <para>The response from a request made through the server request system. 
    /// This takes the HTTP response and converts the data to an easier to work with 
    /// format for later converting to a server API call specific response. This 
    /// converting the body to a dictionary (i.e JSON format) and doesn't expose 
    /// headers.</para>
    /// 
    /// <para>This is immutable, meaning it is thread-safe.</para>
    /// </summary>
    public sealed class ServerResponse
    {
        /// <summary>
        /// The result of the server request.
        /// </summary>
        public HttpResult Result { get; private set; }

        /// <summary>
        /// Gets the http response code of a server request. If a connection could not 
        /// be estabilished his will be 0.
        /// </summary>
        public int HttpResponseCode { get; private set; }

        /// <summary>
        /// The body of the response from the server, in json form (represented as a 
        /// dictionary) If no connection could be established this will be an empty 
        /// dictionary.
        /// </summary>
        public ReadOnlyDictionary<string, object> Body { get; private set; }

        /// <summary>
        /// Initialises the new instance of server response from the given HTTP 
        /// response.
        /// </summary>
        /// 
        /// <param name="httpResponse">The HTTP response.</param>
        public ServerResponse(HttpResponse httpResponse)
        {
            Result = httpResponse.Result;
            HttpResponseCode = httpResponse.HttpResponseCode;

            if (httpResponse.Body.Length > 0)
            {
                var bodyString = Encoding.UTF8.GetString(httpResponse.Body);
                var bodyDictionary = Json.Deserialize(bodyString) as Dictionary<string, object>;
                ReleaseAssert.IsTrue(bodyDictionary != null, "Invalid server response JSON.");

                Body = new ReadOnlyDictionary<string, object>(bodyDictionary);
            }
            else
            {
                Body = new ReadOnlyDictionary<string, object>();
            }
        }
    }
}