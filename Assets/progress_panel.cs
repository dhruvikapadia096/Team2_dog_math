using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class ProgressPanel : MonoBehaviour
{
    public Button buttonToDisable;
    public GameObject progressPanel;
    public GameObject[] backgroundElements; // Reference to background elements that should be dimmed
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI gradeText;
    public TextMeshProUGUI gamesPlayedText;
    private SerializableDictionary<string, UserStats> userStats;
    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "userStats.json");
        LoadUserStats();
    }

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

        if (overlayActive) DisplayUserStats();
    }

    private void LoadUserStats()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            userStats = JsonUtility.FromJson<SerializableDictionary<string, UserStats>>(json);
            if (userStats == null) {
                userStats = new SerializableDictionary<string, UserStats>(new Dictionary<string, UserStats>());
            }

        }
        else
        {
            userStats = new SerializableDictionary<string, UserStats>(new Dictionary<string, UserStats>());
        }
    }

    private void DisplayUserStats()
    {
        string username = PlayerPrefs.GetString("PlayerName");
        if (userStats.dictionary.ContainsKey(username))
        {
            UserStats stats = userStats.dictionary[username];
            highScoreText.text = "High Score: " + stats.highestScore.ToString();
            accuracyText.text = "Accuracy: " + stats.averageAccuracy.ToString("F1") + "%";
            gamesPlayedText.text = "Games Played: " + stats.gamesPlayed.ToString();
            string grade;
            if (stats.averageAccuracy >= 90)
                grade = "A";
            else if (stats.averageAccuracy >= 80)
                grade = "B";
            else if (stats.averageAccuracy >= 70)
                grade = "C";
            else if (stats.averageAccuracy >= 60)
                grade = "D";
            else
                grade = "F";
                gradeText.text = "Grade: " + grade;
        }
        else
        {
            highScoreText.text = "High Score: 0";
            accuracyText.text = "Accuracy: 0%";
            gamesPlayedText.text = "Games Played: 0";
            gradeText.text = "Grade: NA";
        }
    }

    public void ExitToMainScene()
    {
        SceneManager.LoadScene("UI");
    }
}