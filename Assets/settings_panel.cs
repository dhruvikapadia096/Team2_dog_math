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
    public Button soundToggleButton; // Assign this in the Unity Inspector
    public AudioSource backgroundAudioSource; // Assign your background AudioSource in the Unity Inspector

    private bool isSoundOn = true; // Track the sound state

    void Start()
    {
        // Add a listener to your sound toggle button
        soundToggleButton.onClick.AddListener(ToggleSound);
        UpdateButtonLabel();

    }

    private void ToggleSound()
    {
        // Toggle the sound state
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt("Sound", isSoundOn ? 1 : 0);

        // Enable or disable the AudioSource accordingly
        backgroundAudioSource.mute = !isSoundOn;

        // Update the button label or icon
        UpdateButtonLabel();
    }

    private void UpdateButtonLabel()
    {
        // This method updates the button's label or icon based on whether sound is ON or OFF
        if (isSoundOn)
        {
            soundToggleButton.GetComponentInChildren<TextMeshProUGUI>().text = "Sound ON"; // Ensure your button has a Text child component
        }
        else
        {
            soundToggleButton.GetComponentInChildren<TextMeshProUGUI>().text = "Sound OFF";
        }
    }

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
