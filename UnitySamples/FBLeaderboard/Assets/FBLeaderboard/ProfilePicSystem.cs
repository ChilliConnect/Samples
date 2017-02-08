using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

/// Handles the downloading of FB profile pics for the local user and their friends. Caches
/// the pictures in memory.
/// 
/// NOTE: In a genuine production game you would cache the images to file for a given period of time
/// 
public class ProfilePicSystem
{
	private static ProfilePicSystem s_singletonInstance = null;

	private ProfilePicDownloadHelper m_downloadHelper;
	private HashSet<string> m_downloadsInProgress = new HashSet<string>();
	private Dictionary<string, Texture2D> m_cache = new Dictionary<string, Texture2D>();

	/// @return Singleton instance if system has been created (not lazily created)
	/// 
	public static ProfilePicSystem Get()
	{
		return s_singletonInstance;
	}

	/// 
	public ProfilePicSystem()
	{
		s_singletonInstance = this;
	}

	/// 
	/// Create the download helper
	/// 
	public void Initialise()
	{
		GameObject downloadHelper = new GameObject("ProfileDownloadHelper");
		m_downloadHelper = downloadHelper.AddComponent<ProfilePicDownloadHelper>();
	}

	/// 
	/// Attempts to retrieve the cached profile picture for the given user and 
	/// if none exists downloads and caches the pic.
	/// 
	/// @param accountId
	/// 	Player to whom the profile pic belongs
	/// @param picUrl
	/// 	Url from which to download the pic
	/// @param onComplete
	/// 	Delegate called when pic has downloaded (or failed).
	/// 
	public void FetchProfilePicture(string accountId, string picUrl, System.Action<Texture2D> onComplete)
	{
		//Check to make sure we are not about to download something that is already downloading
		if(m_downloadsInProgress.Contains(accountId) == true)
		{
			onComplete(null);
			return;
		}

		//Try and get the cached version
		Texture2D cachedTexture;
		if(m_cache.TryGetValue(accountId, out cachedTexture))
		{
			onComplete(cachedTexture);
			return;
		}
	
		//Failed to find a cached version. Begin downloading
		m_downloadsInProgress.Add(accountId);
		m_downloadHelper.DownloadProfilePic(picUrl, (texture) => 
		{
			m_cache.Add(accountId, texture);
			m_downloadsInProgress.Remove(accountId);
			onComplete(texture);
		});
	}
}
