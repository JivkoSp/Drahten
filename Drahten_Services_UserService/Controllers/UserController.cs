using Microsoft.AspNetCore.Mvc;

namespace Drahten_Services_UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        //The purpose of this endpoint is ONLY for testing.
        //Must be removed.
        [HttpGet]
        public IActionResult GetEndpoint()
        {
            return Ok("Ok GET from UserService");
        }
    }
}
