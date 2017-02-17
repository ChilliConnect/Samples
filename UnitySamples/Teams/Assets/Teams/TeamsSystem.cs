using UnityEngine;
using System.Collections;
using ChilliConnect;
using System.Linq;
using System;
using System.Collections.Generic;
using SdkCore;
using SdkCore.MiniJSON;

public class TeamsSystem 
{
	private const string TEAMS_KEY = "TEAMS";
	private const string PLAYER_DATA_TEAM_KEY = "TEAM";

	private const string SCRIPT_LEAVE_TEAM = "LEAVE_TEAM";
	private const string SCRIPT_CREATE_TEAM = "CREATE_TEAM";
	private const string SCRIPT_JOIN_TEAM = "JOIN_TEAM";

	public event Action<List<Team>> OnTeamsRefreshed = delegate {};
	public event Action<Team> OnPlayerTeamRefreshed = delegate {};
	public event Action<Team> OnTeamCreated = delegate {};

	private static TeamsSystem s_singletonInstance = null;

	public List<Team> Teams { get; set; }
	public Team PlayerTeam { get; set; }

	private ChilliConnectSdk m_chilliConnect;

	public static TeamsSystem Get()
	{
		return s_singletonInstance;
	}

	public TeamsSystem()
	{
		s_singletonInstance = this;
		Teams = new List<Team> ();
	}

	public void Initialise(ChilliConnectSdk chilliConnect)
	{
		m_chilliConnect = chilliConnect;
	}

	public void LeaveTeam()
	{
		var runScriptRequest = new RunScriptRequestDesc(SCRIPT_LEAVE_TEAM);

		m_chilliConnect.CloudCode.RunScript( runScriptRequest, 
			(request, response) => LeaveTeamCallBack(response.Output.AsDictionary() ),
			(request, error) => Debug.LogError(error.ErrorDescription) );
	}

	private void LeaveTeamCallBack(MultiTypeDictionary output)
	{
		var wasError = output.GetBool("Error");
		if ( wasError ) {
			UnityEngine.Debug.Log("Error leaving team");
		}
		else {
			PlayerTeam = null;
			OnPlayerTeamRefreshed(null);
		}
	}

	public void JoinTeam (Team team)
	{
		var scriptParams = new Dictionary<string, SdkCore.MultiTypeValue> ();
		scriptParams.Add ("TeamID", team.ID);

		var runScriptRequest = new RunScriptRequestDesc(SCRIPT_JOIN_TEAM);
		runScriptRequest.Params = scriptParams;

		m_chilliConnect.CloudCode.RunScript( runScriptRequest, 
			(request, response) => JoinTeamCallBack( response.Output.AsDictionary(), team ),
			(request, error) => Debug.LogError(error.ErrorDescription) );
	}

	private void JoinTeamCallBack(MultiTypeDictionary output, Team team)
	{
		var wasError = output.GetBool("Error");
		if ( wasError ) {
			UnityEngine.Debug.Log("Error joining team");
		}
		else {
			PlayerTeam = team;
			OnPlayerTeamRefreshed(team);
		}
	}

	public void CreateTeam (string teamName)
	{
		var scriptParams = new Dictionary<string, SdkCore.MultiTypeValue> ();
		scriptParams.Add ("Name", teamName);

		var runScriptRequest = new RunScriptRequestDesc(SCRIPT_CREATE_TEAM);
		runScriptRequest.Params = scriptParams;

		m_chilliConnect.CloudCode.RunScript( runScriptRequest, 
			(request, response) => TeamCreatedCallBack(response.Output.AsDictionary()),
			(request, error) => Debug.LogError(error.ErrorDescription) );
	}

	private void TeamCreatedCallBack(MultiTypeDictionary output)
	{
		//Check the script returned an error as part of the response
		var wasError = output.GetBool("Error");
		if ( wasError ) {
			UnityEngine.Debug.Log("Error creating team");
			return;
		}

		//Get the Team data returned from the script
		var teamProperties = output.GetDictionary("Team");

		//Construct a new team object
		var team = new Team();
		team.ID = teamProperties.GetString("TeamID");
		team.Name = teamProperties.GetString("Name");
		team.PlayerCount = 1;

		//Set this as the players current team
		PlayerTeam = team;

		//Add to the local list of stored teams
		Teams.Insert(0, team);

		//Inform listeners that a new team is created, the players current team
		//has changed and that the team list has been updated
		OnTeamCreated(team);
		OnTeamsRefreshed(Teams);
		OnPlayerTeamRefreshed(PlayerTeam);
	}

	private void OnPlayerTeamFetched(GetPlayerDataResponse response)
	{
		if (response.Values.Count > 0 ) { 
			var key = response.Values [0].Value;
			if (key.IsDictionary ()) {
				var teamProperties = key.AsDictionary ();

				var team = new Team ();
				team.ID = teamProperties.GetString ("TeamID");
				team.Name = teamProperties.GetString ("TeamName");

				PlayerTeam = team;
			}
		}

		OnPlayerTeamRefreshed (PlayerTeam);
	}

	public void FetchPlayerTeam()
	{
		PlayerTeam = null;

		var keys = new List<string>();
		keys.Add ( PLAYER_DATA_TEAM_KEY);

		m_chilliConnect.CloudData.GetPlayerData (keys, 
			(request, response) => OnPlayerTeamFetched (response), 
			(request, error) => Debug.LogError (error.ErrorDescription) );
	}

	public void FetchTeams()
	{
		var desc = new QueryCollectionRequestDesc(TEAMS_KEY);

		m_chilliConnect.CloudData.QueryCollection( desc, 
			(request, response ) => OnTeamsFetched(response) , 
			(request, error) => Debug.LogError(error.ErrorDescription));
	}

	private void OnTeamsFetched(QueryCollectionResponse response) 
	{
		Teams.Clear ();
		foreach( CollectionDataObject teamObject in response.Objects) {
			var teamProperties = teamObject.Value.AsDictionary ();

			var team = new Team ();
			team.ID = teamObject.ObjectId;
			team.Name = teamProperties.GetString ("Name");
			team.PlayerCount = teamProperties.GetList ("Players").Count;
			Teams.Add( team );
		}

		OnTeamsRefreshed (Teams);
	}

}
