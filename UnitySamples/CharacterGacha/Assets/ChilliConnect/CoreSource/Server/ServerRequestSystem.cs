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
using System.Collections.Generic;
using System;
using System.Text;
using System.Diagnostics;

namespace SdkCore
{
    /// <summary>
    /// <para>Manages all server requests. Holds on to active requests for their 
    /// entire lifetime and provides logging of the different event types.</para>
    /// 
    /// <para>This is immutable, meaning it is thread-safe.</para>
    /// </summary>
    public sealed class ServerRequestSystem
    {
        private TaskScheduler m_taskScheduler;
        private HttpSystem m_httpSystem;

        /// <summary>
        /// Initialises a new instance of the server request system.
        /// </summary>
        /// 
        /// <param name="taskScheduler">The task scheduler.</param>
        /// <param name="httpSystem">The HTTP system.</param>
        /// <param name="appToken">The app token.</param>
        public ServerRequestSystem(TaskScheduler taskScheduler, HttpSystem httpSystem)
        {
            m_taskScheduler = taskScheduler;
            m_httpSystem = httpSystem;
        }

        /// <summary>
        /// Performs a server request with the given request parameters. This is 
        /// performed asynchronously.
        /// </summary>
        /// 
        /// <param name="request">The request that should be performed, which must adhere 
        /// to the immediate request protocol.</param>
        /// <param name="callback">The callback containing the response. The callback 
        /// will be on a background thread.</param>
        public void SendImmediateRequest(IImmediateServerRequest request, Action<IImmediateServerRequest, ServerResponse> callback)
        {
            m_taskScheduler.ScheduleBackgroundTask(() => 
            {
                if (request.HttpRequestMethod == HttpRequestMethod.Post)
                {
                    var bodyString = MiniJSON.Json.Serialize(request.SerialiseBody());
                    ReleaseAssert.IsTrue(bodyString != null, "Invalid body.");
                    var bodyData = Encoding.UTF8.GetBytes(bodyString);

                    var httpRequestDesc = new HttpPostRequestDesc(request.Url, bodyData);
                    httpRequestDesc.Headers = request.SerialiseHeaders();
                    httpRequestDesc.ContentType = "application/json";

                    var httpRequest = new HttpPostRequest(httpRequestDesc);
                    m_httpSystem.SendRequest(httpRequest, (HttpPostRequest receivedHttpRequest, HttpResponse httpResponse) => 
                    {
                        ReleaseAssert.IsTrue(httpRequest == receivedHttpRequest, "Received response for wrong request.");

                        var serverResponse = new ServerResponse(httpResponse);
                        callback(request, serverResponse);
                    });
                }
                else
                {
                    var httpRequestDesc = new HttpGetRequestDesc(request.Url);
                    httpRequestDesc.Headers = request.SerialiseHeaders();
                    
                    var httpRequest = new HttpGetRequest(httpRequestDesc);
                    m_httpSystem.SendRequest(httpRequest, (HttpGetRequest receivedHttpRequest, HttpResponse httpResponse) => 
                    {
                        ReleaseAssert.IsTrue(httpRequest == receivedHttpRequest, "Received response for wrong request.");
                        
                        var serverResponse = new ServerResponse(httpResponse);
                        callback(request, serverResponse);
                    });
                }
            });
        }
    }
}