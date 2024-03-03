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

    private int questionAnswer;
    public int totalQuestions; 
    private int remainingQuestions;
    private int currentQuestionNumber; // New variable to track current question number

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