#if !UNITY_IPHONE && UNITY_ANDROID
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//------------------------------------------------------------------
/// Listener for Google Cloud Messaging events.
//------------------------------------------------------------------
public class GooglePushNotificationListener : MonoBehaviour
{
	public System.Action<String> RegistrationSucceededEvent;

    //------------------------------------------------------------------
    private void Awake()
    {
        GooglePushNotificationHandler.SetRecieverGameObject(name);
    }

#region Broadcasts
    
	//------------------------------------------------------------------
	/// On successfully generated a Device ID.
	/// 
	/// @param in_deviceID - The Device ID
	//------------------------------------------------------------------
	void OnGeneratedDeviceID (string in_deviceID)
	{
		RegistrationSucceededEvent(in_deviceID);
		UnityEngine.Debug.Log ("GoogleCloudMessagingListener : OnRegistered: " + in_deviceID);
	}
#endregion
}
#endif