using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ChilliConnect;

/// Handles main UI interactions including team creation, leaving a team and 
/// creating a new player account. Is responsible for refreshing UI elements
/// when player account or team status is changed.
///  
public class GachaUIController : MonoBehaviour 
{
	private GameObject m_createTeamButton;
	private GameObject m_leaveTeamButton;
	private GameObject m_teamName;
	private GameObject m_teamsPanel;
	private Text m_playerTeamLabel;
	private Text m_loginLabel;
	private Text m_balanceAmount;


	private RecipeListUIController m_recipeListUIController;
	private GameObject m_recipeListPanel;

	/// Gather the UI
	/// 
	private void Awake()
	{
		m_loginLabel = transform.FindChild ("LoginLabel").GetComponent<Text> ();
		m_balanceAmount = transform.FindChild ("BalanceLabel").GetComponent<Text> ();

		m_recipeListPanel = transform.FindChild("RecipeList").gameObject;
		m_recipeListUIController= m_recipeListPanel.GetComponent<RecipeListUIController>();

		var createNewPlayerButton = transform.FindChild("CreateNewPlayerButton").GetComponent<Button>();
		createNewPlayerButton.onClick.AddListener (OnCreateNewPlayerClicked);
	}

	/// Registers handlers for team and account system
	/// 
	private void Start()
	{
		AccountSystem.Get().OnPlayerLoggedIn += OnPlayerLoggedIn;

		EconomySystem.Get().CurrencyBalanceRetrieved += CurrencyBalanceRetrieved;
		EconomySystem.Get ().OnRecipeListPopulate += (recipes) => m_recipeListUIController.populateRecipeList(recipes);
	}
		
	/// Handler for the player logging in. Refreshes the team list from
	/// ChilliConnect as well as the players selected team.
	/// 
	/// @param chilliConnectId
	/// 	The ChilliConnectId of the player logged in
	/// 
	private void OnPlayerLoggedIn( string chilliConnectId ) {
		EconomySystem.Get ().GetPlayerCoinBalance();
		EconomySystem.Get ().GetRecipeMetaData();

		m_loginLabel.text = "Logged in as " + chilliConnectId;
	}

	private void CurrencyBalanceRetrieved( int coinBalance ) 
	{
		m_balanceAmount.text = "Coins: " + coinBalance;
	}
		
	/// Handler for the create player button being clicked. Will use the
	/// AccountSystem to create a new player account
	/// 
	private void OnCreateNewPlayerClicked() 
	{
		AccountSystem.Get ().CreateNewAccount ();
	}
}
