using DrahtenWeb.Dtos;
using DrahtenWeb.Models;
using DrahtenWeb.Services;
using DrahtenWeb.Services.IServices;
using DrahtenWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;

namespace DrahtenWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var userSearchOptionsViewModel = new UserSearchOptionsViewModel();

            //Get the user id.
            //Here the NameIdentifier claim type represents the user id.
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _userService.GetUserTopics<ResponseDto>(userId ?? "", accessToken ?? "");

            if(response != null && response.IsSuccess)
            {
                userSearchOptionsViewModel.UserTopics = JsonConvert.DeserializeObject<List<ReadUserTopicDto>>(Convert.ToString(response.Result));
            }

            foreach(var userTopic in userSearchOptionsViewModel.UserTopics ?? new List<ReadUserTopicDto>())
            {
                response = await _userService.GetRootTopicWithChildren<ResponseDto>(userTopic.TopicId, accessToken ?? "");

                if(response != null && response.IsSuccess)
                {
                    userSearchOptionsViewModel.Topics.Add(JsonConvert.DeserializeObject<ReadTopicDto>(Convert.ToString(response.Result)));
                }
            }

            return View(userSearchOptionsViewModel);
        }

        public IActionResult Logout()
        {
            return new SignOutResult(
                new[]{
                        OpenIdConnectDefaults.AuthenticationScheme,
                        CookieAuthenticationDefaults.AuthenticationScheme
                }
            );
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
