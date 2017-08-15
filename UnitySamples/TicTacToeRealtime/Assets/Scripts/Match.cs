public class Match
{
	public const string MATCHSTATE_COMPLETE = "COMPLETE";
	public const string MATCHSTATE_WAITING = "WAITING_FOR_PLAYERS";
	public const string MATCHSTATE_XPLAYER = "X_PLAYER_TURN";
	public const string MATCHSTATE_OPLAYER = "O_PLAYER_TURN";
	public const string MATCHSTATE_GAMEOVER = "GAME_OVER";

	public const string PLAYER_X = "X";
	public const string PLAYER_O = "O";

	public const string BOARD_STARTING_STATE = "?????????";

	public string Board { get; set; }
	public string MatchState { get; set; }
	public string PlayerO { get; set; }
	public string PlayerX { get; set; }
	public string MatchId { get; set; }

	public Match()
	{
		Board = BOARD_STARTING_STATE;
		MatchState = MATCHSTATE_WAITING;
		PlayerO = string.Empty;
		PlayerX = string.Empty;
		MatchId = string.Empty;
	}

	/// @return MultiTypeDictionary representing the MatchState
	/// 
	public SdkCore.MultiTypeDictionary AsMultiTypeDictionary()
	{
		var dictionary = new SdkCore.MultiTypeDictionaryBuilder();

		dictionary.Add("PlayerO", PlayerO);
		dictionary.Add("PlayerX", PlayerX);
		dictionary.Add("MatchState", MatchState);
		dictionary.Add("Board", Board);

		return dictionary.Build();
	}

	public void SetNewGame(string selectedSide, string chilliConnectId)
	{
		if (selectedSide.Equals(PLAYER_X)) 
		{
			PlayerX = chilliConnectId;
			PlayerO = string.Empty;
		} 
		else 
		{
			PlayerO = chilliConnectId;
			PlayerX = string.Empty;
		}

		MatchState = MATCHSTATE_WAITING;
		Board = BOARD_STARTING_STATE;
		MatchId = string.Empty;

	}

	public void SwitchTurn (string nextPlayer)
	{
		MatchState = nextPlayer.Equals (PLAYER_X) ? MATCHSTATE_XPLAYER : MATCHSTATE_OPLAYER;
	}

	public bool IsPlayersTurn(string chilliConnectId)
	{
		if (MatchState == Match.MATCHSTATE_OPLAYER) {
			return chilliConnectId == PlayerO;
		}

		if (MatchState == Match.MATCHSTATE_XPLAYER) {
			return chilliConnectId == PlayerX;
		}

		return false;
	}

	public bool IsWaitingForTurn ()
	{
		return MatchState == MATCHSTATE_OPLAYER || MatchState == MATCHSTATE_XPLAYER;
	}

	/// Update the match state from the provided dictionary
	/// 
	public void Update(SdkCore.MultiTypeDictionary multiTypeDictionary)
	{
		PlayerO = multiTypeDictionary.GetString("PlayerO");
		PlayerX = multiTypeDictionary.GetString("PlayerX");
		MatchState = multiTypeDictionary.GetString("MatchState");
		Board = multiTypeDictionary.GetString("Board");
	}
		
	/// puts the given playerID player into an empty player position in the data
	/// 
	/// @return the player position that was occupied
	/// 
	public string OccupyEmptyPlayerPosition(string chilliId)
	{
		if (PlayerO == string.Empty) 
		{
			PlayerO = chilliId;
			MatchState = MATCHSTATE_OPLAYER;
			return PLAYER_O;
		}

		PlayerX = chilliId;
		MatchState = MATCHSTATE_XPLAYER;
		return PLAYER_X;
	}

	public string GetPlayerSide (string m_chilliConnectId)
	{
		if (m_chilliConnectId == PlayerO) {
			return PLAYER_O;
		}

		return PLAYER_X;
	}

	public Match Copy ()
	{
		var copy = new Match ();
		copy.MatchId = MatchId;
		copy.Update (AsMultiTypeDictionary ());

		return copy;
	}
}
