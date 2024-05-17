using UnityEngine;
using TMPro;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class TTSManager : MonoBehaviour
{
    public TextMeshProUGUI ip_TextToSpeech;
    private string stringToEdit = "Can you speak subtract logic";

    private static TTSManager instance;

    public static TTSManager Instance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<TTSManager>();
            if (instance == null)
            {
                GameObject obj = new GameObject("TTSManager");
                instance = obj.AddComponent<TTSManager>();
            }
        }
        return instance;
    }

    private void Awake()
    {
        // Ensure that there's only one instance of TTSManager
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        EasyTTSUtil.Initialize(EasyTTSUtil.UnitedStates);
    }

    public void Speak()
    {
        stringToEdit = ip_TextToSpeech.text;

        if (string.IsNullOrEmpty(stringToEdit))
        {
            EasyTTSUtil.SpeechAdd("your input field Should not be empty");
            return;
        }
        EasyTTSUtil.SpeechAdd(stringToEdit);
    }

    public void Stop()
    {
        EasyTTSUtil.StopSpeech();
    }

    public void Clear()
    {
        ip_TextToSpeech.text = "";
        stringToEdit = ip_TextToSpeech.text;
    }

    public void Speak(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            EasyTTSUtil.SpeechAdd("your result should not be empty");
            return;
        }
        EasyTTSUtil.SpeechAdd(text);
    }

    private void OnApplicationQuit()
    {
        EasyTTSUtil.Stop();
    }
}