/// 
public class MatchState
{
	public const string MATCHSTATE_COMPLETE = "COMPLETE";
	public const string MATCHSTATE_WAITING = "WAITING_FOR_PLAYERS";
	public const string MATCHSTATE_XPLAYER = "X_PLAYER_TURN";
	public const string MATCHSTATE_OPLAYER = "O_PLAYER_TURN";
	public const string MATCHSTATE_GAMEOVER = "GAME_OVER";

	public const string PLAYER_X = "X";
	public const string PLAYER_O = "O";

	public const string BOARD_STARTING_STATE = "?????????";

	//TODO Change to getters/setters
	public string m_board = BOARD_STARTING_STATE;
	public string m_matchState = MATCHSTATE_WAITING;
    public string m_playerO = string.Empty;
	public string m_playerX = string.Empty;
	public string m_matchId = string.Empty;

	/// @return MultiTypeDictionary representing the MatchState
	/// 
	public SdkCore.MultiTypeDictionary AsMultiTypeDictionary()
	{
		var dictionary = new SdkCore.MultiTypeDictionaryBuilder();

		dictionary.Add("PlayerO", m_playerO);
		dictionary.Add("PlayerX", m_playerX);
		dictionary.Add("MatchState", m_matchState);
		dictionary.Add("Board", m_board);

		return dictionary.Build();
	}

	public void SetNewGame(string selectedSide, string chilliConnectId)
	{
		if (selectedSide.Equals(PLAYER_X)) 
		{
			m_playerX = chilliConnectId;
			m_playerO = string.Empty;
		} 
		else 
		{
			m_playerO = chilliConnectId;
			m_playerX = string.Empty;
		}

		m_matchState = MATCHSTATE_WAITING;
		m_board = BOARD_STARTING_STATE;
		m_matchId = string.Empty;

	}

	public void SwitchTurn (string nextPlayer)
	{
		m_matchState = nextPlayer.Equals (PLAYER_X) ? MATCHSTATE_XPLAYER : MATCHSTATE_OPLAYER;
	}

	public bool IsPlayersTurn(string chilliConnectId)
	{
		if (m_matchState == MatchState.MATCHSTATE_OPLAYER) {
			return chilliConnectId == m_playerO;
		}

		if (m_matchState == MatchState.MATCHSTATE_XPLAYER) {
			return chilliConnectId == m_playerX;
		}

		return false;
	}

	public bool IsWaitingForTurn ()
	{
		return m_matchState == MATCHSTATE_OPLAYER || m_matchState == MATCHSTATE_XPLAYER;
	}

	/// Update the match state from the provided dictionary
	/// 
	public void Update(SdkCore.MultiTypeDictionary multiTypeDictionary)
	{
		m_playerO = multiTypeDictionary.GetString("PlayerO");
		m_playerX = multiTypeDictionary.GetString("PlayerX");
		m_matchState = multiTypeDictionary.GetString("MatchState");
		m_board = multiTypeDictionary.GetString("Board");
	}
		
	/// puts the given playerID player into an empty player position in the data
	/// 
	/// @return the player position that was occupied
	/// 
	public string OccupyEmptyPlayerPosition(string chilliId)
	{
		if (m_playerO == string.Empty) 
		{
			m_playerO = chilliId;
			m_matchState = MATCHSTATE_OPLAYER;
			return PLAYER_O;
		}

		m_playerX = chilliId;
		m_matchState = MATCHSTATE_XPLAYER;
		return PLAYER_X;
	}

	public string GetPlayerSide (string m_chilliConnectId)
	{
		if (m_chilliConnectId == m_playerO) {
			return PLAYER_O;
		}

		return PLAYER_X;
	}

	public string GetOpponentId (string m_chilliConnectId)
	{
		if (m_chilliConnectId == m_playerO) {
			return m_playerX;
		}

		return m_playerO;
	}

	public MatchState copy ()
	{
		var copy = new MatchState ();
		copy.m_matchId = m_matchId;
		copy.Update (AsMultiTypeDictionary ());

		return copy;

	}
}
