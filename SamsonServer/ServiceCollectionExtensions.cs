using SamsonConsoleApp.Actions.Interfaces;
using SamsonConsoleApp.Actions;
using SamsonConsoleApp.Client;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.Options;
using SamsonConsoleApp.Speech;
using SamsonConsoleApp.Models.Spotify;
using SamsonConsoleApp.Actions.Spotify;

namespace SamsonServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServerDependencies(this IServiceCollection services)
        {
            services.AddScoped<ISpotifyClientFactory, SpotifyClientFactory>();
            services.AddScoped<IWebBrowser, WebBrowser>();
            services.AddScoped<ISpotifyIntegration, SpotifyIntegration>();
            services.AddScoped<ISpeechRecognition, SpeechRecognition>();
            services.AddScoped<ISpotifyCredentials, SpotifyCredentials>();
            return services;
        }
    }
}
