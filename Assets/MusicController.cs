using UnityEngine;

public class UIController : MonoBehaviour
{
    public void ToggleMusic()
    {
        if (MusicManager.instance != null)
        {
            MusicManager.instance.ToggleMusic();
        }
        else
        {
            Debug.LogError("MusicManager instance not found.");
        }
    }
}