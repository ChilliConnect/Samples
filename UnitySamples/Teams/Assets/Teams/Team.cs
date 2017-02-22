using System;
using UnityEngine;
using System.Collections.Generic;

/// Basic team definition. This allows the rest of the code
/// to interact with teams without being explicitly coupled 
/// the ChilliConnectSDK types.
/// 
public class Team 
{
	public string ID { get; set; }
    public string Name { get; set; }
	public int PlayerCount { get; set; }
}
