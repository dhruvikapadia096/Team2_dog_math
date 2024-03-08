using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public Text numberText;

    private int currentNumberIndex = 0;

    void Start()
    {
        StartCoroutine(DisplayNumbersWithAnimation());
    }

    IEnumerator DisplayNumbersWithAnimation()
    {
        while (currentNumberIndex < 3)
        {
            AnimateNumber(currentNumberIndex + 1);
            yield return new WaitForSeconds(0.75f); // Wait for 1 second before showing the next number
            currentNumberIndex++;
        }

        // After displaying numbers 1, 2, and 3, load the next scene after a delay
        yield return new WaitForSeconds(0.75f);
        LoadNextScene();
    }

    void AnimateNumber(int number)
    {
        // Set text color
        numberText.text = number.ToString();
        numberText.color = Color.white;

        // Move the number up
        StartCoroutine(MoveNumberUp());
    }

    IEnumerator MoveNumberUp()
    {
        float targetPositionY = numberText.rectTransform.anchoredPosition.y; // Adjust as needed
        float elapsedTime = 0f;
        float duration = 1f; // Duration of the movement animation

        while (elapsedTime < duration)
        {
            float newY = Mathf.Lerp(numberText.rectTransform.anchoredPosition.y, targetPositionY, elapsedTime / duration);
            numberText.rectTransform.anchoredPosition = new Vector2(numberText.rectTransform.anchoredPosition.x, newY);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure number is at the target position
        numberText.rectTransform.anchoredPosition = new Vector2(numberText.rectTransform.anchoredPosition.x, targetPositionY);
    }

    void LoadNextScene()
    {
        // Replace "YourNextSceneName" with the name of your next scene
        SceneManager.LoadScene("UI");
    }
}
