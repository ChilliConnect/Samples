using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// Wraps around the UI for a leaderboard element allowing the data to be set on it
///
public class LeaderboardElementUIController : MonoBehaviour 
{
	private Text m_name;
	private Text m_score;
	private Image m_image;
	private Image m_background;
	private Sprite m_originalProfileSprite;

	/// Gather the UI
	/// 
	private void Awake()
	{
		m_name = transform.FindChild("Name").GetComponent<Text>();
		m_score = transform.FindChild("Score").GetComponent<Text>();
		m_image = transform.FindChild("Image").GetComponent<Image>();
		m_background = GetComponent<Image>();

		m_originalProfileSprite = m_image.sprite;
	}

	/// Setup the element with name, score and profile
	/// 
	/// @param accountId
	/// 	Chilli Id - required to retrieve profile pic
	/// @param name
	/// 	Name to display
	/// @param profileUrl
	/// 	Picture to display
	/// @param score
	/// 	Highscore to display
	/// @param col
	/// 	Colour of the element
	/// 
	public void Init(string accountId, string name, string profileUrl, int score, Color col)
	{
		m_name.text = name;
		m_score.text = score.ToString();
		m_background.color = col;
		m_image.sprite = m_originalProfileSprite;

		//Fetch the profile pic
		ProfilePicSystem.Get().FetchProfilePicture(accountId, profileUrl, (texture) =>
		{
			if(texture != null)
			{
				m_image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			}
		});
	}
}
