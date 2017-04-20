using System;
using ChilliConnect;
using UnityEngine;
using System.Collections.Generic;

public class MatchSystem
{
	const string GAMESTATE_COLLECTION = "GAMESTATE";
	const string QUERY_FIND_MATCH = "Value.MatchState = \"{0}\""; 
    
	public event System.Action<MatchState, MatchState> OnMatchUpdated = delegate {};
	public event System.Action OnMatchMakingStarted = delegate {};
	public event System.Action OnMatchMakingFailed = delegate {};
	public event System.Action<MatchState> OnMatchMakingSuceeded = delegate {};
	public event System.Action<MatchState> OnNewMatchCreated = delegate {};
	public event System.Action<MatchState> OnMatchSavedOnServer = delegate {};

	private static MatchSystem s_singletonInstance;

	public MatchState CurrentMatch { get; set; }

	private ChilliConnectSdk m_chilliConnect;

	private string m_chilliConnectId;

	public static MatchSystem Get()
	{
		return s_singletonInstance;
	}

	public MatchSystem ()
	{
		s_singletonInstance = this;	
	}

	public void Initialise(ChilliConnectSdk chilliConnect)
	{
		m_chilliConnect = chilliConnect;
		CurrentMatch = new MatchState ();
	}

	public void LoadExistingOrFindNewGame(string chilliConnectId)
	{
		m_chilliConnectId = chilliConnectId;

		var existingMatchId = LoadMatchId ();
		if (existingMatchId.Length == 0 ) {
			UnityEngine.Debug.Log("No existing game, looking for a new match");
			StartMatchmaking ();
		}
		else {
			UnityEngine.Debug.Log("Found existing game [" +  existingMatchId + "], refreshing from server");
			CurrentMatch.m_matchId = existingMatchId;
			RefreshMatchFromServer ();
		}
	}

	private void SaveMatchId(string matchId)
	{
		PlayerPrefs.SetString("MatchId", matchId);
	}

	private string LoadMatchId()
	{
		return PlayerPrefs.GetString ("MatchId");
	}

	public void ClearMatchId ()
	{
		PlayerPrefs.SetString ("MatchId", "");
	}
	
	public void SetGameComplete()
	{
		CurrentMatch.m_matchState = MatchState.MATCHSTATE_COMPLETE;
		SaveMatchOnServer();
	}

	/// Uses QueryCollection to Query for games that are waiting for a player to join
	/// 
	void StartMatchmaking()
	{
		UnityEngine.Debug.Log("Looking for new matches");

		OnMatchMakingStarted ();

		QueryCollectionRequestDesc requestDesc = new QueryCollectionRequestDesc(GAMESTATE_COLLECTION);
		requestDesc.Query = string.Format(QUERY_FIND_MATCH, MatchState.MATCHSTATE_WAITING);

		m_chilliConnect.CloudData.QueryCollection(requestDesc, 
			(request, response ) => StartMatchmakingCallback(response),
			(request, error) => Debug.Log(error.ErrorDescription) );
	}

	private void StartMatchmakingCallback(QueryCollectionResponse response )
	{
		if (response.Total > 0)
		{
			var matchObject = response.Objects [0];

			SaveMatchId(matchObject.ObjectId);

			CurrentMatch.m_matchId = matchObject.ObjectId;
			CurrentMatch.Update (matchObject.Value.AsDictionary ());
			CurrentMatch.OccupyEmptyPlayerPosition (m_chilliConnectId);

			//TODO Write lock to make sure not taken
			SaveMatchOnServer();
			OnMatchMakingSuceeded (CurrentMatch);
		}
		else
		{
			OnMatchMakingFailed ();
		}
	}

	public void CreateNewGame (string selectedSide)
	{
		CurrentMatch.SetNewGame (selectedSide, m_chilliConnectId);
		AddCollectionObject (CurrentMatch);
	}

	/// Uses AddCollectionObject to add a new game to the existing collection
	/// 
	private void AddCollectionObject(MatchState matchState)
	{
		UnityEngine.Debug.Log("Saving new game");

		m_chilliConnect.CloudData.AddCollectionObject( GAMESTATE_COLLECTION, matchState.AsMultiTypeDictionary(), 
			(request, response) => AddCollectionObjectCallBack(response),
			(request, error) => Debug.Log(error.ErrorDescription) );
	}

	private void AddCollectionObjectCallBack(AddCollectionObjectResponse response)
	{
		UnityEngine.Debug.Log("New Game Created On Server");

		var newMatchId = response.ObjectId;

		CurrentMatch.m_matchId = newMatchId;
		SaveMatchId(newMatchId);
		OnNewMatchCreated(CurrentMatch);
	}

	/// Uses UpdateCollectionObject to update an existing game in the collection
	/// 
	public void SaveMatchOnServer()
	{
		UnityEngine.Debug.Log("Saving match on server");

		UpdateCollectionObjectRequestDesc desc = new UpdateCollectionObjectRequestDesc(GAMESTATE_COLLECTION, 
			CurrentMatch.m_matchId, CurrentMatch.AsMultiTypeDictionary());

		m_chilliConnect.CloudData.UpdateCollectionObject(desc, 
			(request, response) => OnUpdateGameOnServer(),
			(request, error) => Debug.Log(error.ErrorDescription) );
	}
		
	private void OnUpdateGameOnServer()
	{
		OnMatchSavedOnServer(CurrentMatch);
	}

	public void RefreshMatchFromServer()
	{
		UnityEngine.Debug.Log("Refreshing game: " + Time.time);

		m_chilliConnect.CloudData.GetCollectionObjects(GAMESTATE_COLLECTION, new List<string>{CurrentMatch.m_matchId}, 
			(request, response) => RefreshGameCallback(response),
			(request, error) => Debug.Log(error.ErrorDescription) );
	}

	private void RefreshGameCallback(GetCollectionObjectsResponse response)
	{
		if (response.Objects.Count > 0)
		{
			var previous = CurrentMatch.copy ();
			CurrentMatch.Update(response.Objects[0].Value.AsDictionary());
			OnMatchUpdated (CurrentMatch, previous);
		}
		else
		{
			UnityEngine.Debug.Log("Error, match not found");
		}
	}

}
