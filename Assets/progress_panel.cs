using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ProgressPanel : MonoBehaviour
{
    public Button buttonToDisable;
    public GameObject progressPanel;
    public GameObject[] backgroundElements; // Reference to background elements that should be dimmed

    public void ToggleProgressOverlay()
    {
        bool overlayActive = !progressPanel.activeSelf;

        // Toggle the overlay's visibility
        progressPanel.SetActive(overlayActive);
        buttonToDisable.interactable = !overlayActive;

        foreach (Transform child in progressPanel.transform)
        {
            child.gameObject.SetActive(overlayActive);
        }

        // Adjust transparency of overlay
        Color overlayColor = progressPanel.GetComponent<Image>().color;
        overlayColor.a = overlayActive ? 1f : 0f; // Adjust as needed
        progressPanel.GetComponent<Image>().color = overlayColor;

        // Adjust transparency of background elements
        foreach (GameObject backgroundElement in backgroundElements)
        {
            Color backgroundColor = backgroundElement.GetComponent<Image>().color;
            backgroundColor.a = overlayActive ? 0.2f : 1f; // Adjust as needed
            backgroundElement.GetComponent<Image>().color = backgroundColor;
        }
    }
    public void ExitToMainScene()
    {
        SceneManager.LoadScene("UI");
    }
}