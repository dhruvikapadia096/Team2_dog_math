using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scoreboard : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI rateText;
    public TextMeshProUGUI wrongText;
    public Button restart;
    public Button exit;

    void Start() {
        scoreText.text = PlayerPrefs.GetInt("Score", 0).ToString();
        accuracyText.text = PlayerPrefs.GetFloat("Accuracy", 0f).ToString();
        rateText.text = PlayerPrefs.GetFloat("Rate", 0f).ToString("0.0");
        wrongText.text = PlayerPrefs.GetInt("Wrong", 0).ToString();

        restart.GetComponent<Button>().onClick.AddListener(restartGame);
        exit.GetComponent<Button>().onClick.AddListener(exitGame);
    }

    void restartGame()
    {
        SceneManager.LoadScene("Subtraction");
    }

    void exitGame()
    {
        SceneManager.LoadScene("UI");
    }

}
