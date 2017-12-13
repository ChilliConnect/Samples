using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using ChilliConnect;

public class SceneController : MonoBehaviour
{
	private const int POLL_WAIT_SECONDS = 5;

	private const string MESSAGE_STARTUP = "Starting Up";
	private const string MESSAGE_LOADING_DATA = "Loading Match Data";
	private const string MESSAGE_CREATING_ACCOUNT = "Creating Chilli Account";
	private const string MESSAGE_LOGGING_IN = "Logging In";
	private const string MESSAGE_MATCHMAKING = "Matchmaking";
	private const string MESSAGE_CHOOSE_SIDE = "Choose A Side";
	private const string MESSAGE_WAITING_OPPONENT = "Finding Opponent";
	private const string MESSAGE_TIMEOUT = "Match Timeout";
	private const string MESSAGE_OPPONENT_TURN = "Opponent's Turn";

	private const string GAME_TOKEN = "uKU6L4tB2c4AVusKPRddS2eAXASnWnfh";

    private ChilliConnectSdk m_chilliConnect = null;
	private MatchSystem m_matchSystem = new MatchSystem ();
	private AccountSystem m_accountSystem = new AccountSystem();

	private string m_chilliConnectId = string.Empty;

	public GameController gameController = null;

    private void Awake()
    {
		m_chilliConnect = new ChilliConnectSdk(GAME_TOKEN,true);

		m_matchSystem.Initialise (m_chilliConnect);
		m_matchSystem.OnMatchUpdated += OnMatchUpdated;
		m_matchSystem.OnMatchMakingStarted += OnMatchMakingStarted;
		m_matchSystem.OnMatchMakingFailed += OnMatchMakingFailed;
		m_matchSystem.OnMatchMakingSuceeded += OnMatchMakingSuceeded;
		m_matchSystem.OnNewMatchCreated += OnNewMatchCreated;
		m_matchSystem.OnMatchSavedOnServer += OnMatchSavedOnServer;

		m_accountSystem.Initialise (m_chilliConnect);
		m_accountSystem.OnPlayerLoggedIn += OnPlayerLoggedIn;
			
		gameController.ShowChilliInfoPanel (MESSAGE_LOGGING_IN);
		gameController.OnSideSelected += OnSideSelected;
		gameController.OnTurnEnded += OnTurnEnded;
		gameController.OnNewGameSelected += OnNewGameSelected;
    }

	private void OnPlayerLoggedIn(string chilliConnectId)
	{
		m_chilliConnectId = chilliConnectId;
		gameController.ShowChilliInfoPanel (MESSAGE_LOADING_DATA);
		m_matchSystem.SkillLevel = m_accountSystem.SkillLevel;
		m_matchSystem.LoadExistingOrFindNewGame (m_chilliConnectId);
	}

	public void OnNewGameSelected()
	{
		m_matchSystem.ClearMatchId ();
		gameController.RestartGame ();
		gameController.ShowChilliInfoPanel (MESSAGE_LOADING_DATA);
		m_matchSystem.LoadExistingOrFindNewGame (m_chilliConnectId);
	}

	public void OnNewMatchCreated(GameState state)
	{
		UpdateGameController (state);
		WaitThenRefreshMatchFromServer();
	}

	public void OnMatchSavedOnServer(GameState state)
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

	public void OnMatchMakingSuceeded(GameState state)
	{
		UpdateGameController (state);
	}

	public void OnTurnEnded(string nextPlayer, string boardState)
	{
		var matchState = m_matchSystem.CurrentGame;
		matchState.Board = boardState;
		if (!gameController.IsGameOver())
		{
			gameController.ShowChilliInfoPanel(MESSAGE_OPPONENT_TURN);
		}
		else 
		{
			matchState.MatchState = GameState.MATCHSTATE_COMPLETE;
		}

		m_matchSystem.SubmitTurn();
	}

	public void OnMatchUpdated(GameState updated, GameState previous)
	{
		UpdateGameController (updated);

		bool isWaitingOnOtherPlayer = !updated.IsPlayersTurn (m_chilliConnectId);
		if ( isWaitingOnOtherPlayer && !updated.IsFinished()) {
			WaitThenRefreshMatchFromServer ();
		}
	}

    public void OnDestroy()
	{
		m_chilliConnect.Dispose();
	}

	private void UpdateGameController(GameState state)
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
        else if (state.MatchState == GameState.MATCHSTATE_WAITING) 
		{
			gameController.ShowChilliInfoPanel (MESSAGE_WAITING_OPPONENT);
		} 
		else if (state.MatchState == GameState.MATCHSTATE_TIMEOUT)
		{
			gameController.Timeout();
		}
		else 
		{
			gameController.ShowChilliInfoPanel (MESSAGE_OPPONENT_TURN);
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
}