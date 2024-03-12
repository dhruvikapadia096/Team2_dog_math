using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.IO;

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
    public int correctAnswersCount;

    private int numberOfChoices = 3;
    public float accuracy;
    public float rate;
    private int totalWrongAnswers;
    public float startTime, endTime, totalTime;

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

        int operand1 = UnityEngine.Random.Range(1, 10);
        int operand2 = UnityEngine.Random.Range(1, operand1);
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
        int correctAnswerIndex = UnityEngine.Random.Range(0, numberOfChoices);
        answerOptions[correctAnswerIndex] = questionAnswer;

        for (int i = 0; i < numberOfChoices; i++)
        {
            if (i != correctAnswerIndex)
            {
                int potentialAnswer;
                do
                {
                    int operation = UnityEngine.Random.Range(0, 2) == 0 ? 1 : -1;
                    potentialAnswer = questionAnswer + operation * UnityEngine.Random.Range(1, 3);
                } while (Array.IndexOf(answerOptions, potentialAnswer) != -1);

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

        yield return new WaitForSeconds(1f);

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
        Debug.Log($"Rate: {rate}");
        Debug.Log($"totalQuestions: {totalQuestions}");
        Debug.Log($"totalTime: {totalTime}");

        string currentDirectory = Application.dataPath;
        string filePath = Path.Combine(currentDirectory, "output.txt");
        Debug.Log($"File Path: {filePath}");

        string csvContent = $"{totalQuestions},{correctAnswersCount},{accuracy},{rate:F2}";

        try
        {
            File.WriteAllText(filePath, csvContent);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error writing to file: {e.Message}");
        }

        scoreText.text = $"Quiz Completed!!\nScore: {correctAnswersCount}\nAccuracy: {accuracy}%\nRate: {rate:F2}/min\nWrong: {totalWrongAnswers}\n";
        Debug.Log(scoreText.text);

        // Check if it's the last question
        if (currentQuestionNumber >= totalQuestions)
        {
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(0.20f); // Adjust the delay as needed

        // Load the next scene named "ScoreDisplay"
        SceneManager.LoadScene("ScoreDisplay");
    }

    void ShuffleArray<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
