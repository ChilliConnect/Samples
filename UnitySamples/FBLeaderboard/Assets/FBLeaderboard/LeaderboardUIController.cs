using UnityEngine;
using System.Collections;
using ChilliConnect;

/// Manages the displaying of elements in the leaderboard. 
/// 
/// NOTE: In most production products this would be an infinite scroll list rather
/// than fixed size.
/// 
public class LeaderboardUIController : MonoBehaviour 
{
	private const int k_maxNumElements = 100;

	private LeaderboardElementUIController[] m_elementPool = new LeaderboardElementUIController[k_maxNumElements];
	private Transform m_contentParent;
	private Transform m_pooledParent;

	private int m_numElementsUsed = 0;

	/// Initialise the pool of leaderboard elements
	/// 
	private void Awake() 
	{
		m_contentParent = transform.FindChild("Content");

		GameObject prefab = Resources.Load<GameObject>("LeaderboardUIElement");
		m_pooledParent = new GameObject("LeaderboardUIElementPool").transform;

		for(int i=0; i<m_elementPool.Length; ++i)
		{
			m_elementPool[i] = (GameObject.Instantiate(prefab, m_pooledParent) as GameObject).GetComponent<LeaderboardElementUIController>();
		}
	}

	/// Update the leaderboard with the latest scores
	///
	public void Refresh(FacebookScore[] scores)
	{
		int numCurrentlyUsedElements = m_numElementsUsed;
		m_numElementsUsed = Mathf.Min(m_elementPool.Length, scores.Length);

		int numToRemove = Mathf.Max(m_numElementsUsed - numCurrentlyUsedElements, 0);

		for(int i=0; i<m_numElementsUsed; ++i)
		{
			bool isLocalPlayer = scores[i].ChilliConnectId == AccountSystem.Get().GetLocalPlayerId();
			m_elementPool[i].transform.SetParent(m_contentParent, false);
			m_elementPool[i].Init(scores[i].FacebookName, scores[i].Score, isLocalPlayer ? Color.green : Color.white);
		}

		//Remove unused elements from the leaderboard
		for(int i=m_numElementsUsed; i<m_numElementsUsed+numToRemove; ++i)
		{
			m_elementPool[i].transform.SetParent(m_pooledParent, false);
		}
	}
}
