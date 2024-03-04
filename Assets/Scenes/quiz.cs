using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SubtractionQuiz : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI questionNumberText; // New Text field for current question number
    public TextMeshProUGUI scoreText;

    private int questionAnswer;
    public int totalQuestions; 
    private int remainingQuestions;
    private int currentQuestionNumber; // New variable to track current question number
    private int correctAnswersCount; // Variable to track the number of correct answers


    private int numberOfChoices = 3;


    void Start()
    {
        totalQuestions = 15;
        // remainingQuestions = totalQuestions;
        currentQuestionNumber = 0; // Initialize current question number
        remainingQuestions = 15 - currentQuestionNumber;
        UpdateQuestionCounter();
        GenerateQuestion();

    }

    void GenerateQuestion()
    {
        if (remainingQuestions <= 0)
        {
            Debug.Log("Quiz completed!");
            ShowScore(); // Add this line to display the score when the quiz completes
            return;
        }

        int operand1 = Random.Range(1, 10);
        int operand2 = Random.Range(1, operand1);
        questionAnswer = operand1 - operand2;

        currentQuestionNumber++; // Increment current question number
        questionNumberText.text = $"Question {currentQuestionNumber}/15"; // Update text field

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
                int operation = Random.Range(0, 2) == 0 ? 1 : -1;
                answerOptions[i] = questionAnswer + operation * Random.Range(1, 3);
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
        correctAnswersCount++; // Increment correct answer count if the answer is correct
    }


        Debug.Log($"Selected Answer: {chosenAnswer}. {feedbackText.text}");

        yield return new WaitForSeconds(2f);

        feedbackText.text = "";

        remainingQuestions--;

        GenerateQuestion();

        UpdateQuestionCounter();
        //code
    }

    void UpdateQuestionCounter()
    {
        questionNumberText.text = $"Question: {currentQuestionNumber}/{totalQuestions}";
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

        scoreText.text = $"Quiz Completed!!\nScore: {correctAnswersCount}/{totalQuestions}"; // Display the score with additional text;

        if (scoreText != null)
        {
            scoreText.text = $"Quiz Completed!!!\nScore: {correctAnswersCount}/{totalQuestions}"; // Display the score with additional text // Display the score if scoreText is not null
        }
        else
        {
            Debug.LogError("scoreText is not assigned in the Inspector."); // Log an error if scoreText is null
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