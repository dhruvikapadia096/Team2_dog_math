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
    // Audio clips for feedback
    public AudioClip correctAnswerClip;
    public AudioClip incorrectAnswerClip;

    // Audio sources for playing the clips
    private AudioSource correctAnswerAudioSource;
    private AudioSource incorrectAnswerAudioSource;
    
    private int questionAnswer;
    public int totalQuestions;
    private int remainingQuestions;
    private int currentQuestionNumber = 0;
    public int correctAnswersCount = 0;
    public GameObject Incorretgif;
    public GameObject correctgif;

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
        StartCoroutine(RepeatedPopEffect());
        correctAnswerAudioSource = gameObject.AddComponent<AudioSource>();
        incorrectAnswerAudioSource = gameObject.AddComponent<AudioSource>();

        correctAnswerAudioSource.clip = correctAnswerClip;
        incorrectAnswerAudioSource.clip = incorrectAnswerClip;
    }
IEnumerator RepeatedPopEffect()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // Wait for 5 seconds
            foreach (Image dogUnit in dogUnits)
            {
                if (dogUnit.color == Color.white && dogUnit.gameObject.activeSelf)
                {
                    StartCoroutine(PopInOutEffect(dogUnit.transform));
                }
            }
        }
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
            answerButtons[i].gameObject.SetActive(true);
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
            dogUnit.transform.localScale = Vector3.one; // Reset scale to default
        }

        // Activate the normal dog units for operand1
        for (int i = 0; i < operand1 && i < dogUnits.Length; i++)
        {
            Image dogUnit = dogUnits[i];
            dogUnit.gameObject.SetActive(true);
        }

        // Calculate the starting index for dark brown dog units
        // Since we are making dogs dark brown from the end, we subtract operand2 from operand1
        int startIndexForDarkBrownDogs = operand1 - operand2;

        // Activate and color the dark brown dog units for operand2 from the end
        for (int i = startIndexForDarkBrownDogs; i < operand1 && i < dogUnits.Length; i++)
        {
            Image dogUnit = dogUnits[i];
            dogUnit.color = new Color(1.0f, 0.4f, 0.2f, 0.8f); // Dark brown color with transparency (alpha = 0.8f)
        }
    }


    IEnumerator PopInOutEffect(Transform transform)
    {
        float duration = 0.50f;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 1.2f; // Scale up by 10%

        // Pop out effect
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;

        // Pop in effect
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale; // Reset scale to original
    }


    IEnumerator CheckAnswerWithDelay(int chosenAnswer)
    {
        bool isCorrect = chosenAnswer == questionAnswer;

        // Deactivate all answer buttons temporarily
        foreach (Button button in answerButtons)
        {
            // Assuming button text is the answer, compare it and deactivate if incorrect
        if (button.GetComponentInChildren<Text>().text != questionAnswer.ToString())
        {
            button.gameObject.SetActive(false);
        }
        }
        
        // Show feedback text
        //feedbackText.text = isCorrect ? "Correct!" : "Incorrect";

        // Show appropriate feedback image
        if (isCorrect)
        {
            correctAnswersCount++;
            correctgif.SetActive(true);
            correctAnswerAudioSource.Play();
            
        }
        else
        {
            Incorretgif.SetActive(true);
            incorrectAnswerAudioSource.Play();
            
        }

        // Wait for a short duration
        yield return new WaitForSeconds(2f);

        // Hide feedback text and images
        feedbackText.text = "";
        correctgif.SetActive(false);
        Incorretgif.SetActive(false);
        // Reactivate all buttons and set correct button to be active only
    foreach (Button button in answerButtons)
    {
        if (button.GetComponentInChildren<Text>().text == questionAnswer.ToString())
        {
            button.gameObject.SetActive(true);
        }
        else
        {
            button.gameObject.SetActive(false);
        }
    }
    
    // Move to the next question
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
