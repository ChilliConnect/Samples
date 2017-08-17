using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using ChilliConnect;

public class SceneController : Photon.PunBehaviour
{
	private const int POLL_WAIT_SECONDS = 5;
	private const string MESSAGE_STARTUP = "Starting Up";
	private const string MESSAGE_LOADING_DATA = "Loading Player Data";
	private const string MESSAGE_CREATING_ACCOUNT = "Creating Chilli Account";
	private const string MESSAGE_LOGGING_IN = "Logging In";
	private const string MESSAGE_MATCHMAKING = "Matchmaking";
	private const string MESSAGE_CHOOSE_SIDE = "Choose A Side";
	private const string MESSAGE_WAITING_OPPONENT = "Finding Opponent";
	private const string MESSAGE_OPPONENT_TURN = "Opponent's Turn";

	private const string GAME_TOKEN = "ETS6a0CydqViNvOWqlH8U3ftcrmyeix7";

    private ChilliConnectSdk m_chilliConnect = null;
	private MatchSystem m_matchSystem = new MatchSystem ();
	private AccountSystem m_accountSystem = new AccountSystem ();

	private string m_chilliConnectId = string.Empty;

	public GameController gameController = null;
	public PhotonController photonController = null;

    private void Awake()
    {
		m_chilliConnect = new ChilliConnectSdk (GAME_TOKEN, false);

		PhotonNetwork.OnEventCall += this.OnEvent;

		m_matchSystem.Initialise (m_chilliConnect);
		m_matchSystem.OnMatchUpdated += OnMatchUpdated;
		m_matchSystem.OnMatchMakingStarted += OnMatchMakingStarted;
		m_matchSystem.OnMatchMakingFailed += OnMatchMakingFailed;
		m_matchSystem.OnMatchMakingSuceeded += OnMatchMakingSuceeded;
		m_matchSystem.OnNewMatchCreated += OnNewMatchCreated;
		m_matchSystem.OnMatchSavedOnServer += OnMatchSavedOnServer;

		m_accountSystem.Initialise (m_chilliConnect);
		m_accountSystem.OnPlayerLoggedIn += OnPlayerLoggedIn;
		 
		photonController.Initialise (m_chilliConnect);
		photonController.OnPhotonConnect += OnPhotonConnect;
			
		gameController.ShowChilliInfoPanel (MESSAGE_LOGGING_IN);
		gameController.OnSideSelected += OnSideSelected;
		gameController.OnTurnEnded += OnTurnEnded;
		gameController.OnNewGameSelected += OnNewGameSelected;
    }

	private void OnPlayerLoggedIn(string chilliConnectId)
	{
		m_chilliConnectId = chilliConnectId;
		gameController.ShowChilliInfoPanel (MESSAGE_LOADING_DATA);
		photonController.LoadPhotonInstance (m_chilliConnectId);
	}

	private void OnEvent(byte EventCode, object BoardState, int reliable)
	{
		if (EventCode == (byte)0) {
			UnityEngine.Debug.Log ("Player Has just taken a turn! Board: " + (string)BoardState);
			gameController.SetBoardState ((string)BoardState);
			gameController.StartGame ();
		}
	}

	private void OnPhotonConnect()
	{
		//TODO: Rename this to just load the chilliId
		m_matchSystem.LoadExistingOrFindNewGame (m_chilliConnectId);
	}

	public void OnNewGameSelected()
	{
		m_matchSystem.ClearMatchId ();
		gameController.RestartGame ();
		gameController.ShowChilliInfoPanel (MESSAGE_LOADING_DATA);
		m_matchSystem.LoadExistingOrFindNewGame (m_chilliConnectId);
	}

	public void OnNewMatchCreated(Match state)
	{
		UnityEngine.Debug.Log ("Match state: " + state.MatchState);
		UpdateGameController (state);
	}

	public void OnMatchSavedOnServer(Match state)
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
		gameController.ShowChilliInfoPanel (MESSAGE_WAITING_OPPONENT, false);
	}

	public void OnMatchMakingSuceeded(Match state)
	{
		UpdateGameController (state);
	}

	public void OnTurnEnded(string boardState)
	{
		var matchState = m_matchSystem.CurrentMatch;

		matchState.Board = boardState;

		if (!gameController.IsGameOver())
		{
			gameController.ShowChilliInfoPanel(MESSAGE_OPPONENT_TURN);

			byte evCode = 0;    // my event 0. could be used as "group units"
			string content = boardState;    
			bool reliable = true;
			PhotonNetwork.RaiseEvent(evCode, content, reliable, null);
		}
		else 
		{
			byte evCode = 1;    // my event 0. could be used as "group units"
			string content = m_chilliConnectId;    
			bool reliable = true;
			PhotonNetwork.RaiseEvent(evCode, content, reliable, new RaiseEventOptions() { ForwardToWebhook = true });

			matchState.MatchState = Match.MATCHSTATE_GAMEOVER;
		}
	}

	public void OnMatchUpdated(Match updated, Match previous)
	{
		bool isWaitingOnOpponent = updated.MatchState == Match.MATCHSTATE_WAITING;
		if (!updated.Board.Equals (previous.Board)|| isWaitingOnOpponent ) {
			UpdateGameController (updated);
		}

		bool isWaitingOnOtherPlayer = !updated.IsPlayersTurn (m_chilliConnectId) && updated.IsWaitingForTurn ();
		if ( isWaitingOnOtherPlayer || isWaitingOnOpponent ) {
			WaitThenRefreshMatchFromServer ();
		}
	}

    public void OnDestroy()
	{
		m_chilliConnect.Dispose();
	}

	private void UpdateGameController(Match state)
	{
		gameController.SetLocalPlayerSide (state.GetPlayerSide(m_chilliConnectId));
        gameController.StartGame();
		gameController.SetBoardState(state.Board);

        if (gameController.IsGameOver())
        {
            gameController.HideChilliInfoPanel();
			gameController.SetNewGameButton (true);
        }
        else if (state.IsPlayersTurn(m_chilliConnectId))
        {
            gameController.HideChilliInfoPanel();
        }
        else
        {
			if (state.MatchState == Match.MATCHSTATE_WAITING) {
				gameController.ShowChilliInfoPanel (MESSAGE_WAITING_OPPONENT);
			} else {
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
		StartCoroutine(DoSomethingAfterWait(POLL_WAIT_SECONDS, m_matchSystem.RefreshMatchFromServer));
    }

    IEnumerator DoSomethingAfterWait(float wait, System.Action callBack)
    {
        yield return new WaitForSeconds(wait);
		callBack.Invoke();
    }

	public override void OnJoinedRoom()
	{
		UnityEngine.Debug.Log ("Current Player Count: " + PhotonNetwork.room.PlayerCount);
		if (PhotonNetwork.room.PlayerCount == 2)
		{
			Debug.Log("Room Found, Connected.");

			//I should be circle and second to go
			gameController.ShowChilliInfoPanel(MESSAGE_OPPONENT_TURN);
			m_matchSystem.OccupyVacantPlayerPosition ();
			gameController.SetLocalPlayerSide ("O");
		}
		else
		{
			gameController.ShowChilliInfoPanel(MESSAGE_WAITING_OPPONENT);
			Debug.Log("Waiting for another player");
		}
	}

	public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		Debug.Log("Other player arrived");
		UnityEngine.Debug.Log ("Current Player Count: " + PhotonNetwork.room.PlayerCount);

		if (PhotonNetwork.room.PlayerCount == 2)
		{
			Debug.Log("Player has joined. Start Turns.");
			//I should be cross and first to go

			gameController.SetLocalPlayerSide ("X");
			m_matchSystem.CreateNewGame ("X");

		}
	}
}