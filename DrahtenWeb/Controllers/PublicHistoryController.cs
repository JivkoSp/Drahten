using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrahtenWeb.Controllers
{
    [Authorize]
    public class PublicHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
