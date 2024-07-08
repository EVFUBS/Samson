namespace SamsonLocal.Speech
{
    public interface ISpeechRecognition
    {
        Task WakeWordStart();
        Task TestAction();
        Task TestWake();

        Task Start();
    }
}