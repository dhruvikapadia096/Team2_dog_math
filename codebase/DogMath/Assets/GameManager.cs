using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text displayText;

    public void OnPlayButtonClick()
    {
        // Update text when the big button is clicked
        displayText.text = "Start Game";

        // Additional actions to start the game can be added here
        StartGame();
    }

    public void OnPauseButtonClick()
    {
        // Update text when a small button is clicked
        displayText.text = "Pause Button Clicked";
    }

    public void OnSettingsButtonClick(int buttonIndex)
    {
        // Update text when a small button is clicked
        displayText.text = "Settings Button Clicked";
    }

    // Additional method to start the game
    private void StartGame()
    {
        // Implement actions to start the game, e.g., load a new scene, enable game objects, etc.
        Debug.Log("Game is starting!");
    }
}
