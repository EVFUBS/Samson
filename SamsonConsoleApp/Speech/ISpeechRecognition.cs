namespace SamsonConsoleApp.Speech
{
    public interface ISpeechRecognition
    {
        Task Start();
        Task TestStart();
    }
}