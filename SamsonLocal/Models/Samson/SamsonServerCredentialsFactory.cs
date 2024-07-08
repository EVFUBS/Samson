namespace SamsonLocal.Models.Samson;

public class SamsonServerCredentialsFactory(IServiceScopeFactory serviceScopeFactory) : ISamsonServerCredentialsFactory
{
    public ISamsonServerCredentials CreateSamsonCredentialsInstance()
    {
        using var scope = serviceScopeFactory.CreateScope();
        return scope.ServiceProvider.GetRequiredService<ISamsonServerCredentials>();
    }
}