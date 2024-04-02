using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SamsonConsoleApp.Execute.Spotfiy;
using SamsonConsoleApp.Execute.Spotfiy.Interfaces;
using SamsonConsoleApp.Client;
using SamsonConsoleApp.Clients;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.Context;
using SamsonConsoleApp.DAL;
using SamsonConsoleApp.DAL.interfaces;
using SamsonConsoleApp.Execute.ExecuteActions;
using SamsonConsoleApp.Execute.General;
using SamsonConsoleApp.Execute.General.Greet;
using SamsonConsoleApp.Execute.General.Question;
using SamsonConsoleApp.Execute.General.WebBrowser;
using SamsonConsoleApp.Execute.Spotfiy;
using SamsonConsoleApp.Models.Samson;
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
            services.AddScoped<IAiClientFactory, AiClientFactory>();
            services.AddScoped<IWebBrowser, WebBrowser>();

            services.AddScoped<ISpeechDeepgram, SpeechDeepgram>();
            services.AddScoped<ITextToSpeech,  TextToSpeech>();

            services.AddScoped<IExecuteAction, ExecuteAction>();
            services.AddScoped<IExecuteGeneral, ExecuteGeneral>();
            services.AddScoped<IExecuteSpotify, ExecuteSpotify>();

            services.AddScoped<IGreetAction, GreetAction>();
            services.AddScoped<IQuestionAction, QuestionAction>();

            services.AddScoped<ISpotifyIntegrationOptions, SpotifyIntegrationOptions>();
            services.AddScoped<ISpotifyPlayer, SpotifyPlayer>();
            services.AddScoped<ISpotifyAuthorisation, SpotifyAuthorisation>();
            services.AddScoped<ISpotifyAuthProvider, SpotifyAuthProvider>();
            services.AddScoped<ISpotifyDAL, SpotifyDAL>();
            services.AddScoped<SamsonContext>();

            services.AddSingleton<ISpeechRecognition, SpeechRecognition>();
            services.AddSingleton<ISamsonServerCredentials, SamsonServerCredentials>();

            return services;
        }
    }
}
