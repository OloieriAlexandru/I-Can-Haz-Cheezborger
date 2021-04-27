using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models.Users;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDto newUser)
        {
            UserGetAllDto user = await userService.Create(newUser);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }
    }
}
