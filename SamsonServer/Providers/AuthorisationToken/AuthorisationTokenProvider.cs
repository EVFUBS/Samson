using Newtonsoft.Json.Linq;
using SamsonServer.DAL.AuthorisationToken;
using SamsonServer.DAL.Users;
using SamsonServer.Exceptions;
using SamsonServer.Helpers;
using SamsonServer.Models.User;
using SamsonServer.Providers.Users;
using System.Security.Cryptography;

namespace SamsonServer.Providers.AuthorisationToken
{
    public class AuthorisationTokenProvider(IAuthorisationTokenDal authorisationTokenDal) : IAuthorisationTokenProvider
    {
        public async Task<Models.AuthorisationToken.AuthorisationToken> GetById(int userId)
        {
            try
            {
                var authToken = await authorisationTokenDal.GetById(userId);
                return authToken;
            }
            catch (DataNotFoundException)
            {
                var authToken = await RefreshToken(userId);
                return authToken;
            }
        }

        public async Task<Models.AuthorisationToken.AuthorisationToken> RefreshToken(int userId)
        {
            var authToken = await authorisationTokenDal.Update(userId, TokenHelper.GenerateAuthorisationToken(), DateTimeOffset.UtcNow.AddDays(30));
            return authToken;
        }

        public async Task<Models.AuthorisationToken.AuthorisationToken> Create(int userId)
        {
            var authToken = await authorisationTokenDal.Create(userId, TokenHelper.GenerateAuthorisationToken(), DateTimeOffset.UtcNow.AddDays(30));
            return authToken;
        }

        public bool CompareTokens(string token)
        {
            // this will need caching later
            var allTokens = authorisationTokenDal.GetAllAuthorisationTokens();
            return allTokens.Exists(x => x.Token == token);
        }
    }
}