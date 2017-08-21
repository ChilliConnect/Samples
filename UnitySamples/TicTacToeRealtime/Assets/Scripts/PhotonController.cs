using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine; 

using ChilliConnect;

public class PhotonController : MonoBehaviour {

	private string m_photonApplicationId = "PHOTON_APPLICATION_ID";

	private ChilliConnectSdk m_chilliConnect;

	/// Make instance of ChilliConnectId.
	/// 
	/// @param chilliConnect
	///		Instance of the chilliConnect SDK
	/// 
	public void Initialise(ChilliConnectSdk chilliConnect)
	{
		m_chilliConnect = chilliConnect;
	}

	/// Gets a new access token for use with connecting to Photon Multiplayer 
	/// Token lasts for 5 minutes
	/// 
	public void LoadPhotonInstance()
	{
		UnityEngine.Debug.Log ("Photon Multiplayer - Starting Photon Access Token Generation");

		m_chilliConnect.Multiplayer.GeneratePhotonAccessToken(m_photonApplicationId, (request, response) => OnPhotonAccessTokenRetrieved(response), (request, createError) => Debug.LogError(createError.ErrorDescription));
	}

	/// Handler for successful PhotonAccessToken retrieval, will connect to Photon Services
	/// 
	private void OnPhotonAccessTokenRetrieved(GeneratePhotonAccessTokenResponse photonAccessToken)
	{
		UnityEngine.Debug.Log ("Photon Multiplayer - Retrieved Initial Access Token: " + photonAccessToken.PhotonAccessToken);

		PhotonNetwork.AuthValues = new AuthenticationValues();
		PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Custom;
		PhotonNetwork.AuthValues.AddAuthParameter("PhotonAccessToken", photonAccessToken.PhotonAccessToken);
		PhotonNetwork.ConnectUsingSettings("1.0");	
	}

	void OnConnectedToMaster ()
	{
		UnityEngine.Debug.Log ("Photon Multiplayer - Connected: " + PhotonNetwork.connected);

		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed(object[] codeAndMsg)
	{
		Debug.Log("Photon Multiplayer - No Rooms Available, Creating New Room");
		PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2, PlayerTtl = 20000 }, null);
	}
}
