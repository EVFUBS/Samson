using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
using SamsonConsoleApp.Models.Samson;
using SamsonConsoleApp.Options;
using SamsonConsoleApp.Providers;
using SamsonConsoleApp.Speech;
using SamsonConsoleApp.Speech.GoogleTTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamsonConsoleApp.Execute.Spotfiy.Player;
using SamsonConsoleApp.Execute.Spotfiy.Auth;
using SamsonConsoleApp.Execute;
using SamsonConsoleApp.Execute.Spotfiy;
using SamsonConsoleApp.Execute.DidNotUnderstand;
using SamsonCommon.Execute.General;
using AutoMapper;

namespace SamsonConsoleApp
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<SpotifyIntegrationOptions>(config.GetSection(nameof(SpotifyIntegrationOptions)));

            services.AddScoped<ISpotifyClientFactory, SpotifyClientFactory>();
            services.AddScoped<IWebBrowser, WebBrowser>();

            services.AddScoped<ITextToSpeech,  TextToSpeech>();

            services.AddScoped<IGreetAction, GreetAction>();
            services.AddScoped<IQuestionAction, QuestionAction>();

            services.AddSingleton<IActionCollection, ActionCollection>();
            services.AddScoped<IExecuteGeneral, ExecuteGeneral>();
            services.AddScoped<IExecuteSpotify, ExecuteSpotify>();
            services.AddScoped<IExecuteDNU, ExecuteDNU>();
            services.AddSingleton<IActionsRegister, ActionsRegister>();

            services.AddScoped<ISpotifyIntegrationOptions, SpotifyIntegrationOptions>();
            services.AddScoped<ISpotifyPlayer, SpotifyPlayer>();
            services.AddScoped<ISpotifyAuthorisation, SpotifyAuthorisation>();
            services.AddScoped<ISpotifyAuthProvider, SpotifyAuthProvider>();
            services.AddScoped<ISpotifyDAL, SpotifyDAL>();
            services.AddScoped<SamsonContext>();

            services.AddSingleton<ISpeechRecognition, SpeechRecognition>();
            services.AddSingleton<ISamsonServerCredentials, SamsonServerCredentials>();
            services.AddScoped<IServerClientFactory, ServerClientFactory>();
            services.AddScoped<IExecuteAction, ExecuteGeneral>();
            services.AddScoped<IExecuteAction, ExecuteSpotify>();
            services.AddScoped<IExecuteAction, ExecuteDNU>();
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
