#if UNITY_IPHONE
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//------------------------------------------------------------------
/// Wrapper around the Unity iOS Push Notification service.
//------------------------------------------------------------------
public static class iOSPushNotificationHandler
{
	private static bool s_registrationPending = false;

	//------------------------------------------------------------------
	/// Register for push notifications on iOS, this has no call-backs
	/// so have to poll for result in iOSPushNotificationListener.
	//------------------------------------------------------------------
	public static void Register (params string[] senderIds) 
	{
		UnityEngine.iOS.NotificationServices.RegisterForNotifications(UnityEngine.iOS.NotificationType.Alert | UnityEngine.iOS.NotificationType.Badge | UnityEngine.iOS.NotificationType.Sound);
		s_registrationPending = true;
	}

	//------------------------------------------------------------------
	public static void ConsumePendingRegistration()
	{
		UnityEngine.Debug.Log("iOSMessagingPluginHandler : ConsumePendingRegistration - Consuming pending bool flag!");
		s_registrationPending = false;
	}

	//------------------------------------------------------------------
	public static bool IsRegistrationPending()
	{
		return s_registrationPending;
	}
}
#endif