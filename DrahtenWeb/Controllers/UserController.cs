using Microsoft.AspNetCore.Mvc;

namespace DrahtenWeb.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult UserSearchOptions()
        {
            return View();
        }
    }
}
