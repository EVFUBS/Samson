using Microsoft.EntityFrameworkCore;
using SamsonConsoleApp.Context;
using SamsonServer.DAL.Users;
using SamsonServer.Exceptions;
using SamsonServer.Models.User;

namespace SamsonServer.DAL
{
    public class UsersDAL : IUsersDAL
    {
        public readonly SamsonContext _samsonContext;

        public UsersDAL(SamsonContext samsonContext)
        {
            _samsonContext = samsonContext;
        }

        public async Task<User> GetUserAsync(string emailOrUsername, string hashedPassword)
        {
            var user = await _samsonContext.Users.FirstOrDefaultAsync(x =>
                (x.Username == emailOrUsername || x.Email == emailOrUsername) && x.Password == hashedPassword);

            if (user != null)
            {
                return user;
            }

            throw new UserNotFoundException("Email/Username or password is incorrect");
        }

        public async Task<User> AddUserAsync(string email, string hashedPassword, string username)
        {
            var users = await _samsonContext.Users.FromSql($"EXEC spAddUser @email={email}, @password={hashedPassword}, @username={username}").ToListAsync();
            var user = users.FirstOrDefault(x => x.Email == email);

            if (user == null)
                throw new UserNotFoundException("Error occured while creating new user");

            return user;
        }
    }
}
