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
using System;
using System.Collections;

namespace SdkCore
{
    /// <summary>
    /// <para>A convenience class for creating GameObjects for the SdkCore.</para>
    /// 
    /// <para>As this uses the Unity API it is not thread-safe and must be called 
    /// from the main thread.</para>
    /// </summary>
    public static class GameObjectFactory
    {
        /// <summary> 
        /// Creates a CoreSdk game object, which will contain all components required
        /// by the SDK.
        /// </summary>
        /// <returns>The CoreSdk game object.</returns>
        public static GameObject CreateCoreSdkGameObject()
        {
            var random = new System.Random();
            string objectName = "_SdkCore-" + random.Next(0, Int32.MaxValue);

            var gameObject = new GameObject(objectName);
            gameObject.AddComponent<TaskScheduler>();

            UnityEngine.Object.DontDestroyOnLoad(gameObject);

            return gameObject;
        }
    }
}
