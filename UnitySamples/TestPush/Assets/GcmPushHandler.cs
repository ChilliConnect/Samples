using UnityEngine;
using System.Collections;
using System;

public class GcmPushHandler : MonoBehaviour
{
	private const string PLUGIN_CLASS_NAME = "com.chilliconnect.unitygcmplugin.GcmPlugin";

	public System.Action<String> OnTokenRecieved = delegate {};

	private bool hasSetReceiver = false;

    public void GetDeviceToken(string senderId) 
	{
		Debug.Log("GcmPushHandler::GetDeviceToken with senderId: " + senderId);
	
		CheckRecieverSet ();

		using (AndroidJavaClass cls = new AndroidJavaClass (PLUGIN_CLASS_NAME))
		{
			cls.CallStatic ("getDeviceToken", senderId);
		}
	}

	public void OnToken(string token)
	{
		Debug.Log("GcmPushHandler::OnToken: " + token);

		OnTokenRecieved (token);
	}

	private void CheckRecieverSet()
	{
		if (hasSetReceiver) 
		{
			return;
		}

		using (AndroidJavaClass cls = new AndroidJavaClass(PLUGIN_CLASS_NAME))
		{
			cls.CallStatic("setReceiverGameObject", name);
		}

		hasSetReceiver = true;
	}
	/*
	public static void SetNotificationIcon (string in_identifier) 
	{
		using (AndroidJavaClass cls = new AndroidJavaClass (PLUGIN_CLASS_NAME))
		{
			cls.CallStatic ("SetNotificationIcon", in_identifier);
		}
	}

	//------------------------------------------------------------------
	public static bool IsRegistered ()
	{
		using (AndroidJavaClass cls = new AndroidJavaClass (PLUGIN_CLASS_NAME))
		{
			return cls.CallStatic<bool> ("IsRegistered");
		}
	}

	//------------------------------------------------------------------
	public static string GetNotificationTypes()
	{
		using (AndroidJavaClass cls = new AndroidJavaClass (PLUGIN_CLASS_NAME))
		{
			return cls.CallStatic<string> ("GetAllNotifications");
		}
	}
	
	//------------------------------------------------------------------
	/// As well as returning whether app was launched from a notification,
	/// method also consumes that boolean (resets to false).
	///
	/// @return Whether app was launched from a notification.
	//------------------------------------------------------------------
	public static bool CheckAndConsumeWasLaunchedFromNotification()
	{
		using (AndroidJavaClass cls = new AndroidJavaClass (PLUGIN_CLASS_NAME))
		{
			return cls.CallStatic<bool> ("CheckAndConsumeWasLaunchedFromNotification");
		}
	}*/
}