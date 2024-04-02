using Microsoft.EntityFrameworkCore;
using SamsonConsoleApp.Context;
using SamsonServer.Exceptions;

namespace SamsonServer.DAL.AuthorisationToken
{
    public class AuthorisationTokenDAL : IAuthorisationTokenDAL
    {
        public readonly SamsonContext _samsonContext;

        public AuthorisationTokenDAL(SamsonContext samsonContext)
        {
            _samsonContext = samsonContext;
        }

        public async Task<Models.AuthorisationToken.AuthorisationToken> Create(int userId, string token, DateTimeOffset expirationDate)
        {
            var authTokens = await _samsonContext.AuthorisationTokens.FromSql($"EXEC spCreateAuthorisationToken @userId={userId}, @token={token}, @expirationDate={expirationDate}").ToListAsync();
            var authToken = authTokens.FirstOrDefault();

            if (authToken == null)
                throw new DataNotFoundException($"Could not update authorisation token with userId {userId}");

            return authToken;
        }

        public async Task<Models.AuthorisationToken.AuthorisationToken> Update(int userId, string token, DateTimeOffset expirationDate)
        {
            var authTokens = _samsonContext.AuthorisationTokens.FromSql($"EXEC spUpdateAuthorisationToken @userId={userId} @token={token} @expirationDate={expirationDate}");
            var authToken = await authTokens.FirstOrDefaultAsync();

            if (authToken == null )
                throw new DataNotFoundException($"Could not update authorisation token with userId {userId}");

            return authToken;
        }

        public async Task<Models.AuthorisationToken.AuthorisationToken> GetById(int userId)
        {
            var authToken = await _samsonContext.AuthorisationTokens.FirstOrDefaultAsync(x => x.UserId == userId);

            if (authToken == null)
                throw new DataNotFoundException($"Could not find authorisation token with id: {userId}");

            return authToken;
        }
    }
}
