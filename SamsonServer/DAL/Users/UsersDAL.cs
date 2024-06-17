using Microsoft.EntityFrameworkCore;
using SamsonServer.Context;
using SamsonServer.Exceptions;
using SamsonServer.Models.ReturnModels.User;
using SamsonServer.Models.User;

namespace SamsonServer.DAL.Users
{
    public class UsersDal(SamsonContext samsonContext) : IUsersDal
    {
        public async Task<User> GetUserAsync(string emailOrUsername, string hashedPassword)
        {
            var user = await samsonContext.Users.FirstOrDefaultAsync(x =>
                (x.Username == emailOrUsername || x.Email == emailOrUsername) && x.Password == hashedPassword);

            if (user != null)
            {
                return user;
            }

            throw new UserNotFoundException("Email/Username or password is incorrect");
        }

        public async Task<User> AddUserAsync(string email, string hashedPassword, string username)
        {
            var users = await samsonContext.Users.FromSql($"EXEC spAddUser @email={email}, @password={hashedPassword}, @username={username}").ToListAsync();
            return users.FirstOrDefault(x => x.Email == email) ?? throw new InvalidOperationException();
        }

        public async Task<UserSettings> GetUserSettingsByIdAsync(int id)
        {
            var user = await samsonContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                var userSettings = new UserSettings
                {
                    Id = user.Id,
                    ListenDuration = user.ListenDuration,
                    ListenMode = user.ListenMode
                };

                return userSettings;
            }

            throw new Exception($"could not find user with id: {id}");
        }
    }
}
