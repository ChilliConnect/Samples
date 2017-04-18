/// Represents the collection in chilli cloud data
/// 
public class ChilliConnectGameState
{
    public string m_board = "?????????";
    public string m_matchState = "WAITING";
    public string m_playerO = string.Empty;
	public string m_playerX = string.Empty;

	/// @return MultiTypeDictionary representing the ChilliConnectGameState
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

	/// @return ChilliConnectGameState built from the data in the MultiTypeDictionary
	/// 
	public static ChilliConnectGameState FromMultiTypeDictionary(SdkCore.MultiTypeDictionary multiTypeDictionary)
	{
		ChilliConnectGameState chilliConnectGameState = new ChilliConnectGameState ();

		chilliConnectGameState.m_playerO = multiTypeDictionary.GetString("PlayerO");
		chilliConnectGameState.m_playerX = multiTypeDictionary.GetString("PlayerX");
		chilliConnectGameState.m_matchState = multiTypeDictionary.GetString("MatchState");
		chilliConnectGameState.m_board = multiTypeDictionary.GetString("Board");

		return chilliConnectGameState;
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
