using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

	public const string SENDER_ID = "914054364243";

	private GcmPushHandler gcmPushHandler;

	public void Start () {
		gcmPushHandler = transform.FindChild ("GcmPushHandler").GetComponent<GcmPushHandler> ();
		gcmPushHandler.OnTokenRecieved += OnToken;
		gcmPushHandler.GetDeviceToken (SENDER_ID);
	}

	public void Awake(){
		
	}

	public void OnToken(string deviceToken) {
		Debug.Log ("Token in unity:" + deviceToken);
	}
}

