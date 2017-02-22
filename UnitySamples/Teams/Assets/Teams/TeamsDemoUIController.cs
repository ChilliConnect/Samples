using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// Handles main UI interactions including team creation, leaving a team and 
/// creating a new player account. Is responsible for refreshing UI elements
/// when player account or team status is changed.
///  
public class TeamsDemoUIController : MonoBehaviour 
{
	private GameObject m_createTeamButton;
	private GameObject m_leaveTeamButton;
	private GameObject m_teamName;
	private GameObject m_teamsPanel;
	private Text m_playerTeamLabel;
	private Text m_loginLabel;

	private TeamListUIController m_teamListUIController;

	/// Gather the UI
	/// 
	private void Awake()
	{
		m_playerTeamLabel = transform.FindChild ("PlayerTeamLabel").GetComponent<Text> ();
		m_teamsPanel = transform.FindChild("Teams").gameObject;
		m_teamListUIController= m_teamsPanel.GetComponent<TeamListUIController>();

		var createTeamButton = transform.FindChild("CreateTeamButton").GetComponent<Button>();
		createTeamButton.onClick.AddListener(OnCreateTeamClicked);
		m_createTeamButton = createTeamButton.gameObject;

		var leaveTeamButton = transform.FindChild("LeaveTeamButton").GetComponent<Button>();
		leaveTeamButton.onClick.AddListener (OnLeaveTeamClicked);
		m_leaveTeamButton = leaveTeamButton.gameObject;

		m_loginLabel = transform.FindChild ("LoginLabel").GetComponent<Text> ();
		m_teamName = transform.FindChild ("TeamName").gameObject;

		var createNewPlayerButton = transform.FindChild("CreateNewPlayerButton").GetComponent<Button>();
		createNewPlayerButton.onClick.AddListener (OnCreateNewPlayerClicked);
	}

	/// Registers handlers for team and account system
	/// 
	private void Start()
	{
		AccountSystem.Get().OnPlayerLoggedIn += OnPlayerLoggedIn;

		TeamsSystem.Get ().OnPlayerTeamRefreshed += OnPlayerTeamRefreshed;
		TeamsSystem.Get ().OnTeamsRefreshed += (teams) => m_teamListUIController.Refresh(teams, TeamsSystem.Get().PlayerTeam );
		TeamsSystem.Get ().OnTeamCreated += OnTeamCreated;
	}

	/// Handler for the players team changing, either joining a new
	/// team or leaving a team
	/// 
	/// @param team 
	/// 	The team the player has joined. Null if the player is not a 
	/// 	member of any team.
	/// 
	private void OnPlayerTeamRefreshed(Team team) {
		var hasTeam = team != null;
		if ( hasTeam) {
			m_playerTeamLabel.text = team.Name;
			m_playerTeamLabel.color = Color.white;
		}
		else {
			m_playerTeamLabel.text = "None";	
			m_playerTeamLabel.color = Color.grey;
		}

		m_createTeamButton.SetActive (!hasTeam);
		m_teamName.SetActive (!hasTeam);
		m_leaveTeamButton.SetActive (hasTeam);

		//Refresh the team list 
		m_teamListUIController.Refresh(TeamsSystem.Get().Teams, TeamsSystem.Get().PlayerTeam );
	}

	/// Handler for the player logging in. Refreshes the team list from
	/// ChilliConnect as well as the players selected team.
	/// 
	/// @param chilliConnectId
	/// 	The ChilliConnectId of the player logged in
	/// 
	private void OnPlayerLoggedIn( string chilliConnectId ) {
		TeamsSystem.Get ().FetchTeams ();
		TeamsSystem.Get ().FetchPlayerTeam();
		m_loginLabel.text = "Logged in as " + chilliConnectId;
	}

	/// Handler for a new team being created. Clears the create team input text
	/// 
	private void OnTeamCreated(Team team)
	{
		m_teamName.GetComponent<InputField>().text = "";
	}

	/// Handler for the create team button being clicked. If a team name has 
	/// been provided, will use the TeamsSystem to create a new team.
	/// 
	private void OnCreateTeamClicked()
	{
		var teamName = m_teamName.GetComponent<InputField>().text;
		if (teamName.Length > 0) {
			TeamsSystem.Get ().CreateTeam (teamName);
		}
	}

	/// Handler for the create player button being clicked. Will use the
	/// AccountSystem to create a new player account
	/// 
	private void OnCreateNewPlayerClicked() 
	{
		AccountSystem.Get ().CreateNewAccount ();
	}

	/// Handler for the leave team button being clicked. Will use the
	/// Teams system to leave the current team.
	/// 
	private void OnLeaveTeamClicked()
	{
		TeamsSystem.Get ().LeaveTeam ();
	}
}
