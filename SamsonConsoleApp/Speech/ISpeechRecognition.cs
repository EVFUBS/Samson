namespace SamsonConsoleApp.Speech
{
    public interface ISpeechRecognition
    {
        Task Start();
        Task TestAction();
        Task TestWake();
    }
}