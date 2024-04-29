using UnityEngine;
using TMPro;
using UnityEngine.UI;

[ExecuteInEditMode()]

public class TTSManager : MonoBehaviour
{
    private static TTSManager instance;
    public static TTSManager Instance()
    {
        if (instance == null)
        {
            instance = FindFirstObjectByType<TTSManager>();
        }
        return instance;
    }

    private string stringToEdit = "Can you speak subtract logic";


    public TextMeshProUGUI ip_TextToSpeech;
    public Button btnSpeak;
    public Button btnStop;
    public Button btnClear;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        EasyTTSUtil.Initialize(EasyTTSUtil.UnitedStates);

        if (btnSpeak)
            btnSpeak.onClick.AddListener(Speak);

        if (btnStop)
            btnStop.onClick.AddListener(Stop);

        //if (btnClear)
        //    btnClear.onClick.AddListener(Clear);
    }
    private void OnDisable()
    {
        if (btnSpeak)
            btnSpeak.onClick.RemoveListener(Speak);

        if (btnStop)
            btnStop.onClick.RemoveListener(Stop);
        // btnClear.onClick.RemoveListener(Clear);
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

    void OnApplicationQuit()
    {
        EasyTTSUtil.Stop();
    }
}
