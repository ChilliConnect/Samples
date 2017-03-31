using UnityEngine;
using System.Collections;
using ChilliConnect;

/// Top level controller for the Gacha project. Create the systems and initialises 
/// Chilliconnect.
/// 
public class GachaSceneController : MonoBehaviour 
{
	//Replace this with the game token from your ChilliConnect dashboard
	const string GAME_TOKEN = "";

	private ChilliConnectSdk m_chilliConnect = null;
	private AccountSystem m_accountSystem = new AccountSystem();
	private EconomySystem m_economySystem = new EconomySystem();

	private void Awake()
	{
		m_chilliConnect = new ChilliConnectSdk(GAME_TOKEN, true); 
		m_accountSystem.Initialise(m_chilliConnect);
		m_economySystem.Initialise(m_chilliConnect);
	}
}
