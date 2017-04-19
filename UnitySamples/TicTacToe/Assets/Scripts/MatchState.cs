/// 
public class MatchState
{
	private const string MATCHSTATE_COMPLETE = "COMPLETE";
	private const string MATCHSTATE_WAITING = "WAITING_FOR_PLAYERS";
	private const string MATCHSTATE_XPLAYER = "X_PLAYER_TURN";
	private const string MATCHSTATE_OPLAYER = "O_PLAYER_TURN";
	private const string MATCHSTATE_GAMEOVER = "GAME_OVER";

	//TODO Change to getters/setters

    public string m_board = "?????????";
    public string m_matchState = "WAITING";
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
			return "O";
		}
		else if (m_playerX == string.Empty) 
		{
			m_playerX = chilliId;
			return "X";
		}
		return "None";
	}
}
