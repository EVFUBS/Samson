using SamsonServer.DAL.AuthorisationToken;
using SamsonServer.DAL.Users;
using SamsonServer.Models.User;
using SamsonServer.Providers.Users;
using SamsonServer.Utility;
using System.Security.Cryptography;

namespace SamsonServer.Providers.AuthorisationToken
{
    public class AuthorisationTokenProvider : IAuthorisationTokenProvider
    {
        private readonly IAuthorisationTokenDAL _authorisationTokenDAL;

        public AuthorisationTokenProvider(IAuthorisationTokenDAL authorisationTokenDAL)
        {
            _authorisationTokenDAL = authorisationTokenDAL;
        }

        public async Task<Models.AuthorisationToken.AuthorisationToken> GetById(int userId)
        {
            var authToken = await _authorisationTokenDAL.GetById(userId);
            return authToken;
        }

        public async Task<Models.AuthorisationToken.AuthorisationToken> RefreshToken(int userId)
        {
            var authToken = await _authorisationTokenDAL.Update(userId, TokenHelper.GenerateAuthorisationToken(), DateTimeOffset.UtcNow.AddDays(30));
            return authToken;
        }

        public async Task<Models.AuthorisationToken.AuthorisationToken> Create(int userId)
        {
            var authToken = await _authorisationTokenDAL.Create(userId, TokenHelper.GenerateAuthorisationToken(), DateTimeOffset.UtcNow.AddDays(30));
            return authToken;
        }
    }
}