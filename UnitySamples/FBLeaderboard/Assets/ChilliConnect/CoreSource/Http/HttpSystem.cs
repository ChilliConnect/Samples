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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SdkCore
{
    /// <summary>
    /// <para>Provides a means to make both GET and POST requests to the given
    /// web-server. Requests can be both HTTP and HTTPS.</para> 
    /// 
    /// <para>This is thread-safe.</para>
    /// </summary>
    public sealed class HttpSystem
    {
        private TaskScheduler m_taskScheduler;

        /// <summary>
        /// Initializes a new instance of the HTTP system with the given task
        /// scheduler.
        /// </summary>
        /// 
        /// <param name="taskScheduler">The task scheduler.</param>
        public HttpSystem(TaskScheduler taskScheduler)
        {
            ReleaseAssert.IsTrue(taskScheduler != null, "The task scheduler in a HTTP request system must not be null.");

            m_taskScheduler = taskScheduler;
        }

        /// <summary>
        /// Makes a HTTP GET request with the given request object. This is 
        /// performed asynchronously, with the callback block run on a background 
        /// thread.
        /// </summary>
        /// 
        /// <param name="request">The GET HTTP request.</param>
        /// <param name="callback">The callback which will provide the response from the server. 
        /// The callback will be made on a background thread.</param>
        public void SendRequest(HttpGetRequest request, Action<HttpGetRequest, HttpResponse> callback)
        {
            ReleaseAssert.IsTrue(request != null, "The HTTP GET request must not be null when sending a request.");
            ReleaseAssert.IsTrue(callback != null, "The callback must not be null when sending a request.");

            SendRequest(request.Url, request.Headers, null, (HttpResponse response) => 
            {
                callback(request, response);
            });
        }

        /// <summary>
        /// Makes a HTTP POST request with the given request object. This is 
        /// performed asynchronously, with the callback block run on a background 
        /// thread.
        /// </summary>
        /// 
        /// <param name="request">The POST HTTP request.</param>
        /// <param name="callback">The callback which will provide the response from the server. 
        /// The callback will be made on a background thread.</param>
        public void SendRequest(HttpPostRequest request, Action<HttpPostRequest, HttpResponse> callback)
        {
            ReleaseAssert.IsTrue(request != null, "The HTTP POST request must not be null when sending a request.");
            ReleaseAssert.IsTrue(callback != null, "The callback must not be null when sending a request.");

            var headers = new Dictionary<string, string>(request.Headers);

            if (request.ContentType != null)
            {
                headers.Add("Content-Type", request.ContentType);
            }

            SendRequest(request.Url, headers, request.Body, (HttpResponse response) => 
            {
                callback(request, response);
            });
        }

        /// <summary>
        /// Provides the means to send both GET and POST requests depending on the
        /// input data.
        /// </summary>
        /// 
        /// <param name="url">The URL that the request is targetting.</param>
        /// <param name="headers">The headers for the HTTP request.</param>
        /// <param name="body">The body of the request. If null, a GET request will be sent.</param>
        /// <param name="callback">The callback providing the response from the server.</param>
        private void SendRequest(String url, IDictionary<string, string> headers, byte[] body, Action<HttpResponse> callback)
        {
            ReleaseAssert.IsTrue(url != null, "The URL must not be null when sending a request.");
            ReleaseAssert.IsTrue(headers != null, "The headers must not be null when sending a request.");
            ReleaseAssert.IsTrue(callback != null, "The callback must not be null when sending a request.");

            // Unity's WWW class works with the Dictionary concrete class rather than the abstract
            // IDictionary. Rather than cast a copy is made so we can be sure other dictionary types
            // will work.
            Dictionary<string, string> headersConcreteDict = new Dictionary<string, string>(headers);

            m_taskScheduler.ScheduleMainThreadTask(() => 
            {
                var www = new WWW(url, body, headersConcreteDict);
                m_taskScheduler.StartCoroutine(ProcessRequest(www, callback));
            });
        }

        /// <summary>
        /// <para>The coroutine for processing the HTTP request. This will yield until the 
        /// request has completed then parse the information required by a HTTP response
        /// from the WWW object.</para>
        /// </summary>
        /// 
        /// <returns>The coroutine enumerator.</returns>
        /// 
        /// <param name="www">The WWW object.</param>
        /// <param name="callback">The callback providing the response from the server.</param>
        private IEnumerator ProcessRequest(WWW www, Action<HttpResponse> callback)
        {
            ReleaseAssert.IsTrue(www != null, "The WWW must not be null when sending a request.");
            ReleaseAssert.IsTrue(callback != null, "The callback must not be null when sending a request.");

            yield return www;

            HttpResponseDesc desc = null;
            if (string.IsNullOrEmpty(www.error))
            {
                ReleaseAssert.IsTrue(www.responseHeaders != null, "A successful HTTP response must have a headers object.");

                desc = new HttpResponseDesc(HttpResult.Success);
                desc.Headers = new Dictionary<string, string>(www.responseHeaders);

                if (www.bytes != null)
                {
                    desc.Body = www.bytes;
                }

                var httpStatus = www.responseHeaders ["STATUS"];
                ReleaseAssert.IsTrue(!string.IsNullOrEmpty(httpStatus), "A successful HTTP response must have a HTTP status value in the header.");

                desc.HttpResponseCode = ParseHttpStatus(httpStatus);
            }
            else
            {
				var bytes = www.bytes;
				int httpResponseCode = 0;

				#if UNITY_IPHONE && !UNITY_EDITOR
					var text = www.text;
					if ( !string.IsNullOrEmpty(text) ) 
					{
						var bodyDictionary = Json.Deserialize(text) as Dictionary<string, object>;
						if (bodyDictionary != null && bodyDictionary.ContainsKey ("HttpCode")) 
						{
							httpResponseCode = Convert.ToInt32(bodyDictionary ["HttpCode"]);
							bytes = Encoding.UTF8.GetBytes(text);
						}
					}
				#else 
					httpResponseCode = ParseHttpError(www.error);
				#endif
                
                if (httpResponseCode != 0)
                {
                    desc = new HttpResponseDesc(HttpResult.Success);
                    desc.Headers = new Dictionary<string, string>(www.responseHeaders);
                    
                    if (www.bytes != null)
                    {
                        desc.Body = www.bytes;
                    }

                    desc.HttpResponseCode = httpResponseCode;
                }
                else
                {
                    desc = new HttpResponseDesc(HttpResult.CouldNotConnect);
                }
            }

            HttpResponse response = new HttpResponse(desc);
            m_taskScheduler.ScheduleBackgroundTask(() => 
            {
                callback(response);
            });
        }

        /// <summary>
        /// Parses the HTTP response code from the given HTTP STATUS string. The string should
        /// be in the format 'HTTP/X YYY...' or 'HTTP/X.X YYY...' where YYY is the response 
        /// code.
        /// </summary>
        /// 
        /// <returns>The response code.</returns>
        /// 
        /// <param name="httpStatus">The HTTP status string in the format 'HTTP/X YYY...' 
        /// or 'HTTP/X.X YYY...'.</param>
        private int ParseHttpStatus(string httpStatus)
        {
            ReleaseAssert.IsTrue(httpStatus != null, "The HTTP status string must not be null when parsing a response code.");
            
            var regex = new Regex("[a-zA-Z]*\\/\\d+(\\.\\d)?\\s(?<httpResponseCode>\\d+)\\s");
            var match = regex.Match(httpStatus);
            ReleaseAssert.IsTrue(match.Groups.Count == 3, "There must be exactly 3 match groups when using a regex on a HTTP status.");
            
            var responseCodeString = match.Groups ["httpResponseCode"].Value;
            ReleaseAssert.IsTrue(responseCodeString != null, "The response code string cannot be null when using a regex on a HTTP status.");
            
            return Int32.Parse(responseCodeString);
        }

        /// <summary>
        /// Parses the HTTP response code from the given HTTP error string. The string should
        /// be in the format 'XXX ...' where XXX is the HTTP response code.
        /// </summary>
        /// 
        /// <returns>The response code parsed from the error, or 0 if there wasn't one.</returns>
        /// 
        /// <param name="httpError">The HTTP error string.</param>
        private int ParseHttpError(string httpError)
        {
            ReleaseAssert.IsTrue(httpError != null, "The HTTP error string must not be null when parsing a response code.");
            
            var regex = new Regex("(?<httpResponseCode>[0-9][0-9][0-9])\\s");
            if (regex.IsMatch(httpError))
            {
                var match = regex.Match(httpError);
                ReleaseAssert.IsTrue(match.Groups.Count == 2, "There must be exactly 2 match groups when using a regex on a HTTP error.");
                
                var responseCodeString = match.Groups ["httpResponseCode"].Value;
                ReleaseAssert.IsTrue(responseCodeString != null, "The response code string cannot be null when using a regex on a HTTP error.");

                return Int32.Parse(responseCodeString);
            }

            return 0;
        }
    }
}