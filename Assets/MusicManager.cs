using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioSource audioSource; // Attach the AudioSource component with your music clip here

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep the music manager across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy any new instances that are created
        }

        // Check player preferences
        if (PlayerPrefs.GetInt("MusicMuted", 0) == 1)
        {
            audioSource.mute = true;
        }
    }

    public void ToggleMusic()
    {
        audioSource.mute = !audioSource.mute;
        // Save the preference
        PlayerPrefs.SetInt("MusicMuted", audioSource.mute ? 1 : 0);
        PlayerPrefs.Save();
    }
}
