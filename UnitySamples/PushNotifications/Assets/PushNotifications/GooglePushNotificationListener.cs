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
	public System.Action<String> RegistrationFailedEvent;

    //------------------------------------------------------------------
    private void Awake()
    {
        GooglePushNotificationHandler.SetRecieverGameObject(name);
    }

#region Broadcasts
    //------------------------------------------------------------------
    /// On Registration Error.
    /// 
    /// @param in_message - The message form the plugin
    //------------------------------------------------------------------
    void OnRegistrationError(string in_message)
	{
		RegistrationFailedEvent(in_message);
		UnityEngine.Debug.Log ("GoogleCloudMessagingListener : OnRegistrationError: " + in_message);
	}
	//------------------------------------------------------------------
	/// On Message received.
	///
	/// @param in_message - The message form the plugin 
	//------------------------------------------------------------------
	void OnMessage (string in_message)
	{
		UnityEngine.Debug.Log("Message: " + in_message);
	}
	//------------------------------------------------------------------
	/// On OnRegistered.
	/// 
	/// @param registrationId - The Registration ID
	//------------------------------------------------------------------
	void OnRegistered (string in_deviceID)
	{
		RegistrationSucceededEvent(in_deviceID);
		UnityEngine.Debug.Log ("GoogleCloudMessagingListener : OnRegistered: " + in_deviceID);
	}
#endregion
}
#endif