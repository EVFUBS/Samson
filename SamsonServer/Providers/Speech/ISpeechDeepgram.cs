using Deepgram.Models;

namespace SamsonServer.Providers.Speech
{
    public interface ISpeechDeepgram
    {
        Task<PrerecordedTranscription> SpeechToTextFromFile(Stream data);
    }
}