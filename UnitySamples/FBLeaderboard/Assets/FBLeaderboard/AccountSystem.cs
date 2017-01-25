using UnityEngine;
using System.Collections;
using Facebook.Unity;
using ChilliConnect;

/// Wrapper around the Facebook API and ChilliConnect calls for managing login and local user account
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
	/// either successfully or not.
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
	/// If no account exists then will pair with the current anonymous chilli account
	/// 
	/// @param fbAccessToken
	/// 	The string version of the FB access token for the current FB user.
	/// 
	public void ChilliConnectFBLogin(string fbAccessToken)
	{
		m_chilliConnect.PlayerAccounts.LogInUsingFacebook(fbAccessToken, (request, response) => OnChilliConnectFBLoggedIn(response), (request, error) => OnChilliConnectLoginFailed(error));
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

			//First attempt to login to ChilliConnect with this FB token as an account may exist 
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

	/// Called when the ChilliConnect login call has completed. This means
	/// that a ChilliConnect account exists for the FB user.
	/// 
	/// @param response
	/// 		Holds the response data including FB name
	/// 
	private void OnChilliConnectFBLoggedIn(LogInUsingFacebookResponse response)
	{
		//TODO: Download local player avatar.

		Debug.Log("ChilliConnect logged in with FB account for " + response.FacebookName);

		m_chilliId = response.ChilliConnectId;
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
	private void OnChilliConnectLoginFailed(LogInUsingFacebookError error)
	{
		Debug.Log("ChilliConnect logged in failed. Reason: " + error.ErrorDescription);

		if(error.ErrorCode == LogInUsingFacebookError.Error.LoginNotFound)
		{
			LinkFacebookAccountRequestDesc requestDesc = new LinkFacebookAccountRequestDesc(m_fbAccessToken);
			m_chilliConnect.PlayerAccounts.LinkFacebookAccount(requestDesc, (request, linkResponse) => OnChilliConnectLinked(linkResponse), (request, linkError) => Debug.LogError(linkError.ErrorDescription));
		}
	}

	/// Called when a new ChilliConnect account is created
	/// 
	/// @param response
	/// 	Holds the Id and Secret required for future anonymouse logins
	///
	private void OnChilliConnectAccountCreated(CreatePlayerResponse response)
	{
		Debug.Log("Created new CC account");

		PlayerPrefs.SetString("CCId", response.ChilliConnectId);
		PlayerPrefs.SetString("CCSecret", response.ChilliConnectSecret);

		m_chilliId = response.ChilliConnectId;

		//We consider logging in finished at this point.
		OnAccountStatusChanged(AccountStatus.LOGIN_ANONYMOUS);
	}

	/// Called when the request to link FB and Chilli accounts has completed successfully
	///
	/// @param response
	/// 		Holds the response data including FB name
	/// 
	private void OnChilliConnectLinked(LinkFacebookAccountResponse response)
	{
		//TODO: Download local player avatar.

		Debug.Log("ChilliConnect now linked with FB account for " + response.FacebookName);

		m_localPlayerName = response.FacebookName;

		//We consider logging in finished at this point.
		OnAccountStatusChanged(AccountStatus.LOGIN_FB);
	}

	///
	/// @return The ChilliConnect account Id for the logged in player
	///
	public string GetLocalPlayerId()
	{
		return m_chilliId;
	}

	///
	/// @return Facebook name of the logged in player.
	/// 
	public string GetLocalPlayerName()
	{
		return m_localPlayerName;
	}
}
