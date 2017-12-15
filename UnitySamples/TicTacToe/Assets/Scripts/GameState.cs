using ChilliConnect;

public class GameState
{
	public const string MATCHSTATE_COMPLETE = "COMPLETE";
	public const string MATCHSTATE_WAITING = "WAITING";
	public const string MATCHSTATE_IN_PROGRESS = "IN_PROGRESS";
	public const string MATCHSTATE_TIMEOUT = "TIMEOUT";
	public const string MATCHSTATE_READY = "READY";

	public const string PLAYER_X = "X";
	public const string PLAYER_O = "O";

	public const string BOARD_STARTING_STATE = "?????????";

	public string Board { get; set; }
	public string MatchState { get; set; }
	public string PlayerO { get; set; }
	public string PlayerX { get; set; }
	public string MatchId { get; set; }

	private string nextPlayerChilliConnectId;

	public GameState()
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

	public bool IsPlayersTurn(string chilliConnectId)
	{
		return MatchState == MATCHSTATE_IN_PROGRESS && nextPlayerChilliConnectId == chilliConnectId;
	}

	public bool IsWaitingForTurn ()
	{
		return MatchState == MATCHSTATE_IN_PROGRESS;
	}

	public bool IsFinished ()
	{
		return MatchState == MATCHSTATE_COMPLETE || MatchState == MATCHSTATE_TIMEOUT;
	}

	public void Update(Match match)
	{
		var matchStateData = match.StateData.AsDictionary ();
		PlayerO = matchStateData.GetString ("PlayerO");
		PlayerX = matchStateData.GetString ("PlayerX");
		Board = matchStateData.GetString ("Board");

		MatchState = match.State;

		if (MatchState == MATCHSTATE_IN_PROGRESS) {
			nextPlayerChilliConnectId = match.CurrentTurn.PlayersWaitingFor[0].ChilliConnectId;
		}
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
			return PLAYER_O;
		}

		PlayerX = chilliId;
		return PLAYER_X;
	}

	public string GetPlayerSide (string m_chilliConnectId)
	{
		if (m_chilliConnectId == PlayerO) {
			return PLAYER_O;
		}

		return PLAYER_X;
	}

	public GameState Copy ()
	{
		var copy = new GameState ();
		copy.MatchId = MatchId;
		copy.PlayerO = PlayerO;
		copy.PlayerX = PlayerX;
		copy.Board = Board;
		copy.MatchState = MatchState;

		return copy;
	}
}
