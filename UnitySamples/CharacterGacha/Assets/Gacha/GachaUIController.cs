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
	private Text m_loginLabel;
	private Text m_balanceAmount;

	private RecipeListUIController m_recipeListUIController;
	private GameObject m_recipeListPanel;

	private CharacterListUIController m_characterListUIController;
	private GameObject m_characterListPanel;


	/// Gather the UI
	/// 
	private void Awake()
	{
		m_loginLabel = transform.FindChild ("LoginLabel").GetComponent<Text> ();
		m_balanceAmount = transform.FindChild ("BalanceLabel").GetComponent<Text> ();

		m_recipeListPanel = transform.FindChild("RecipeList").gameObject;
		m_recipeListUIController= m_recipeListPanel.GetComponent<RecipeListUIController>();

		m_characterListPanel = transform.FindChild("CharacterList").gameObject;
		m_characterListUIController = m_characterListPanel.GetComponent<CharacterListUIController>();

		var createNewPlayerButton = transform.FindChild("CreateNewPlayerButton").GetComponent<Button>();
		createNewPlayerButton.onClick.AddListener (OnCreateNewPlayerClicked);
	}

	/// Registers handlers for team and account system
	/// 
	private void Start()
	{
		AccountSystem.Get().OnPlayerLoggedIn += OnPlayerLoggedIn;

		EconomySystem.Get().CurrencyBalanceRetrieved += CurrencyBalanceRetrieved;
		EconomySystem.Get().OnRecipeListPopulate += (recipes) => m_recipeListUIController.populateRecipeList(recipes);
		EconomySystem.Get().OnCharacterListPopulate += (characters) => m_characterListUIController.populateCharacterList(characters);
	}
		
	/// Handler for the player logging in. Refreshes the team list from
	/// ChilliConnect as well as the players selected team.
	/// 
	/// @param chilliConnectId
	/// 	The ChilliConnectId of the player logged in
	/// 
	private void OnPlayerLoggedIn( string chilliConnectId ) {
		EconomySystem.Get ().GetPlayerCoinBalance();

		EconomySystem.Get ().GetRecipeMetaData(AccountSystem.Get().TestGroup);

		EconomySystem.Get ().GetPlayersCharacterList();

		m_loginLabel.text = "Logged in as " + chilliConnectId;
	}

	public void CurrencyBalanceRetrieved( int coinBalance ) 
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
