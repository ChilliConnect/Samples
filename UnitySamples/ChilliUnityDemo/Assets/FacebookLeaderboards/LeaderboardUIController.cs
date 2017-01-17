using UnityEngine;
using System.Collections;
using ChilliConnect;

/// Manages the loading and streaming of leaderboard elements
/// 
public class LeaderboardUIController : MonoBehaviour 
{
	private LeaderboardElementUIController[] m_elementPool = new LeaderboardElementUIController[10];
	private Transform m_contentParent;

	/// Initialise the pool of leaderboard elements
	/// 
	private void Awake() 
	{
		m_contentParent = transform.FindChild("Content");

		GameObject prefab = Resources.Load<GameObject>("LeaderboardUIElement");
		GameObject pooledParent = new GameObject("LeaderboardUIElementPool");

		for(int i=0; i<m_elementPool.Length; ++i)
		{
			m_elementPool[i] = (GameObject.Instantiate(prefab, pooledParent.transform) as GameObject).GetComponent<LeaderboardElementUIController>();
		}
	}
	
	public void Refresh(FacebookScore[] scores)
	{
		for(int i=0; i<m_elementPool.Length; ++i)
		{
			m_elementPool[i].transform.SetParent(m_contentParent, false);
		}
	}

	private void OnScrollContentMoved()
	{
		//Whenever the topmost visible element moves above the visible area then put it back into the free pool

		//Whenever the bottommost element moves into the visible area then add an element from the free pool to the bottom.

		//Whenever the bottommost visible element moves below the visible area then put it back into the free pool.

		//Whenever the topmost element moves into the visible area then add an element from the free pool to the top.
	}
}
