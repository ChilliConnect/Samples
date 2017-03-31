using System;
using UnityEngine;
using System.Collections.Generic;
using SdkCore;

/// Basic recipe definition. This allows the rest of the code
/// to interact with recipes without being explicitly coupled 
/// the ChilliConnectSDK types.
/// 
public class Recipe 
{
	public string RecipeName { get; set; }
	public string RecipeKey { get; set; }
	public int LevelRangeMin { get; set; }
	public int LevelRangeMax { get; set; }
	public int CoinCost { get; set; }
}
