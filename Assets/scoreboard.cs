using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class StarDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject[] stars; // Array of star GameObjects
    public float maxScale = 1.2f; // Maximum scale for popping in
    public float minScale = 0.8f; // Minimum scale for popping out
    public float popInDuration = 1f; // Duration for popping in
    public float popOutDuration = 1f; // Duration for popping out

    public Button restart;
    public Button exit;

    public float rotationSpeed = 30f; // Adjust this to change the rotation speed

    void Start()
    {
        scoreText.text = PlayerPrefs.GetInt("Score", 0).ToString();

        restart.onClick.AddListener(restartGame);
        exit.onClick.AddListener(exitGame);

        // Start the pop-in/out coroutine
        StartCoroutine(PopInAndOut());
    }

    // Coroutine to handle popping in and out of stars
    IEnumerator PopInAndOut()
    {
        while (true)
        {
            yield return PopIn();
            yield return PopOut();
        }
    }

    IEnumerator PopIn()
    {
        float elapsedTime = 0f;
        Vector3 originalScale = Vector3.one;

        while (elapsedTime < popInDuration)
        {
            foreach (GameObject star in stars)
            {
                star.transform.localScale = Vector3.Lerp(originalScale, Vector3.one * maxScale, elapsedTime / popInDuration);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set scale to maxScale to ensure it's exactly at max scale
        foreach (GameObject star in stars)
        {
            star.transform.localScale = Vector3.one * maxScale;
        }
    }

    IEnumerator PopOut()
    {
        float elapsedTime = 0f;
        Vector3 originalScale = Vector3.one * maxScale;

        while (elapsedTime < popOutDuration)
        {
            foreach (GameObject star in stars)
            {
                star.transform.localScale = Vector3.Lerp(originalScale, Vector3.one * minScale, elapsedTime / popOutDuration);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set scale to minScale to ensure it's exactly at min scale
        foreach (GameObject star in stars)
        {
            star.transform.localScale = Vector3.one * minScale;
        }
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
