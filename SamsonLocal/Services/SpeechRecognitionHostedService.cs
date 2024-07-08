using SamsonLocal.Models.Samson;
using SamsonLocal.Speech;

namespace SamsonLocal.Services;

public class SpeechRecognitionHostedService(ISamsonServerCredentialsFactory samsonCredentialsFactory,
    ISpeechRecognitionFactory speechRecognitionFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Running Speech Recognition");

        var samsonCredentials = samsonCredentialsFactory.CreateSamsonCredentialsInstance();
        var speechRecognition = speechRecognitionFactory.CreateSpeechRecognitionInstance();
        await samsonCredentials.Login();
        
        Console.WriteLine("Samson is now listening");
        await speechRecognition.Start();
    }
}