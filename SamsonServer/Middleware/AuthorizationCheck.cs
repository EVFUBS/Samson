using SamsonServer.Providers.AuthorisationToken;

namespace SamsonServer.Middleware;

public class AuthorizationCheck(RequestDelegate next, AuthorisationTokenProvider authorisationTokenProvider)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.ContainsKey("Authorization") &&
            context.Request.Headers.Authorization.ToString().StartsWith("Bearer "))
        {
            var token = context.Request.Headers.Authorization.ToString()["Bearer ".Length..];
            var isValidToken = authorisationTokenProvider.CompareTokens(token);

            if (isValidToken == false)
            {
                return;
            }
        }
        
        await next(context);
    }
}