using System;
using System.Collections.Generic;
using UnityEngine;
using ChilliConnect;

/// The main controller class for the IAP demo. Creates the systems and handles the main control flow
/// and intialises ChilliConnect.
/// 
/// IAP Demo: 
/// - Shows a simple purchase catalogue populated by items specified via ChilliConnect dashboard
/// - Allows the user to purchase and redeem items which will update the ChilliConnect and local inventory
/// - Demo works for Apple and GooglePlay (but Kindle is also supported).
/// 
/// More info on setting up ChilliConnect can be found here: https://docs.chilliconnect.com/guide/setup/
/// 
public class IAPDemoSceneController : MonoBehaviour
{
	const string GAME_TOKEN = "Vv7VANzImRtEUeiYaoz4lWKqB6t349iy";

	private ChilliConnectSdk m_chilliConnect = null;
	private IAPSystem m_iapSystem = new IAPSystem();
	private InventorySystem m_inventorySystem = new InventorySystem();

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
			var loginRequestDesc = new LogInUsingChilliConnectRequestDesc(PlayerPrefs.GetString("CCId"), PlayerPrefs.GetString("CCSecret"));
			m_chilliConnect.PlayerAccounts.LogInUsingChilliConnect(loginRequestDesc, (loginRequest, loginResponse) => OnLoggedIn(), (loginRequest, error) => Debug.LogError(error.ErrorDescription));
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

		var loginRequestDesc = new LogInUsingChilliConnectRequestDesc(response.ChilliConnectId, response.ChilliConnectSecret);
		m_chilliConnect.PlayerAccounts.LogInUsingChilliConnect(loginRequestDesc, (loginRequest, loginResponse) => OnLoggedIn(), (loginRequest, error) => Debug.LogError(error.ErrorDescription));
	}

	/// Called on successful login to ChilliConnect and initialises the systems we
	/// require for this demo. Now jump to CatalogueController.cs to see the purchase flow or
	/// Inventory system to see how the inventory is obtained.
	/// 
	private void OnLoggedIn()
	{
		Debug.Log("Login successful. Initialising IAP and Inventory");

		//Now we have logged in, we can pull down the player inventory and the catalogue
		m_iapSystem.Initialise(m_chilliConnect);
		m_inventorySystem.Initialise(m_chilliConnect);
	}
}
