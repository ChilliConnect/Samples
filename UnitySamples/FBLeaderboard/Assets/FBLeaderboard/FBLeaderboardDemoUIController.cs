using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/// Manages the elements that make up the Facebook UI interface
///
public class FBLeaderboardDemoUIController : MonoBehaviour 
{
	private GameObject m_fbLoginButton;
	private GameObject m_playerInfoPanel;
	private GameObject m_postScoreButton;
	private GameObject m_refreshButton;
	private GameObject m_leaderboardPanel;
	private GameObject m_title;

	private Text m_localPlayerName;
	private Text m_postScoreText;
	private LeaderboardUIController m_leaderboardUIController;

	private int m_currentPostScore = 0;
	private float m_postScoreTimer = 0.0f;

	/// Gather all the UI elements
	/// 
	private void Awake()
	{
		var fbLoginButton = transform.FindChild("FBLoginButton").GetComponent<Button>();
		fbLoginButton.onClick.AddListener(OnFBLoginSelected);
		m_fbLoginButton = fbLoginButton.gameObject;

		m_title = transform.FindChild("Title").gameObject;

		m_leaderboardPanel = transform.FindChild("Leaderboard").gameObject;
		m_leaderboardUIController = m_leaderboardPanel.GetComponent<LeaderboardUIController>();

		m_playerInfoPanel = transform.FindChild("PlayerInfo").gameObject;
		m_localPlayerName = m_playerInfoPanel.transform.FindChild("Name").GetComponent<Text>();

		var postScoreButton = transform.FindChild("PostScoreButton").GetComponent<Button>();
		postScoreButton.onClick.AddListener(OnPostScoreSelected);
		m_postScoreButton = postScoreButton.gameObject;
		m_postScoreText = m_postScoreButton.transform.FindChild("Text").GetComponent<Text>();

		var refreshButton = transform.FindChild("LeaderboardRefreshButton").GetComponent<Button>();
		refreshButton.onClick.AddListener(OnRefreshLeaderboardSelected);
		m_refreshButton = refreshButton.gameObject;

		SetState(AccountSystem.AccountStatus.NONE);
	}

	/// Listen to events that will change the UI
	/// 
	private void Start()
	{
		AccountSystem.Get().OnAccountStatusChanged += SetState;
		LeaderboardSystem.Get().OnLeaderboardRefreshed += (scores) => m_leaderboardUIController.Refresh(scores);
	}

	/// Decides what to display based on the users login status
	///
	private void SetState(AccountSystem.AccountStatus status)
	{
		switch(status)
		{
		case AccountSystem.AccountStatus.NONE:
			m_fbLoginButton.SetActive(false);
			m_playerInfoPanel.SetActive(false);
			m_postScoreButton.SetActive(false);
			m_refreshButton.SetActive(false);
			m_leaderboardPanel.SetActive(false);
			m_title.SetActive(false);
			break;
		case AccountSystem.AccountStatus.LOGIN_ANONYMOUS:
			m_fbLoginButton.SetActive(true);
			m_playerInfoPanel.SetActive(true);
			m_postScoreButton.SetActive(true);
			m_refreshButton.SetActive(false);
			m_leaderboardPanel.SetActive(false);
			m_title.SetActive(false);

			m_localPlayerName.text = "Anonymous";
			break;
		case AccountSystem.AccountStatus.LOGIN_FB:
			m_fbLoginButton.SetActive(false);
			m_playerInfoPanel.SetActive(true);
			m_postScoreButton.SetActive(true);
			m_refreshButton.SetActive(true);
			m_leaderboardPanel.SetActive(true);
			m_title.SetActive(true);

			m_localPlayerName.text = AccountSystem.Get().GetLocalPlayerName();

			LeaderboardSystem.Get().FetchFriendLeaderboard();
			break;
		}
	}

	/// Called when the user presses the FB login button.
	///
	private void OnFBLoginSelected()
	{
		AccountSystem.Get().FBLogin();
	}

	/// Called when the user presses the post score button.
	/// The current score is captured and set to ChilliConnect. Depending on the
	/// leaderboard config the score is accepted or discarded. In our case it will only
	/// be accepted if it is higher than our existing score
	/// 
	private void OnPostScoreSelected()
	{
		LeaderboardSystem.Get().PostScore(m_currentPostScore);
	}

	/// Called when the user presses the refresh button. Pulls the latest
	/// friends scores from ChilliConnect and updates the leaderboard display
	/// 
	private void OnRefreshLeaderboardSelected()
	{
		LeaderboardSystem.Get().FetchFriendLeaderboard();
	}

	/// Updates the score button
	///
	private void Update()
	{
		float duration = 5.0f;
		m_postScoreTimer = Mathf.Repeat(m_postScoreTimer + Time.deltaTime, duration);
		m_currentPostScore = Mathf.FloorToInt(Mathf.Lerp(0, 1000, m_postScoreTimer/duration));
		m_postScoreText.text = string.Format("Post Score: {0}", m_currentPostScore.ToString());
	}
}
