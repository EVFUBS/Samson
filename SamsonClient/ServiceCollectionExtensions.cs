using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SamsonClient.Clients;
using SamsonClient.Clients.Interfaces;
using SamsonClient.Context;
using SamsonClient.DAL;
using SamsonClient.DAL.interfaces;
using SamsonClient.Execute;
using SamsonClient.Execute.DidNotUnderstand;
using SamsonClient.Execute.ExecuteActions;
using SamsonClient.Execute.General;
using SamsonClient.Execute.General.Greet;
using SamsonClient.Execute.General.Question;
using SamsonClient.Execute.General.WebBrowser;
using SamsonClient.Execute.Spotfiy;
using SamsonClient.Execute.Spotfiy.Auth;
using SamsonClient.Execute.Spotfiy.Interfaces;
using SamsonClient.Execute.Spotfiy.Player;
using SamsonClient.Models.Samson;
using SamsonClient.Options;
using SamsonClient.Providers;
using SamsonClient.Speech;
using SamsonClient.Speech.TextToSpeech;
using SamsonCommon;

namespace SamsonClient
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
            services.AddScoped<IExecuteDnu, ExecuteDnu>();
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
            services.AddScoped<IExecuteAction, ExecuteDnu>();

            return services;
        }
    }
}
