using UnityEngine;
using System.Collections;
using ChilliConnect;

/// The main controller class for the Facebook Leaderboard demo. Creates the systems and handles the main control flow
/// including intialising ChilliConnect.
/// 
/// Demo: 
/// - Creates an anonymous ChilliConnect player
/// - Allows the player to login to Facebook via a button press and associates the Facebook account with ChilliConnect.
/// - Allows the player to post a score to a ChilliConnect leaderboard.
/// - Pulls and displays the player's FB friends scores and profile pictures as a leaderboard.
/// 
/// More info on setting up ChilliConnect can be found here: https://docs.chilliconnect.com/guide/setup/
/// 
public class FBLeaderboardDemoSceneController : MonoBehaviour 
{
	//Replace this with your own game token found on the game dashboard of ChilliConnect
	const string GAME_TOKEN = "Vv7VANzImRtEUeiYaoz4lWKqB6t349iy";

	private ChilliConnectSdk m_chilliConnect = null;
	private AccountSystem m_accountSystem = new AccountSystem();
	private LeaderboardSystem m_leaderboardSystem = new LeaderboardSystem();
	private ProfilePicSystem m_profilePicSystem = new ProfilePicSystem();

	/// Initialise all the main systems
	/// 
	private void Awake()
	{
		m_chilliConnect = new ChilliConnectSdk(GAME_TOKEN, true); 
		m_accountSystem.Initialise(m_chilliConnect);
		m_leaderboardSystem.Initialise(m_chilliConnect);
		m_profilePicSystem.Initialise();
	}
}
