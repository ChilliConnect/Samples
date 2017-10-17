using UnityEngine;
using System.Collections;
using ChilliConnect;
using System.IO;

public class RoomController : MonoBehaviour {

	public event System.Action OnRoomJoin = delegate {};
	public event System.Action OnGameStart = delegate {};
	public event System.Action<string> OnNextTurn = delegate {};
	public event System.Action<string> OnGameEnded = delegate {};
	public event System.Action OnPlayerQuit = delegate {};

	public void Initialise()
	{
		PhotonNetwork.OnEventCall += this.OnEvent;
	}

	private void OnEvent(byte EventCode, object BoardState, int reliable)
	{
		string boardState = (string)BoardState;

		if (EventCode == (byte) 0) {
			UnityEngine.Debug.Log ("Photon Multiplayer - Player Has just taken a turn, New Board: " + boardState);

			OnNextTurn (boardState);
		} else if (EventCode == (byte) 1) {
			UnityEngine.Debug.Log ("Photon Multiplayer - Game Over, Player 2 wins");

			OnGameEnded (boardState);
		}
	}

	void OnJoinedRoom()
	{
		UnityEngine.Debug.Log ("Photon Multiplayer - Current Player Count: " + PhotonNetwork.room.PlayerCount);

		if (PhotonNetwork.room.PlayerCount == 2) {
			UnityEngine.Debug.Log ("Photon Multiplayer - Room Found, Connected.");

			OnRoomJoin ();
		}
	}

	void OnPhotonPlayerDisconnected(PhotonPlayer newPlayer)
	{
		UnityEngine.Debug.Log ("Photon Multiplayer - Other Player has Left");
		UnityEngine.Debug.Log ("Photon Multiplayer - New Room Count: " + PhotonNetwork.room.PlayerCount);

		OnPlayerQuit ();
	}

	void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		UnityEngine.Debug.Log ("Photon Multiplayer - Current Player Count: " + PhotonNetwork.room.PlayerCount);

		if (PhotonNetwork.room.PlayerCount == 2)
		{
			UnityEngine.Debug.Log("Photon Multiplayer - Starting Turn for player X");

			OnGameStart ();
		}
	}

	public void TriggerEvent(byte evCode, string payload, bool setWebFlag)
	{
		RaiseEventOptions options = new RaiseEventOptions ();

		if(setWebFlag) {
			options.ForwardToWebhook = true;
		};

		PhotonNetwork.RaiseEvent(evCode, payload, true, options);
	}
}
