using SamsonServer.Models.ReturnModels.User;
using SamsonServer.Models.User;

namespace SamsonServer.Providers.Users
{
    public interface IUsersProvider
    {
        Task<User> AddUserAsync(string email, string password, string username);
        Task<User> GetUserAsync(string emailOrUsername, string password);
        Task<UserSettings> GetUserSettingsAsync(int id);
    }
}