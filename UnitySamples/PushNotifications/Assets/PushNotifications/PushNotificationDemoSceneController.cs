using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using ChilliConnect;

//------------------------------------------------------------------
/// The main controller class for the Push Notification demo.
//------------------------------------------------------------------
public class PushNotificationDemoSceneController : MonoBehaviour
{
    private const string k_chilliConnectToken = "GVDOaMiUayWiGmoLNkaasrr6nKGIwLNz ";
#if UNITY_ANDROID
    private const string k_senderID = "658616921793";
    private const string k_pushService = "GCM";
#elif UNITY_IOS
	private const string k_pushService = "APNS";
#else
    private const string k_pushService = "";
#endif

    public enum AppStates { NONE =-1, WAIT, CREATE_PLAYERS, LOGIN, REGISTER_PUSH_NOTIFICATIONS }

    private AppStates m_appState = AppStates.NONE;

    private ChilliConnectSdk m_chilliConnect;

    private string m_chilliConnectId = string.Empty;
    private string m_chilliConnectSecret = string.Empty;

    private string m_deviceIdentifier = string.Empty;

	//------------------------------------------------------------------
	/// Initilisation that creates an instance of ChilliConnect and subscribes
    /// to Push Notification listener events.
	//------------------------------------------------------------------
    private void Awake()
    {
        m_chilliConnect = new ChilliConnectSdk(k_chilliConnectToken, false);

#if UNITY_ANDROID && !UNITY_EDITOR
        GooglePushNotificationHandler.SetRecieverGameObject("GooglePushNotificationListener");
		GameObject.FindObjectOfType<GooglePushNotificationListener>().RegistrationSucceededEvent = OnPluginRegistered;
		GameObject.FindObjectOfType<GooglePushNotificationListener>().RegistrationFailedEvent = OnPluginFailed;
#elif UNITY_IOS

		GameObject.FindObjectOfType<iOSPushNotificationListener>().RegistrationSucceededEvent = OnPluginRegistered;
		GameObject.FindObjectOfType<iOSPushNotificationListener>().RegistrationFailedEvent = OnPluginFailed;
#endif
    }

    //------------------------------------------------------------------
    private void Start()
    {
        ChangeAppState(AppStates.CREATE_PLAYERS);
    }

	//------------------------------------------------------------------
    private void ChangeAppState(AppStates in_newState)
    {
        m_appState = in_newState;
    }
    //------------------------------------------------------------------
    /// Attempts to create a player and on success attempts to log that 
    /// player into ChilliConnect.
    //------------------------------------------------------------------
    private void CreatePlayer ()
    {
        Action<CreatePlayerRequest, CreatePlayerResponse> successCallback = (CreatePlayerRequest request, CreatePlayerResponse response) =>
        {
            m_chilliConnectId = response.ChilliConnectId;
            m_chilliConnectSecret = response.ChilliConnectSecret;

            UnityEngine.Debug.Log("Player created with ChilliConnectId: " + m_chilliConnectId);

            ChangeAppState(AppStates.LOGIN);
        };

        Action<CreatePlayerRequest, CreatePlayerError> errorCallback = (CreatePlayerRequest request, CreatePlayerError error) =>
        {
            UnityEngine.Debug.Log("An error occurred while creating a new player: " + error.ErrorDescription);
        };

        var requestDesc = new CreatePlayerRequestDesc();
        requestDesc.DisplayName = "TestPlayer";

        var playerAccounts = m_chilliConnect.PlayerAccounts;
        playerAccounts.CreatePlayer(requestDesc, successCallback, errorCallback);
    }
	//------------------------------------------------------------------
	/// Attempts to login to ChilliConnect. On a successful login attempts
    /// to transition to Register Push notifications state.
	//------------------------------------------------------------------
    private void LoginPlayer()
    {
        Action<LogInUsingChilliConnectRequest> successCallback = (LogInUsingChilliConnectRequest request) =>
        {
            UnityEngine.Debug.Log("Successfully logged in!");

            ChangeAppState(AppStates.REGISTER_PUSH_NOTIFICATIONS);
        };

        Action<LogInUsingChilliConnectRequest, LogInUsingChilliConnectError> errorCallback = (LogInUsingChilliConnectRequest request, LogInUsingChilliConnectError error) =>
        {
            UnityEngine.Debug.Log("An error occurred while logging in: " + error.ErrorDescription);
        };

        var playerAccounts = m_chilliConnect.PlayerAccounts;
        playerAccounts.LogInUsingChilliConnect(m_chilliConnectId, m_chilliConnectSecret, successCallback, errorCallback);
    }

    #region Push Notifications
	//------------------------------------------------------------------
	/// Requests a Device Identifier for Push Notifications.
	//------------------------------------------------------------------
    private void RegisterForPushNotification()
    {
		UnityEngine.Debug.Log("PushNotificationDemoSceneController : RegisterForPushNotification - Attempting to register!");

#if UNITY_ANDROID
		GooglePushNotificationHandler.GenerateDeviceID(k_senderID);
		GooglePushNotificationHandler.SetNotificationIcon("notificationicon");
#elif UNITY_IOS
        iOSPushNotificationHandler.Register();
#endif
    }
    //------------------------------------------------------------------
    /// Callback for succeeding to request a Push Notification Device ID.
    /// Attempts to register Push Notifications with ChilliConnect.
    /// 
    /// @param in_deviceID - Device Identifier.
    //------------------------------------------------------------------
    private void OnPluginRegistered(string in_deviceID)
    {
		m_deviceIdentifier = in_deviceID;
		
        Action<RegisterTokenRequest> successCallback = (RegisterTokenRequest request) =>
        {
			UnityEngine.Debug.Log("PushNotificationDemoSceneController : OnPluginRegistered - Success!");
        };

        Action<RegisterTokenRequest, RegisterTokenError> errorCallback = (RegisterTokenRequest request, RegisterTokenError error) =>
        {
            UnityEngine.Debug.Log(string.Format("An error occurred while Registering Push Notifications: {0} - Device Token -", error.ErrorDescription));
        };

        RegisterTokenRequestDesc desc = new RegisterTokenRequestDesc(k_pushService, m_deviceIdentifier);

        ChilliConnect.PushNotifications pushNotificationModule = m_chilliConnect.PushNotifications;
        pushNotificationModule.RegisterToken(desc, successCallback, errorCallback);
    }
	//------------------------------------------------------------------
	/// Callback for failing to register for push notifications.
	//------------------------------------------------------------------
    private void OnPluginFailed(string in_error)
    {
        UnityEngine.Debug.Log("An error occurred while REgistering for Push Notifications: " + in_error);
    }

    #endregion
	
	//------------------------------------------------------------------
	/// Handles actions to perform on states.
	//------------------------------------------------------------------
	void Update ()
    {
		switch(m_appState)
        {
            case AppStates.LOGIN:
                {
                    LoginPlayer();
                    ChangeAppState(AppStates.WAIT);
                }
                break;
            case AppStates.CREATE_PLAYERS:
                {
                    CreatePlayer();
                    ChangeAppState(AppStates.WAIT);
                }
                break;
            case AppStates.REGISTER_PUSH_NOTIFICATIONS:
                {
                    RegisterForPushNotification();
                    ChangeAppState(AppStates.WAIT);
                }
                break;
            default:
                break;
        }
	}
	//------------------------------------------------------------------
	/// Cleanup ChilliConnect.
	//------------------------------------------------------------------
    private void OnApplicationQuit()
    {
        m_chilliConnect.Dispose();
    }
}
