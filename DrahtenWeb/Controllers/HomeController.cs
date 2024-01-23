using DrahtenWeb.Models;
using DrahtenWeb.Services;
using DrahtenWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DrahtenWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Testing the api gateway with user service endpoint.
        //TODO: Remove this endpoint after testing.
        public async Task SendGet()
        {
            var response = await _userService.GetEndpointAsync<string>("");

            if(response != null)
            {
                Console.WriteLine($"Response from USER SERVICE: {response}");
            }
            else
            {
                Console.WriteLine($"Response from USER SERVICE: Null");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
