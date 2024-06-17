using Microsoft.AspNetCore.Mvc;
using SamsonServer.Exceptions;
using SamsonServer.Models.ReturnModels.User;
using SamsonServer.Models.User;
using SamsonServer.Providers.AuthorisationToken;
using SamsonServer.Providers.Users;

namespace SamsonServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(
        ILogger<UsersController> logger,
        IUsersProvider usersProvider,
        IAuthorisationTokenProvider authorisationTokenProvider) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserLogin userLogin)
        {
            var user = await usersProvider.AddUserAsync(userLogin.Email, userLogin.Password, userLogin.Username);
            var token = await authorisationTokenProvider.Create(user.Id);
            return Ok(token.Token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var emailOrUsername = !string.IsNullOrEmpty(userLogin.Email) ? userLogin.Email : userLogin.Username;
            
            try
            {
                var user = await usersProvider.GetUserAsync(emailOrUsername, userLogin.Password);
                var token = await authorisationTokenProvider.GetById(user.Id);
                return Ok(token.Token);
            } 
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("settings/{id:int}")]
        public async Task<ActionResult<UserSettings>> GetUserSettingsAsync(int id)
        {
            try
            {
                var userSettings = await usersProvider.GetUserSettingsAsync(id);
                return Ok(userSettings);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
