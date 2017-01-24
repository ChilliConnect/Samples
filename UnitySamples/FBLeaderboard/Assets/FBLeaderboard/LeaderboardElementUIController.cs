using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// Wraps around the UI for a leaderboard element allowing the data to be set on it
///
public class LeaderboardElementUIController : MonoBehaviour 
{
	//TODO: Profile image
	private Text m_name;
	private Text m_score;

	/// Gather the UI
	/// 
	private void Awake()
	{
		m_name = transform.FindChild("Name").GetComponent<Text>();
		m_score = transform.FindChild("Score").GetComponent<Text>();
	}

	/// Setup the element with name, score and profile
	/// 
	public void Init(string name, int score)
	{
		m_name.text = name;
		m_score.text = score.ToString();
	}
}
