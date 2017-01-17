using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// Simple script that changes to the given scene when the sibling button is pressed
///
public class SceneChangeOnButton : MonoBehaviour 
{
	public string m_sceneName;

	private void Awake () 
	{
		transform.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(m_sceneName));
	}
}
