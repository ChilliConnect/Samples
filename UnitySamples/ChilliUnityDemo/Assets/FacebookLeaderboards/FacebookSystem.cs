using UnityEngine;
using System.Collections;
using Facebook.Unity;
using ChilliConnect;

/// Wrapper around the Facebook API calls required by this demo
///
public class FacebookSystem 
{
	public event System.Action OnInitialised = delegate {};
	public event System.Action OnLocalPlayerChanged = delegate {};

	private static FacebookSystem s_singletonInstance = null;

	private ChilliConnectSdk m_chilliConnect;
	private string m_localPlayerName = "Unknown";

	/// @return Singleton instance if system has been created (not lazily created)
	/// 
	public static FacebookSystem Get()
	{
		return s_singletonInstance;
	}

	/// 
	public FacebookSystem()
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
		FB.LogInWithReadPermissions(new string[] {"public_profile", "user_friends"}, OnLoginComplete);
	}

	/// Called when the login call has completed. May or may not have been
	/// successful
	/// 
	private void OnLoginComplete(ILoginResult result)
	{
		if(FB.IsLoggedIn == true)
		{
			Debug.Log("FB login successful");

			//This access token is required to link the players FB account to their ChilliConnect account.
			var accessToken = Facebook.Unity.AccessToken.CurrentAccessToken;

			LinkFacebookAccountRequestDesc requestDesc = new LinkFacebookAccountRequestDesc(accessToken.TokenString);
			m_chilliConnect.PlayerAccounts.LinkFacebookAccount(requestDesc, (request, response) => OnChilliConnectLinked(response), (request, error) => Debug.LogError(error.ErrorDescription));
		}
		else
		{
			Debug.LogError("FB login failed. Reason: " + result.Error);
		}
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
