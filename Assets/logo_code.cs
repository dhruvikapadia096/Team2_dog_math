using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class logo_code : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadNextSceneAfterDelay(2.00f));
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Load the next scene
        SceneManager.LoadScene("Start");
    }
}
