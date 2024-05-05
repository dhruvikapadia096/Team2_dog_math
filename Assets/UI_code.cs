using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    
    public void OnPlayButtonClick()
    {
        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            // set player name to default value if it doesn't exist
            PlayerPrefs.SetString("PlayerName", "Player");
        }
        SceneManager.LoadScene("Subtraction");
    }
    
}