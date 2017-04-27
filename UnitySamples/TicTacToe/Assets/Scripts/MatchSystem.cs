using System;
using ChilliConnect;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class MatchSystem
{
	private const string MATCH_FILE = "MatchId.txt";

	const string MATCHES_COLLECTION = "GAMESTATE";
	const string QUERY_FIND_MATCH = "Value.MatchState = \"{0}\""; 
    
	public event System.Action<Match, Match> OnMatchUpdated = delegate {};
	public event System.Action OnMatchMakingStarted = delegate {};
	public event System.Action OnMatchMakingFailed = delegate {};
	public event System.Action<Match> OnMatchMakingSuceeded = delegate {};
	public event System.Action<Match> OnNewMatchCreated = delegate {};
	public event System.Action<Match> OnMatchSavedOnServer = delegate {};

	private static MatchSystem s_singletonInstance;

	public Match CurrentMatch { get; set; }

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
		CurrentMatch = new Match ();
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
			CurrentMatch.MatchId = existingMatchId;
			RefreshMatchFromServer ();
		}
	}

	private void SaveMatchId(string matchId)
	{
		File.WriteAllText(MATCH_FILE, matchId);
	}

	private string LoadMatchId()
	{
		if (!File.Exists (MATCH_FILE)) {
			return "";
		}

		return File.ReadAllText(MATCH_FILE);
	}

	public void ClearMatchId ()
	{
		File.WriteAllText(MATCH_FILE, "");
	}

	public void StartMatchmaking()
	{
		UnityEngine.Debug.Log("Looking for new matches");

		OnMatchMakingStarted ();

		QueryCollectionRequestDesc requestDesc = new QueryCollectionRequestDesc(MATCHES_COLLECTION);
		requestDesc.Query = string.Format(QUERY_FIND_MATCH, Match.MATCHSTATE_WAITING);

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

			CurrentMatch.MatchId = matchObject.ObjectId;
			CurrentMatch.Update (matchObject.Value.AsDictionary ());
			CurrentMatch.OccupyEmptyPlayerPosition (m_chilliConnectId);

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
	private void AddCollectionObject(Match matchState)
	{
		UnityEngine.Debug.Log("Saving new game");

		m_chilliConnect.CloudData.AddCollectionObject( MATCHES_COLLECTION, matchState.AsMultiTypeDictionary(), 
			(request, response) => AddCollectionObjectCallBack(response),
			(request, error) => Debug.Log(error.ErrorDescription) );
	}

	private void AddCollectionObjectCallBack(AddCollectionObjectResponse response)
	{
		UnityEngine.Debug.Log("New Game Created On Server");

		var newMatchId = response.ObjectId;

		CurrentMatch.MatchId = newMatchId;
		SaveMatchId(newMatchId);
		OnNewMatchCreated(CurrentMatch);
	}

	public void SaveMatchOnServer()
	{
		UnityEngine.Debug.Log("Saving match on server");

		UpdateCollectionObjectRequestDesc desc = new UpdateCollectionObjectRequestDesc(MATCHES_COLLECTION, 
			CurrentMatch.MatchId, CurrentMatch.AsMultiTypeDictionary());

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

		m_chilliConnect.CloudData.GetCollectionObjects(MATCHES_COLLECTION, new List<string>{CurrentMatch.MatchId}, 
			(request, response) => RefreshGameCallback(response),
			(request, error) => Debug.Log(error.ErrorDescription) );
	}

	private void RefreshGameCallback(GetCollectionObjectsResponse response)
	{
		if (response.Objects.Count > 0)
		{
			var previous = CurrentMatch.Copy ();
			CurrentMatch.Update(response.Objects[0].Value.AsDictionary());
			OnMatchUpdated (CurrentMatch, previous);
		}
		else
		{
			ClearMatchId ();
			UnityEngine.Debug.Log("Error, match not found");
		}
	}

}
