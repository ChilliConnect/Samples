using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using ChilliConnect;
/// 
/// Handles Registeration of device push notifications
/// 
public class PushSystem 
{	
	private static PushSystem s_singletonInstance = null;

	private ChilliConnectSdk m_chilliConnect;

	#if UNITY_ANDROID
	private const string k_pushService = "GCM";

	//Daves
	//private const string k_senderID = "658616921793";

	//GCM Test App
	private const string k_senderID = "182016186583";

	private const string PLUGIN_CLASS_NAME = "com.chilliexamplecloudmessaging.unitygcmplugin.UnityGCMHandler";
	#elif UNITY_IOS
	private const string k_pushService = "APNS";
	#else
	private const string k_pushService = "";
	#endif 

	public static PushSystem Get()
	{
		return s_singletonInstance;
	}

	public PushSystem()
	{
		s_singletonInstance = this;
	}

	public void Initialise(ChilliConnectSdk chilliConnect)
	{
		m_chilliConnect = chilliConnect;
	}

	public void RegisterForPushNotification()
	{
		UnityEngine.Debug.Log("PushSystem : RegisterForPushNotification - Attempting to register!");

#if UNITY_ANDROID
		GooglePushNotificationHandler.GenerateDeviceID(k_senderID);
		GooglePushNotificationHandler.SetNotificationIcon("notificationicon");
#elif UNITY_IOS
		iOSPushNotificationHandler.Register();
#endif
	}

	public void SendPush(string nextPlayerChilliId)
	{
		if (nextPlayerChilliId == "") {
			UnityEngine.Debug.Log ("PushSystem : SendPush - No opponent yet, waiting.");
		} else {
			UnityEngine.Debug.Log ("PushSystem : SendPush - Turn Complete: Sending push to opponent!");

			var scriptParams = new Dictionary<string, SdkCore.MultiTypeValue> ();
			scriptParams.Add ("opponentChilliId", nextPlayerChilliId);

			var runScriptRequest = new RunScriptRequestDesc ("OPPONENT_TURN");
			runScriptRequest.Params = scriptParams;

			m_chilliConnect.CloudCode.RunScript (runScriptRequest, 
				(request, response) => Debug.Log ("Push Message Sent to: " + nextPlayerChilliId),
				(request, error) => Debug.LogError (error.ErrorDescription));
		}
	}
}
