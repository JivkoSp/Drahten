using DrahtenWeb.Dtos;
using DrahtenWeb.Dtos.PrivateHistoryService;
using DrahtenWeb.Dtos.TopicArticleService;
using DrahtenWeb.Extensions;
using DrahtenWeb.Services.IServices;
using DrahtenWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace DrahtenWeb.Controllers
{
    [Authorize]
    public class PrivateHistoryArticleController : Controller
    {
        private readonly IPrivateHistoryService _privateHistoryService;
        private readonly ITopicArticleService _topicArticleService;

        public PrivateHistoryArticleController(IPrivateHistoryService privateHistoryService, ITopicArticleService topicArticleService)
        {
            _privateHistoryService = privateHistoryService;
            _topicArticleService = topicArticleService;
        }

        [HttpPost]
        public IActionResult ViewArticle(string articleJson)
        {
            var article = JsonConvert.DeserializeObject<ArticleDto>(articleJson);

            return View(article);
        }

        [HttpGet]
        public async Task<IActionResult> SearchedArticleData(string articleId)
        {
            try
            {
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _privateHistoryService.GetSearchedArticlesAsync<ResponseDto>(userId, accessToken);

                var allSearchedArticles = response.Map<List<SearchedArticleDataDto>>();

                var searchedArticles = allSearchedArticles.Where(x => x.ArticleId == articleId).ToList();

                return new JsonResult(searchedArticles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
