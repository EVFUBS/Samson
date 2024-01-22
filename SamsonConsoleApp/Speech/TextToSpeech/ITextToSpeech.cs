

namespace SamsonConsoleApp.Speech.GoogleTTS
{
    public interface ITextToSpeech
    {
        Task Say(string summary);
    }
}