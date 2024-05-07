using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsButton : MonoBehaviour
{
    public Button buttonToDisable;
    public GameObject settingsOverlay;
    public GameObject[] backgroundElements; // Reference to background elements that should be dimmed
    public TMP_InputField userNameInput;
    public Button saveButton;

    public void ToggleSettingsOverlay()
    {
        bool overlayActive = !settingsOverlay.activeSelf;

        // Toggle the overlay's visibility
        settingsOverlay.SetActive(overlayActive);
        buttonToDisable.interactable = !overlayActive;

        foreach (Transform child in settingsOverlay.transform)
        {
            child.gameObject.SetActive(overlayActive);
        }

        // Adjust transparency of overlay
        Color overlayColor = settingsOverlay.GetComponent<Image>().color;
        overlayColor.a = overlayActive ? 1f : 0f; // Adjust as needed
        settingsOverlay.GetComponent<Image>().color = overlayColor;

        // Adjust transparency of background elements
        foreach (GameObject backgroundElement in backgroundElements)
        {
            Color backgroundColor = backgroundElement.GetComponent<Image>().color;
            backgroundColor.a = overlayActive ? 0.2f : 1f; // Adjust as needed
            backgroundElement.GetComponent<Image>().color = backgroundColor;
        }
    }

    public void SavePlayerName()
    {
        string userName = userNameInput.text;
        Debug.Log("Player Name: " + userName);
        PlayerPrefs.SetString("PlayerName", userName);
        // Do something with the player name, such as save it or use it in your game logic
    }

    public void ExitToMainScene()
    {
        SceneManager.LoadScene("UI");
    }
}
