using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{
    [Route("/")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("It's working - Deployed by Olo");
        }
    }
}
