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
/// - Pulls and displays the player's FB friends scores and profile pictures.
/// 
/// More info on setting up ChilliConnect can be found here: https://docs.chilliconnect.com/guide/setup/
/// 
public class FacebookLeaderboardDemoSceneController : MonoBehaviour 
{
	const string GAME_TOKEN = "KI5EjX8EU65TNM5fTdCmvGKWixMpENtZ";//"Vv7VANzImRtEUeiYaoz4lWKqB6t349iy";

	private ChilliConnectSdk m_chilliConnect = null;
	private FacebookSystem m_fbSystem = new FacebookSystem();
	private LeaderboardSystem m_leaderboardSystem = new LeaderboardSystem();

	/// Initialised ChilliConnect, create and log in a player.
	/// 
	private void Awake()
	{
		// Initialise ChilliConnect. Game token can be found on the game dashboard of ChilliConnect
		m_chilliConnect = new ChilliConnectSdk(GAME_TOKEN, true); 

		// Create a new ChilliConnect player with the given display name if we don't have any credentials saved for the local player
		if(PlayerPrefs.HasKey("CCId") == true && PlayerPrefs.HasKey("CCSecret") == true)
		{
			Debug.Log("Player already exists. Logging in");
			m_chilliConnect.PlayerAccounts.LogInUsingChilliConnect(PlayerPrefs.GetString("CCId"), PlayerPrefs.GetString("CCSecret"), (loginRequest) => OnLoggedIn(), (loginRequest, error) => Debug.LogError(error.ErrorDescription));
		}
		else
		{
			Debug.Log("Creating Player");
			var requestDesc = new CreatePlayerRequestDesc();
			requestDesc.DisplayName = "TestyMcTestface";
			m_chilliConnect.PlayerAccounts.CreatePlayer(requestDesc, OnPlayerCreated, (AddEventRequest, error) => Debug.LogError(error.ErrorDescription));
		}
	}

	/// Called when player creation has completed allowing us to log the
	/// player in
	/// 
	/// @param request
	/// 	Info on request made to create player
	/// @param response
	/// 	Holds the id and secret to log the player in
	/// 
	private void OnPlayerCreated(CreatePlayerRequest request, CreatePlayerResponse response)
	{
		Debug.Log("Player created. Logging in");

		//Save the credentials so we don't create a new player next time we launch the app
		PlayerPrefs.SetString("CCId", response.ChilliConnectId);
		PlayerPrefs.SetString("CCSecret", response.ChilliConnectSecret);
		PlayerPrefs.Save();
		m_chilliConnect.PlayerAccounts.LogInUsingChilliConnect(response.ChilliConnectId, response.ChilliConnectSecret, (loginRequest) => OnLoggedIn(), (loginRequest, error) => Debug.LogError(error.ErrorDescription));
	}

	/// Called on successful login to ChilliConnect and begins Facebook initialisation. Once this
	/// is complete we can then make calls to the FB API.
	/// 
	private void OnLoggedIn()
	{
		Debug.Log("ChilliConnect Login successful.");

		// Initialise FB.
		m_fbSystem.Initialise(m_chilliConnect);

		// Init leaderboards
		m_leaderboardSystem.Initialise(m_chilliConnect);
	}
}
