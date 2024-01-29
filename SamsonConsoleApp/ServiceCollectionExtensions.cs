using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SamsonConsoleApp.Actions.Execute;
using SamsonConsoleApp.Actions.General;
using SamsonConsoleApp.Actions.General.Greet;
using SamsonConsoleApp.Actions.General.Question;
using SamsonConsoleApp.Actions.General.WebBrowser;
using SamsonConsoleApp.Actions.Spotfiy;
using SamsonConsoleApp.Actions.Spotfiy.Interfaces;
using SamsonConsoleApp.Client;
using SamsonConsoleApp.Clients;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.DAL;
using SamsonConsoleApp.DAL.interfaces;
using SamsonConsoleApp.Options;
using SamsonConsoleApp.Providers;
using SamsonConsoleApp.Speech;
using SamsonConsoleApp.Speech.Deepgram;
using SamsonConsoleApp.Speech.GoogleTTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<SpotifyIntegrationOptions>(config.GetSection(nameof(SpotifyIntegrationOptions)));

            services.AddScoped<ISpotifyClientFactory, SpotifyClientFactory>();
            services.AddScoped<IWebBrowser, WebBrowser>();
            services.AddScoped<ISamsonAIClientFactory, SamsonAIClientFactory>();

            services.AddScoped<ISpeechDeepgram, SpeechDeepgram>();
            services.AddScoped<ITextToSpeech,  TextToSpeech>();

            services.AddScoped<IExecuteSamsonAction, ExecuteSamsonAction>();
            services.AddScoped<IExecuteGeneral, ExecuteGeneral>();
            services.AddScoped<IExecuteSpotify, ExecuteSpotify>();

            services.AddScoped<IGreetAction, GreetAction>();
            services.AddScoped<IQuestionAction, QuestionAction>();

            services.AddScoped<ISpotifyIntegrationOptions, SpotifyIntegrationOptions>();
            services.AddScoped<ISpotifyPlayer, SpotifyPlayer>();
            services.AddScoped<ISpotifyAuthorisation, SpotifyAuthorisation>();
            services.AddScoped<ISpotifyAuthProvider, SpotifyAuthProvider>();
            services.AddScoped<ISpotifyDAL, SpotifyDAL>();

            services.AddSingleton<ISpeechRecognition, SpeechRecognition>();

            return services;
        }
    }
}
