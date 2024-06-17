using SamsonServer.DAL.Users;
using SamsonServer.Helpers;
using SamsonServer.Models.ReturnModels.User;
using SamsonServer.Models.User;

namespace SamsonServer.Providers.Users
{
    public class UsersProvider : IUsersProvider
    {
        private readonly IUsersDal _usersDAL;

        public UsersProvider(IUsersDal usersDAL)
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

        public async Task<UserSettings> GetUserSettingsAsync(int id)
        {
            return await _usersDAL.GetUserSettingsByIdAsync(id);
        }
    }
}
