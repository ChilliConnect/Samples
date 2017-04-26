using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

using ChilliConnect;

/// Controller for all interaction with Chilli Connect
/// 
public class SceneController : MonoBehaviour
{
	private const string MESSAGE_STARTUP = "Starting Up";
	private const string MESSAGE_LOADING_DATA = "Loading Player Data";
	private const string MESSAGE_CREATING_ACCOUNT = "Creating Chilli Account";
	private const string MESSAGE_LOGGING_IN = "Logging In";
	private const string MESSAGE_MATCHMAKING = "Matchmaking";
	private const string MESSAGE_CHOOSE_SIDE = "Choose A Side";
	private const string MESSAGE_WAITING_OPPONENT = "Finding Opponent";
	private const string MESSAGE_OPPONENT_TURN = "Opponent's Turn";

	private const string GAME_TOKEN = "Vv7VANzImRtEUeiYaoz4lWKqB6t349iy";

    private ChilliConnectSdk m_chilliConnect = null;
	private MatchSystem m_matchSystem = new MatchSystem ();
	private AccountSystem m_accountSystem = new AccountSystem();
	private PushSystem m_pushSystem = new PushSystem();

	private string m_chilliConnectId = string.Empty;

	public GameController gameController = null;

	#if UNITY_ANDROID
	private const string k_pushService = "GCM";

	//Daves
	//private const string k_senderID = "658616921793";

	//GCM Test App
	private const string k_senderID = "182016186583";


	private const string PLUGIN_CLASS_NAME = "com.chilliexamplecloudmessaging.unitygcmplugin.UnityGCMHandler";
	#elif UNITY_IOS
	private const string k_pushService = "APNS";
	#else
	private const string k_pushService = "";
	#endif 

	private string m_deviceIdentifier = string.Empty;

    /// Initialization
	/// 
    private void Awake()
    {
		m_chilliConnect = new ChilliConnectSdk(GAME_TOKEN,false);

#if UNITY_ANDROID && !UNITY_EDITOR
		GooglePushNotificationHandler.SetRecieverGameObject("GooglePushNotificationListener");
		GameObject.FindObjectOfType<GooglePushNotificationListener>().RegistrationSucceededEvent = OnPluginRegistered;
		GameObject.FindObjectOfType<GooglePushNotificationListener>().RegistrationFailedEvent = OnPluginFailed;
#elif UNITY_IOS
		GameObject.FindObjectOfType<iOSPushNotificationListener>().RegistrationSucceededEvent = OnPluginRegistered;
		GameObject.FindObjectOfType<iOSPushNotificationListener>().RegistrationFailedEvent = OnPluginFailed;
#endif

		m_matchSystem.Initialise (m_chilliConnect);
		m_matchSystem.OnMatchUpdated += OnMatchUpdated;
		m_matchSystem.OnMatchMakingStarted += OnMatchMakingStarted;
		m_matchSystem.OnMatchMakingFailed += OnMatchMakingFailed;
		m_matchSystem.OnMatchMakingSuceeded += OnMatchMakingSucceeded;
		m_matchSystem.OnNewMatchCreated += OnNewMatchCreated;
		m_matchSystem.OnMatchSavedOnServer += OnMatchSavedOnServer;

		m_accountSystem.Initialise (m_chilliConnect);
		m_accountSystem.OnPlayerLoggedIn += OnPlayerLoggedIn;

		gameController.ShowChilliInfoPanel (MESSAGE_LOGGING_IN);
		gameController.onSideSelected += OnSideSelected;
		gameController.onTurnEnded += OnTurnEnded;
		gameController.OnNewGameSelected += OnNewGameSelected;
    }

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

	private void OnPluginFailed(string in_error)
	{
		UnityEngine.Debug.Log("An error occurred while REgistering for Push Notifications: " + in_error);
	}

	public void OnPlayerLoggedIn(string chilliConnectId)
	{
		m_chilliConnectId = chilliConnectId;
		gameController.ShowChilliInfoPanel (MESSAGE_LOADING_DATA);
		m_matchSystem.LoadExistingOrFindNewGame (m_chilliConnectId);
	}

	public void OnNewGameSelected()
	{
		m_matchSystem.ClearMatchId ();
		gameController.RestartGame ();
		gameController.ShowChilliInfoPanel (MESSAGE_CHOOSE_SIDE, false);
	}

	public void OnNewMatchCreated(MatchState state)
	{
		UpdateGameController (state);
	}

	public void OnMatchSavedOnServer(MatchState state)
	{
		if (!state.IsPlayersTurn(m_chilliConnectId) && state.IsWaitingForTurn()) {
			WaitThenRefreshMatchFromServer();
		}
	}

	public void OnMatchMakingStarted ()
	{
		gameController.ShowChilliInfoPanel (MESSAGE_MATCHMAKING);
	}

	public void OnMatchMakingFailed()
	{
		gameController.ShowChilliInfoPanel (MESSAGE_CHOOSE_SIDE, false);
	}

	public void OnMatchMakingSucceeded(MatchState state)
	{
		UpdateGameController (state);
	}

	public void OnTurnEnded(string nextPlayer, string boardState)
	{
		var matchState = m_matchSystem.CurrentMatch;
		matchState.m_board = boardState;
		if (!gameController.IsGameOver())
		{
			matchState.SwitchTurn (nextPlayer);

			m_pushSystem.Initialise (m_chilliConnect);
			string opponentid = matchState.GetOpponentId (m_chilliConnectId);

			m_pushSystem.SendPush(opponentid);

			gameController.ShowChilliInfoPanel(MESSAGE_OPPONENT_TURN);
		}
		else 
		{
			matchState.m_matchState = MatchState.MATCHSTATE_GAMEOVER;
		}

		m_matchSystem.SaveMatchOnServer();
	}

	public void OnMatchUpdated(MatchState updated, MatchState previous)
	{
		if (!updated.m_board.Equals (previous.m_board)) {
			UpdateGameController (updated);
		} 

		if (!updated.IsPlayersTurn (m_chilliConnectId) && updated.IsWaitingForTurn ()) {
			WaitThenRefreshMatchFromServer ();
		}
	}

    public void OnDestroy()
	{
		m_chilliConnect.Dispose();
	}

	private void UpdateGameController(MatchState state)
	{
		UnityEngine.Debug.Log ("UpdateGameContoller, State:" + state.m_matchState);
		gameController.SetLocalPlayerSide (state.GetPlayerSide(m_chilliConnectId));
        gameController.StartGame();
        gameController.SetBoardState(state.m_board);

        if (gameController.IsGameOver())
        {
			UnityEngine.Debug.Log ("UpdateGameContoller, GameOver");
			m_matchSystem.SetGameComplete ();
            gameController.HideChilliInfoPanel();
			gameController.SetNewGameButton (true);
        }
        else if (state.IsPlayersTurn(m_chilliConnectId))
        {
			UnityEngine.Debug.Log ("UpdateGameContoller, IsPlayersTurns");
            gameController.HideChilliInfoPanel();
        }
        else
        {
			if (state.m_matchState == MatchState.MATCHSTATE_WAITING) {
				UnityEngine.Debug.Log ("UpdateGameContoller, waiting");
				gameController.ShowChilliInfoPanel (MESSAGE_WAITING_OPPONENT);
			} else {
				UnityEngine.Debug.Log ("UpdateGameContoller, opponents turn");
				gameController.ShowChilliInfoPanel (MESSAGE_OPPONENT_TURN);
			}
        }
	}

	private void OnSideSelected(string selectedSide)
    {
		gameController.ShowChilliInfoPanel(MESSAGE_WAITING_OPPONENT);
		m_matchSystem.CreateNewGame (selectedSide);
    }
 
    private void WaitThenRefreshMatchFromServer()
    {
		StartCoroutine(DoSomethingAfterWait(3, m_matchSystem.RefreshMatchFromServer));
    }

    IEnumerator DoSomethingAfterWait(float wait, System.Action callBack)
    {
        yield return new WaitForSeconds(wait);
		callBack.Invoke();
    }
}