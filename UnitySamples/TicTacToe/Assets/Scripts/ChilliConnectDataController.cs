using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using ChilliConnect;

/// Controller for all interaction with Chilli Connect
/// 
public class ChilliConnectDataController : MonoBehaviour
{
	public delegate void SetGameState(ChilliConnectGameState gameState);
	public event SetGameState onGameStateRetrievedFromServer;
	public delegate void LogInDelegate();
	public event LogInDelegate onReadyToLogIn;

    private const string k_matchState_Complete = "COMPLETE";
    private const string k_matchState_WaitingForPlayers = "WAITING_FOR_PLAYERS";
    private const string k_matchState_xPlayerTurn = "X_PLAYER_TURN";
    private const string k_matchState_oPlayerTurn = "O_PLAYER_TURN";
    private const string k_matchState_GameOver = "GAME_OVER";

    private const string k_newGameQueryFormat = "Value.MatchState = \"{0}\""; 
    private const string k_activeGameQueryFormat = "Value.MatchState != \"{0}\" AND (Value.PlayerX = \"{1}\" OR Value.PlayerO = \"{1}\")";

	private const string k_message_startingUp = "Starting Up";
	private const string k_message_loadingPlayerData = "Loading Player Data";
	private const string k_message_creatingChilliAccount = "Creating Chilli Account";
	private const string k_message_loggingIn = "Logging In";
    private const string k_message_matchmaking = "Matchmaking";
    private const string k_message_ChooseSide = "Choose A Side";
    private const string k_message_waitingForOpponent = "Opponent's Turn";

    ChilliConnectSdk chilliConnect = null;
    string fileName = "SaveData.txt";
	const string k_keyGamestate = "GAMESTATE";
	string m_chilliConnectId = string.Empty;
	string m_chilliConnectSecret = string.Empty;
	string m_collectionObjectId = string.Empty;
    ChilliConnectGameState chilliConnectGameState = null;

	public GameController gameController = null;

    /// Initialization
	/// 
    void Start()
    {
		gameController.ShowChilliInfoPanel (k_message_startingUp);
        onReadyToLogIn += CallLogIn;
        gameController.onSideSelected += CreateNewGame;
        gameController.onTurnEnded += UpdateChilliConnectGameState;
		onGameStateRetrievedFromServer += InitialiseGameController;
		chilliConnect = new ChilliConnectSdk("Vv7VANzImRtEUeiYaoz4lWKqB6t349iy", false);
        chilliConnectGameState = new ChilliConnectGameState();
        LoadOrCreatePlayerData();
    }

	/// De-Initialization
	/// 
    void OnDestroy()
	{
		onReadyToLogIn -= CallLogIn;
        chilliConnect.Dispose();
	}

	/// Game Controller Initialization
	/// 
	void InitialiseGameController(ChilliConnectGameState gameState)
	{
		if (gameState.m_playerO.CompareTo (m_chilliConnectId) == 0) 
		{
			gameController.SetLocalPlayerSide ("O");
		} 
		else 
		{
			gameController.SetLocalPlayerSide ("X");
        }
        gameController.StartGame();
        gameController.SetBoardState(gameState.m_board);
        if (gameController.IsGameOver())
        {
            chilliConnectGameState.m_matchState = k_matchState_Complete;
            UpdateCollection();
            gameController.HideChilliInfoPanel();
        }
        else
        {
            if (IsLocalPlayersTurn())
            {
                gameController.HideChilliInfoPanel();
            }
            else
            {
                gameController.ShowChilliInfoPanel(k_message_waitingForOpponent);
            }
        }
	}

	/// Attempts to load player account data from file and log it into Chilli Connect
	/// if there is no player file, creates it
	/// 
	void LoadOrCreatePlayerData()
	{
		if (LoadPlayerData() == false)
		{
			CreatePlayer();
		}
	}

	/// Loads player account data from file and logs it into Chilli Connect
	/// 
	bool LoadPlayerData()
	{
		gameController.ShowChilliInfoPanel (k_message_loadingPlayerData);

		var chilliConnectId = PlayerPrefs.GetString ("ChilliConnectID");
		var chilliConnectSecret = PlayerPrefs.GetString ("ChilliConnectSecret");

		if (chilliConnectId != null && chilliConnectSecret != null) 
		{
			m_chilliConnectId = chilliConnectId;
			m_chilliConnectSecret = chilliConnectSecret;
			if (onReadyToLogIn != null) 
			{
				onReadyToLogIn.Invoke();
			}

			return true;
		}

		return false;
	}

	/// Saves player account data to file
	/// 
	void SavePlayerData(string id, string secret)
	{
		PlayerPrefs.SetString("ChilliConnectID", id);
		PlayerPrefs.SetString("ChilliConnectSecret", secret);
	}

	/// Creates a player account and logs it into chilli connect
	/// 
    void CreatePlayer()
	{
		gameController.ShowChilliInfoPanel (k_message_creatingChilliAccount);
        var playerAccounts = chilliConnect.PlayerAccounts;

        System.Action<CreatePlayerRequest, CreatePlayerResponse> successCallback = (CreatePlayerRequest request, CreatePlayerResponse response) =>
        {
            m_chilliConnectId = response.ChilliConnectId;
            m_chilliConnectSecret = response.ChilliConnectSecret;

            UnityEngine.Debug.Log("Player created with ChilliConnectId: " + m_chilliConnectId);

			SavePlayerData(m_chilliConnectId, m_chilliConnectSecret);
			if (onReadyToLogIn != null)
			{
				onReadyToLogIn.Invoke();
			}
        };

        System.Action<CreatePlayerRequest, CreatePlayerError> errorCallback = (CreatePlayerRequest request, CreatePlayerError error) =>
        {
            UnityEngine.Debug.Log("An error occurred while creating a new player: " + error.ErrorDescription);
        };

        var requestDesc = new CreatePlayerRequestDesc();
        requestDesc.DisplayName = "TicTacToePlayer";

        playerAccounts.CreatePlayer(requestDesc, successCallback, errorCallback);
	}

	/// Calls the log in function
	/// 
	void CallLogIn()
	{
		LogIn (m_chilliConnectId, m_chilliConnectSecret);
	}

	/// Logs player in to Chilli Connect
	/// 
    void LogIn(string chilliConnectID, string chilliConnectSecret)
	{
		gameController.ShowChilliInfoPanel (k_message_loggingIn);
        var playerAccounts = chilliConnect.PlayerAccounts;

        System.Action<LogInUsingChilliConnectRequest> successCallback = (LogInUsingChilliConnectRequest request) =>
        {
            UnityEngine.Debug.Log("Successfully logged in!");

            QueryGameTableCollectionForActiveGames();
        };

        System.Action<LogInUsingChilliConnectRequest, LogInUsingChilliConnectError> errorCallback = (LogInUsingChilliConnectRequest request, LogInUsingChilliConnectError error) =>
        {
            UnityEngine.Debug.Log("An error occurred while logging in: " + error.ErrorDescription);
            CreatePlayer();
        };

        playerAccounts.LogInUsingChilliConnect(chilliConnectID, chilliConnectSecret, successCallback, errorCallback);
	}

	/// Uses QueryCollection to Query for games that this player is already part of
	/// 
    void QueryGameTableCollectionForActiveGames()
	{
		gameController.ShowChilliInfoPanel (k_message_matchmaking);
        System.Action<QueryCollectionRequest, QueryCollectionResponse> successCallback = (QueryCollectionRequest request, QueryCollectionResponse response) =>
        {
            if (response.Total > 0)
			{
				//we have a match
				m_collectionObjectId = response.Objects[0].ObjectId;

				chilliConnectGameState = ChilliConnectGameState.FromMultiTypeDictionary(response.Objects[0].Value.AsDictionary());
				if (onGameStateRetrievedFromServer != null)
				{
					onGameStateRetrievedFromServer.Invoke(chilliConnectGameState);
				}
            }
            else
            {
				//we don't have a match
                QueryGameTableCollectionForAvailableGames ();
            }
        };

        System.Action<QueryCollectionRequest, QueryCollectionError> errorCallback = (QueryCollectionRequest request, QueryCollectionError error) =>
		{
			UnityEngine.Debug.Log("An error occurred while querying collection: " + error.ErrorDescription);
			UnityEngine.Debug.Log("The query that was issued was: " + request.Query);
        };
        QueryCollectionRequestDesc requestDesc = new QueryCollectionRequestDesc(k_keyGamestate);
		requestDesc.Query = string.Format(k_activeGameQueryFormat, k_matchState_Complete, m_chilliConnectId);
        chilliConnect.CloudData.QueryCollection(requestDesc, successCallback, errorCallback);
    }

	/// Uses QueryCollection to Query for games that are waiting for a player to join
	/// 
    void QueryGameTableCollectionForAvailableGames()
	{
		gameController.ShowChilliInfoPanel (k_message_matchmaking);
        System.Action<QueryCollectionRequest, QueryCollectionResponse> successCallback = (QueryCollectionRequest request, QueryCollectionResponse response) =>
        {
            if (response.Total > 0)
			{
				//we have a match
				m_collectionObjectId = response.Objects[0].ObjectId;
				chilliConnectGameState = ChilliConnectGameState.FromMultiTypeDictionary(response.Objects[0].Value.AsDictionary());
				string side = chilliConnectGameState.OccupyEmptyPlayerPosition(m_chilliConnectId);
                //if this game hasn't started yet, we go first
                if (side.CompareTo("X") == 0)
                {
                    chilliConnectGameState.m_matchState = k_matchState_xPlayerTurn;
                }
                else
                {
                    chilliConnectGameState.m_matchState = k_matchState_oPlayerTurn;
                }
				UpdateCollection();

				if (onGameStateRetrievedFromServer != null)
				{
					onGameStateRetrievedFromServer.Invoke(chilliConnectGameState);
				}
				gameController.HideChilliInfoPanel();
            }
            else
			{
				//we don't have a match
				gameController.ShowChilliInfoPanel(k_message_ChooseSide, false);
            }
        };

        System.Action<QueryCollectionRequest, QueryCollectionError> errorCallback = (QueryCollectionRequest request, QueryCollectionError error) =>
        {
			UnityEngine.Debug.Log("An error occurred while querying collection: " + error.ErrorDescription);
			UnityEngine.Debug.Log("The query that was issued was: " + request.Query);
        };

		QueryCollectionRequestDesc requestDesc = new QueryCollectionRequestDesc(k_keyGamestate);
		requestDesc.Query = string.Format(k_newGameQueryFormat, k_matchState_WaitingForPlayers);
        chilliConnect.CloudData.QueryCollection(requestDesc, successCallback, errorCallback);
	}

	/// Sets the chilli connect game state for a new game
	/// 
	void CreateNewGame(string selectedSide)
    {
		if (selectedSide.CompareTo ("X") == 0) 
		{
			chilliConnectGameState.m_playerX = m_chilliConnectId;
		} 
		else 
		{
			chilliConnectGameState.m_playerO = m_chilliConnectId;
		}
		chilliConnectGameState.m_matchState = k_matchState_WaitingForPlayers;

		// Push game state to collection
		AddCollectionObject (chilliConnectGameState);
    }

    /// Returns true if it's the local player to play
    /// 
    bool IsLocalPlayersTurn()
    {
        if ((chilliConnectGameState.m_matchState != k_matchState_xPlayerTurn) && (chilliConnectGameState.m_matchState != k_matchState_oPlayerTurn))
        {
            return false;//it's no one's turn
        }
        bool isPlayerX = chilliConnectGameState.m_playerX == m_chilliConnectId;
        bool isXToPlay = chilliConnectGameState.m_matchState == k_matchState_xPlayerTurn; 

        return isPlayerX == isXToPlay;
    }

	/// Uses AddCollectionObject to add a new game to the existing collection
	/// 
	void AddCollectionObject(ChilliConnectGameState gameState)
	{
		System.Action<AddCollectionObjectRequest, AddCollectionObjectResponse> successCallback = (AddCollectionObjectRequest request, AddCollectionObjectResponse response) =>
		{
			m_collectionObjectId = response.ObjectId;
			if (onGameStateRetrievedFromServer != null)
			{
				onGameStateRetrievedFromServer.Invoke(gameState);
			}
			UnityEngine.Debug.Log("New Game Created On Server");
		};

		System.Action<AddCollectionObjectRequest, AddCollectionObjectError> errorCallback = (AddCollectionObjectRequest request, AddCollectionObjectError error) =>
		{
			UnityEngine.Debug.Log("An error occurred while adding to collection: " + error.ErrorDescription);
		};

		chilliConnect.CloudData.AddCollectionObject(k_keyGamestate, gameState.AsMultiTypeDictionary(), successCallback, errorCallback);
	}

	/// Uses UpdateCollectionObject to update an existing game in the collection
	/// 
	void UpdateCollection()
	{
		System.Action<UpdateCollectionObjectRequest, UpdateCollectionObjectResponse> successCallback = (UpdateCollectionObjectRequest request, UpdateCollectionObjectResponse response) =>
		{
			UnityEngine.Debug.Log("Game Updated On Server");
		};

		System.Action<UpdateCollectionObjectRequest, UpdateCollectionObjectError> errorCallback = (UpdateCollectionObjectRequest request, UpdateCollectionObjectError error) =>
		{
			UnityEngine.Debug.Log("An error occurred while Updating collection: " + error.ErrorDescription);
		};

		UpdateCollectionObjectRequestDesc desc = new UpdateCollectionObjectRequestDesc(k_keyGamestate, m_collectionObjectId, chilliConnectGameState.AsMultiTypeDictionary());

		chilliConnect.CloudData.UpdateCollectionObject(desc, successCallback, errorCallback);
    }

    /// Updates the game's state and pushes to the collection
    /// 
    void UpdateChilliConnectGameState(string player, string board)
    {
        chilliConnectGameState.m_board = board;
        if (gameController.IsGameOver() == false)
        {
            if (player.CompareTo("X") == 0)// change who's turn it is
            {
                chilliConnectGameState.m_matchState = k_matchState_xPlayerTurn;
            }
            else
            {
                chilliConnectGameState.m_matchState = k_matchState_oPlayerTurn;
            }

            gameController.ShowChilliInfoPanel(k_message_waitingForOpponent);
        }
        else 
        {
            chilliConnectGameState.m_matchState = k_matchState_GameOver;
        }

        UpdateCollection();
    }

    /// Uses QueryCollection to Query for games that this player is already part of
    /// 
    void GetCurrentCollectionObject()
    {
        System.Action<GetCollectionObjectsRequest, GetCollectionObjectsResponse> successCallback = (GetCollectionObjectsRequest request, GetCollectionObjectsResponse response) =>
        {
            if (response.Objects.Count > 0)
            {
                //we have a match
                if (chilliConnectGameState.m_board.CompareTo(response.Objects[0].Value.AsDictionary().GetString("Board")) == 0)
                {
                    //no changes
                    WaitThenCheckCollectionForUpdates();
                }
                else
                {
                    //changes found - assimilate them
                    chilliConnectGameState = ChilliConnectGameState.FromMultiTypeDictionary(response.Objects[0].Value.AsDictionary());
                    gameController.HideChilliInfoPanel();
                }
            }
            else
            {
                //we don't have a match
                WaitThenCheckCollectionForUpdates();
            }
        };

        System.Action<GetCollectionObjectsRequest, GetCollectionObjectsError> errorCallback = (GetCollectionObjectsRequest request, GetCollectionObjectsError error) =>
        {
            UnityEngine.Debug.Log("An error occurred while getting collection objects: " + error.ErrorDescription);
        };
        chilliConnect.CloudData.GetCollectionObjects(k_keyGamestate, new List<string>{m_collectionObjectId}, successCallback, errorCallback);
    }

    /// Waits for a given time before performing a given action
    /// 
    void WaitThenCheckCollectionForUpdates()
    {
        StartCoroutine(DoSomethingAfterWait(3, GetCurrentCollectionObject));
    }

    /// Waits for a given time before performing a given action
    /// 
    IEnumerator DoSomethingAfterWait(float wait, System.Action thingToDo)
    {
        yield return new WaitForSeconds(wait);
        thingToDo.Invoke();
    }
}
