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
	private AccountSystem m_accountSystem = new AccountSystem ();

	private string m_chilliConnectId = string.Empty;

	public Match CurrentMatch { get; set; }

	public GameController gameController = null;
	public PhotonController photonController = null;

    private void Awake()
    {
		m_chilliConnect = new ChilliConnectSdk (GAME_TOKEN, false);

		PhotonNetwork.OnEventCall += this.OnEvent;

		CurrentMatch = new Match ();
		m_accountSystem.Initialise (m_chilliConnect);
		m_accountSystem.OnPlayerLoggedIn += OnPlayerLoggedIn;
		 
		photonController.Initialise (m_chilliConnect);
			
		gameController.ShowChilliInfoPanel (MESSAGE_LOGGING_IN);
		gameController.OnTurnEnded += OnTurnEnded;
    }

	private void OnPlayerLoggedIn(string chilliConnectId)
	{
		m_chilliConnectId = chilliConnectId;
		gameController.ShowChilliInfoPanel (MESSAGE_LOADING_DATA);

		photonController.LoadPhotonInstance ();
	}

	private void OnEvent(byte EventCode, object BoardState, int reliable)
	{
		if (EventCode == (byte) 0) {
			
			UnityEngine.Debug.Log ("Player Has just taken a turn! Board: " + (string)BoardState);
			gameController.SetBoardState ((string)BoardState);

			gameController.StartGame ();

		} else if (EventCode == (byte) 1) {
			
			UnityEngine.Debug.Log ("Other player has won, update board and display loss message: " + (string)BoardState);
			gameController.SetBoardState ((string)BoardState);

			gameController.IsGameOver ();
			gameController.HideChilliInfoPanel();

			CurrentMatch.MatchState = Match.MATCHSTATE_GAMEOVER;
		}
	}

	public void NewMatchCreated()
	{
		UnityEngine.Debug.Log ("Match state: " + CurrentMatch.MatchState);

		UpdateGameController (CurrentMatch);
	}

	public void OnTurnEnded(string boardState)
	{
		CurrentMatch.Board = boardState;

		if (!gameController.IsGameOver())
		{
			
			gameController.ShowChilliInfoPanel(MESSAGE_OPPONENT_TURN);
	
			byte evCode = 0;    //eventCode = 0 means that the player has taken a turn and the game continues
			string content = boardState;    
			bool reliable = true;
			PhotonNetwork.RaiseEvent(evCode, content, reliable, null);
		}
		else 
		{
			byte evCode = 1;    //eventCode = 1 means that the player has taken a turn and the game has ended, 
								//so forward to a webhook and notify other player
			string content = boardState;    
			bool reliable = true;
			PhotonNetwork.RaiseEvent(evCode, content, reliable, new RaiseEventOptions() { ForwardToWebhook = true });

			CurrentMatch.MatchState = Match.MATCHSTATE_GAMEOVER;
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

		if (gameController.IsGameOver() || state.IsPlayersTurn(m_chilliConnectId))
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
		
	public override void OnJoinedRoom()
	{
		UnityEngine.Debug.Log ("Current Player Count: " + PhotonNetwork.room.PlayerCount);
		if (PhotonNetwork.room.PlayerCount == 2)
		{

			Debug.Log("Photon Multiplayer - Room Found, Connected.");

			gameController.ShowChilliInfoPanel(MESSAGE_OPPONENT_TURN);
			gameController.SetLocalPlayerSide ("O");

			CurrentMatch.OccupyEmptyPlayerPosition (m_chilliConnectId);
		}
		else
		{
			Debug.Log("Waiting for another player");
			gameController.ShowChilliInfoPanel(MESSAGE_WAITING_OPPONENT);
		}
	}

	public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		Debug.Log("Photon Multiplayer - Player 2 has entered.");

		UnityEngine.Debug.Log ("Current Player Count: " + PhotonNetwork.room.PlayerCount);

		if (PhotonNetwork.room.PlayerCount == 2)
		{

			Debug.Log("Photon Multiplayer - Starting Turn for player X");

			gameController.SetLocalPlayerSide ("X");
			CurrentMatch.SetNewGame ("X", m_chilliConnectId);
			NewMatchCreated ();
		}
	}
}