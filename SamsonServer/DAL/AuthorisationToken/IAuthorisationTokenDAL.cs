






namespace SamsonServer.DAL.AuthorisationToken
{
    public interface IAuthorisationTokenDAL
    {
        Task<Models.AuthorisationToken.AuthorisationToken> Create(int userId, string token, DateTimeOffset expirationDate);
        Task<Models.AuthorisationToken.AuthorisationToken> GetById(int userId);
        Task<Models.AuthorisationToken.AuthorisationToken> Update(int userId, string token, DateTimeOffset expirationDate);
    }
}