using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextMainSceneButton : MonoBehaviour
{
    // Method to load the next MainScene
    public void GoToNextMainScene()
    {
        // Assuming "MainScene2" is the name of your next MainScene
        SceneManager.LoadScene("MainScene2");
    }
}
