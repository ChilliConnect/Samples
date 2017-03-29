using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PushNotifications
{
	//------------------------------------------------------------------
	public static class PushNotificationUtils
	{
		//------------------------------------------------------------------
		public static string Base64Encode(byte[] in_bytes)
		{
			return System.Convert.ToBase64String(in_bytes);
		}
	}
}