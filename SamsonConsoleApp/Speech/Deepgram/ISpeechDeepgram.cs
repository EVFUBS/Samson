using Deepgram.Models;

namespace SamsonConsoleApp.Speech.Deepgram
{
    public interface ISpeechDeepgram
    {
        Task<PrerecordedTranscription> SpeechToTextFromFile(string fileUrl);
    }
}