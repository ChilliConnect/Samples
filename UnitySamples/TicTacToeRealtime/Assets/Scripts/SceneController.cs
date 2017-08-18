using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using ChilliConnect;

public class SceneController : MonoBehaviour
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
	private const string MESSAGE_OPPONENT_LEFT = "Opponent Has Left";

	private const string GAME_TOKEN = "ETS6a0CydqViNvOWqlH8U3ftcrmyeix7";

    private ChilliConnectSdk m_chilliConnect = null;
	private AccountSystem m_accountSystem = new AccountSystem ();

	private string m_chilliConnectId = string.Empty;

	public Match CurrentMatch { get; set; }

	public GameController gameController = null;
	public PhotonController photonController = null;
	public RoomController roomController = null;

    private void Awake()
    {
		m_chilliConnect = new ChilliConnectSdk (GAME_TOKEN, false);

		CurrentMatch = new Match ();

		m_accountSystem.Initialise (m_chilliConnect);
		m_accountSystem.OnPlayerLoggedIn += OnPlayerLoggedIn;
		 
		photonController.Initialise (m_chilliConnect);

		roomController.Initialise (m_chilliConnect);
		roomController.OnGameStart += OnGameStart;
		roomController.OnRoomJoin += OnRoomJoin;
		roomController.OnNextTurn += OnNextTurn;
		roomController.OnGameEnded += OnGameEnded;
		roomController.OnPlayerQuit += OnPlayerQuit;
			
		gameController.ShowChilliInfoPanel (MESSAGE_LOGGING_IN);
		gameController.OnTurnEnded += OnTurnEnded;
    }

	private void OnPlayerLoggedIn(string chilliConnectId)
	{
		m_chilliConnectId = chilliConnectId;

		gameController.ShowChilliInfoPanel (MESSAGE_LOADING_DATA);

		photonController.LoadPhotonInstance ();

		gameController.ShowChilliInfoPanel (MESSAGE_WAITING_OPPONENT);
	}

	public void NewMatchCreated()
	{
		UnityEngine.Debug.Log ("Match state: New Match Created");

		UpdateGameController (CurrentMatch);
	}

	public void OnTurnEnded(string boardState)
	{
		CurrentMatch.Board = boardState;

		if (!gameController.IsGameOver())
		{
			gameController.ShowChilliInfoPanel(MESSAGE_OPPONENT_TURN);
			roomController.TriggerEvent (0, boardState, false);
		}
		else 
		{
			roomController.TriggerEvent (1, boardState, true);
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

	public void OnGameStart()
	{
		gameController.SetLocalPlayerSide ("X");
		CurrentMatch.SetNewGame ("X", m_chilliConnectId);
		NewMatchCreated ();
	}

	public void OnRoomJoin()
	{
		gameController.ShowChilliInfoPanel(MESSAGE_OPPONENT_TURN);
		gameController.SetLocalPlayerSide ("O");

		CurrentMatch.OccupyEmptyPlayerPosition (m_chilliConnectId);
	}	
		
	public void OnNextTurn(string boardState)
	{
		gameController.SetBoardState (boardState);
		gameController.StartGame ();
	}	

	public void OnGameEnded(string boardState)
	{
		gameController.SetBoardState (boardState);

		gameController.IsGameOver ();
		gameController.HideChilliInfoPanel();

		CurrentMatch.MatchState = Match.MATCHSTATE_GAMEOVER;
	}	

	public void OnPlayerQuit()
	{
		gameController.RestartGame ();
		gameController.ShowChilliInfoPanel(MESSAGE_OPPONENT_LEFT);
	}
}