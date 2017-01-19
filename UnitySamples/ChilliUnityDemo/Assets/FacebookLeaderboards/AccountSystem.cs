using UnityEngine;
using System.Collections;
using Facebook.Unity;
using ChilliConnect;

/// Wrapper around the Facebook API and ChilliConnect calls for managing login and local user account
///
public class AccountSystem 
{
	public event System.Action OnInitialised = delegate {};
	public event System.Action OnLocalPlayerChanged = delegate {};

	private static AccountSystem s_singletonInstance = null;

	private ChilliConnectSdk m_chilliConnect;

	private string m_fbAccessToken;
	private string m_localPlayerName = "Unknown";

	//TODO: Auto login

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
	public void Initialise(ChilliConnectSdk chilliConnect)
	{
		m_chilliConnect = chilliConnect;

		if(FB.IsInitialized == false)
		{
			FB.Init(OnFBInitComplete);
		}
		else
		{
			FB.ActivateApp();
			OnInitialised();
		}
	}

	/// Called when the FBInit call has completed
	/// either successfully or not
	///
	private void OnFBInitComplete()
	{
		if(FB.IsInitialized == true)
		{
			FB.ActivateApp();
			OnInitialised();
		}
		else
		{
			Debug.LogError("Failed to initialise FB SDK");
		}
	}

	/// Will attempt to log the user into FB with the required permissions. 
	/// If there is no cached session this will show the FB login dialogue.
	///
	public void Login()
	{
		FB.LogInWithReadPermissions(new string[] {"public_profile", "user_friends"}, OnFBLoginComplete);
	}

	/// Called when the FB login call has completed. May or may not have been
	/// successful
	/// 
	private void OnFBLoginComplete(ILoginResult result)
	{
		if(FB.IsLoggedIn == true)
		{
			Debug.Log("FB login successful");

			//This access token is required to link the players FB account to their ChilliConnect account or to login to a ChilliConnect account.
			m_fbAccessToken = Facebook.Unity.AccessToken.CurrentAccessToken.TokenString;

			//First attempt to login to ChilliConnect with this FB token as an account may exist 
			m_chilliConnect.PlayerAccounts.LogInUsingFacebook(m_fbAccessToken, (request, response) => OnChilliConnectLoggedIn(response), (request, error) => OnChilliConnectLoginFailed(error));
		}
		else
		{
			Debug.LogError("FB login failed. Reason: " + result.Error);
		}
	}

	/// Called when the ChilliConnect login call has completed. This means
	/// that a ChilliConnect account exists for the FB user.
	/// 
	/// @param response
	/// 		Holds the response data including FB name
	/// 
	private void OnChilliConnectLoggedIn(LogInUsingFacebookResponse response)
	{
		//TODO: Download local player avatar.

		Debug.Log("ChilliConnect logged in with FB account for " + response.FacebookName);

		m_localPlayerName = response.FacebookName;

		//We consider logging into FB finished at this point.
		OnLocalPlayerChanged();
	}

	/// Called when the ChilliConnect login call has failed. This could mean either
	/// a general error or that the FB player has no ChilliConnect account. In the latter
	/// case we create a CC account and link it to the FB player.
	/// 
	/// @param error
	/// 		Error type and description
	/// 
	private void OnChilliConnectLoginFailed(LogInUsingFacebookError error)
	{
		Debug.Log("ChilliConnect logged in failed. Reason: " + error.ErrorDescription);

		if(error.ErrorCode == LogInUsingFacebookError.Error.LoginNotFound)
		{
			//Create a new ChilliConnect account and link it with the FB token
			var requestDesc = new CreatePlayerRequestDesc();
			m_chilliConnect.PlayerAccounts.CreatePlayer(requestDesc, (request, response) => OnChilliConnectAccountCreated(response), (request, createError) => Debug.LogError(createError.ErrorDescription));
		}
	}

	/// Called when player creation has completed allowing us to link the newly created
	/// account with FB
	/// 
	/// @param response
	/// 
	private void OnChilliConnectAccountCreated(CreatePlayerResponse response)
	{
		LinkFacebookAccountRequestDesc requestDesc = new LinkFacebookAccountRequestDesc(m_fbAccessToken);
		m_chilliConnect.PlayerAccounts.LinkFacebookAccount(requestDesc, (request, linkResponse) => OnChilliConnectLinked(linkResponse), (request, error) => Debug.LogError(error.ErrorDescription));
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

		//We consider logging into FB finished at this point.
		OnLocalPlayerChanged();
	}

	///
	/// @return Facebook name of the logged in player.
	/// 
	public string GetLocalPlayerName()
	{
		return m_localPlayerName;
	}
}
