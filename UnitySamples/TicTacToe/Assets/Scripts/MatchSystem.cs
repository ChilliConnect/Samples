using System;
using ChilliConnect;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class MatchSystem
{
	private const string MATCH_FILE = "MatchId.txt";

	const string MATCH_TYPE = "TIC_TAC_TOE";
	const string TURN_TYPE_SEQUENTIAL = "SEQUENTIAL";

	const string MATCHES_COLLECTION = "GAMESTATE";
	const string QUERY_FIND_MATCH = "Value.MatchState = \"{0}\""; 
    
	public event System.Action<GameState, GameState> OnMatchUpdated = delegate {};
	public event System.Action OnMatchMakingStarted = delegate {};
	public event System.Action OnMatchMakingFailed = delegate {};
	public event System.Action<GameState> OnMatchMakingSuceeded = delegate {};
	public event System.Action<GameState> OnNewMatchCreated = delegate {};
	public event System.Action<GameState> OnMatchSavedOnServer = delegate {};

	private static MatchSystem s_singletonInstance;

	public GameState CurrentGame { get; set; }

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
		CurrentGame = new GameState();
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
			CurrentGame.MatchId = existingMatchId;
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

		var joinMatchRequest = new JoinAvailableMatchRequestDesc(MATCH_TYPE);
		m_chilliConnect.AsyncMultiplayer.JoinAvailableMatch(joinMatchRequest,
			(request, response ) => JoinAvailableMatchCallBack(response),
			(request, error) => Debug.Log(error.ErrorDescription) );
	}

	private void JoinAvailableMatchCallBack(JoinAvailableMatchResponse response )
	{
		if (response.Success)
		{
			var matchObject = response.Match;
			if (matchObject.State != "READY") {
				UnityEngine.Debug.Log("INVALID MATCH STATE:" + matchObject.State);
			}

			SaveMatchId(matchObject.MatchId);

			CurrentGame.MatchId = matchObject.MatchId;
			CurrentGame.Update(matchObject);
			CurrentGame.OccupyEmptyPlayerPosition(m_chilliConnectId);

			StartMatch (matchObject);
			OnMatchMakingSuceeded (CurrentGame);
		}
		else
		{
			OnMatchMakingFailed ();
		}
	}

	private void StartMatch(Match match)
	{
		var startMatchDesc = new StartMatchRequestDesc (match.MatchId);
		startMatchDesc.StateData = CurrentGame.AsMultiTypeDictionary ();

		m_chilliConnect.AsyncMultiplayer.StartMatch (startMatchDesc,
			(request, response) => StartMatchCallBack(response),
			(request, error) => Debug.Log(error.ErrorDescription) );
	}
		
	private void StartMatchCallBack(StartMatchResponse response)
	{
		UnityEngine.Debug.Log("New Game Created On Server");

		//TODO: Will return full match pbiect
		var previous = CurrentGame.Copy ();
		CurrentGame.Update (response.Match);

		OnMatchUpdated (CurrentGame, previous);
	}

	public void CreateNewGame (string selectedSide)
	{
		CurrentGame.SetNewGame (selectedSide, m_chilliConnectId);

		var createMatchRequest = new CreateMatchRequestDesc (MATCH_TYPE, TURN_TYPE_SEQUENTIAL, 2);
		createMatchRequest.StateData = CurrentGame.AsMultiTypeDictionary ();
		m_chilliConnect.AsyncMultiplayer.CreateMatch(createMatchRequest,
			(request, response) => CreateMatchCallBack(response),
			(request, error) => Debug.Log(error.ErrorDescription) );		
	}

	private void CreateMatchCallBack(CreateMatchResponse response)
	{
		UnityEngine.Debug.Log("New game created");

		var newMatchId = response.MatchId;

		CurrentGame.MatchId = newMatchId;
		CurrentGame.MatchState = GameState.MATCHSTATE_WAITING;
		SaveMatchId(newMatchId);
		OnNewMatchCreated(CurrentGame);
	}

	public void SubmitTurn()
	{
		UnityEngine.Debug.Log("Submitting turn");

		var submitTurnRequest = new SubmitTurnRequestDesc (CurrentGame.MatchId);
		submitTurnRequest.StateData = CurrentGame.AsMultiTypeDictionary ();
		if (CurrentGame.MatchState == GameState.MATCHSTATE_COMPLETE) {
			submitTurnRequest.Completed = true;
		}

		//TODO: Add outcome data
		m_chilliConnect.AsyncMultiplayer.SubmitTurn(submitTurnRequest,
			(request, response) => SubmitTurnCallBack(response),
			(request, error) => Debug.Log(error.ErrorDescription) );
	}
		
	private void SubmitTurnCallBack(SubmitTurnResponse response)
	{
		var previous = CurrentGame.Copy ();
		CurrentGame.Update(response.Match);
		OnMatchUpdated(CurrentGame, previous);
	}

	public void RefreshMatchFromServer()
	{
		UnityEngine.Debug.Log("Refreshing game: " + Time.time);

		m_chilliConnect.AsyncMultiplayer.GetMatch (CurrentGame.MatchId,
			(request, response) => GetMatchCallBack(response),
			(request, error) => Debug.Log(error.ErrorDescription) );
	}

	private void GetMatchCallBack(GetMatchResponse response)
	{
		var previous = CurrentGame.Copy ();
		CurrentGame.Update (response.Match);
		OnMatchUpdated (CurrentGame, previous);
	}

}
