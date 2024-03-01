using System.Collections;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubtractionQuiz : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public TextMeshProUGUI feedbackText;

    private int questionAnswer; // Variable to store the correct answer

    void Start()
    {
        GenerateQuestion();
    }

    void GenerateQuestion()
    {
        int operand1 = Random.Range(1, 10);
        int operand2 = Random.Range(1, operand1);
        questionAnswer = operand1 - operand2;

        questionText.text = $"{operand1} - {operand2} =";

        int[] answerOptions = { questionAnswer, questionAnswer + 1, questionAnswer - 1 };
        ShuffleArray(answerOptions);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            // Assign the correct answer to one of the buttons randomly
            answerButtons[i].GetComponentInChildren<Text>().text = answerOptions[i].ToString();
            answerButtons[i].onClick.RemoveAllListeners(); // Remove previous listeners
            int chosenAnswer = answerOptions[i];
            answerButtons[i].onClick.AddListener(() => StartCoroutine(CheckAnswerWithDelay(chosenAnswer)));
        }
    }

    IEnumerator CheckAnswerWithDelay(int chosenAnswer)
    {
        bool isCorrect = chosenAnswer == questionAnswer;
        feedbackText.text = isCorrect ? "Correct!" : "Incorrect";

        Debug.Log($"Selected Answer: {chosenAnswer}. {feedbackText.text}");

        // Wait for 2 seconds
        yield return new WaitForSeconds(1f);

        // Clear the feedback text
        feedbackText.text = "";

        // Generate a new question after checking the answer
        GenerateQuestion();
    }

    void ShuffleArray<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
