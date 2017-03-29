using UnityEngine;
using System.Collections;

//------------------------------------------------------------------
/// Controls the Google Cloud Messaging plugin.
//------------------------------------------------------------------
public static class GooglePushNotificationHandler
{
	//--- Name of Java class Unity interacts with for registering
	private const string PLUGIN_CLASS_NAME = "com.chilliexamplecloudmessaging.unitygcmplugin.UnityGCMHandler";

    //------------------------------------------------------------------
    public static void SetRecieverGameObject(string in_name)
    {
		using (AndroidJavaClass cls = new AndroidJavaClass(PLUGIN_CLASS_NAME))
        {
            cls.CallStatic("SetRecieverGameObject", in_name);
        }
    }

    //------------------------------------------------------------------
    ///	Register the Sender IDs
    ///
    /// @param senderIds - params - The sender IDs
    //------------------------------------------------------------------
	public static void GenerateDeviceID (params string[] senderIds) 
	{
		string senderIdsStr = string.Join (",", senderIds);
		Debug.Log("GoogleCloudMessagingPluginHandler : Register : Registering IDS: " + senderIdsStr);
		
		using (AndroidJavaClass cls = new AndroidJavaClass (PLUGIN_CLASS_NAME))
		{
			cls.CallStatic ("GenerateDeviceID", senderIdsStr);
		}
	}

	//------------------------------------------------------------------
	///	Register the Sender IDs
	///
	/// @param senderIds - params - The sender IDs
	//------------------------------------------------------------------
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
	}
}