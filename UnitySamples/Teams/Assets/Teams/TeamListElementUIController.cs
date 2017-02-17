using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using ChilliConnect;

public class TeamListElementUIController : MonoBehaviour
{
	private Team m_team;
	private Text m_name;
	private Text m_players;
	private GameObject m_joinButton;
	private Image m_background;

	private void Awake()
	{
		var joinButton = transform.FindChild ("JoinTeamButton").GetComponent<Button> ();
		joinButton.onClick.AddListener (OnJoinButtonClicked);

		m_joinButton = joinButton.gameObject;
		m_name = transform.FindChild("Name").GetComponent<Text>();
		m_players = transform.FindChild("Players").GetComponent<Text>();
		m_background = GetComponent<Image>();
	}

	private void OnJoinButtonClicked ()
	{
		TeamsSystem.Get ().JoinTeam (m_team);
	}

	public void Init(Team team, bool isPlayersTeam, bool playerHasTeam)
	{
		m_background.color = isPlayersTeam ? Color.yellow : Color.white;
		m_joinButton.SetActive (!playerHasTeam);

		m_name.text = team.Name;
		m_players.text = team.PlayerCount + " Players";
		m_team = team;
	}
}
