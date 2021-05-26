using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models.Users;
using System;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await userService.GetAll());
        } 

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            UserGetByIdDto user = await userService.GetById(id);
            
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("{id:guid}/image")]
        public async Task<IActionResult> GetImageUrl([FromRoute] Guid id)
        {
            UserGetImageUrlDto imageUrlDto = await userService.GetImageUrl(id);

            if (imageUrlDto == null)
            {
                return NotFound();
            }

            return Ok(imageUrlDto);
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Patch([FromRoute] Guid id, UserPatchDto patchDto)
        {
            if (id != patchDto.Id)
            {
                return BadRequest();
            }

            await userService.PatchUser(patchDto);
            return NoContent();
        }

        [HttpPatch("{id:guid}/roles")]
        public async Task<IActionResult> PatchModeratorRole([FromRoute] Guid id, UserPatchModeratorRoleDto patchModelDto)
        {
            await userService.PatchRole(patchModelDto);

            return NoContent();
        }

        [HttpDelete("{id:guid}/roles")]
        public async Task<IActionResult> DeleteModeratorRole([FromRoute] Guid id, UserDeleteModeratorRoleDto deleteModelDto)
        {
            await userService.DeleteRole(deleteModelDto);

            return NoContent();
        }
    }
}
