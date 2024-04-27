using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject blackPanel; // Reference to the black panel

    public void Pause()
    {
        Time.timeScale = 0f;
        SetPauseMenuActive(true);
        // Enable blur effect when pausing

        // Show the black panel when pausing
        if (blackPanel != null)
        {
            blackPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Black panel reference is not set!");
        }
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

    public void Cancel()
    {
        Time.timeScale = 1f;
        SetPauseMenuActive(false);
    }

    private void SetPauseMenuActive(bool active)
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(active);

            // Activate/deactivate the black panel accordingly
            if (blackPanel != null)
            {
                blackPanel.SetActive(active);
            }
            else
            {
                Debug.LogError("Black panel is not assigned!");
            }
        }
        else
        {
            Debug.LogError("Pause menu panel is not assigned!");
        }
    }
}
