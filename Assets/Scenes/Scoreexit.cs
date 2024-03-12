using UnityEngine;
using UnityEngine.SceneManagement;

public class Scoreexit : MonoBehaviour
{
    private float timeScaleValue = 0.25f;

    public void RestartGame()
    {
        SetTimeScale();
        LoadScene("subtraction");
    }

    public void ExitToUI()
    {
        SetTimeScale();
        LoadScene("UI");
    }

    private void SetTimeScale()
    {
        Time.timeScale = timeScaleValue;
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
