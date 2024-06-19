using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrahtenWeb.Controllers
{
    [Authorize]
    public class TermsAndConditionsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(); 
        }

        // Action to handle user consent.
        [HttpPost]
        public IActionResult Consent()
        {
            Response.Cookies.Append("TermsAndConditionsConsent", "true");

            // Redirect to the home page.
            return RedirectToAction("Index", "Home");
        }
    }
}
