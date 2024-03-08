using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class settingsExit : MonoBehaviour
{
    [SerializeField] public GameObject settingsBackground;

    public void Exit()
    {
        settingsBackground.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("UI");
    }

}
