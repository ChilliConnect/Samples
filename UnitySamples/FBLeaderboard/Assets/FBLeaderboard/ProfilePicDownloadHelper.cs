using UnityEngine;
using System.Collections;

/// Helper for the ProfilePicSystem that allows it to use coroutines to download
/// images. The WWW class that is used to download the images enforce the use of coroutines
/// and coroutines can only be used on MonoBehaviours
/// 
public class ProfilePicDownloadHelper : MonoBehaviour
{
	///
	/// Starts the coroutine that Downloads the FB profile pic from the given url
	/// 
	/// @param picUrl
	/// 	Url from which to download the pic
	/// @param onComplete
	/// 	Delegate called when pic has downloaded (or failed).
	///
	public void DownloadProfilePic(string picUrl, System.Action<Texture2D> onComplete)
	{
		StartCoroutine(AsyncDownloadProfilePic(picUrl, onComplete));
	}

	///
	/// Downloads the FB profile pic from the given url
	/// 
	/// @param picUrl
	/// 	Url from which to download the pic
	/// @param onComplete
	/// 	Delegate called when pic has downloaded (or failed).
	/// 
	/// @return IEnumerator for running as coroutine
	///
	private IEnumerator AsyncDownloadProfilePic(string picUrl, System.Action<Texture2D> onComplete)
	{
		WWW www = new WWW(picUrl);

		yield return www;

		if(www.error != null)
		{
			Debug.LogWarning("AsyncDownloadProfilePic Failed: " + www.error + " URL: " + picUrl);
			onComplete(null);
			yield break;
		}
			
		onComplete(www.texture as Texture2D);

		www.Dispose();
	}
}
