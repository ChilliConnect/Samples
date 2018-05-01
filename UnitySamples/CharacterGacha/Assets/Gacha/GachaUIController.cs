using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ChilliConnect;

/// Handles main UI interactions including creating a new character for the logged in user, 
/// creating a new player account. Is responsible for refreshing UI elements
/// when player account or character list is updated/changed.
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
		m_loginLabel = transform.Find ("LoginLabel").GetComponent<Text> ();
		m_balanceAmount = transform.Find ("BalanceLabel").GetComponent<Text> ();

		m_recipeListPanel = transform.Find("RecipeList").gameObject;
		m_recipeListUIController= m_recipeListPanel.GetComponent<RecipeListUIController>();

		m_characterListPanel = transform.Find("CharacterList").gameObject;
		m_characterListUIController = m_characterListPanel.GetComponent<CharacterListUIController>();

		var createNewPlayerButton = transform.Find("CreateNewPlayerButton").GetComponent<Button>();
		createNewPlayerButton.onClick.AddListener (OnCreateNewPlayerClicked);
	}

	/// Registers handlers for economy and account system
	/// 
	private void Start()
	{
		AccountSystem.Get().OnPlayerLoggedIn += OnPlayerLoggedIn;

		EconomySystem.Get().CurrencyBalanceRetrieved += CurrencyBalanceRetrieved;
		EconomySystem.Get().OnRecipeListPopulate += (recipes) => m_recipeListUIController.populateRecipeList(recipes);
		EconomySystem.Get().OnCharacterListPopulate += (characters) => m_characterListUIController.populateCharacterList(characters);
	}
		
	/// Handler for the player logging in. Refreshes the recipe and 
	/// character list from ChilliConnect
	/// 
	/// @param chilliConnectId
	/// 	The ChilliConnectId of the player logged in
	/// 
	private void OnPlayerLoggedIn( string chilliConnectId ) {
		EconomySystem.Get ().GetPlayerCoinBalance();
		EconomySystem.Get ().GetRecipeMetaData();
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
