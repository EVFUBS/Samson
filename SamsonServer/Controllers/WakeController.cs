using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SamsonServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WakeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
