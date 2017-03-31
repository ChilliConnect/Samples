using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using ChilliConnect;

/// Controls the elements rendered in the list of teams.
/// 
public class RecipeListElementUIController : MonoBehaviour
{
	private Recipe m_recipe;
	private Text m_recipeName;
	private Text m_recipeKey;
	private Text m_recipeCost;
	private GameObject m_createButton;
	private Image m_background;

	/// Gather the UI
	/// 
	private void Awake()
	{
		var createButton = transform.FindChild ("CreateCharacterButton").GetComponent<Button> ();
		createButton.onClick.AddListener (OnCreateButtonClicked);

		m_createButton = createButton.gameObject;
		m_recipeName = transform.FindChild("RecipeName").GetComponent<Text>();
		m_recipeKey = transform.FindChild("RecipeKey").GetComponent<Text>();
		m_recipeCost = transform.FindChild("RecipeCost").GetComponent<Text>();
		m_background = GetComponent<Image>();
	}

	/// When the Join button is clicked, use the TeamsSystem to 
	/// join the team
	/// 
	private void OnCreateButtonClicked ()
	{
		EconomySystem.Get ().CookRecipe (m_recipe);
	}

	/// Update the element with the team data
	///
	/// @param team
	/// 	The Team to displau
	/// 
	/// @param isPlayersTeam
	/// 	Is player a memeber of the provided team?
	/// 
	/// @param playerHasTeam
	/// 	Is the player a member of any team?
	/// 
	public void Init(Recipe recipe)
	{
		m_recipeName.text = recipe.RecipeName;
		m_recipeKey.text = recipe.RecipeKey;
		m_recipeCost.text = "Coins: " + recipe.CoinCost.ToString();

		m_recipe = recipe;
	}
}
