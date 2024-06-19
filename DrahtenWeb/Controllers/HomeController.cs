using DrahtenWeb.Attributes;
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

        [CheckTermsAndConditions]
        public async Task<IActionResult> Index()
        {
            var response = new ResponseDto();
            var userSearchOptionsViewModel = new UserSearchOptionsViewModel(); //maybe change the name??

            //Get the user id.
            //Here the NameIdentifier claim type represents the user id.
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            response = await _topicArticleService.GetTopicsRelatedToUserAsync<ResponseDto>(userId, accessToken);

            userSearchOptionsViewModel.UserTopics = response.Map<List<UserTopicDto>>();

            response = await _topicArticleService.GetUserArticlesAsync<ResponseDto>(userId.ToString(), accessToken);

            userSearchOptionsViewModel.Articles = response.Map<List<ArticleDto>>();

            foreach(var article in userSearchOptionsViewModel.Articles)
            {
                if (userSearchOptionsViewModel.ArticleComments.ContainsKey(article.ArticleId) == false)
                {
                    userSearchOptionsViewModel.ArticleComments.Add(article.ArticleId, new List<ReadArticleCommentDto>());
                }

                article.TopicFullName = article.TopicFullName.SplitSnakeCase();

                response = await _topicArticleService.GetArticleCommentsAsync<ResponseDto>(article.ArticleId, accessToken);

                userSearchOptionsViewModel.ArticleComments[article.ArticleId] = response.Map<List<ReadArticleCommentDto>>();

                response = await _topicArticleService.GetUsersRelatedToArticleAsync<ResponseDto>(article.ArticleId, accessToken);

                userSearchOptionsViewModel.UsersRelatedToArticle[article.ArticleId] = response.Map<List<ReadUserArticleDto>>();
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
