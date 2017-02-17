using UnityEngine;
using System.Collections;
using ChilliConnect;

public class AccountSystem 
{	
	public event System.Action<string> OnPlayerLoggedIn = delegate {};

	private static AccountSystem s_singletonInstance = null;

	private ChilliConnectSdk m_chilliConnect;

	private string m_chilliId;

	public static AccountSystem Get()
	{
		return s_singletonInstance;
	}

	public AccountSystem()
	{
		s_singletonInstance = this;
	}

	public void Initialise(ChilliConnectSdk chilliConnect)
	{
		m_chilliConnect = chilliConnect;
		if (PlayerPrefs.HasKey ("CCId") && PlayerPrefs.HasKey ("CCSecret")) {
			Login (PlayerPrefs.GetString ("CCId"), PlayerPrefs.GetString ("CCSecret"));
		} else {
			CreateNewAccount ();
		}
	}

	public void CreateNewAccount()
	{
		var requestDesc = new CreatePlayerRequestDesc();
		m_chilliConnect.PlayerAccounts.CreatePlayer(requestDesc, (request, response) => OnChilliConnectAccountCreated(response), (request, createError) => Debug.LogError(createError.ErrorDescription));
	}

	private void OnChilliConnectLoggedIn(string chilliConnectId, string chilliConnectSecret)
	{
		OnPlayerLoggedIn(chilliConnectId);
	}

	private void OnChilliConnectAccountCreated(CreatePlayerResponse response)
	{
		PlayerPrefs.SetString("CCId", response.ChilliConnectId);
		PlayerPrefs.SetString("CCSecret", response.ChilliConnectSecret);

		Login (response.ChilliConnectId, response.ChilliConnectSecret);
	}

	private void Login(string chilliConnectId, string chilliConnectSecret)
	{
		m_chilliConnect.PlayerAccounts.LogInUsingChilliConnect(chilliConnectId, chilliConnectSecret, 
			(loginRequest) => OnChilliConnectLoggedIn( chilliConnectId, chilliConnectSecret), 
			(loginRequest, error) => Debug.LogError(error.ErrorDescription));
	}
}
