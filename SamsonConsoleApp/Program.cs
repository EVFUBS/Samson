using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SamsonConsoleApp;
using SamsonConsoleApp.Context;
using SamsonConsoleApp.Speech;

internal class Program
{
    private static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Environment.EnvironmentName = "development";

        // Read Config
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

        // Dependency Injection
        builder.Services.AddDependencies(builder.Configuration);

        builder.Services.AddDbContext<SamsonContext>();

        var app = builder.Build();
        var provider = builder.Services.BuildServiceProvider();
        var speechRecognition = provider.GetService<ISpeechRecognition>();

        if (speechRecognition != null)
        {
            Console.WriteLine("Samson is now listening");
            speechRecognition.Start();
            app.Run();
        }
        else
        {
            throw new Exception("Error Occured attempting to start speech recognition");
        }
    }
}