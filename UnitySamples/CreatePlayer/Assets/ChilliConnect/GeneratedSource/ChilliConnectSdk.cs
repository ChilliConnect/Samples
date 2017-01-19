//
//  This file was auto-generated using the ChilliConnect SDK Generator.
//
//  The MIT License (MIT)
//
//  Copyright (c) 2015 Tag Games Ltd
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using SdkCore;
using UnityEngine;

namespace ChilliConnect 
{
	/// <summary>
	/// <para>The manager object for the ChilliConnect SDK. The SDK is broken up 
	/// into different sections, or modules, for each collection of features. Each of 
	/// the modules are accessible as properties.</para>
	///
	/// <para>This is thread-safe.</para>
	/// </summary>
	public sealed class ChilliConnectSdk : IDisposable
	{
		private GameObject m_sdkObject;
		
		/// <summary>
		/// The ChilliConnect Player Accounts module. Provides the means to create new
		/// players, log in to existing accounts and modify account data.
		/// </summary>
		public PlayerAccounts PlayerAccounts { get; private set; }
	
		/// <summary>
		/// The ChilliConnect Cloud Data module. Provides the means to store custom data
		/// against Player Accounts for retrieval.
		/// </summary>
		public CloudData CloudData { get; private set; }
	
		/// <summary>
		/// The ChillConnect Leaderboards module. Provides the means to add to and query from
		/// leaderboards.
		/// </summary>
		public Leaderboards Leaderboards { get; private set; }
	
		/// <summary>
		/// The ChilliConnect Cloud Code module. Provides the means to create excute custom
		/// server side scripts.
		/// </summary>
		public CloudCode CloudCode { get; private set; }
	
		/// <summary>
		/// The ChillConnect Push Notifications module. Provides the means to send push
		/// messages to players using Amazon Device Messaging, Apple Push Notification
		/// Service and Google Cloud Messaging.
		/// </summary>
		public PushNotifications PushNotifications { get; private set; }
	
		/// <summary>
		/// The ChillConnect In-App Purchase Validation module. Provides the means to
		/// validate in-app purchases using Amazon Receipt Validation Service, Apple AppStore
		/// and Google Play Store.
		/// </summary>
		public InAppPurchase InAppPurchase { get; private set; }
	
		/// <summary>
		/// Bring back a list of requested DLC packages along with their contained files.
		/// When a request provides multiple Tags, only packages that have all Tags will be
		/// returned.
		/// </summary>
		public Dlc Dlc { get; private set; }
	
		/// <summary>
		/// The ChillConnect Metrics module. This provides the means to log metrics events
		/// with the server.
		/// </summary>
		public Metrics Metrics { get; private set; }
	
		/// <summary>
		/// The ChillConnect Economy Management module. Provides the means to retrieve and
		/// modify player currencies and inventory.
		/// </summary>
		public Economy Economy { get; private set; }

		/// <summary>
		/// Initialises a new instance with the given App Token. Must be called on the
		/// main thread.
		/// </summary>
		///
		/// <param name="appToken">The App Token.</param>
		/// <param name="verboseLogging">Whether or not to enable verbose logging. This is typically only
		/// enabled while debugging.</param>
		public ChilliConnectSdk(string appToken, bool verboseLogging)
		{
			var logging = new Logging(verboseLogging);
			m_sdkObject = GameObjectFactory.CreateCoreSdkGameObject();
            var taskScheduler = m_sdkObject.GetComponent<TaskScheduler>();
			var httpSystem = new HttpSystem(taskScheduler);
			var serverRequestSystem = new ServerRequestSystem(taskScheduler, httpSystem);
			var dataStore = new DataStore();
            
            dataStore.Set("AppToken", appToken);
			
			PlayerAccounts = new PlayerAccounts(logging, taskScheduler, serverRequestSystem, dataStore);
			CloudData = new CloudData(logging, taskScheduler, serverRequestSystem, dataStore);
			Leaderboards = new Leaderboards(logging, taskScheduler, serverRequestSystem, dataStore);
			CloudCode = new CloudCode(logging, taskScheduler, serverRequestSystem, dataStore);
			PushNotifications = new PushNotifications(logging, taskScheduler, serverRequestSystem, dataStore);
			InAppPurchase = new InAppPurchase(logging, taskScheduler, serverRequestSystem, dataStore);
			Dlc = new Dlc(logging, taskScheduler, serverRequestSystem, dataStore);
			Metrics = new Metrics(logging, taskScheduler, serverRequestSystem, dataStore);
			Economy = new Economy(logging, taskScheduler, serverRequestSystem, dataStore);
		}
		
		/// <summary>
		/// Cleans up all non-managed resources. This must be called when the SDK is
		/// no longer needed. Use of the SDK after calling this will result in undefined
		/// behaviour. This must be called from the main thread.
		/// </summary>
		public void Dispose()
		{
            UnityEngine.Object.Destroy(m_sdkObject);
            m_sdkObject = null;
		}
	}
}
