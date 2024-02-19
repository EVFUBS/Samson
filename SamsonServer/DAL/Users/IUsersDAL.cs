using SamsonServer.Models.User;

namespace SamsonServer.DAL.Users
{
    public interface IUsersDAL
    {
        Task<User> AddUserAsync(string email, string hashedPassword, string username);
        Task<User> GetUserAsync(string emailOrUsername, string hashedPassword);
    }
}