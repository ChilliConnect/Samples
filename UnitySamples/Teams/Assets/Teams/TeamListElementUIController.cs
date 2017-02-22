using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using ChilliConnect;

/// Controls the elements rendered in the list of teams.
/// 
public class TeamListElementUIController : MonoBehaviour
{
	private Team m_team;
	private Text m_name;
	private Text m_players;
	private GameObject m_joinButton;
	private Image m_background;

	/// Gather the UI
	/// 
	private void Awake()
	{
		var joinButton = transform.FindChild ("JoinTeamButton").GetComponent<Button> ();
		joinButton.onClick.AddListener (OnJoinButtonClicked);

		m_joinButton = joinButton.gameObject;
		m_name = transform.FindChild("Name").GetComponent<Text>();
		m_players = transform.FindChild("Players").GetComponent<Text>();
		m_background = GetComponent<Image>();
	}

	/// When the Join button is clicked, use the TeamsSystem to 
	/// join the team
	/// 
	private void OnJoinButtonClicked ()
	{
		TeamsSystem.Get ().JoinTeam (m_team);
	}

	/// Update the element with the team data
	///
	/// @param team
	/// 	The Team to displau
	/// 
	/// @param isPlayersTeam
	/// 	Is player a memeber of the provided team?
	/// 
	/// @param playerHasTeam
	/// 	Is the player a member of any team?
	/// 
	public void Init(Team team, bool isPlayersTeam, bool playerHasTeam)
	{
		//Set the background yellow if this is the players team
		m_background.color = isPlayersTeam ? Color.yellow : Color.white;

		//Hide the Join button if the player is already in a team
		m_joinButton.SetActive (!playerHasTeam);

		//Display the team details and player count
		m_name.text = team.Name;
		m_players.text = team.PlayerCount + " Players";
		m_team = team;
	}
}
