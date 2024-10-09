using SamsonCommon;
using SamsonServer.Context;
using SamsonServer.DAL.AuthorisationToken;
using SamsonServer.DAL.Users;
using SamsonServer.Helpers;
using SamsonServer.Providers.AuthorisationToken;
using SamsonServer.Providers.Question;
using SamsonServer.Providers.Speech;
using SamsonServer.Providers.Users;

namespace SamsonServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<SamsonContext>();

            services.AddScoped<IUsersDal, UsersDal>();
            services.AddScoped<IUsersProvider, UsersProvider>();

            services.AddScoped<IAuthorisationTokenDal, AuthorisationTokenDal>();
            services.AddScoped<IAuthorisationTokenProvider, AuthorisationTokenProvider>();

            services.AddScoped<IQuestionProvider, QuestionProvider>();
            services.AddScoped<ISpeechProvider, SpeechProvider>();
            services.AddScoped<ISpeechDeepgram, SpeechDeepgram>();

            services.AddScoped<IPredEngineHelper, PredEngineHelper>();
            return services;
        }
    }
}