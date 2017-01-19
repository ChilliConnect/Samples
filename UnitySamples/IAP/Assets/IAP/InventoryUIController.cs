using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// Listens for changes in the inventory and updates the UI
/// 
public class InventoryUIController : MonoBehaviour 
{
	private Text m_coinText;
	private Text m_gemText;

	///
	private void Start () 
	{
		m_coinText = transform.FindChild("CoinText").GetComponent<Text>();
		m_coinText.text = "Coins: Fetching";
		m_gemText = transform.FindChild("GemText").GetComponent<Text>();
		m_gemText.text = "Gems: Fetching";

		InventorySystem.Get().OnInventoryUpdated += OnInventoryUpdated;
	}

	/// Called whenever the inventory is updated to refresh the UI
	/// 
	public void OnInventoryUpdated()
	{
		m_coinText.text = string.Format("Coins: {0}", InventorySystem.Get().GetAmount(InventorySystem.k_coinsId));
		m_gemText.text = string.Format("Gems: {0}", InventorySystem.Get().GetAmount(InventorySystem.k_gemsId));
	}
}
