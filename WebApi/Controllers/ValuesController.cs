using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policies.PublicSecure)]
    public class ValuesController : ControllerBase
    {
        [HttpGet("auth")]
        public async Task<IActionResult> Auth()
        {
            var claims = User.Claims;
            return Ok();
        }
    }
}
