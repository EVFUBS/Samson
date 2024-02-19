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
            try
            {
                var user = await _usersProvider.AddUserAsync(userLogin.Email, userLogin.Password, userLogin.Username);
                var token = await _authorisationTokenProvider.Create(user.Id);
                return Ok(token.Token);
            }
            catch (UserNotFoundException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginInfo userLoginInfo)
        {
            if (userLoginInfo == null)
            {
                return BadRequest("There is no login information");
            }
            
            try
            {
                var user = await _usersProvider.GetUserAsync(userLoginInfo.EmailOrUserName, userLoginInfo.Password);

                Models.AuthorisationToken.AuthorisationToken token;
                try
                {
                    token = await _authorisationTokenProvider.GetById(user.Id);
                }
                catch (DataNotFoundException)
                {
                    try
                    {
                        token = await _authorisationTokenProvider.RefreshToken(user.Id);
                    }
                    catch (DataNotFoundException) { 
                        token = await _authorisationTokenProvider.Create(user.Id);
                    }
                }

                return Ok(token.Token);
            } 
            catch (UserNotFoundException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
