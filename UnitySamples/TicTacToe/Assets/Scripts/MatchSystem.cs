using System;
using ChilliConnect;
using UnityEngine;

public class MatchSystem
{
	const string GAMESTATE_COLLECTION = "GAMESTATE";

	public event System.Action<MatchState> OnMatchUpdated = delegate {};

	private static MatchSystem s_singletonInstance;

	public MatchState CurrentMatch { get; set; }

	private ChilliConnectSdk m_chilliConnect;

	public static MatchSystem Get()
	{
		return s_singletonInstance;
	}

	public MatchSystem ()
	{
		return s_singletonInstance = this;	
	}

	public void Initialise(ChilliConnectSdk chilliConnect)
	{
		m_chilliConnect = chilliConnect;
		CurrentMatch = new MatchState ();
	}

	public void LoadExistingOrFindNewGame()
	{
		var existingMatchId = PlayerPrefs.GetString ("MatchId");
		if (existingMatchId.Length == 0 ) {
			UnityEngine.Debug.Log("No existing game, lLooking for a new match");
			StartMatchmaking ();
		}
		else {
			UnityEngine.Debug.Log("Found existing game, refreshing from server");
			m_existing
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

	/// Uses 
	/// 
	private void RefreshGame()
	{
		UnityEngine.Debug.Log("Refreshing game: " + Time.time);

		m_chilliConnect.CloudData.GetCollectionObjects(GAMESTATE_COLLECTION, new List<string>{m_collectionObjectId}, 
			(request, response) => RefreshGameCallback(response.Objects),
			(request, error) => Debug.Log("Error refreshing game:"  + error.ErrorDescription) );
	}

	private void RefreshGameCallback(GetCollectionObjectsResponse response)
	{
		if (response.Objects.Count > 0)
		{
			CurrentMatch.Update(response.Objects[0].Value.AsDictionary());
			OnMatchUpdated (CurrentMatch);
			/*if ( IsLocalPlayersTurn() ) {
				gameController.HideChilliInfoPanel();
				UnityEngine.Debug.Log("Is Local players turn");
			}
			else {
				UnityEngine.Debug.Log("wairting");
				WaitThenCheckCollectionForUpdates();
			}*/
		}
		else
		{
			UnityEngine.Debug.Log("Error, match not found");

			//we don't have a match
			WaitThenCheckCollectionForUpdates();
		}
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
