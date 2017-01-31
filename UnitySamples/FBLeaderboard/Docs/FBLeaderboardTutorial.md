## Overview

There are many social reasons why a developer would wish to incorporate Facebook into their app (in our case to fetch information about friends in order to create a leaderboard) but one of the main advantages of using Facebook in conjunction with ChilliConnect is that it allows players to link their ChilliConnect account to Facebook.

When a ChilliConnect account is linked to Facebook, players can retrieve their game data across multiple devices or after a fresh reinstall of your app. ChilliConnect provides other means for players to re-access accounts (notably by creating an account with an email address and password) but Facebook is usually more convenient and has an established trust with your users.

This tutorial covers the following:

- Setting up a Facebook App and logging into Facebook using the Unity Facebook SDK.
- Attaching a Facebook account to a ChilliConnect account so that players can login to ChilliConnect using Facebook.
- Creating a leaderboard on ChilliConnect
- Posting scores
- Retrieving and displaying scores and Facebook profiles for a player's friends.

The full Unity project for this tutorial is available on the ChilliConnect GitHub [samples respository](https://github.com/ChilliConnect/Samples) under the folder "FBLeaderboard". In order to run the project you must configure it with your own ChilliConnect game token and Facebook App Id (see below).

## Creating a Facebook App

As well as having a game configured on the ChilliConnect dashboard you must also create the game (or app) on Facebook. Creating the app will generate the token required to communicate with Facebook and allow us to query which of the player's friends are also playing our game. A more in depth walkthrough about integrating Unity and Facebook can be found [here](https://developers.facebook.com/docs/unity/gettingstarted), this section will cover the most important aspects.

Here are the steps:

- Login to the Facebook account that will be used to create the application (you can add more developers later if you choose).
- Locate the "Manage Apps" link on the left most navigation bar

TODO: Link to manage apps png

- Click "Add a New App"

TODO: Link to add new app png

- Configure your app's information including display name, support email and category. For this tutorial our app is called "ChilliConnectSamples".

TODO: Link to ConfigureAppId png

- Once the app creation has finished, select "Dashboard" from the navigation bar and take note of your "App ID" (we will need this in future steps when setting up our Unity project).

TODO: Link to AppDashboard.png

- Next choose "Settings" from the navigation menu and select "+ Add Platform".

TODO: Link to FBAddPlatform.png

- Select iOS and then add your bundle id, this is usually com.companyname.appname and is created when you add your app via the iOS developer portal (NOTE: The same bundle id should be used in your Unity project player settings).

TODO: Link to FBBundleId.png

- Follow the same steps but this time setup for Android.

### Inviting your team

It is important to note that at this stage your app is in developer mode and is not available to anyone outside the development team. If you are working as part of a team you should invite members to the app by selecting the "Roles" option from the navigation menu and adding admins, developers and testers as you see fit (at a minimum make sure and add some testers even if they are fake accounts otherwise your leaderboard will be very boring!).

TODO: Link to AddDevelopers.png

### Making your app live (when the time is right)

When you are ready to launch your app you must select "App Review" from the navigation menu and change "Make App public" to "Yes". You should also "Start a submission" and follow the instructions provided.

TODO: Link to ReviewApp.png

## Creating a ChilliConnect leaderboard

This section covers the creation of leaderboard on ChilliConnect. We are going to create a single highscore leaderboard but you can create as many as you like (for example each level of your game may require its own leaderboard).

- Login to your ChilliConnect dashboard and select leaderboard from the navigation menu.
- Click "Add Leaderboard"

TODO: Link to LeaderboardDash.png

- Configure the leaderboard settings including name and description.

TODO: Link to LeaderboardConfig.png

The important settings are "Key" (which is used to identify the leaderboard), "Ranking Type" (that controls whether the leaderboard is shown in ascending or descending order) and "Update Type" (that determines whether a player's submitted score should be discarded or accepted). You can edit the configuration of a leaderboard at any time via the dashboard.

This demo is using a simple highscore leaderboard that only updates a player's leaderboard entry if it is higher that their existing score. The key for our leaderboard is "HIGHSCORES", make a note of yours as we will need it in the upcoming steps.

## Setting up the Facebook SDK

Downloading and setting up the Unity Facebook SDK is the final preparation step before we can login to FB and start posting and downloading scores.

The SDK can be downloaded [here](https://developers.facebook.com/docs/unity/)

- Download the SDK, unzip and import the Unity package.
- From the top menu bar select "Facebook" > "Edit Settings" (this will open the FB settings in your inspector tab).
- Set the appropriate app name and app Id (the app Id is the string you took note of when setting up your Facebook App earlier).

TODO: Link to FacebookSDKSettings.png

## Logging in with Facebook

Now that all the prep work is out of the way we can finally concentrate on writing some code. From previous tutorials you should know how to create new (and login to existing) ChilliConnect accounts (if not check out the tutorial [here](https://docs.chilliconnect.com/guide/tutorial-setup/)). The login flow is different when using Facebook and this part covers how to login to Facebook, link FB with a ChilliConnect account and from then login to ChilliConnect using Facebook.

NOTE: All the source code for this section lives in "AccountSystem.cs" in the demo project.

Below is a diagram showing the login flow when using Facebook:

TODO: Link to FacebookLoginFlow.png

The usual convention is to give the player the option of using Facebook (by presenting a FB login button). The login flow for first time users creates an anonymous ChilliConnect account and links that to Facebook if and when the player signs in to Facebook. The login flow for returning users is slightly more complicated as we must determine whether they have a pre-existing ChilliConnect account and link the local one if not.

### Initialise FB SDK

Firstly, initialise the FB SDK:

```c#
using Facebook.Unity;
using ChilliConnect;
...
public void Initialise()
{
    if(FB.IsInitialized == false)
    {
        FB.Init(OnFBInitComplete);
    }
    else
    {
        OnFBInitComplete();
    }
}
```

At this stage we can check to see if a FB session already exists (i.e. if the user previously signed in and is now returning to the app) and based on that information decide how best to login to ChilliConnect (refer back to the login diagram above).

```c#
private void OnFBInitComplete()
{
    if(FB.IsInitialized == true)
    {
        FB.ActivateApp();

        //Check if we have an existing FB session and if so attempt to login to Chilli using it
        if(FB.IsLoggedIn == true)
        {
            //This is the access token required to login to ChilliConnect via FB.
            string fbAccessToken = Facebook.Unity.AccessToken.CurrentAccessToken.TokenString;
            m_chilliConnect.PlayerAccounts.LogInUsingFacebook(fbAccessToken, (request, response) => OnChilliConnectFBLoggedIn(response), (request, error) => OnChilliConnectLoginFailed(error));
        }
        else
        {
            //Check if we have a stored anonymous ChilliConnect token to login with
            if(PlayerPrefs.HasKey("CCId") == true && PlayerPrefs.HasKey("CCSecret") == true)
            {
                m_chilliConnect.PlayerAccounts.LogInUsingChilliConnect(PlayerPrefs.GetString("CCId"), PlayerPrefs.GetString("CCSecret"), (loginRequest) => OnChilliConnectAnonLoggedIn(), (loginRequest, error) => Debug.LogError(error.ErrorDescription));
            }
            else
            {
                //Create a new ChilliConnect account and allow the player to attach FB later.
                var requestDesc = new CreatePlayerRequestDesc();
                m_chilliConnect.PlayerAccounts.CreatePlayer(requestDesc, (request, response) => OnChilliConnectAccountCreated(response), (request, createError) => Debug.LogError(createError.ErrorDescription));
            }
        }
    }
    else
    {
        Debug.LogError("Failed to initialise FB SDK");
    }
}
```
### Logging into ChilliConnect using FB

Creating a new ChilliConnect account and logging in using the ID and secret are covered in the initial setup tutorial and should be familiar to you. Logging into ChilliConnect with the Facebook token uses the following API:

```c#
string fbAccessToken = Facebook.Unity.AccessToken.CurrentAccessToken.TokenString;
m_chilliConnect.PlayerAccounts.LogInUsingFacebook(fbAccessToken, (request, response) => OnChilliConnectFBLoggedIn(response), (request, error) => OnChilliConnectLoginFailed(error));
```

A handy feature is that the response contains the player's FB name:

```c#
private void OnChilliConnectFBLoggedIn(LogInUsingFacebookResponse response)
{
    Debug.Log("ChilliConnect logged in with FB account for " + response.FacebookName);
}
```

### Linking FB to a ChilliConnect account

When 'LogInUsingFacebook' is called (either after FB init or via user button press), ChilliConnect will attempt to find a player account associated with the given FB access token. If no account exists then the FB player is not yet associated with a ChilliConnect account and we link the current logged in FB account with the current local ChilliConnect account. Future attempts to login with the FB access token will return this account.

```c#
private void OnChilliConnectLoginFailed(LogInUsingFacebookError error)
{
    if(error.ErrorCode == LogInUsingFacebookError.Error.LoginNotFound)
    {
        LinkFacebookAccountRequestDesc requestDesc = new LinkFacebookAccountRequestDesc(m_fbAccessToken);
        m_chilliConnect.PlayerAccounts.LinkFacebookAccount(requestDesc, (request, linkResponse) => OnChilliConnectLinked(linkResponse), (request, linkError) => Debug.LogError(linkError.ErrorDescription));
    }
}
```

### Logging into FB

If the player has yet to login to Facebook then they may do so by pressing the FB button that performs the following function:

```c#
public void Login()
{
    FB.LogInWithReadPermissions(new string[] {"public_profile", "user_friends"}, OnFBLoginComplete);
}
```

Facebook allows you to login with either read permissions or publish permissions. In this demo we only need the above read permissions in order to access the name and profile image of the player and their friends.

Calling login causes one of two things to happen:

1. If no session exists the FB login dialogue is shown to the player asking them to login.
2. If the player has already granted your app permission and the session is still valid no dialogue will show.

Successful login will return an access token and a similar flow as above is performed where we attempt to login to ChilliConnect with the access token and link FB to the local account if no other account is found.

#### Existing accounts

It may be the case (post re-install or on a different device for example) that the player logs in to ChilliConnect with a FB token and already has an account. The player may have even made progress in the game forgetting that they had an existing account. Different games handle this in different ways but the most common solution is to determine whether the local anonymous account or the FB linked account has more progress and use that data.

## Leaderboard

Compared with the login flow the actual leaderboard aspect of the demo is incredibly straightforward. The source for this section is found in 'LeaderboardSystem.cs' and comprises two main parts: posting scores and retrieving scores.

### Submitting scores to leaderboard

Use the leaderboard API to post a score to a named leaderboard (in our case the leaderboard key is "HIGHSCORES").

```c#
public void PostScore(int score)
{
    string LEADERBOARD_KEY = "HIGHSCORES";
    m_chilliConnect.Leaderboards.AddScore(new AddScoreRequestDesc(score, LEADERBOARD_KEY), (request, response) => OnScoreAdded(response), (request, error) => Debug.LogError(error.ErrorDescription));
}
```
The response contains the players score and rank on the leaderboard.

```c#
private void OnScoreAdded(AddScoreResponse response)
{
    Debug.Log(string.Format("Player's high score {0}. Player is {1}/{2} on global leaderboard", response.Score, response.GlobalRank, response.GlobalTotal));
}
```

NOTE: The score is the player's score on the leaderboard which may not be the score they just submitted (in our case it returns their highscore).

### Fetching leaderboard scores

Fetching scores is also a straightforward process (again it requires the leaderboard key):

```c#
public void FetchFriendLeaderboard()
{
    var desc = new GetScoresForFacebookFriendsRequestDesc(LEADERBOARD_KEY);

    //Setting this to true ensures the logged in player appears in the leaderboard results
    desc.IncludeMe = true;

    m_chilliConnect.Leaderboards.GetScoresForFacebookFriends(desc, (request, response) => OnLeaderboardFetched(response), (request, error) => Debug.LogError(error.ErrorDescription));
}

```

Our demo is only interested in showing scores for a player's FB friends but ChilliConnect provides other methods for querying the leaderboard:

- 'GetScoresForChilliConnectIds': Returns leaderboard scores for the given players (if they have submitted a score).
- 'GetScores': Used to display the entire leaderboard (NOTE: the API returns leaderboard pages and the page must be specified in the request).
- 'GetScoresAroundPlayer': Returns scores above and below the player in the leaderboard.

The response contains a collection of 'FacebookScore' objects, each object has a friend's facebook name, profile picture url, score and position on the leaderboard:

```c#
private void OnLeaderboardFetched(GetScoresForFacebookFriendsResponse response)
{
    FacebookScore[] sorted = response.Scores.OrderBy((friend) => friend.LocalRank).ToArray();

    //Populate leaderboard UI.
}
```

The data can then be used to popluate the leaderboard UI (NOTE: in our demo the size of the leaderboard is clamped for simplicity, it may make more sense in your game to create a streaming leaderboard in order to handle a large number of scores).

#### Fetching FB profile pictures

While it is trivial to display the score and name in the leaderboard, further steps are required to display the FB profile picture. A basic downloading solution can be found in the demo source code in the "ProfilePicSystem.cs" file. This system uses the url (from the FacebookScore object above) and Unity's WWW class to download the profile images as textures. If you choose to implement image downloading in your own game you should implement a caching system that saves downloaded profile images to file. This will allow your player to access images offline and also reduce data usage. It is worth refreshing this cache every so often to keep up to date with changes to profile pictures.

## Troubleshooting

Here are some of the more common issues that might crop up:

- Login to FB using an account that has been added as an admin, developer or tester on the FB app dashboard.
- Logout of FB via iOS settings prior to deploying the app.
- Check the bundle id on the FB dashboard matches that in Unity > Player Settings.
- Changes on the FB dashboard may take time to propagate.

## Next steps

That concludes our tutorial on FB and leaderboards, you may wish to expand on the demo by trying out the following:

- Create a streaming leaderboard that can support hundreds of FB friends.
- Add other leaderboard styles (i.e. around player, global, etc).
- Cache the downloaded FB images to file.
- Explore other elements of the FB SDK such as sharing stories or uploading images.
- Implement a logout or player switching system that allows multiple players to share the same device but have different game data.
