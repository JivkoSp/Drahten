using DrahtenWeb.Dtos;
using DrahtenWeb.Dtos.PublicHistoryService;
using DrahtenWeb.Dtos.TopicArticleService;
using DrahtenWeb.Extensions;
using DrahtenWeb.Models;
using DrahtenWeb.Services.IServices;
using DrahtenWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace DrahtenWeb.Controllers
{
    [Authorize]
    public class PublicHistoryController : Controller
    {
        private readonly IPublicHistoryService _publicHistoryService;
        private readonly ITopicArticleService _topicArticleService;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;
        private const string ViewedArticlesCacheKey = "ViewedArticlesDataCache";
        private const string RetrievedViewedArticlesCacheKey = "RetrievedViewedArticlesDataCache";

        public PublicHistoryController(IPublicHistoryService publicHistoryService, ITopicArticleService topicArticleService, IMemoryCache cache)
        {
            _publicHistoryService = publicHistoryService;
            _topicArticleService = topicArticleService;
            _cache = cache;

            _cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };
        }

        [HttpGet]
        public async Task<IActionResult> ViewedArticles(int pageNumber = 1)
        {
            try
            {
                ArticleDto articleDto;
                List<ReadViewedArticleDto> allArticles;

                // The response type that will be returned from calling the services.
                var response = new ResponseDto();

                var historyArticleViewModel = new HistoryArticleViewModel();

                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                if (!_cache.TryGetValue(ViewedArticlesCacheKey, out allArticles))
                {
                    response = await _publicHistoryService.GetViewedArticlesAsync<ResponseDto>(userId, accessToken);

                    var articlesForCaching = new List<ViewedArticleViewModel>();

                    allArticles = response.Map<List<ReadViewedArticleDto>>();

                    foreach (var article in allArticles)
                    {
                        response = await _topicArticleService.GetArticleByIdAsync<ResponseDto>(article.ArticleId, accessToken);

                        articleDto = response.Map<ArticleDto>();

                        var viewedArticleViewModel = new ViewedArticleViewModel
                        {
                            ViewedArticleId = article.ViewedArticleId,
                            Article = articleDto,
                            UserId = article.UserId,
                            DateTime = article.DateTime
                        };

                        articlesForCaching.Add(viewedArticleViewModel);

                        // Save data in cache
                        _cache.Set(article.ArticleId, viewedArticleViewModel, _cacheEntryOptions);
                    }

                    // Save data in cache
                    _cache.Set(ViewedArticlesCacheKey, allArticles, _cacheEntryOptions);

                    //This is done in order to be able to have keyword searching for all articles.
                    _cache.Set(RetrievedViewedArticlesCacheKey, articlesForCaching, _cacheEntryOptions);
                }

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                int articlesCount = allArticles.Count;

                var pagination = new Pagination(articlesCount, pageNumber, pageSize);

                int skipArticles = (pageNumber - 1) * pageSize;

                var articles = allArticles.Skip(skipArticles).Take(pagination.PageSize).ToList();

                foreach (var article in articles)
                {
                    var viewedArticleViewModel = _cache.Get<ViewedArticleViewModel>(article.ArticleId);

                    historyArticleViewModel.Articles.Add(viewedArticleViewModel);
                }

                historyArticleViewModel.Pagination = pagination;

                return new JsonResult(historyArticleViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task CommentedArticle(string articleId, string userId, string articleComment, DateTimeOffset dateTime)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            
            var writeCommentedArticleDto = new WriteCommentedArticleDto
            { 
                ArticleId = Guid.Parse(articleId),
                UserId = Guid.Parse(userId),
                ArticleComment = articleComment,
                DateTime = dateTime
            };

            await _publicHistoryService.AddCommentedArticleAsync<ResponseDto>(writeCommentedArticleDto, accessToken);
        }

        [HttpPost]
        public async Task SearchedArticleData(string articleId, string userId, string searchedData, DateTimeOffset dateTime)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var writeSearchedArticleDataDto = new WriteSearchedArticleDataDto
            {
                ArticleId = Guid.Parse(articleId),
                UserId = Guid.Parse(userId),
                SearchedData = searchedData,
                DateTime = dateTime
            };

            await _publicHistoryService.AddSearchedArticleDataAsync<ResponseDto>(writeSearchedArticleDataDto, accessToken);
        }

        [HttpPost]
        public async Task ViewedArticle(string articleId, string userId, DateTimeOffset dateTime)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var writeViewedArticleDto = new WriteViewedArticleDto
            {
                ArticleId = Guid.Parse(articleId),
                UserId = Guid.Parse(userId),
                DateTime = dateTime
            };

            await _publicHistoryService.AddViewedArticleAsync<ResponseDto>(writeViewedArticleDto, accessToken);
        }
    }
}
