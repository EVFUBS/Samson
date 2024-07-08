namespace SamsonLocal.Speech;

public class SpeechRecognitionFactory(IServiceScopeFactory serviceScopeFactory) : ISpeechRecognitionFactory
{
    public ISpeechRecognition CreateSpeechRecognitionInstance()
    {
        using var scope = serviceScopeFactory.CreateScope();
        return scope.ServiceProvider.GetRequiredService<ISpeechRecognition>();
    }
}