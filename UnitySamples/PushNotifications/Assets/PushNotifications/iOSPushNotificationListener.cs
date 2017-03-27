#if UNITY_IPHONE
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//------------------------------------------------------------------
/// Listener for iOS Push Notification events.
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
					UnityEngine.Debug.Log ("iOSPushNotificationListener : OnRegistered: Stabdard Token - " + System.BitConverter.ToString(token).Replace("-", "").ToLower());
					string tokenString = PushNotifications.PushNotificationUtils.Base64Encode(token);

					UnityEngine.Debug.Log ("iOSPushNotificationListener : OnRegistered: Base64 Token - " + tokenString);

					iOSPushNotificationHandler.ConsumePendingRegistration();
					RegistrationSucceededEvent(tokenString);
	            }
            }
        }
	}
}
#endif