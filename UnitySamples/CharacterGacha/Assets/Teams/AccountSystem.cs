using UnityEngine;
using System.Collections;
using ChilliConnect;

/// 
/// Handles login and creation of anonymous ChilliConnect account if none
/// 
public class AccountSystem 
{	
	public event System.Action<string> OnPlayerLoggedIn = delegate {};

	private static AccountSystem s_singletonInstance = null;

	private ChilliConnectSdk m_chilliConnect;

	public static AccountSystem Get()
	{
		return s_singletonInstance;
	}

	public AccountSystem()
	{
		s_singletonInstance = this;
	}

	/// If a player account has already been created and saved in PlayerPrefs
	/// log that player in. Otherwise, will create a new account.
	/// 
	/// @param chilliConnect
	///		Instance of the chilliConnect SDK
	/// 
	public void Initialise(ChilliConnectSdk chilliConnect)
	{
		m_chilliConnect = chilliConnect;

		if (PlayerPrefs.HasKey ("CCId") && PlayerPrefs.HasKey ("CCSecret")) {
			UnityEngine.Debug.Log ("Current ChilliConnectID: " + PlayerPrefs.GetString ("CCId"));
		
			Login (PlayerPrefs.GetString ("CCId"), PlayerPrefs.GetString ("CCSecret"));
		} else {
			CreateNewAccount ();
		}
	}

	/// Creates a new ChilliConnect player account, replacing the currently 
	/// persisted credentials. This effectivley logs out the existing player
	/// 
	public void CreateNewAccount()
	{
		var requestDesc = new CreatePlayerRequestDesc();
		m_chilliConnect.PlayerAccounts.CreatePlayer(requestDesc, (request, response) => OnChilliConnectAccountCreated(response), (request, createError) => Debug.LogError(createError.ErrorDescription));
	}

	/// Handler for succesfull log in, will notify listeners a new player has been logged in
	/// 
	private void OnChilliConnectLoggedIn(string chilliConnectId, string chilliConnectSecret)
	{
		OnPlayerLoggedIn(chilliConnectId);
	}

	/// Handler for succesfull player account creation. Will persist the new 
	/// players ChilliConnectID and ChilliConnectSecret and login the player
	/// 
	/// @param response
	/// 	The CreatePlayerReponse from the ChilliConnect SDK
	/// 
	private void OnChilliConnectAccountCreated(CreatePlayerResponse response)
	{
		PlayerPrefs.SetString("CCId", response.ChilliConnectId);
		PlayerPrefs.SetString("CCSecret", response.ChilliConnectSecret);
	
		Login (response.ChilliConnectId, response.ChilliConnectSecret);
	}

	/// Logs in the player identified
	/// 
	/// @param chilliConnectId
	/// 	The players chilliConnectId
	/// 
	/// @param chilliConnectSecret
	/// 	The players chilliConnectSecret
	/// 
	private void Login(string chilliConnectId, string chilliConnectSecret)
	{
		m_chilliConnect.PlayerAccounts.LogInUsingChilliConnect(chilliConnectId, chilliConnectSecret, 
			(loginRequest) => OnChilliConnectLoggedIn( chilliConnectId, chilliConnectSecret), 
			(loginRequest, error) => Debug.LogError(error.ErrorDescription));
	}
}
