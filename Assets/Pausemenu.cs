using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;

    public void Pause()
    {
        Time.timeScale = 0f;
        SetPauseMenuActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        SetPauseMenuActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("subtraction");
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("UI");
    }

    private void SetPauseMenuActive(bool active)
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(active);
        }
        else
        {
            Debug.LogError("Pause menu panel is not assigned!");
        }
    }
}
