//
//  Created by Ian Copland on 2015-11-24
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

namespace SdkCore
{
    /// <summary>
    /// The interface for all immediate server request objects. All server requests will 
    /// implement this interface, providing a common API for calling them.
    /// </summary>
    public interface IImmediateServerRequest
    {
        /// <summary>
        /// The full URL that the request is targeting.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// The HTTP request method that should be used.
        /// </summary>
        HttpRequestMethod HttpRequestMethod { get; }

        /// <summary>
        /// Serialises all header properties. The output will be a dictionary containing 
        /// the extra header key-value pairs in addition the standard headers sent with all
        /// server requests. Will return an empty dictionary if there are no headers.
        /// </summary>
        /// 
        /// <returns>The header dictionary.</returns>
        IDictionary<string, string> SerialiseHeaders();

        /// <summary>
        /// Serialises all body properties. The output will be a dictionary containing the 
        /// body of the request in a form that can easily be converted to Json. Will return
        /// an empty dictionary if there is no body. Will always be empty if the GET HTTP 
        /// request method is used.
        /// </summary>
        /// 
        /// <returns>The body dictionary.</returns>
        IDictionary<string, object> SerialiseBody();
    } 
}