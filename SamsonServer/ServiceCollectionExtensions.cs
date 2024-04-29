using SamsonConsoleApp.Context;
using SamsonServer.DAL;
using SamsonServer.DAL.AuthorisationToken;
using SamsonServer.DAL.Users;
using SamsonServer.Helpers;
using SamsonServer.Providers.AuthorisationToken;
using SamsonServer.Providers.Speech;
using SamsonServer.Providers.Users;

namespace SamsonServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<SamsonContext>();

            services.AddScoped<IUsersDAL, UsersDAL>();
            services.AddScoped<IUsersProvider, UsersProvider>();

            services.AddScoped<IAuthorisationTokenDAL, AuthorisationTokenDAL>();
            services.AddScoped<IAuthorisationTokenProvider, AuthorisationTokenProvider>();

            services.AddScoped<ISpeechDeepgram, SpeechDeepgram>();

            services.AddScoped<IPredEngineHelper, PredEngineHelper>();

            return services;
        }
    }
}