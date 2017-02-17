using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeamsDemoUIController : MonoBehaviour 
{
	private GameObject m_createTeamButton;
	private GameObject m_leaveTeamButton;
	private GameObject m_teamName;
	private GameObject m_teamsPanel;
	private Text m_playerTeamLabel;
	private Text m_loginLabel;

	private TeamListUIController m_teamListUIController;

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

	private void Start()
	{
		AccountSystem.Get().OnPlayerLoggedIn += OnPlayerLoggedIn;

		TeamsSystem.Get ().OnPlayerTeamRefreshed += OnPlayerTeamRefreshed;
		TeamsSystem.Get ().OnTeamsRefreshed += (teams) => m_teamListUIController.Refresh(teams, TeamsSystem.Get().PlayerTeam );
		TeamsSystem.Get ().OnTeamCreated += OnTeamCreated;
	}

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

		m_teamListUIController.Refresh(TeamsSystem.Get().Teams, TeamsSystem.Get().PlayerTeam );
	}

	private void OnPlayerLoggedIn( string chilliConnectId ) {
		TeamsSystem.Get ().FetchTeams ();
		TeamsSystem.Get ().FetchPlayerTeam();
		m_loginLabel.text = "Logged in as " + chilliConnectId;
	}

	private void OnTeamCreated(Team team)
	{
		m_teamName.GetComponent<InputField>().text = "";
	}

	private void OnCreateTeamClicked()
	{
		var teamName = m_teamName.GetComponent<InputField>().text;
		if (teamName.Length > 0) {
			TeamsSystem.Get ().CreateTeam (teamName);
		}
	}

	private void OnCreateNewPlayerClicked() 
	{
		AccountSystem.Get ().CreateNewAccount ();
	}

	private void OnLeaveTeamClicked()
	{
		TeamsSystem.Get ().LeaveTeam ();
	}
}
