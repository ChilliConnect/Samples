using UnityEngine;
using System.Collections;
using ChilliConnect;
using System.IO;

/// 
/// Handles login and creation of anonymous ChilliConnect account if none
/// 
public class AccountSystem 
{	
	private const string SAVE_FILE = "Player.txt";

	private class PlayerCredentials 
	{
		public string ChilliConnectId { get; set; }
		public string ChilliConnectSecret { get; set; }
	}

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
		var player = LoadPlayer ();
		if (player != null) {
			Login (player.ChilliConnectId, player.ChilliConnectSecret);
		} else {
			CreateNewAccount ();
		}
	}

	private PlayerCredentials LoadPlayer()
	{
		if (!File.Exists (SAVE_FILE)) {
			return null;
		}

		var parts = File.ReadAllLines(SAVE_FILE);
		if (parts.Length != 2) {
			return null;
		}

		var playerCredentials = new PlayerCredentials ();
		playerCredentials.ChilliConnectId = parts[0];
		playerCredentials.ChilliConnectSecret = parts[1];

		return playerCredentials;
	}

	private void SavePlayer(string chilliConnectId, string chilliConnectSecret)
	{
		File.WriteAllLines(SAVE_FILE, new string[]{ chilliConnectId , chilliConnectSecret });
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
		UnityEngine.Debug.Log ("Logged in as player " + chilliConnectId);
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
		SavePlayer( response.ChilliConnectId, response.ChilliConnectSecret);
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
