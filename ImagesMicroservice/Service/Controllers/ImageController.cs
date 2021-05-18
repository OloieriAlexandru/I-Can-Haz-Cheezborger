using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{
    [Route("api/v1/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageBusinessLogic imageBusinessLogic;

        public ImageController(IImageBusinessLogic imageBusinessLogic)
        {
            this.imageBusinessLogic = imageBusinessLogic;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Working");
        }
    }
}
