using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Player {
	public Image panel;
	public Text text;
	public Button button;
}

[System.Serializable]
public class PlayerColor {
	public Color panelColor;
	public Color textColor;
}

public class GameController : MonoBehaviour {

	public event System.Action<string> OnSideSelected = delegate {};
	public event System.Action<string, string> OnTurnEnded = delegate {};
	public event System.Action OnNewGameSelected = delegate {};

	public Text[] buttonList;
	public GameObject gameOverPanel;

	public GameObject newGame;

	public GameObject restartButton;
	public Text gameOverText;
	public Player playerX;
	public Player playerO;
	public PlayerColor activePlayerColor;
	public PlayerColor inactivePlayerColor;
	public GameObject startInfo;
	public GameObject inputBlockingPanel;
	public Image blockingImage;
	public Text chilliInfoText;

	private string playerSide;
	private int moveCount;

	void Awake ()
	{
		SetGameControllerReferenceOnButtons();
		gameOverPanel.SetActive(false);
		moveCount = 0;
		restartButton.SetActive(false);
	}

	public void StartNewGame()
	{
		OnNewGameSelected ();
	}

	void SetGameControllerReferenceOnButtons ()
	{
		for (int i = 0; i < buttonList.Length; i++)
		{
			buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
		}
	}

	public void SetStartingSide (string startingSide)
	{
		OnSideSelected (startingSide);
	}

	/// Sets the local player's side, X or O
	/// 
	public void SetLocalPlayerSide (string side)
	{
		playerSide = side;
		if (playerSide == "X")
		{
			SetPlayerColors(playerX, playerO);
		} 
		else
		{
			SetPlayerColors(playerO, playerX);
		}
	}

	public void StartGame ()
	{
		SetBoardInteractable(true);
		SetPlayerButtons (false);
		SetNewGameButton (false);
		startInfo.SetActive(false);
		HideChilliInfoPanel ();
	}

	public string GetPlayerSide ()
	{
		return playerSide;
    }

    /// Returns a string that represents the state of the board
    /// 
    public string CreateBoardString()
    {
        char[] board = new char[9];
        for(int i = 0; i < buttonList.Length; ++i)
        {
            if (buttonList[i].text.Length > 0)
            {
                board[i] = buttonList[i].text[0];
            }
            else
            {
                board[i] = '?';
            }
        }
        return new string(board);
    }

    /// Sets the board state to match the string
    /// 
    public void SetBoardState(string board)
    {
		moveCount = 0;
        char[] boardChars = board.ToCharArray();
        for(int i = 0; i < boardChars.Length; ++i)
        {
            if (boardChars[i] != '?')
            {
                buttonList[i].GetComponentInParent<Button>().interactable = false;
                buttonList[i].text = string.Format("{0}", boardChars[i]);
                moveCount++;
            }
        }
    }

    public bool IsGameOver()
    {
        bool gameOver = false;
        if (HasPlayerWon("X"))
        {
            GameOver("X");
            gameOver = true;
        }
        else if (HasPlayerWon("O"))
        {
            GameOver("O");
            gameOver = true;
        }
        else if (moveCount >= 9)
        {
            GameOver("draw");
            gameOver = true;
        } 
        return gameOver;
    }

    public bool HasPlayerWon(string player)
    {
        bool hasWon = false;

        if (buttonList [0].text == player && buttonList [1].text == player && buttonList [2].text == player)
        {
            GameOver(player);
            hasWon = true;
        } 
        else if (buttonList [3].text == player && buttonList [4].text == player && buttonList [5].text == player)
        {
            GameOver(player);
            hasWon = true;
        } 
        else if (buttonList [6].text == player && buttonList [7].text == player && buttonList [8].text == player)
        {
            GameOver(player);
            hasWon = true;
        } 
        else if (buttonList [0].text == player && buttonList [3].text == player && buttonList [6].text == player)
        {
            GameOver(player);
            hasWon = true;
        } 
        else if (buttonList [1].text == player && buttonList [4].text == player && buttonList [7].text == player)
        {
            GameOver(player);
            hasWon = true;
        } 
        else if (buttonList [2].text == player && buttonList [5].text == player && buttonList [8].text == player)
        {
            GameOver(player);
            hasWon = true;
        } 
        else if (buttonList [0].text == player && buttonList [4].text == player && buttonList [8].text == player)
        {
            GameOver(player);
            hasWon = true;
        } 
        else if (buttonList [2].text == player && buttonList [4].text == player && buttonList [6].text == player)
        {
            GameOver(player);
            hasWon = true;
        } 
        return hasWon;
    }

	public void EndTurn ()
	{
        moveCount++;
		
        if (IsGameOver() == false)
		{
			ChangeSides();
        }

        OnTurnEnded(playerSide, CreateBoardString());
	}

	void ChangeSides ()
	{
		playerSide = (playerSide == "X") ? "O" : "X";
		if (playerSide == "X")
		{
			SetPlayerColors(playerX, playerO);
		} 
		else
		{
			SetPlayerColors(playerO, playerX);
		}

	}

	void SetPlayerColors (Player newPlayer, Player oldPlayer)
	{
		newPlayer.panel.color = activePlayerColor.panelColor;
		newPlayer.text.color = activePlayerColor.textColor;
		oldPlayer.panel.color = inactivePlayerColor.panelColor;
		oldPlayer.text.color = inactivePlayerColor.textColor;
	}

	void GameOver (string winningPlayer)
	{
		SetBoardInteractable(false);
		SetNewGameButton (true);
		if (winningPlayer == "draw")
		{
			SetGameOverText("It's a Draw!");
			SetPlayerColorsInactive();
		} 
		else
		{
			SetGameOverText(winningPlayer + " Wins!");
		}
	}

	void SetGameOverText (string value)
	{
		gameOverPanel.SetActive(true);
		gameOverText.text = value;
	}

	/// Shows ChilliInfoPanel. Sets text and blocking on the ChilliInfoPanel
	/// 
	public void ShowChilliInfoPanel (string value, bool blocking = true)
	{
		inputBlockingPanel.SetActive(true);
		blockingImage.enabled = blocking;
		chilliInfoText.text = value;
	}

	/// Hides the ChilliInfoPanel
	/// 
	public void HideChilliInfoPanel ()
	{
		inputBlockingPanel.SetActive(false);
	} 

	public void RestartGame ()
	{
		moveCount = 0;
		gameOverPanel.SetActive(false);
		restartButton.SetActive(false);
		SetPlayerButtons (true);
		SetPlayerColorsInactive();
		startInfo.SetActive(true);

		for (int i = 0; i < buttonList.Length; i++)
		{
			buttonList [i].text = "";
		}
	}

	void SetBoardInteractable (bool toggle)
	{
		for (int i = 0; i < buttonList.Length; i++)
		{
			buttonList[i].GetComponentInParent<Button>().interactable = toggle;
		}
	}

	void SetPlayerButtons (bool toggle)
	{
		playerX.button.interactable = toggle;
		playerO.button.interactable = toggle;  
	}

	public void SetNewGameButton (bool toggle)
	{
		newGame.SetActive (toggle);
	}

	void SetPlayerColorsInactive ()
	{
		playerX.panel.color = inactivePlayerColor.panelColor;
		playerX.text.color = inactivePlayerColor.textColor;
		playerO.panel.color = inactivePlayerColor.panelColor;
		playerO.text.color = inactivePlayerColor.textColor;
	}
}