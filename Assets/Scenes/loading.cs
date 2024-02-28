using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public Text numberText;
    public Image backgroundImage;

    private Color[] backgroundColors = { Color.blue, Color.green, Color.yellow };
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
            yield return new WaitForSeconds(1.0f); // Wait for 2 seconds before showing the next number
            currentNumberIndex++;
        }

        // After displaying numbers 1, 2, and 3, load next scene after a delay
        yield return new WaitForSeconds(1.0f);
        LoadNextScene();
    }

    void AnimateNumber(int number)
    {
        // Set text and background color
        numberText.text = number.ToString();
        numberText.color = Color.white;
        backgroundImage.color = backgroundColors[currentNumberIndex];

        // Move the number and background up
        StartCoroutine(MoveNumberAndBackgroundUp());
    }

    IEnumerator MoveNumberAndBackgroundUp()
    {
        float targetPositionY = backgroundImage.rectTransform.anchoredPosition.y; // Adjust as needed
        float elapsedTime = 0f;
        float duration = 1f; // Duration of the movement animation

        while (elapsedTime < duration)
        {
            float newY = Mathf.Lerp(backgroundImage.rectTransform.anchoredPosition.y, targetPositionY, elapsedTime / duration);
            backgroundImage.rectTransform.anchoredPosition = new Vector2(backgroundImage.rectTransform.anchoredPosition.x, newY);
            numberText.rectTransform.anchoredPosition = new Vector2(numberText.rectTransform.anchoredPosition.x, newY); // Move number along with background
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure background and number are at the target position
        backgroundImage.rectTransform.anchoredPosition = new Vector2(backgroundImage.rectTransform.anchoredPosition.x, targetPositionY);
        numberText.rectTransform.anchoredPosition = new Vector2(numberText.rectTransform.anchoredPosition.x, targetPositionY);
    }

    void LoadNextScene()
    {
       // Replace "YourNextSceneName" with the name of your next scene
        SceneManager.LoadScene("main");
        
    }
    
    
}