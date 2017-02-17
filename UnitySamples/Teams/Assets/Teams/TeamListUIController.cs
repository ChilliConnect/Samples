using UnityEngine;
using System.Collections;
using ChilliConnect;
using System.Collections.Generic;

public class TeamListUIController: MonoBehaviour 
{
	private const int k_maxNumElements = 100;

	private TeamListElementUIController[] m_elementPool = new TeamListElementUIController[k_maxNumElements];
	private Transform m_contentParent;
	private Transform m_pooledParent;

	private int m_numElementsUsed = 0;

	private void Awake() 
	{
		m_contentParent = transform.FindChild("Content");
		GameObject prefab = Resources.Load<GameObject>("TeamListUIElement");
		m_pooledParent = new GameObject("TeamListUIElementPool").transform;

		for(int i=0; i<m_elementPool.Length; ++i)
		{
			m_elementPool[i] = (GameObject.Instantiate(prefab, m_pooledParent) as GameObject).GetComponent<TeamListElementUIController>();
		}
	}

	public void Refresh(List<Team> teams, Team playersTeam)
	{
		var playerHasTeam = playersTeam != null;

		int numCurrentlyUsedElements = m_numElementsUsed;
		m_numElementsUsed = Mathf.Min(m_elementPool.Length, teams.Count);
		int numToRemove = Mathf.Max(m_numElementsUsed - numCurrentlyUsedElements, 0);

		for(int i=0; i<m_numElementsUsed; ++i)
		{
			m_elementPool[i].transform.SetParent(m_contentParent, false);
			m_elementPool[i].Init(teams[i], playerHasTeam && teams[i].ID == playersTeam.ID, playerHasTeam);
		}

		for(int i=m_numElementsUsed; i<m_numElementsUsed+numToRemove; ++i)
		{
			m_elementPool[i].transform.SetParent(m_pooledParent, false);
		}
	}
}
