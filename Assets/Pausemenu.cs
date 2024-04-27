using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject blurPanel; // Reference to the panel for blurring the background

    public void Pause()
    {
        Time.timeScale = 0f;
        SetPauseMenuActive(true);
        // Enable blur effect when pausing
        if (blurPanel != null)
        {
            blurPanel.SetActive(true);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        SetPauseMenuActive(false);
        // Disable blur effect when resuming
        if (blurPanel != null)
        {
            blurPanel.SetActive(false);
        }
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
    public void cancel()
    {
        Time.timeScale = 1f;
        SetPauseMenuActive(false);
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