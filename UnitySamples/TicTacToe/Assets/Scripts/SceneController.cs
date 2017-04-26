using System.Collections;
using System.Collections.Generic;
using System.IO;
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

	private string m_chilliConnectId = string.Empty;

	public GameController gameController = null;

    /// Initialization
	/// 
    private void Awake()
    {
		m_chilliConnect = new ChilliConnectSdk(GAME_TOKEN,false);

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
		gameController.OnSideSelected += OnSideSelected;
		gameController.onTurnEnded += OnTurnEnded;
		gameController.OnNewGameSelected += OnNewGameSelected;
    }

	private void OnPlayerLoggedIn(string chilliConnectId)
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

	public void OnNewMatchCreated(Match state)
	{
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
		gameController.ShowChilliInfoPanel (MESSAGE_CHOOSE_SIDE, false);
	}

	public void OnMatchMakingSucceeded(Match state)
	{
		UpdateGameController (state);
		gameController.HideChilliInfoPanel ();
	}

	public void OnTurnEnded(string nextPlayer, string boardState)
	{
		var matchState = m_matchSystem.CurrentMatch;
		matchState.Board = boardState;
		if (!gameController.IsGameOver())
		{
			matchState.SwitchTurn (nextPlayer);
			gameController.ShowChilliInfoPanel(MESSAGE_OPPONENT_TURN);
		}
		else 
		{
			matchState.MatchState = Match.MATCHSTATE_GAMEOVER;
		}

		m_matchSystem.SaveMatchOnServer();
	}

	public void OnMatchUpdated(Match updated, Match previous)
	{
		if (!updated.Board.Equals (previous.Board)) {
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

	private void UpdateGameController(Match state)
	{
		gameController.SetLocalPlayerSide (state.GetPlayerSide(m_chilliConnectId));
        gameController.StartGame();
		gameController.SetBoardState(state.Board);

        if (gameController.IsGameOver())
        {
			m_matchSystem.SetGameComplete ();
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
		StartCoroutine(DoSomethingAfterWait(3, m_matchSystem.RefreshMatchFromServer));
    }

    IEnumerator DoSomethingAfterWait(float wait, System.Action callBack)
    {
        yield return new WaitForSeconds(wait);
		callBack.Invoke();
    }
}