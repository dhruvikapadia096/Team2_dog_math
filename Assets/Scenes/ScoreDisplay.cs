using UnityEngine;
using TMPro;
using System.IO;

public class HelloWorld : MonoBehaviour
{
    public TextMeshProUGUI print;

    // Start is called before the first frame update
    void Start()
    {

        if (print != null)
        {
            // Read the contents of the "output.txt" file
            string currentDirectory = Application.dataPath;
            string filePath = Path.Combine(currentDirectory, "output.txt");

            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);

                // Split the data by commas
                string[] data = fileContent.Split(',');

                // Check if there are enough elements in the array
                if (data.Length >= 4)
                {
                    int totalQuestions = int.Parse(data[0]);
                    int correctAnswersCount = int.Parse(data[1]);
                    float accuracy = float.Parse(data[2]);
                    float rate = float.Parse(data[3]);

                    // Display the data in the TextMeshProUGUI component
                    print.text = $"Total Questions: {totalQuestions}\nCorrect Answers: {correctAnswersCount}\nAccuracy: {accuracy}%\nRate: {rate:F2}/min";
                }
                else
                {
                    Debug.LogError("Invalid data format in the 'output.txt' file.");
                    print.text = "Error: Invalid data format";
                }
            }
            else
            {
                Debug.LogError("The 'output.txt' file does not exist.");
                print.text = "Error: File not found";
            }
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned to the 'print' variable.");
        }
    }
}