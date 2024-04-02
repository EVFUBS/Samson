using Microsoft.AspNetCore.Mvc;
using SamsonServer.DAL;
using SamsonServer.DAL.AuthorisationToken;
using SamsonServer.Exceptions;
using SamsonServer.Models.User;
using SamsonServer.Providers.AuthorisationToken;
using SamsonServer.Providers.Users;

namespace SamsonServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersProvider _usersProvider;
        private readonly IAuthorisationTokenProvider _authorisationTokenProvider;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            ILogger<UsersController> logger,
            IUsersProvider usersProvider,
            IAuthorisationTokenProvider authorisationTokenProvider)
        {
            _logger = logger;
            _usersProvider = usersProvider;
            _authorisationTokenProvider = authorisationTokenProvider;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserLogin userLogin)
        {
            var user = await _usersProvider.AddUserAsync(userLogin.Email, userLogin.Password, userLogin.Username);
            var token = await _authorisationTokenProvider.Create(user.Id);
            return Ok(token.Token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            if (userLogin == null)
            {
                return BadRequest("There is no login information");
            }

            string emailOrUsername;
            if (!string.IsNullOrEmpty(userLogin.Email))
                emailOrUsername = userLogin.Email;
            else
                emailOrUsername = userLogin.Username;
            try
            {
                var user = await _usersProvider.GetUserAsync(emailOrUsername, userLogin.Password);

                Models.AuthorisationToken.AuthorisationToken token;
                token = await _authorisationTokenProvider.GetById(user.Id);
                return Ok(token.Token);
            } 
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
