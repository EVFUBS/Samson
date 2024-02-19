







namespace SamsonServer.Providers.AuthorisationToken
{
    public interface IAuthorisationTokenProvider
    {
        Task<Models.AuthorisationToken.AuthorisationToken> Create(int userId);
        Task<Models.AuthorisationToken.AuthorisationToken> GetById(int userId);
        Task<Models.AuthorisationToken.AuthorisationToken> RefreshToken(int userId);
    }
}