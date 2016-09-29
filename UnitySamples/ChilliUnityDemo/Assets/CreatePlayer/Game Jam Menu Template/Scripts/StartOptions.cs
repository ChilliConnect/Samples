using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using ChilliConnect;


public class StartOptions : MonoBehaviour {

	const string GAME_TOKEN = "ENTER_YOUR_GAME_TOKEN_HERE";

	public int sceneToStart = 1;										//Index number in build settings of scene to load if changeScenes is true
	public bool changeScenes;											//If true, load a new scene when Start is pressed, if false, fade out UI and continue in single scene
	public bool changeMusicOnStart;										//Choose whether to continue playing menu music or start a new music clip


	[HideInInspector] public bool inMainMenu = true;					//If true, pause button disabled in main menu (Cancel in input manager, default escape key)
	[HideInInspector] public Animator animColorFade; 					//Reference to animator which will fade to and from black when starting game.
	[HideInInspector] public Animator animMenuAlpha;					//Reference to animator that will fade out alpha of MenuPanel canvas group
	 public AnimationClip fadeColorAnimationClip;		//Animation clip fading to color (black default) when changing scenes
	[HideInInspector] public AnimationClip fadeAlphaAnimationClip;		//Animation clip fading out UI elements alpha


	private PlayMusic playMusic;										//Reference to PlayMusic script
	private float fastFadeIn = .01f;									//Very short fade time (10 milliseconds) to start playing music immediately without a click/glitch
	private ShowPanels showPanels;										//Reference to ShowPanels script on UI GameObject, to show and hide panels

	private ChilliConnectSdk chilliConnect;
	private string ChilliConnectId;
	private string ChilliConnectSecret;

	void Awake()
	{
		//Get a reference to ShowPanels attached to UI object
		showPanels = GetComponent<ShowPanels> ();

		//Get a reference to PlayMusic attached to UI object
		playMusic = GetComponent<PlayMusic> ();

		//setup ChilliConnect SDK with game token and verbose logging
		chilliConnect = new ChilliConnectSdk( GAME_TOKEN, true);
		ChilliConnectId = PlayerPrefs.GetString ("ChilliConnectId");
		ChilliConnectSecret = PlayerPrefs.GetString ("ChilliConnectSecret");
		UnityEngine.Debug.Log("Loaded from PlayerPrefs ChilliconnectId: "+ChilliConnectId+" ChilliConnectSecret: "+ChilliConnectSecret);

		//if player not created
		if (ChilliConnectId.Length == 0) {
			CreateAndLoginPlayer ();
		} else {
			LoginPlayer ();
		}
	}

	void CreateAndLoginPlayer()
	{
		System.Action<CreatePlayerRequest, CreatePlayerResponse> successCallback = (CreatePlayerRequest request, CreatePlayerResponse response) =>
		{
			ChilliConnectId = response.ChilliConnectId;
			ChilliConnectSecret = response.ChilliConnectSecret;

			//persist identifiers
			PlayerPrefs.SetString("ChilliConnectId", ChilliConnectId);
			PlayerPrefs.SetString("ChilliConnectSecret", ChilliConnectSecret);

			UnityEngine.Debug.Log("Player created with ChilliConnectId: " + ChilliConnectId + " ChilliSecret: " + ChilliConnectSecret);

			LoginPlayer();
		};

		System.Action<CreatePlayerRequest, CreatePlayerError> errorCallback = (CreatePlayerRequest request, CreatePlayerError error) =>
		{
			UnityEngine.Debug.Log("An error occurred while creating a new player: " + error.ErrorDescription);
		};

		var requestDesc = new CreatePlayerRequestDesc();
		chilliConnect.PlayerAccounts.CreatePlayer(requestDesc, successCallback, errorCallback);
	}

	void LoginPlayer()
	{
		System.Action<LogInUsingChilliConnectRequest> successCallback = (LogInUsingChilliConnectRequest request) =>
		{
			UnityEngine.Debug.Log("Player logged in");
		};

		System.Action<LogInUsingChilliConnectRequest, LogInUsingChilliConnectError> errorCallback = (LogInUsingChilliConnectRequest request, LogInUsingChilliConnectError error) =>
		{
			UnityEngine.Debug.Log("An error occurred while logging in: " + error.ErrorDescription);
		};

		chilliConnect.PlayerAccounts.LogInUsingChilliConnect(ChilliConnectId, ChilliConnectSecret, successCallback, errorCallback);
	}

	public void StartButtonClicked()
	{
		SaveLeaderboardScore ();

		//If changeMusicOnStart is true, fade out volume of music group of AudioMixer by calling FadeDown function of PlayMusic, using length of fadeColorAnimationClip as time. 
		//To change fade time, change length of animation "FadeToColor"
		if (changeMusicOnStart) 
		{
			playMusic.FadeDown(fadeColorAnimationClip.length);
		}

		//If changeScenes is true, start fading and change scenes halfway through animation when screen is blocked by FadeImage
		if (changeScenes) 
		{
			//Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
			Invoke ("LoadDelayed", fadeColorAnimationClip.length * .5f);

			//Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
			animColorFade.SetTrigger ("fade");
		} 

		//If changeScenes is false, call StartGameInScene
		else 
		{
			//Call the StartGameInScene function to start game without loading a new scene.
			StartGameInScene();
		}

	}

	void SaveLeaderboardScore()
	{
		System.Action<AddScoreRequest, AddScoreResponse> successCallback = (AddScoreRequest request, AddScoreResponse response) =>
		{
			UnityEngine.Debug.Log("Player score rank: " + response.GlobalRank + " out of: " + response.GlobalTotal);
		};

		System.Action<AddScoreRequest, AddScoreError> errorCallback = (AddScoreRequest request, AddScoreError error) =>
		{
			UnityEngine.Debug.Log("An error occurred while adding score: " + error.ErrorDescription);
		};

		var requestDesc = new AddScoreRequestDesc(25, "DEMO");
		chilliConnect.Leaderboards.AddScore(requestDesc, successCallback, errorCallback);
	}

	//Once the level has loaded, check if we want to call PlayLevelMusic
	void OnLevelWasLoaded()
	{
		//if changeMusicOnStart is true, call the PlayLevelMusic function of playMusic
		if (changeMusicOnStart)
		{
			playMusic.PlayLevelMusic ();
		}	
	}


	public void LoadDelayed()
	{
		//Pause button now works if escape is pressed since we are no longer in Main menu.
		inMainMenu = false;

		//Hide the main menu UI element
		showPanels.HideMenu ();

		//Load the selected scene, by scene index number in build settings
		SceneManager.LoadScene (sceneToStart);
	}

	public void HideDelayed()
	{
		//Hide the main menu UI element after fading out menu for start game in scene
		showPanels.HideMenu();
	}

	public void StartGameInScene()
	{
		//Pause button now works if escape is pressed since we are no longer in Main menu.
		inMainMenu = false;

		//If changeMusicOnStart is true, fade out volume of music group of AudioMixer by calling FadeDown function of PlayMusic, using length of fadeColorAnimationClip as time. 
		//To change fade time, change length of animation "FadeToColor"
		if (changeMusicOnStart) 
		{
			//Wait until game has started, then play new music
			Invoke ("PlayNewMusic", fadeAlphaAnimationClip.length);
		}
		//Set trigger for animator to start animation fading out Menu UI
		animMenuAlpha.SetTrigger ("fade");
		Invoke("HideDelayed", fadeAlphaAnimationClip.length);
		Debug.Log ("Game started in same scene! Put your game starting stuff here.");
	}


	public void PlayNewMusic()
	{
		//Fade up music nearly instantly without a click 
		playMusic.FadeUp (fastFadeIn);
		//Play music clip assigned to mainMusic in PlayMusic script
		playMusic.PlaySelectedMusic (1);
	}
}
