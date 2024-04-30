
#if UNITY_EDITOR
using SpeechLib;

public static class WindowTTS
{

    static SpVoice voice = new SpVoice();
    public static void Speak(string text)
    {
        voice.Speak(text, SpeechVoiceSpeakFlags.SVSFlagsAsync | SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
    }

    public static void Stop()
    {
        voice.Pause();
    }

}
#endif

