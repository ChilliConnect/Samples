using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChilliConnect;
using System;

public class EconomySystem 
{
	public event System.Action<int> CurrencyBalanceRetrieved = delegate {};
	public event Action<List<Recipe>> OnRecipeListPopulate = delegate {};
	public event Action<List<Character>> OnCharacterListPopulate = delegate {};

	private const string SPAWN_CHARACTER = "SPAWN_CHARACTER";
	private const string CURRENCY_KEY = "COINS";
	private const string RECIPE_TAG = "Recipe";
	private const string CHARACTER_KEY = "CHARACTER";

	/// The list of available recipes 
	/// 
	public List<Recipe> Recipes { get; set; }

	/// The list of available characters for the logged in character 
	/// 
	public List<Character> Characters { get; set; }

	private static EconomySystem s_singletonInstance = null;

	private ChilliConnectSdk m_chilliConnect;

	public static EconomySystem Get()
	{
		return s_singletonInstance;
	}

	public EconomySystem()
	{
		s_singletonInstance = this;
	}

	public void Initialise(ChilliConnectSdk chilliConnect)
	{
		m_chilliConnect = chilliConnect;
		Recipes = new List<Recipe> ();
		Characters = new List<Character> ();
	}


	/// Gets the players character list. invokes a call to ChilliConnect GetInventoryByKeys 
	/// with a list of keys to retrieve, only one needed here.
	/// 
	public void GetPlayersCharacterList()
	{
		var keys = new List<string>();
		keys.Add ( CHARACTER_KEY );

		m_chilliConnect.Economy.GetInventoryForKeys (keys,
			(request, response) => RenderCharacterList(response), 
			(request, error) => Debug.LogError (error.ErrorDescription));
	}

	/// Gets the recipe list for the user depending on their TestGroup. 
	/// TestGroup is populated before the login event happens and defaults to 
	/// a static defined on the  AccountSystem
	///
	public void GetRecipeMetaData()
	{			
		var tags = new List<string>();
		tags.Add ( RECIPE_TAG );

		var getMetadataDefinitionsRequestDesc = new GetMetadataDefinitionsRequestDesc ();
		getMetadataDefinitionsRequestDesc.Tags = tags;

		m_chilliConnect.Catalog.GetMetadataDefinitions (getMetadataDefinitionsRequestDesc,
			(request, response) => RenderRecipeList(response), 
			(request, error) => Debug.LogError (error.ErrorDescription));
	}

	/// Gets the currently logged in players "Coins" balance.
	/// Updates the display on completion
	///
	public void GetPlayerCoinBalance()
	{
		var keys = new List<string>();
		keys.Add ( CURRENCY_KEY );

		var getCurrencyBalanceRequestDesc = new GetCurrencyBalanceRequestDesc ();
		getCurrencyBalanceRequestDesc.Keys = keys;

		m_chilliConnect.Economy.GetCurrencyBalance (getCurrencyBalanceRequestDesc, 
			(request, response) => UpdatePlayerCoinDisplay (response), 
			(request, error) => Debug.LogError (error.ErrorDescription));
	}

	private void UpdatePlayerCoinDisplay(GetCurrencyBalanceResponse response)
	{
		//Update to get by name rather than index, incase there is more than one currency...
		CurrencyBalanceRetrieved(response.Balances[0].Balance);
	}

	/// Handles the response of the GetRecipeMetaData call to ChilliConnect.
	/// Loads the returned objects to a list of Recipe definitions and notifies listeners
	///
	private void RenderRecipeList(GetMetadataDefinitionsResponse response)
	{
		Recipes.Clear ();

		foreach( MetadataDefinition metadataItem in response.Items) {
			var recipeName = metadataItem.Name;
			var recipeKey = metadataItem.Key;
			var itemCustomData = metadataItem.CustomData;
			var minLvlRange = itemCustomData.AsDictionary ().GetInt ("AttributesMin");
			var maxLvlRange = itemCustomData.AsDictionary ().GetInt ("AttributesMax");
			var coinCost = itemCustomData.AsDictionary ().GetInt ("Cost");

			var recipe = new Recipe ();
			recipe.RecipeName = recipeName;
			recipe.RecipeKey = recipeKey;
			recipe.LevelRangeMin = minLvlRange;
			recipe.LevelRangeMax = maxLvlRange;
			recipe.CoinCost = coinCost;
			Recipes.Add( recipe );
		};
	
		OnRecipeListPopulate (Recipes);
	}

	/// Handles the response of the GetPlayersCharacterList call to ChilliConnect.
	/// Loads the returned objects to a list of Character definitions and notifies listeners
	///
	private void RenderCharacterList(GetInventoryForKeysResponse response)
	{
		Characters.Clear ();

		foreach( PlayerInventoryItem inventoryItem in response.Items) {
			var instanceData = inventoryItem.InstanceData.AsDictionary();

			var characterId = inventoryItem.ItemId;
			var characterLevel = instanceData.GetInt ("Level");
			var characterAttack = instanceData.GetInt ("Attack");
			var characterDefence = instanceData.GetInt ("Defence");

			var character = new Character ();
			character.CharacterID = characterId;
			character.CharacterLevel = characterLevel;
			character.CharacterAtk = characterAttack;
			character.CharacterDef = characterDefence;
			Characters.Add( character );
		};

		OnCharacterListPopulate (Characters);
	}

	/// Invokes ChilliConnect to run the SPAWN_CHARACTER script with the recipe key from
	/// the clicked recipe on the UI.
	///
	public void CookRecipe(Recipe recipeKey)
	{
		var scriptParams = new Dictionary<string, SdkCore.MultiTypeValue> ();
		scriptParams.Add ("Recipe", recipeKey.RecipeKey);

		var runScriptRequest = new RunScriptRequestDesc(SPAWN_CHARACTER);
		runScriptRequest.Params = scriptParams;

		m_chilliConnect.CloudCode.RunScript( runScriptRequest, 
			(request, response) => PostCharacterCreationActions(response),
			(request, error) => Debug.LogError(error.ErrorDescription) );
	}

	/// Handles the response of the CookRecipe call. Calls the method responsible for 
	/// rendering characters and updates the currency display.
	///
	public void PostCharacterCreationActions(RunScriptResponse response)
	{
		var scriptResponse = response.Output.AsDictionary ();
		var wasError = scriptResponse.GetBool ("Error");

		if (wasError) {
			UnityEngine.Debug.Log ("Not Enough Coins");
		} else {
			GetPlayersCharacterList ();

			var remainingCoins = scriptResponse.GetInt("CoinBalance");
			CurrencyBalanceRetrieved (remainingCoins);
		}
	}	
}
