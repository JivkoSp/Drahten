using DrahtenWeb.Dtos.TopicArticleService;
using DrahtenWeb.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    }
}
