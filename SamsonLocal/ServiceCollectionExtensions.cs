using SamsonLocal.Execute.Spotfiy.Interfaces;
using SamsonLocal.Client;
using SamsonLocal.Clients;
using SamsonLocal.Clients.Interfaces;
using SamsonLocal.Context;
using SamsonLocal.DAL;
using SamsonLocal.DAL.interfaces;
using SamsonLocal.Execute.ExecuteActions;
using SamsonLocal.Execute.General;
using SamsonLocal.Execute.General.Greet;
using SamsonLocal.Execute.General.Question;
using SamsonLocal.Execute.General.WebBrowser;
using SamsonLocal.Models.Samson;
using SamsonLocal.Options;
using SamsonLocal.Providers;
using SamsonLocal.Speech;
using SamsonLocal.Speech.GoogleTTS;
using SamsonLocal.Execute.Spotfiy.Player;
using SamsonLocal.Execute.Spotfiy.Auth;
using SamsonLocal.Execute;
using SamsonLocal.Execute.Spotfiy;
using SamsonLocal.Execute.DidNotUnderstand;
using SamsonCommon.Execute.General;

namespace SamsonLocal
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<SpotifyIntegrationOptions>(config.GetSection(nameof(SpotifyIntegrationOptions)));

            services.AddScoped<ISpotifyClientFactory, SpotifyClientFactory>();
            services.AddScoped<IWebBrowser, WebBrowser>();

            services.AddScoped<ITextToSpeech,  TextToSpeech>();

            services.AddScoped<IGreetAction, GreetAction>();
            services.AddScoped<IQuestionAction, QuestionAction>();

            services.AddScoped<IActionCollection, ActionCollection>();
            services.AddScoped<IExecuteGeneral, ExecuteGeneral>();
            services.AddScoped<IExecuteSpotify, ExecuteSpotify>();
            services.AddScoped<IExecuteDNU, ExecuteDNU>();
            services.AddScoped<IActionsRegister, ActionsRegister>();

            services.AddScoped<ISpotifyIntegrationOptions, SpotifyIntegrationOptions>();
            services.AddScoped<ISpotifyPlayer, SpotifyPlayer>();
            services.AddScoped<ISpotifyAuthorisation, SpotifyAuthorisation>();
            services.AddScoped<ISpotifyAuthProvider, SpotifyAuthProvider>();
            services.AddScoped<ISpotifyDal, SpotifyDal>();
            services.AddScoped<SamsonContext>();

            services.AddScoped<ISpeechRecognition, SpeechRecognition>();
            services.AddScoped<ISamsonServerCredentials, SamsonServerCredentials>();
            services.AddScoped<IServerClientFactory, ServerClientFactory>();
            services.AddScoped<IExecuteAction, ExecuteGeneral>();
            services.AddScoped<IExecuteAction, ExecuteSpotify>();
            services.AddScoped<IExecuteAction, ExecuteDNU>();

            services.AddSingleton<ISamsonServerCredentialsFactory, SamsonServerCredentialsFactory>();
            services.AddSingleton<ISpeechRecognitionFactory, SpeechRecognitionFactory>();
            
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}