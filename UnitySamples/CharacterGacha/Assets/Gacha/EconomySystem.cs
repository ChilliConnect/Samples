using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChilliConnect;
using System;

public class EconomySystem 
{
	public event System.Action<int> CurrencyBalanceRetrieved = delegate {};

	public event Action<List<Recipe>> OnRecipeListPopulate = delegate {};

	private const string SPAWN_CHARACTER = "SPAWN_CHARACTER";

	private const string CURRENCY_KEY = "COINS";

	private const string RECIPE_TAG = "Recipe";

	/// The list of available teams 
	/// 
	public List<Recipe> Recipes { get; set; }

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
	}

	public void GetRecipeMetaData()
	{
		var tags = new List<string>();
		tags.Add ( RECIPE_TAG );

		var getMetadataDefinitionsRequestDesc = new GetMetadataDefinitionsRequestDesc ();
		getMetadataDefinitionsRequestDesc.Tags = tags;

		m_chilliConnect.Economy.GetMetadataDefinitions (getMetadataDefinitionsRequestDesc,
			(request, response) => RenderRecipeList(response), 
			(request, error) => Debug.LogError (error.ErrorDescription));
	}

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

	public void CookRecipe(Recipe recipeKey)
	{
		var scriptParams = new Dictionary<string, SdkCore.MultiTypeValue> ();
		scriptParams.Add ("Recipe", recipeKey.RecipeKey);

		var runScriptRequest = new RunScriptRequestDesc(SPAWN_CHARACTER);
		runScriptRequest.Params = scriptParams;

		m_chilliConnect.CloudCode.RunScript( runScriptRequest, 
			(request, response) => UnityEngine.Debug.Log("Charcter has been created! Gud Jerb!"),
			(request, error) => Debug.LogError(error.ErrorDescription) );
	}
}
