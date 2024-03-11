using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SubtractionQuiz : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI questionNumberText;
    public TextMeshProUGUI scoreText;

    private int questionAnswer;
    public int totalQuestions;
    private int remainingQuestions;
    private int currentQuestionNumber;
    private int correctAnswersCount;

    private int numberOfChoices = 3;
    private float accuracy;
    private float rate;
    private int totalWrongAnswers;
    private float startTime, endTime, totalTime;

    void Start()
    {
        totalQuestions = 5;
        currentQuestionNumber = 0;
        remainingQuestions = totalQuestions - currentQuestionNumber;
        UpdateQuestionCounter();
        startTime = Time.time;
        GenerateQuestion();
    }

    void GenerateQuestion()
    {
        if (remainingQuestions <= 0)
        {
            Debug.Log("Quiz completed!");
            ShowScore();
            return;
        }

        int operand1 = Random.Range(1, 10);
        int operand2 = Random.Range(1, operand1);
        questionAnswer = operand1 - operand2;

        currentQuestionNumber++;
        questionNumberText.text = $"{currentQuestionNumber}/{totalQuestions}";

        questionText.text = $"{operand1} - {operand2} = __";

        int[] answerOptions = GenerateAnswerOptions();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = answerOptions[i].ToString();
            answerButtons[i].onClick.RemoveAllListeners();
            int chosenAnswer = answerOptions[i];
            answerButtons[i].onClick.AddListener(() => StartCoroutine(CheckAnswerWithDelay(chosenAnswer)));
        }
    }

    int[] GenerateAnswerOptions()
    {
        int[] answerOptions = new int[numberOfChoices];
        int correctAnswerIndex = Random.Range(0, numberOfChoices);
        answerOptions[correctAnswerIndex] = questionAnswer;

        for (int i = 0; i < numberOfChoices; i++)
        {
            if (i != correctAnswerIndex)
            {
                int potentialAnswer;
                do
                {
                    int operation = Random.Range(0, 2) == 0 ? 1 : -1;
                    potentialAnswer = questionAnswer + operation * Random.Range(1, 3);
                } while (System.Array.IndexOf(answerOptions, potentialAnswer) != -1);

                answerOptions[i] = potentialAnswer;
            }
        }

        ShuffleArray(answerOptions);

        return answerOptions;
    }

    IEnumerator CheckAnswerWithDelay(int chosenAnswer)
    {
        bool isCorrect = chosenAnswer == questionAnswer;
        feedbackText.text = isCorrect ? "Correct!" : "Incorrect";

        if (isCorrect)
        {
            correctAnswersCount++;
        }

        Debug.Log($"Selected Answer: {chosenAnswer}. {feedbackText.text}");

        yield return new WaitForSeconds(2f);

        feedbackText.text = "";

        remainingQuestions--;

        GenerateQuestion();

        UpdateQuestionCounter();
    }

    void UpdateQuestionCounter()
    {
        questionNumberText.text = $"{currentQuestionNumber}/{totalQuestions}";
    }

    void ShowScore()
    {
        questionText.gameObject.SetActive(false);
        foreach (var button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
        feedbackText.gameObject.SetActive(false);
        questionNumberText.gameObject.SetActive(false);

        Debug.Log("Inside showScore()");
        Debug.Log($"correctAnswersCount {correctAnswersCount}");

        Debug.Log($"totalQuestions {totalQuestions}");

        accuracy = ((float)correctAnswersCount / totalQuestions) * 100;
        totalWrongAnswers = totalQuestions - correctAnswersCount;

        endTime = Time.time;
        totalTime = endTime - startTime;

        rate = (totalQuestions / totalTime) * 60f;

        // scoreText.text = $"Quiz Completed!!\nScore: {correctAnswersCount}/{totalQuestions}";
        scoreText.text = $"Quiz Completed!!\nScore: {correctAnswersCount}\nWrong: {totalWrongAnswers}\nRate: {rate}\nAccuracy: {accuracy}%";

        if (scoreText != null)
        {
            // scoreText.text = $"Completed!!!\nScore: {correctAnswersCount}/{totalQuestions}";
            // scoreText.text = $"Completed!!\nWrong: {totalWrongAnswers}\nRate: {rate}\nAccuracy: {accuracy}%";
            scoreText.text = $"Quiz Completed!!\nScore: {correctAnswersCount}\nAccuracy: {accuracy}%\nRate: {rate:F2}/min\nWrong: {totalWrongAnswers}\n";
        }
        else
        {
            Debug.LogError("scoreText is not assigned in the Inspector.");
        }
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