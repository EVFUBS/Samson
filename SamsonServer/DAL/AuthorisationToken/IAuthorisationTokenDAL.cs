






namespace SamsonServer.DAL.AuthorisationToken
{
    public interface IAuthorisationTokenDal
    {
        Task<Models.AuthorisationToken.AuthorisationToken> Create(int userId, string token, DateTimeOffset expirationDate);
        Task<Models.AuthorisationToken.AuthorisationToken> GetById(int userId);
        Task<Models.AuthorisationToken.AuthorisationToken> Update(int userId, string token, DateTimeOffset expirationDate);
        List<Models.AuthorisationToken.AuthorisationToken> GetAllAuthorisationTokens();
    }
}