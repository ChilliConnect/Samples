using UnityEngine;
using System.Collections;

//------------------------------------------------------------------
/// Controls the Google Cloud Messaging plugin.
//------------------------------------------------------------------
public static class GooglePushNotificationHandler
{
	//--- Name of Java class Unity interacts with for registering
	private const string PLUGIN_CLASS_NAME = "com.chilliconnect.unitygcmplugin.ChilliConnectGcmPlugin";

    //------------------------------------------------------------------
    public static void SetRecieverGameObject(string in_name)
    {
		using (AndroidJavaClass cls = new AndroidJavaClass(PLUGIN_CLASS_NAME))
        {
			cls.CallStatic("SetReceiverGameObject", in_name);
        }
    }

    //------------------------------------------------------------------
    ///	Register the Sender IDs
    ///
    /// @param senderIds - params - The sender IDs
    //------------------------------------------------------------------
	public static void GenerateDeviceID (params string senderId) 
	{
		Debug.Log("GoogleCloudMessagingPluginHandler : Register : Registering ID: " + senderId);
		
		using (AndroidJavaClass cls = new AndroidJavaClass (PLUGIN_CLASS_NAME))
		{
			cls.CallStatic ("GenerateDeviceID", senderId);
		}
	}

}