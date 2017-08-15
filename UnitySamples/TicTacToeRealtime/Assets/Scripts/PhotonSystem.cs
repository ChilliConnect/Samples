using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using ChilliConnect;

public class PhotonSystem : MonoBehaviour {
	
	public string photonAppId = null;
	public string photonGameId = null;

	private string m_chilliConnectId;

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
	public void LoadPhotonInstance(string chilliConnectId)
	{
		m_chilliConnectId = chilliConnectId;

		UnityEngine.Debug.Log ("Photon Multiplayer - Starting Photon Access Token Generation");

		var requestDesc = new GeneratePhotonAccessTokenRequestDesc();
		m_chilliConnect.Multiplayer.GeneratePhotonAccessToken(requestDesc, (request, response) => OnPhotonAccessTokenRetrieved(response), (request, createError) => Debug.LogError(createError.ErrorDescription));
	}

	/// Handler for succesfull log in, will notify listeners a new player has been logged in
	/// 
	private void OnPhotonAccessTokenRetrieved(string photonAccessToken)
	{
		UnityEngine.Debug.Log ("Photon Multiplayer - Retrieved Initial Access Token: " + photonAccessToken);
		photonConnect(photonAccessToken);
	}

	public void photonConnect(string photonAccessToken)
	{
		PhotonNetwork.AuthValues.UserId = m_chilliConnectId;

		UnityEngine.Debug.Log ("Photon Multiplayer - Photon UserID set: " + m_chilliConnectId);

		PhotonNetwork.AuthValues = new AuthenticationValues();
		PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Custom;
		PhotonNetwork.AuthValues.AddAuthParameter("PhotonAccessToken", photonAccessToken);
		PhotonNetwork.ConnectUsingSettings("1.0");

		// this way we can force timeouts by pausing the client (in editor)
		PhotonHandler.StopFallbackSendAckThread();
	}
}
