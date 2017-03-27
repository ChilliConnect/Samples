#if UNITY_IPHONE
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//------------------------------------------------------------------
///  Listener for iOS Push Notification events.
//------------------------------------------------------------------
public class iOSPushNotificationListener : MonoBehaviour
{
	public System.Action<String> RegistrationSucceededEvent;
	public System.Action<String> RegistrationFailedEvent;

    //------------------------------------------------------------------
    /// If a registration Token request is pending then attempt
    /// to retrieve the token.
    //------------------------------------------------------------------
    private void FixedUpdate()
	{
		if (iOSPushNotificationHandler.IsRegistrationPending() == true)
		{
			if (UnityEngine.iOS.NotificationServices.registrationError != null)
			{
				UnityEngine.Debug.Log("NotificationServices.registrationError: " + UnityEngine.iOS.NotificationServices.registrationError);
				iOSPushNotificationHandler.ConsumePendingRegistration();
            }
            else
            {
				byte[] token = UnityEngine.iOS.NotificationServices.deviceToken;
	            if(token != null)
	            {
					string tokenString =  System.BitConverter.ToString(token).Replace("-", "").ToLower();

					UnityEngine.Debug.Log ("iOSPushNotificationListener : OnRegistered: tokenString - " + tokenString);

					iOSPushNotificationHandler.ConsumePendingRegistration();
					RegistrationSucceededEvent(tokenString);
	            }
            }
        }

		for(int i=0; i < UnityEngine.iOS.NotificationServices.remoteNotificationCount; ++i)
		{
			UnityEngine.Debug.LogFormat("Remote Notification: {0}", UnityEngine.iOS.NotificationServices.GetRemoteNotification(i));
		}
	}
}
#endif