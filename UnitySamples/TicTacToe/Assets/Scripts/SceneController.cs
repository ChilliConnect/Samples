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
	private const string MESSAGE_OPPONENT_TURRN = "Opponent's Turn";

	const string GAME_TOKEN = "Vv7VANzImRtEUeiYaoz4lWKqB6t349iy";

	/*private const string k_newGameQueryFormat = "Value.MatchState = \"{0}\""; 
    private const string k_activeGameQueryFormat = "Value.MatchState != \"{0}\" AND (Value.PlayerX = \"{1}\" OR Value.PlayerO = \"{1}\")";

	private const string k_message_startingUp = "Starting Up";
	private const string k_message_loadingPlayerData = "Loading Player Data";
	private const string k_message_creatingChilliAccount = "Creating Chilli Account";
	private const string k_message_loggingIn = "Logging In";
    private const string k_message_matchmaking = "Matchmaking";
    private const string k_message_ChooseSide = "Choose A Side";
    private const string k_message_waitingForOpponent = "Opponent's Turn";
	const string k_keyGamestate = "GAMESTATE";*/

    private ChilliConnectSdk m_chilliConnect = null;
	private MatchSystem m_matchSystem = new MatchSystem ();

	/*string m_chilliConnectId = string.Empty;
	string m_chilliConnectSecret = string.Empty;
	string m_collectionObjectId = string.Empty;*/
    
	public GameController gameController = null;


    /// Initialization
	/// 
    private void Awake()
    {
		m_chilliConnect = new ChilliConnectSdk(GAME_TOKEN);
		m_matchSystem.Initialise (m_chilliConnect);
		m_matchSystem.OnMatchUpdated 
		gameController.ShowChilliInfoPanel (MESSAGE_CHOOSE_SIDE);
        gameController.onSideSelected += CreateNewGame;
        gameController.onTurnEnded += UpdateChilliConnectGameState;
		//onGameStateRetrievedFromServer += InitialiseGameController;

        
        LoadOrCreatePlayerData();
    }

	private void OnMatchUpdated(MatchState match)
	{
		
	}

    void OnDestroy()
	{
        chilliConnect.Dispose();
	}

	/// Game Controller Initialization
	/// 
	void InitialiseGameController(ChilliConnectGameState gameState)
	{
		if (gameState.m_playerO.Equals (m_chilliConnectId)) 
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
			UpdateGameOnServer(false);
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
				WaitThenCheckCollectionForUpdates ();
            }
        }
	}

	/// Attempts to load player account data from file and log it into Chilli Connect
	/// if there is no player file, creates it
	/// 
	void LoadOrCreatePlayerData()
	{
		if (!LoadPlayerData()) {
			CreateAndLoginNewPlayer();
		}
	}

	/// Loads player account data from file and logs it into Chilli Connect
	/// 
	bool LoadPlayerData()
	{
		gameController.ShowChilliInfoPanel (k_message_loadingPlayerData);

		var chilliConnectId = PlayerPrefs.GetString ("ChilliConnectID", null);
		var chilliConnectSecret = PlayerPrefs.GetString ("ChilliConnectSecret", null);

		if (chilliConnectId != null && chilliConnectSecret != null) 
		{
			m_chilliConnectId = chilliConnectId;
			m_chilliConnectSecret = chilliConnectSecret;

			LogInToChilliConnect ();

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

	/// Creates a new player account and logs into ChilliConnect
	/// 
    void CreateAndLoginNewPlayer()
	{
		gameController.ShowChilliInfoPanel (k_message_creatingChilliAccount);
        var playerAccounts = chilliConnect.PlayerAccounts;

        System.Action<CreatePlayerRequest, CreatePlayerResponse> successCallback = (CreatePlayerRequest request, CreatePlayerResponse response) =>
        {
            m_chilliConnectId = response.ChilliConnectId;
            m_chilliConnectSecret = response.ChilliConnectSecret;

            UnityEngine.Debug.Log("Player created with ChilliConnectId: " + m_chilliConnectId);

			SavePlayerData(m_chilliConnectId, m_chilliConnectSecret);
			LogInToChilliConnect ();
        };

        System.Action<CreatePlayerRequest, CreatePlayerError> errorCallback = (CreatePlayerRequest request, CreatePlayerError error) =>
        {
            UnityEngine.Debug.Log("An error occurred while creating a new player: " + error.ErrorDescription);
        };

        playerAccounts.CreatePlayer(new CreatePlayerRequestDesc(), successCallback, errorCallback);
	}

	/// Logs player in to Chilli Connect
	/// 
    void LogInToChilliConnect()
	{
		gameController.ShowChilliInfoPanel (k_message_loggingIn);
        
        System.Action<LogInUsingChilliConnectRequest> successCallback = (LogInUsingChilliConnectRequest request) =>
        {
            UnityEngine.Debug.Log("Successfully logged in!");
			CheckForExistingGame();
        };

        System.Action<LogInUsingChilliConnectRequest, LogInUsingChilliConnectError> errorCallback = (LogInUsingChilliConnectRequest request, LogInUsingChilliConnectError error) =>
        {
            UnityEngine.Debug.Log("An error occurred while logging in: " + error.ErrorDescription);
        };

		chilliConnect.PlayerAccounts.LogInUsingChilliConnect(m_chilliConnectId, m_chilliConnectSecret, successCallback, errorCallback);
	}

	void CheckForExistingGame()
	{
		m_collectionObjectId = PlayerPrefs.GetString ("GameId");
		if (m_collectionObjectId.Length == 0 ) {
			UnityEngine.Debug.Log("Looking for a new match");
			StartMatchmaking ();
		}
		else {
			UnityEngine.Debug.Log("Refreshing existing match:" + m_collectionObjectId);
			RefreshExistingGame ();
		}
	}

	void SetExistingGame(string gameId)
	{
		m_collectionObjectId = gameId;
		PlayerPrefs.SetString("GameId", gameId);
	}

	/// Uses QueryCollection to Query for games that this player is already part of
	/// 
    void FindNewGame()
	{
		gameController.ShowChilliInfoPanel (k_message_matchmaking);

        System.Action<QueryCollectionRequest, QueryCollectionResponse> successCallback = (QueryCollectionRequest request, QueryCollectionResponse response) =>
        {
            if (response.Total > 0)
			{
				var collectionObject = response.Objects[0];
				SetExistingGame( collectionObject.ObjectId );

				chilliConnectGameState = ChilliConnectGameState.FromMultiTypeDictionary(collectionObject.Value.AsDictionary());
				InitialiseGameController( chilliConnectGameState );
            }
            else
            {
				//we don't have a match, find a new one
				m_collectionObjectId = null;
				StartMatchmaking();
            }
        };

        System.Action<QueryCollectionRequest, QueryCollectionError> errorCallback = (QueryCollectionRequest request, QueryCollectionError error) =>
		{
			UnityEngine.Debug.Log("An error occurred while querying collection: " + error.ErrorDescription);

        };
        QueryCollectionRequestDesc requestDesc = new QueryCollectionRequestDesc(k_keyGamestate);
		requestDesc.Query = string.Format(k_activeGameQueryFormat, k_matchState_Complete, m_chilliConnectId);
        chilliConnect.CloudData.QueryCollection(requestDesc, successCallback, errorCallback);
    }

	/// Uses QueryCollection to Query for games that are waiting for a player to join
	/// 
	void StartMatchmaking()
	{
		gameController.ShowChilliInfoPanel (k_message_matchmaking);

        System.Action<QueryCollectionRequest, QueryCollectionResponse> successCallback = (QueryCollectionRequest request, QueryCollectionResponse response) =>
        {
            if (response.Total > 0)
			{
				//we have a match
				SetExistingGame(response.Objects[0].ObjectId);

				chilliConnectGameState = ChilliConnectGameState.FromMultiTypeDictionary(response.Objects[0].Value.AsDictionary());

				string side = chilliConnectGameState.OccupyEmptyPlayerPosition(m_chilliConnectId);

                //if this game hasn't started yet, we go first
				if (side.Equals("X"))
                {
                    chilliConnectGameState.m_matchState = k_matchState_xPlayerTurn;
                }
                else
                {
                    chilliConnectGameState.m_matchState = k_matchState_oPlayerTurn;
                }

				UpdateGameOnServer(false);
				InitialiseGameController( chilliConnectGameState );
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
		if (selectedSide.Equals("X")) 
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
			SetExistingGame(response.ObjectId);
			InitialiseGameController(chilliConnectGameState);
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
	void UpdateGameOnServer(bool pollForUpdates)
	{
		System.Action<UpdateCollectionObjectRequest, UpdateCollectionObjectResponse> successCallback = (UpdateCollectionObjectRequest request, UpdateCollectionObjectResponse response) =>
		{
			UnityEngine.Debug.Log("Game Updated On Server");
			if ( pollForUpdates ) {
				WaitThenCheckCollectionForUpdates();
			}
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
		bool pollForUpdates = false;
        chilliConnectGameState.m_board = board;
        if (gameController.IsGameOver() == false)
        {
            if (player.Equals("X"))// change who's turn it is
            {
                chilliConnectGameState.m_matchState = k_matchState_xPlayerTurn;
            }
            else
            {
                chilliConnectGameState.m_matchState = k_matchState_oPlayerTurn;
            }

			pollForUpdates = true;
            gameController.ShowChilliInfoPanel(k_message_waitingForOpponent);
        }
        else 
        {
            chilliConnectGameState.m_matchState = k_matchState_GameOver;
        }

		UpdateGameOnServer(pollForUpdates );
    }

    /// Uses QueryCollection to Query for games that this player is already part of
    /// 
	void RefreshExistingGame()
    {
		UnityEngine.Debug.Log("Polling for updates..." + Time.time);

        System.Action<GetCollectionObjectsRequest, GetCollectionObjectsResponse> successCallback = (GetCollectionObjectsRequest request, GetCollectionObjectsResponse response) =>
        {
			//we have a match
            if (response.Objects.Count > 0)
            {
				chilliConnectGameState = ChilliConnectGameState.FromMultiTypeDictionary(response.Objects[0].Value.AsDictionary());
				if ( IsLocalPlayersTurn() ) {
					gameController.HideChilliInfoPanel();
					UnityEngine.Debug.Log("Is Local players turn");
				}
				else {
					UnityEngine.Debug.Log("wairting");
					WaitThenCheckCollectionForUpdates();
				}
            }
            else
            {
				UnityEngine.Debug.Log("No match found");

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
		StartCoroutine(DoSomethingAfterWait(3, RefreshExistingGame));
    }

    /// Waits for a given time before performing a given action
    /// 
    IEnumerator DoSomethingAfterWait(float wait, System.Action thingToDo)
    {
        yield return new WaitForSeconds(wait);
        thingToDo.Invoke();
    }
}
