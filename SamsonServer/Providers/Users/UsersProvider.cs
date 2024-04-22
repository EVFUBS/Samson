using SamsonServer.DAL.Users;
using SamsonServer.Helpers;
using SamsonServer.Models.User;

namespace SamsonServer.Providers.Users
{
    public class UsersProvider : IUsersProvider
    {
        private readonly IUsersDAL _usersDAL;

        public UsersProvider(IUsersDAL usersDAL)
        {
            _usersDAL = usersDAL;
        }

        public async Task<User> GetUserAsync(string emailOrUsername, string password)
        {
            var user = await _usersDAL.GetUserAsync(emailOrUsername, Encrypt.GetHashString(password));
            return user;
        }

        public async Task<User> AddUserAsync(string email, string password, string username)
        {
            var hashedPassword = Encrypt.GetHashString(password);
            var user = await _usersDAL.AddUserAsync(email, hashedPassword, username);
            return user;
        }
    }
}
