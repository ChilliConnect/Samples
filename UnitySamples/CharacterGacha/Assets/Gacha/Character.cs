using System;
using UnityEngine;
using System.Collections.Generic;
using SdkCore;

/// Basic character definition. This allows the rest of the code
/// to interact with characters without being explicitly coupled 
/// the ChilliConnectSDK types.
/// 
public class Character 
{
	public string CharacterID { get; set; }
	public int CharacterLevel { get; set; }
	public int CharacterAtk { get; set; }
	public int CharacterDef { get; set; }
}
