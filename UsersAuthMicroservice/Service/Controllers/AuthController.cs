using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models.Auth;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            AuthenticationResponse response = await authService.Authenticate(authenticationRequest);
            
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
