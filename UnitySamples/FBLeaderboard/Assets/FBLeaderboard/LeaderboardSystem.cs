using UnityEngine;
using System.Collections;
using ChilliConnect;
using System.Linq;

/// Handles score submissions and fetching our ChilliConnect leaderboard
///
public class LeaderboardSystem 
{
	public event System.Action<FacebookScore[]> OnLeaderboardRefreshed = delegate {};
	public event System.Action OnScorePosted = delegate {};

	private static LeaderboardSystem s_singletonInstance = null;

	//"HIGHSCORES" is the key that identifies our leaderboard. Change this to match the key for yours
	private const string LEADERBOARD_KEY = "HIGHSCORES";

	private ChilliConnectSdk m_chilliConnect;

	/// @return Singleton instance if system has been created (not lazily created)
	/// 
	public static LeaderboardSystem Get()
	{
		return s_singletonInstance;
	}

	/// 
	public LeaderboardSystem()
	{
		s_singletonInstance = this;
	}

	///
	public void Initialise(ChilliConnectSdk chilliConnect)
	{
		m_chilliConnect = chilliConnect;
	}

	/// Post the given score to our highscore leaderboard. It will be accepted
	/// if it is higher than the previous score for the current player
	/// 
	public void PostScore(int score)
	{
		m_chilliConnect.Leaderboards.AddScore(new AddScoreRequestDesc(score, LEADERBOARD_KEY), (request, response) => OnScoreAdded(response), (request, error) => Debug.LogError(error.ErrorDescription));
	}

	/// Called after a score has successfully been posted to ChilliConnect leaderboard
	///
	private void OnScoreAdded(AddScoreResponse response)
	{
		Debug.Log(string.Format("Player's high score {0}. Player is {1}/{2} on global leaderboard", response.Score, response.GlobalRank, response.GlobalTotal));
		OnScorePosted();
	}

	/// Make a request to ChilliConnect to download the latest leaderboard 
	/// filtered so that only the FB friends of the current player are shown.
	/// 
	/// NOTE: You can use GetScores or GetScoresAroundPlayer to show alternative leaderboards
	/// 
	public void FetchFriendLeaderboard()
	{
		var desc = new GetScoresForFacebookFriendsRequestDesc(LEADERBOARD_KEY);

		//Setting this to true ensures the logged in player appears in the leaderboard results
		desc.IncludeMe = true;

		m_chilliConnect.Leaderboards.GetScoresForFacebookFriends(desc, (request, response) => OnLeaderboardFetched(response), (request, error) => Debug.LogError(error.ErrorDescription));
	}

	/// Called on successful fetch of the friend leaderboard
	///
	private void OnLeaderboardFetched(GetScoresForFacebookFriendsResponse response)
	{
		OnLeaderboardRefreshed(response.Scores.ToArray());
	}
}
