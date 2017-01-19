using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/// Manages the elements that make up the Facebook UI interface
///
public class FBLeaderboardDemoUIController : MonoBehaviour 
{
	private enum State
	{
		INIT,
		FB_LOGGED_IN,
		FB_NOT_LOGGED_IN
	}

	private GameObject m_fbLoginButton;
	private GameObject m_fbPlayerInfoPanel;
	private GameObject m_postScoreButton;
	private GameObject m_refreshButton;

	private Text m_localPlayerName;
	private Image m_localPlayerProfile;
	private Text m_postScoreText;

	private int m_currentPostScore = 0;
	private float m_postScoreTimer = 0.0f;

	/// Gather all the UI elements
	/// 
	private void Awake()
	{
		var fbLoginButton = transform.FindChild("FBLoginButton").GetComponent<Button>();
		fbLoginButton.onClick.AddListener(OnLoginSelected);
		m_fbLoginButton = fbLoginButton.gameObject;

		m_fbPlayerInfoPanel = transform.FindChild("FBPlayerInfo").gameObject;
		m_localPlayerName = m_fbPlayerInfoPanel.transform.FindChild("Name").GetComponent<Text>();
		m_localPlayerProfile = m_fbPlayerInfoPanel.transform.FindChild("ProfileImage").GetComponent<Image>();

		var postScoreButton = transform.FindChild("PostScoreButton").GetComponent<Button>();
		postScoreButton.onClick.AddListener(OnPostScoreSelected);
		m_postScoreButton = postScoreButton.gameObject;
		m_postScoreText = m_postScoreButton.transform.FindChild("Text").GetComponent<Text>();

		var refreshButton = transform.FindChild("LeaderboardRefreshButton").GetComponent<Button>();
		refreshButton.onClick.AddListener(OnRefreshLeaderboardSelected);
		m_refreshButton = fbLoginButton.gameObject;

		SetState(State.INIT);
	}

	/// Listen to events that will change the UI
	/// 
	private void Start()
	{
		LeaderboardUIController leaderboardUIController = GameObject.FindObjectOfType<LeaderboardUIController>();

		AccountSystem.Get().OnInitialised += () => SetState(State.FB_NOT_LOGGED_IN);
		AccountSystem.Get().OnLocalPlayerChanged += () => SetState(State.FB_LOGGED_IN);
		LeaderboardSystem.Get().OnLeaderboardRefreshed += (scores) => leaderboardUIController.Refresh(scores);
	}

	/// Set the state of the UI (which governs what is displayed)
	///
	private void SetState(State state)
	{
		switch(state)
		{
		case State.FB_LOGGED_IN:
			m_fbLoginButton.SetActive(false);
			m_fbPlayerInfoPanel.SetActive(true);
			m_postScoreButton.SetActive(true);
			m_refreshButton.SetActive(true);

			m_localPlayerName.text = AccountSystem.Get().GetLocalPlayerName();
//			m_localPlayerProfile = FacebookSystem.Get().GetLocalPlayerProfilePic();
			break;
		case State.FB_NOT_LOGGED_IN:
			m_fbLoginButton.SetActive(true);
			m_fbPlayerInfoPanel.SetActive(false);
			m_postScoreButton.SetActive(true);
			m_refreshButton.SetActive(false);
			break;
		case State.INIT:
			m_fbLoginButton.SetActive(true);
			m_fbPlayerInfoPanel.SetActive(false);
			m_postScoreButton.SetActive(false);
			m_refreshButton.SetActive(false);
			break;
		}
	}

	/// Called when the user presses the FB login button.
	///
	private void OnLoginSelected()
	{
		AccountSystem.Get().Login();
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
