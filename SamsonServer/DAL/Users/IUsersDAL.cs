using SamsonServer.Models.ReturnModels.User;
using SamsonServer.Models.User;

namespace SamsonServer.DAL.Users
{
    public interface IUsersDal
    {
        Task<User> AddUserAsync(string email, string hashedPassword, string username);
        Task<User> GetUserAsync(string emailOrUsername, string hashedPassword);
        Task<UserSettings> GetUserSettingsByIdAsync(int id);
    }
}