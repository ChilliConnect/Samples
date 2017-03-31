using System;
using UnityEngine;
using System.Collections.Generic;
using SdkCore;

public class Recipe 
{
	public string RecipeName { get; set; }
	public string RecipeKey { get; set; }
	public int LevelRangeMin { get; set; }
	public int LevelRangeMax { get; set; }
	public int CoinCost { get; set; }
}
