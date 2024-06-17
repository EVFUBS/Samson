using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SamsonConsoleApp;
using SamsonConsoleApp.Context;
using SamsonConsoleApp.Models.Samson;
using SamsonConsoleApp.Speech;

internal class Program
{
    private static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Environment.EnvironmentName = "development";

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

        builder.Services.AddDbContext<SamsonContext>(options => 
            options.UseSqlite(builder.Configuration.GetConnectionString("Samsondb")));

        builder.Services.AddDependencies(builder.Configuration);

        var app = builder.Build();
        var provider = builder.Services.BuildServiceProvider();

        var speechRecognition = provider.GetService<ISpeechRecognition>();
        var samsonCredentials = provider.GetService<ISamsonServerCredentials>();

        provider.GetService<IActionsRegister>().RegisterActions();

        if (speechRecognition != null && samsonCredentials != null)
        {
            samsonCredentials.Login();
            Console.WriteLine("Samson is now listening");
            speechRecognition.TestAction();
            app.Run();
        }
        else
        {
            throw new Exception("Error occured attempting to start samson");
        }
    }
}