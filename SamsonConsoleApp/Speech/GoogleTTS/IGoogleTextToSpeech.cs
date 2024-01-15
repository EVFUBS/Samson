
namespace SamsonConsoleApp.Speech.GoogleTTS
{
    public interface IGoogleTextToSpeech
    {
        Task CustomTextToSpeech(string filePath, string summary);
    }
}