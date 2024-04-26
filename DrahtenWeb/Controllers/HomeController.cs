using DrahtenWeb.Dtos;
using DrahtenWeb.Dtos.TopicArticleService;
using DrahtenWeb.Extensions;
using DrahtenWeb.Models;
using DrahtenWeb.Services.IServices;
using DrahtenWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;

namespace DrahtenWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITopicArticleService _topicArticleService;

        public HomeController(ILogger<HomeController> logger, ITopicArticleService topicArticleService)
        {
            _logger = logger;
            _topicArticleService = topicArticleService;
        }

        public async Task<IActionResult> Index()
        {
            var userSearchOptionsViewModel = new UserSearchOptionsViewModel();

            //Get the user id.
            //Here the NameIdentifier claim type represents the user id.
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _topicArticleService.GetTopicsRelatedToUserAsync<ResponseDto>(userId, accessToken);

            userSearchOptionsViewModel.UserTopics = response.Map<List<UserTopicDto>>();

            foreach (var userTopic in userSearchOptionsViewModel.UserTopics)
            {
               response = await _topicArticleService.GetParentTopicWithChildrenAsync<ResponseDto>(userTopic.TopicId, accessToken);

                if(response != null && response.IsSuccess)
                {
                    var topicDto = JsonConvert.DeserializeObject<TopicDto>(Convert.ToString(response.Result));

                    userSearchOptionsViewModel.Topics.Add(topicDto);
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
