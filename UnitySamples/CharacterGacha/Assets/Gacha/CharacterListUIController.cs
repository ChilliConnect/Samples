using UnityEngine;
using System.Collections;
using ChilliConnect;
using System.Collections.Generic;

/// Manages the displaying of character entries on the character list list
/// 
public class CharacterListUIController: MonoBehaviour 
{
	private const int k_maxNumElements = 100;

	private CharacterListElementUIController[] m_elementPool = new CharacterListElementUIController[k_maxNumElements];
	private Transform m_contentParent;
	private Transform m_pooledParent;

	private int m_numElementsUsed = 0;

	private void Awake() 
	{
		m_contentParent = transform.FindChild("Content");
		GameObject prefab = Resources.Load<GameObject>("CharacterListUIElement");
		m_pooledParent = new GameObject("CharacterListUIElementPool").transform;

		for(int i=0; i<m_elementPool.Length; ++i)
		{
			m_elementPool[i] = (GameObject.Instantiate(prefab, m_pooledParent) as GameObject).GetComponent<CharacterListElementUIController>();
		}
	}

	public void populateCharacterList(List<Character> characters)
	{
		for(int i=0; i<m_numElementsUsed; ++i)
		{
			m_elementPool[i].transform.SetParent(m_pooledParent, false);
		}

		m_numElementsUsed = characters.Count;
		for(int i=0; i<m_numElementsUsed; ++i)
		{
			m_elementPool[i].transform.SetParent(m_contentParent, false);
			m_elementPool[i].Init(characters[i]);
		}
	}
}
