using UnityEngine;
using System.Collections;
using Facebook.Unity;
using ChilliConnect;

/// Wrapper around the Facebook API and ChilliConnect calls for managing login and local user accounts. 
/// There are several "paths" through the login flow but the main ones are:
/// 
/// *** First time user flow
/// * Initialise FB SDK
/// * Create new ChilliConnect account
/// * Login to new account
/// * On user interaction login to FB.
/// * Attempt to login to ChilliConnect with FB token and fail
/// * Link FB account with ChilliConnect account.
/// 
/// *** Returning user flow (same device)
/// * Initialise FB SDK
/// * FB already logged in
/// * Login to ChilliConnect using FB token
/// 
/// *** Returning user flow (new device)
/// * Initialise FB SDK
/// * Create new ChilliConnect account
/// * Login to new account
/// * On user interaction login to FB.
/// * Login to ChilliConnect using FB token (switch account causing the leaderboards to refresh with the new accounts friend's)
///
public class AccountSystem 
{	
	public enum AccountStatus
	{
		NONE,
		LOGIN_ANONYMOUS,
		LOGIN_FB
	}
		
	public event System.Action<AccountStatus> OnAccountStatusChanged = delegate {};

	private static AccountSystem s_singletonInstance = null;

	private ChilliConnectSdk m_chilliConnect;

	private string m_fbAccessToken;
	private string m_localPlayerName = "Unknown";
	private string m_chilliId;

	/// @return Singleton instance if system has been created (not lazily created)
	/// 
	public static AccountSystem Get()
	{
		return s_singletonInstance;
	}

	/// 
	public AccountSystem()
	{
		s_singletonInstance = this;
	}

	/// Must be called before any other FB API calls.
	/// 
	/// @param chilliConnect
	/// 	SDK instance
	///
	public void Initialise(ChilliConnectSdk chilliConnect)
	{
		m_chilliConnect = chilliConnect;

		if(FB.IsInitialized == false)
		{
			FB.Init(OnFBInitComplete);
		}
		else
		{
			OnFBInitComplete();
		}
	}

	/// Called when the FBInit call has completed
	/// either successfully or not. Determines if
	/// we have an existing FB/ChilliConnect login or not 
	///
	private void OnFBInitComplete()
	{
		if(FB.IsInitialized == true)
		{
			Debug.Log("FB initialised");

			FB.ActivateApp();

			//Check if we have an existing FB session and if so attempt to login to Chilli using it
			if(FB.IsLoggedIn == true)
			{
				Debug.Log("FB session already exists");

				//This is the access token required to login to ChilliConnect via FB.
				m_fbAccessToken = Facebook.Unity.AccessToken.CurrentAccessToken.TokenString;
				ChilliConnectFBLogin(m_fbAccessToken);
			}
			else
			{
				Debug.Log("No FB session already exists");

				//Check if we have a stored anonymous ChilliConnect token to login with
				if(PlayerPrefs.HasKey("CCId") == true && PlayerPrefs.HasKey("CCSecret") == true)
				{
					Debug.Log("ChilliConnect account already exists. Logging in");
					m_chilliConnect.PlayerAccounts.LogInUsingChilliConnect(PlayerPrefs.GetString("CCId"), PlayerPrefs.GetString("CCSecret"), (loginRequest) => OnChilliConnectAnonLoggedIn(), (loginRequest, error) => Debug.LogError(error.ErrorDescription));
				}
				else
				{
					Debug.Log("No ChilliConnect account exists. Creating one");
					//Create a new ChilliConnect account
					var requestDesc = new CreatePlayerRequestDesc();
					m_chilliConnect.PlayerAccounts.CreatePlayer(requestDesc, (request, response) => OnChilliConnectAccountCreated(response), (request, createError) => Debug.LogError(createError.ErrorDescription));
				}
			}
		}
		else
		{
			Debug.LogError("Failed to initialise FB SDK");
		}
	}

	/// Will attempt to log the user into FB with the required permissions. 
	/// If there is no cached session this will show the FB login dialogue.
	///
	public void FBLogin()
	{
		FB.LogInWithReadPermissions(new string[] {"public_profile", "user_friends"}, OnFBLoginComplete);
	}

	/// Attempts to login to ChilliConnect with the given FB access token
	/// If no account exists then we will pair with the current anonymous chilli account post fail
	/// 
	/// @param fbAccessToken
	/// 	The string version of the FB access token for the current FB user.
	/// 
	public void ChilliConnectFBLogin(string fbAccessToken)
	{
		m_chilliConnect.PlayerAccounts.LogInUsingFacebook(fbAccessToken, (request, response) => OnChilliConnectFBLoggedIn(response), (request, error) => OnChilliConnectFBLoginFailed(error));
	}

	/// Called when the FB login call has completed. May or may not have been
	/// successful
	/// 
	/// @param result
	/// 	Result of the FB login request
	/// 
	private void OnFBLoginComplete(ILoginResult result)
	{
		if(FB.IsLoggedIn == true)
		{
			Debug.Log("FB login successful");

			//This access token is required to link the players FB account to their ChilliConnect account or to login to a ChilliConnect account.
			m_fbAccessToken = Facebook.Unity.AccessToken.CurrentAccessToken.TokenString;

			//Attempt to login to ChilliConnect with this FB token as an account may already exist (if this fails we then link with this account).
			ChilliConnectFBLogin(m_fbAccessToken);
		}
		else
		{
			Debug.LogError("FB login failed. Reason: " + result.Error);
		}
	}

	/// Called when the ChilliConnect login call has completed using the stored
	/// id and secret.
	/// 
	private void OnChilliConnectAnonLoggedIn()
	{
		Debug.Log("ChilliConnect logged in anonymously");

		m_chilliId = PlayerPrefs.GetString("CCId");

		//We consider logging in finished at this point.
		OnAccountStatusChanged(AccountStatus.LOGIN_ANONYMOUS);
	}

	/// Called when the ChilliConnect login call has completed using the stored
	/// id and secret but after the player has already signed into FB. We then need
	/// to link the accounts. 
	/// 
	/// NOTE: This is a bit of an edge case.
	/// 
	private void OnChilliConnectAnonLoggedInPostFB()
	{
		Debug.Log("ChilliConnect logged in anonymously post FB");

		m_chilliId = PlayerPrefs.GetString("CCId");

		LinkFacebookAccountRequestDesc requestDesc = new LinkFacebookAccountRequestDesc(m_fbAccessToken);
		m_chilliConnect.PlayerAccounts.LinkFacebookAccount(requestDesc, (request, linkResponse) => OnChilliConnectLinked(linkResponse), (request, linkError) => Debug.LogError(linkError.ErrorDescription));
	}

	/// Called when the ChilliConnect login call has completed. This means
	/// that a ChilliConnect account exists for the FB user.
	/// 
	/// @param response
	/// 		Holds the response data including FB name
	/// 
	private void OnChilliConnectFBLoggedIn(LogInUsingFacebookResponse response)
	{
		Debug.Log("ChilliConnect logged in with FB account for " + response.FacebookName);

		m_chilliId = response.ChilliConnectId;

		//NOTE: The Chilli Id prior to this response and after this response will probably be different
		//as the player was likely using an anon. account before and has now signed into the account attached to FB, it is
		//up to the game how they wish to handle this but usually you would take the data with the most progress (either the anon. or
		//the FB data) and associate that with the FB linked account (which is the primary account).
		//
		//In this demo there is no local data stored against the signed in player as the leaderboard
		//data is pulled from ChilliConnect it will simply update to reflect the newly signed in player.

		m_localPlayerName = response.FacebookName;

		//We consider logging in finished at this point.
		OnAccountStatusChanged(AccountStatus.LOGIN_FB);
	}

	/// Called when the ChilliConnect login call has failed. This could mean either
	/// a general error or that the FB player has no ChilliConnect account. In the latter
	/// case we link the current anonymous account to the current FB account.
	/// 
	/// @param error
	/// 		Error type and description
	/// 
	private void OnChilliConnectFBLoginFailed(LogInUsingFacebookError error)
	{
		Debug.LogWarning("ChilliConnect logged in failed. Reason: " + error.ErrorDescription);

		//An edge case that is probably not even possible is that the player has never created a
		//ChilliConnect but has managed to login to FB. In this case we just create the account
		//now and link to that.
		if(IsChilliLoggedIn() == false)
		{
			var requestDesc = new CreatePlayerRequestDesc();
			m_chilliConnect.PlayerAccounts.CreatePlayer(requestDesc, (request, response) => OnChilliConnectAccountCreatedPostFB(response), (request, createError) => Debug.LogError(createError.ErrorDescription));
		}
		else if(error.ErrorCode == LogInUsingFacebookError.Error.LoginNotFound)
		{
			LinkFacebookAccountRequestDesc requestDesc = new LinkFacebookAccountRequestDesc(m_fbAccessToken);
			m_chilliConnect.PlayerAccounts.LinkFacebookAccount(requestDesc, (request, linkResponse) => OnChilliConnectLinked(linkResponse), (request, linkError) => Debug.LogError(linkError.ErrorDescription));
		}
	}

	/// Called when a new ChilliConnect account is created. Once a new account is created you still have to login to it.
	/// 
	/// @param response
	/// 	Holds the Id and Secret required for future anonymouse logins
	///
	private void OnChilliConnectAccountCreated(CreatePlayerResponse response)
	{
		Debug.Log("Created new CC account");

		PlayerPrefs.SetString("CCId", response.ChilliConnectId);
		PlayerPrefs.SetString("CCSecret", response.ChilliConnectSecret);

		//Once we've created a new player we need to log them in
		m_chilliConnect.PlayerAccounts.LogInUsingChilliConnect(response.ChilliConnectId, response.ChilliConnectSecret, (loginRequest) => OnChilliConnectAnonLoggedIn(), (loginRequest, error) => Debug.LogError(error.ErrorDescription));
	}

	/// Called when a new ChilliConnect account is created and the player has already logged into FB.
	/// 
	/// @param response
	/// 	Holds the Id and Secret required for future anonymouse logins
	///
	private void OnChilliConnectAccountCreatedPostFB(CreatePlayerResponse response)
	{
		Debug.Log("Created new CC account post FB");

		PlayerPrefs.SetString("CCId", response.ChilliConnectId);
		PlayerPrefs.SetString("CCSecret", response.ChilliConnectSecret);

		//Once we've created a new player we need to log them in
		m_chilliConnect.PlayerAccounts.LogInUsingChilliConnect(response.ChilliConnectId, response.ChilliConnectSecret, (loginRequest) => OnChilliConnectAnonLoggedInPostFB(), (loginRequest, error) => Debug.LogError(error.ErrorDescription));
	}

	/// Called when the request to link FB and Chilli accounts has completed successfully
	///
	/// @param response
	/// 		Holds the response data including FB name
	/// 
	private void OnChilliConnectLinked(LinkFacebookAccountResponse response)
	{
		Debug.Log("ChilliConnect now linked with FB account for " + response.FacebookName);

		m_localPlayerName = response.FacebookName;

		//We consider logging in finished at this point.
		OnAccountStatusChanged(AccountStatus.LOGIN_FB);
	}

	///
	/// @return The ChilliConnect account Id for the logged in player. Could be empty if not logged in.
	///
	public string GetLocalPlayerId()
	{
		return m_chilliId;
	}

	///
	/// @return Facebook name of the logged in player. Could be default if not logged in.
	/// 
	public string GetLocalPlayerName()
	{
		return m_localPlayerName;
	}

	///
	/// @return TRUE if the player is logged into ChilliConnect either anon. or via FB.
	///
	private bool IsChilliLoggedIn()
	{
		return string.IsNullOrEmpty(m_chilliId);
	}
}
