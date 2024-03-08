using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Subtraction");
    }
}
