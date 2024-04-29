using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class StarDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject[] stars; // Array of star GameObjects
    public float blurAlpha = 0.3f; // Alpha value for blurred stars
    public float displayDuration = 2f; // Duration for which stars are displayed
    public float fadeDuration = 1f; // Duration of fading effect

    public Button restart;
    public Button exit;

    public float rotationSpeed = 30f; // Adjust this to change the rotation speed
    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI rateText;
    public TextMeshProUGUI wrongText;

    void Start()
    {
        StarDis();
        restart.onClick.AddListener(restartGame);
        exit.onClick.AddListener(exitGame);
    }

    void StarDis()
    {
        scoreText.text = PlayerPrefs.GetInt("Score", 0).ToString();
        accuracyText.text = PlayerPrefs.GetFloat("Accuracy", 0f).ToString();
        rateText.text = PlayerPrefs.GetFloat("Rate", 0f).ToString("0.0");
        wrongText.text = PlayerPrefs.GetInt("Wrong", 0).ToString();

        StartCoroutine(DisplayStarsCoroutine());
    }

    IEnumerator DisplayStarsCoroutine()
    {
        // Convert scoreText text to integer
        if (int.TryParse(scoreText.text, out int score))
        {
            // Display stars based on score
            if (score == 5)
            {
                DisplayStars(3);
            }
            else if (score == 4 || score == 3)
            {
                DisplayStars(2);
                BlurStar(2);
            }
            else if (score == 2 || score == 1)
            {
                DisplayStars(1);
                BlurStar(1);
                BlurStar(2);
            }
        }

        // Wait for displayDuration
        yield return new WaitForSeconds(displayDuration);

        // Fade out stars
        for (int i = 0; i < stars.Length; i++)
        {
            StartCoroutine(FadeOutStar(stars[i]));
        }

        // Wait for fadeDuration
        yield return new WaitForSeconds(fadeDuration);

        // Restart coroutine to display stars again
        StartCoroutine(DisplayStarsCoroutine());
    }

    void DisplayStars(int count)
    {
        for (int i = 0; i < count; i++)
        {
            stars[i].SetActive(true);
            StartCoroutine(FadeInStar(stars[i]));
        }
    }

    IEnumerator FadeInStar(GameObject star)
    {
        Image starImage = star.GetComponent<Image>();
        Color color = starImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            starImage.color = color;
            yield return null;
        }
    }

    IEnumerator FadeOutStar(GameObject star)
    {
        Image starImage = star.GetComponent<Image>();
        Color color = starImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            starImage.color = color;
            yield return null;
        }

        star.SetActive(false);
    }

    void BlurStar(int index)
    {
        Image starImage = stars[index].GetComponent<Image>();
        Color color = starImage.color;
        color.a = blurAlpha; // Set alpha to blurAlpha
        starImage.color = color;
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
