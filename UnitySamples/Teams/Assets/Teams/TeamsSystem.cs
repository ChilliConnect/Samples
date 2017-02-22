using UnityEngine;
using System.Collections;
using ChilliConnect;
using System.Linq;
using System;
using System.Collections.Generic;
using SdkCore;
using SdkCore.MiniJSON;

/// Handles interactions with ChilliConnect to create, list, join
/// and leave teams.
/// 
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

	/// The list of available teams 
	/// 
	public List<Team> Teams { get; set; }

	/// The players current team. Can be null if the player
	/// is not a member of any team
	/// 
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

	/// Leave the current team that the player is a member of. Invokes the ChilliConnect
	/// Cloud Code script to leave teams.
	/// 
	public void LeaveTeam()
	{
		var runScriptRequest = new RunScriptRequestDesc(SCRIPT_LEAVE_TEAM);

		m_chilliConnect.CloudCode.RunScript( runScriptRequest, 
			(request, response) => LeaveTeamCallBack(response.Output.AsDictionary() ),
			(request, error) => Debug.LogError(error.ErrorDescription) );
	}

	/// Handles the response of the LeaveTeam script from ChilliConnect. Will log any
	/// any errors returned. If the request was succesfull, will clear the current player
	/// team and notify any listeners.
	/// 
	/// @oaram output 
	/// 	The response from the ChilliConnect script
	/// 
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

	/// Join the provided team. Invokes the ChilliConnect Join Team Cloud Code script,
	/// passing the ID of the provided team to the script.
	/// 
	/// @oaram team 
	/// 	The team to join
	/// 
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

	/// Handles the response of the JoinTeam script from ChilliConnect. Will log any
	/// any errors returned. If the request was succesfull, will update the currently
	/// stored player team and notify any listeners
	/// 
	/// @oaram output 
	/// 	The response from the ChilliConnect script
	/// 
	/// @oaram team
	/// 	The team that the player has joined
	/// 
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

	/// Create a new team. Invokes the ChilliConnect Create Team Cloud Code script,
	/// passing the name of the team to be created.
	/// 
	/// @oaram team 
	/// 	The name of the new team to create
	/// 
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

	/// Handles the response of the Create Team script from ChilliConnect. Will log any
	/// errors returned. On success, will add the new team to the local list of teams and
	/// update the players current team.
	/// 
	/// @oaram output 
	/// 	The output of the create team script
	/// 
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

	/// Loads the currently logged in playres team from ChilliConnect using the 
	/// GetPlayerData call.
	/// 
	public void FetchPlayerTeam()
	{
		PlayerTeam = null;

		var keys = new List<string>();
		keys.Add ( PLAYER_DATA_TEAM_KEY);

		m_chilliConnect.CloudData.GetPlayerData (keys, 
			(request, response) => OnPlayerTeamFetched (response), 
			(request, error) => Debug.LogError (error.ErrorDescription) );
	}

	/// Handles the response of the get player team call from ChilliConnect. Will
	/// parse the team name and team id from the returned data and store against the 
	/// current players team.
	/// 
	/// @param response
	/// 	The response of the GetPlayerDataCall from ChilliConnect
	/// 
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
		
	/// Requests a list of available teams from ChilliConnect using the QueryCollection
	/// method.
	/// 
	public void FetchTeams()
	{
		var desc = new QueryCollectionRequestDesc(TEAMS_KEY);

		m_chilliConnect.CloudData.QueryCollection( desc, 
			(request, response ) => OnTeamsFetched(response) , 
			(request, error) => Debug.LogError(error.ErrorDescription));
	}

	/// Handles the response of the QueryCollection call to ChilliConnect. Loads the returned
	/// objects in to a list of Team definitions and notifies listeners.
	/// 
	/// @param response
	/// 		The response of the QueryCollection method
	/// 
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
