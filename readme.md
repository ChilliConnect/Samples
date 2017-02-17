# ChilliConnect Sample Code

This repository contains Sample projects for ChilliConnect.

## Unity

The `UnitySamples`directory contains separate Unity projects for each sample.

### CreatePlayer

The CreatePlayerDemoScene uses the Game Jam Menu Template from the Asset store to create a basic test scene that shows creation of a new ChilliConnect player, logging in and persisting the ChilliConnect credentials using Unity `PlayerPrefs`. The `StartOptions` class contains the integration with the ChilliConnect SDK. To run this project under your own ChilliConnect account, change the `GAME_TOKEN` constant.

### Teams

The Teams scene is the accompanying demo project for the ChilliConnect [Teams tutorial](https://docs.chilliconnect.com/tutorial-teams). The project will create and save an anonymous player account and then provide a UI for listing, creating, joining and leave teams. To run this project under your own ChilliConnect account, change the `GAME_TOKEN` constant in `TeamsDemoSceneController.cs`

### IAP

The IAPDemoScene accompanies the ChilliConnect [IAP tutorial](https://docs.chilliconnect.com/tutorial-iaps) and contains an end-to-end example of integrating the Unity IAP Plugin with ChilliConnect to securely verify and redeem player purchases. 

*To run this project under your own account, you must enable the Unity IAP Plugin from the Services menu and configure the plugin with your own Google Play and App Store settings.* Full instructions are available on the [Unity website](https://unity3d.com/learn/tutorials/topics/analytics/integrating-unity-iap-your-game). You must also supply your own ChilliConnect Game Token in `IAPDemoSceneController.cs`.