//
//  Created by Ian Copland on 2015-11-09
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

namespace SdkCore
{

    /// <summary>
    /// <para>Provides the means to log messages to the console. There are three 
    /// types of messages that can be logged: verbose, warnings and errors. warnings
    /// and errors will always be printed, however verbose messages will be omitted 
    /// unless the logger was created with the verbose flag set.</para> 
    /// 
    /// <para>This is immutable after construction and is therefore thread-safe.
    /// </para>
    /// </summary>
    public sealed class Logging
    {

        /// <summary>
        /// Describes whether or not verbose logging is enabled.
        /// </summary>
        public bool VerboseLoggingEnabled { get; private set; }

        /// <summary>
        /// Constructs a new instances of the logging class with verbose logging either
        /// enabled or disabled.
        /// </summary>
        /// 
        /// <param name="verboseLoggingEnabled">Whether or not verbose logging should be enabled.</param>
        public Logging(bool verboseLoggingEnabled)
        {
            VerboseLoggingEnabled = verboseLoggingEnabled;
        }

        /// <summary>
        /// Logs a verbose message if verbose logging is enabled.
        /// </summary>
        /// 
        /// <param name="message">The message that should be logged.</param>
        public void LogVerboseMessage(string message)
        {
            if (VerboseLoggingEnabled)
            {
                Debug.Log(message);
            }
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// 
        /// <param name="message">The message that should be logged.</param>
        public void LogWarningMessage(string message)
        {
            Debug.Log("<color=orange>WARNING:</color> " + message);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// 
        /// <param name="message">The message that should be logged.</param>
        public void LogErrorMessage(string message)
        {
            Debug.Log("<color=red>ERROR:</color> " + message);
        }
    }
}