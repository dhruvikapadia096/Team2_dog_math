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
    public Image[] dogUnits; // Array to represent dog units
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI questionNumberText;
    public TextMeshProUGUI scoreText;

    private int questionAnswer;
    public int totalQuestions;
    private int remainingQuestions;
    private int currentQuestionNumber = 0;
    public int correctAnswersCount = 0;

    private int numberOfChoices = 3;
    public float accuracy;
    public float rate;
    private int totalWrongAnswers;
    public float startTime, endTime, totalTime;

    void Start()
    {
        totalQuestions = 5;
        remainingQuestions = totalQuestions;
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

        Debug.Log("Before displayDogsForQuestion");
        Debug.Log("Operand1 - " + operand1);
        Debug.Log("Operand2 - " + operand2);
        DisplayDogsForQuestion(operand1, operand2); // Display dogs based on the answer

        int[] answerOptions = GenerateAnswerOptions();

        ShuffleArray(answerOptions);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = answerOptions[i].ToString();
            int chosenAnswer = answerOptions[i];
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => StartCoroutine(CheckAnswerWithDelay(chosenAnswer)));
        }
    }

    void DisplayDogsForQuestion(int operand1, int operand2)
    {
        // Deactivate all dog units initially
        foreach (Image dogUnit in dogUnits)
        {
            dogUnit.gameObject.SetActive(false);
            dogUnit.color = Color.white; // Reset to default color in case they were red before
        }

        // Activate the normal dog units for operand1
        for (int i = 0; i < operand1 && i < dogUnits.Length; i++)
        {
            dogUnits[i].gameObject.SetActive(true);
        }

        // Calculate the starting index for red dog units
        // Since we are making dogs red from the end, we subtract operand2 from operand1
        int startIndexForRedDogs = operand1 - operand2;

        // Activate and color the red dog units for operand2 from the end
        for (int i = startIndexForRedDogs; i < operand1 && i < dogUnits.Length; i++)
        {
            dogUnits[i].color = Color.red; // Change the color of the dog image to red
        }
    }


    IEnumerator CheckAnswerWithDelay(int chosenAnswer)
    {
        bool isCorrect = chosenAnswer == questionAnswer;
        feedbackText.text = isCorrect ? "Correct!" : "Incorrect";

        if (isCorrect)
        {
            correctAnswersCount++;
        }

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
        // Deactivate quiz UI
        questionText.gameObject.SetActive(false);
        foreach (var button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
        feedbackText.gameObject.SetActive(false);
        questionNumberText.gameObject.SetActive(false);

        accuracy = ((float)correctAnswersCount / totalQuestions) * 100;
        totalWrongAnswers = totalQuestions - correctAnswersCount;

        endTime = Time.time;
        totalTime = endTime - startTime;

        rate = (totalQuestions / totalTime) * 60f;

        string currentDirectory = Application.persistentDataPath;
        string filePath = Path.Combine(currentDirectory, "output.txt");

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

        if (currentQuestionNumber >= totalQuestions)
        {
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(2f); // Adjust the delay as needed
        SceneManager.LoadScene("ScoreBoard");
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

    int[] GenerateAnswerOptions()
    {
        int[] options = new int[numberOfChoices];

        int correctAnswerIndex = UnityEngine.Random.Range(0, numberOfChoices);
        options[correctAnswerIndex] = questionAnswer;

        for (int i = 0; i < numberOfChoices; i++)
        {
            if (i != correctAnswerIndex)
            {
                int randomAnswer = UnityEngine.Random.Range(1, 10);
                while (randomAnswer == questionAnswer || Array.IndexOf(options, randomAnswer) > -1)
                {
                    randomAnswer = UnityEngine.Random.Range(1, 10);
                }
                options[i] = randomAnswer;
            }
        }

        return options;
    }
}
