using UnityEngine;
using System.Collections;
using ChilliConnect;

public class TeamsDemoSceneController : MonoBehaviour 
{
	const string GAME_TOKEN = "";

	private ChilliConnectSdk m_chilliConnect = null;
	private AccountSystem m_accountSystem = new AccountSystem();
	private TeamsSystem m_teamsSystem = new TeamsSystem();

	private void Awake()
	{
		m_chilliConnect = new ChilliConnectSdk(GAME_TOKEN, true); 
		m_accountSystem.Initialise(m_chilliConnect);
		m_teamsSystem.Initialise(m_chilliConnect);
	}
}
