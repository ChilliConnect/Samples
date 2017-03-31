using UnityEngine;
using System.Collections;
using ChilliConnect;
using System.Collections.Generic;

/// Manages the displaying of recipe buttons on the recipe button list
/// 
public class RecipeListUIController: MonoBehaviour 
{
	private const int k_maxNumElements = 10;

	private RecipeListElementUIController[] m_elementPool = new RecipeListElementUIController[k_maxNumElements];
	private Transform m_contentParent;
	private Transform m_pooledParent;

	private int m_numElementsUsed = 0;

	private void Awake() 
	{
		m_contentParent = transform.FindChild("Content");
		GameObject prefab = Resources.Load<GameObject>("RecipeListUIElement");
		m_pooledParent = new GameObject("RecipeListUIElementPool").transform;

		for(int i=0; i<m_elementPool.Length; ++i)
		{
			m_elementPool[i] = (GameObject.Instantiate(prefab, m_pooledParent) as GameObject).GetComponent<RecipeListElementUIController>();
		}
	}

	public void populateRecipeList(List<Recipe> recipes)
	{
		int numCurrentlyUsedElements = m_numElementsUsed;
		m_numElementsUsed = Mathf.Min(m_elementPool.Length, recipes.Count);
		int numToRemove = Mathf.Max(m_numElementsUsed - numCurrentlyUsedElements, 0);

		for(int i=0; i<m_numElementsUsed; ++i)
		{
			m_elementPool[i].transform.SetParent(m_contentParent, false);
			m_elementPool[i].Init(recipes[i]);
		}

		for(int i=m_numElementsUsed; i<m_numElementsUsed+numToRemove; ++i)
		{
			m_elementPool[i].transform.SetParent(m_pooledParent, false);
		}
	}
}
