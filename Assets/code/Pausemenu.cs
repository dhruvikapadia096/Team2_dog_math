using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    [SerializeField] public GameObject pausemenuPanel;

    public void Pause()
    {
        pausemenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pausemenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        pausemenuPanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("subtraction");
    }

    public void Exit()
    {
        pausemenuPanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("UI");
    }

    public void Pause_exit()
    {
        pausemenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

}
