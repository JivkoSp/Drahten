﻿using DrahtenWeb.Dtos;
using DrahtenWeb.Dtos.PrivateHistoryService;
using DrahtenWeb.Dtos.TopicArticleService;
using DrahtenWeb.Extensions;
using DrahtenWeb.Models;
using DrahtenWeb.Services.IServices;
using DrahtenWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DrahtenWeb.Controllers
{
    [Authorize]
    public class PrivateHistoryController : Controller
    {
        private readonly IPrivateHistoryService _privateHistoryService;
        private readonly ITopicArticleService _topicArticleService;
        private readonly IUserService _userService;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;
        private const string ViewedArticlesCacheKey = "ViewedArticlesDataCache";
        private const string RetrievedViewedArticlesCacheKey = "RetrievedViewedArticlesDataCache";

        public PrivateHistoryController(IPrivateHistoryService privateHistoryService, ITopicArticleService topicArticleService,
            IUserService userService, IMemoryCache cache)
        {
            _privateHistoryService = privateHistoryService;
            _topicArticleService = topicArticleService;
            _userService = userService;
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
                List<ViewedArticleDto> allArticles;

                // The response type that will be returned from calling the services.
                var response = new ResponseDto();

                var historyArticleViewModel = new HistoryArticleViewModel();

                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                if (!_cache.TryGetValue(ViewedArticlesCacheKey, out allArticles))
                {
                    response = await _privateHistoryService.GetViewedArticlesAsync<ResponseDto>(userId, accessToken);

                    var articlesForCaching = new List<ViewedArticleViewModel>();

                    allArticles = response.Map<List<ViewedArticleDto>>();

                    foreach (var article in allArticles)
                    {
                        response = await _topicArticleService.GetArticleByIdAsync<ResponseDto>(article.ArticleId, accessToken);

                        articleDto = response.Map<ArticleDto>();

                        var viewedArticleViewModel = new ViewedArticleViewModel
                        {
                            ViewedArticleId = article.ViewedArticleId,
                            Article = articleDto,
                            UserId = article.UserId,
                            DateTime = article.DateTime,
                            RetentionUntil = article.RetentionUntil
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
        public IActionResult SearchedViewedArticles(string searchedViewedArticles, int pageNumber = 1)
        {
            var allViewedArticleViewModels = JsonConvert.DeserializeObject<List<ViewedArticleViewModel>>(searchedViewedArticles);

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            const int pageSize = 5;

            int articlesCount = allViewedArticleViewModels.Count;

            var pagination = new Pagination(articlesCount, pageNumber, pageSize);

            int skipArticles = (pageNumber - 1) * pageSize;

            var viewedArticleViewModels = allViewedArticleViewModels.Skip(skipArticles).Take(pagination.PageSize).ToList();

            var historyArticleViewModel = new HistoryArticleViewModel
            {
                Articles = viewedArticleViewModels,
                Pagination = pagination
            };

            return new JsonResult(historyArticleViewModel);
        }

        [HttpGet]
        public IActionResult SearchViewedArticles(string query)
        {
            try
            {
                if (string.IsNullOrEmpty(query))
                {
                    return Json(new List<string>());
                }

                var cachedArticles = _cache.Get<List<ViewedArticleViewModel>>(RetrievedViewedArticlesCacheKey);

                // Check if cachedArticles is null
                if (cachedArticles == null)
                {
                    // Handle the case where the data is not in cache
                    return Json(new List<ViewedArticleDto>());
                }

                cachedArticles.RemoveAll(x => x.Article.ArticleId == null);

                // Filter the cached data
                var result = cachedArticles
                    .Where(x => x.Article.Title.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewedUsers(int pageNumber = 1)
        {
            try
            {
                // The response type that will be returned from calling the services.
                var response = new ResponseDto();

                var historyUserViewModel = new HistoryUserViewModel();

                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                response = await _privateHistoryService.GetViewedUsersAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                var allViewedUsers = response.Map<List<ViewedUserDto>>();

                int viewedUsersCount = allViewedUsers.Count;

                var pagination = new Pagination(viewedUsersCount, pageNumber, pageSize);

                int skipUsers = (pageNumber - 1) * pageSize;

                var viewedUsers = allViewedUsers.Skip(skipUsers).Take(pagination.PageSize).ToList();

                foreach (var viewedUser in viewedUsers)
                {
                    response = await _userService.GetUserByIdAsync<ResponseDto>(Guid.Parse(viewedUser.ViewedUserId), accessToken);

                    var viewedUserViewModel = new ViewedUserViewModel
                    {
                        ViewedUserReadModelId = viewedUser.ViewedUserReadModelId,
                        ViewedUser = response.Map<DrahtenWeb.Dtos.UserService.UserDto>(),
                        DateTime = viewedUser.DateTime,
                        RetentionUntil = viewedUser.RetentionUntil
                    };

                    historyUserViewModel.ViewedUsers.Add(viewedUserViewModel);
                }

                historyUserViewModel.Pagination = pagination;

                return new JsonResult(historyUserViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchedArticleData(int pageNumber = 1)
        {
            try
            {
                // The response type that will be returned from calling the services.
                var response = new ResponseDto();

                var historySeachedArticleDataViewModel = new HistorySeachedArticleDataViewModel();

                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                response = await _privateHistoryService.GetSearchedArticlesAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                var allSearchedArticles = response.Map<List<SearchedArticleDataDto>>();

                int searchedArticlesCount = allSearchedArticles.Count;

                var pagination = new Pagination(searchedArticlesCount, pageNumber, pageSize);

                int skipSearchedArticles = (pageNumber - 1) * pageSize;

                var searchedArticles = allSearchedArticles.Skip(skipSearchedArticles).Take(pagination.PageSize).ToList();

                foreach(var searchedArticle in searchedArticles)
                {
                    response = await _topicArticleService.GetArticleByIdAsync<ResponseDto>(searchedArticle.ArticleId, accessToken);

                    var searchedArticleDataViewModel = new SearchedArticleDataViewModel
                    {
                        SearchedArticleDataId = searchedArticle.SearchedArticleDataId,
                        Article = response.Map<ArticleDto>(),
                        UserId = searchedArticle.UserId,
                        SearchedData = searchedArticle.SearchedData,
                        DateTime = searchedArticle.DateTime,
                        RetentionUntil = searchedArticle.RetentionUntil
                    };

                    historySeachedArticleDataViewModel.SearchedArticles.Add(searchedArticleDataViewModel);
                }

                historySeachedArticleDataViewModel.Pagination = pagination;

                return new JsonResult(historySeachedArticleDataViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> LikedArticles(int pageNumber = 1)
        {
            try
            {
                // The response type that will be returned from calling the services.
                var response = new ResponseDto();

                var historyLikedArticleViewModel = new HistoryLikedArticleViewModel();

                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                response = await _privateHistoryService.GetLikedArticlesAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;
                
                var allLikedArticles = response.Map<List<LikedArticleDto>>();

                int likedArticlesCount = allLikedArticles.Count;

                var pagination = new Pagination(likedArticlesCount, pageNumber, pageSize);

                int skipLikedArticles = (pageNumber - 1) * pageSize;

                var likedArticles = allLikedArticles.Skip(skipLikedArticles).Take(pagination.PageSize).ToList();

                foreach (var likedArticle in likedArticles)
                {
                    response = await _topicArticleService.GetArticleByIdAsync<ResponseDto>(likedArticle.ArticleId, accessToken);

                    var likedArticleViewModel = new LikedArticleViewModel
                    {
                        Article = response.Map<ArticleDto>(),
                        UserId = likedArticle.UserId,
                        DateTime = likedArticle.DateTime,
                        RetentionUntil = likedArticle.RetentionUntil
                    };

                    historyLikedArticleViewModel.LikedArticles.Add(likedArticleViewModel);
                }

                historyLikedArticleViewModel.Pagination = pagination;

                return new JsonResult(historyLikedArticleViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DislikedArticles(int pageNumber = 1)
        {
            try
            {
                // The response type that will be returned from calling the services.
                var response = new ResponseDto();

                var historyDislikedArticleViewModel = new HistoryDislikedArticleViewModel();

                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                response = await _privateHistoryService.GetDislikedArticlesAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                var allDislikedArticles = response.Map<List<DislikedArticleDto>>();

                int dislikedArticlesCount = allDislikedArticles.Count;

                var pagination = new Pagination(dislikedArticlesCount, pageNumber, pageSize);

                int skipDislikedArticles = (pageNumber - 1) * pageSize;

                var dislikedArticles = allDislikedArticles.Skip(skipDislikedArticles).Take(pagination.PageSize).ToList();

                foreach (var dislikedArticle in dislikedArticles)
                {
                    response = await _topicArticleService.GetArticleByIdAsync<ResponseDto>(dislikedArticle.ArticleId, accessToken);

                    var dislikedArticleViewModel = new DislikedArticleViewModel
                    {
                        Article = response.Map<ArticleDto>(),
                        UserId = dislikedArticle.UserId,
                        DateTime = dislikedArticle.DateTime,
                        RetentionUntil = dislikedArticle.RetentionUntil
                    };

                    historyDislikedArticleViewModel.DislikedArticles.Add(dislikedArticleViewModel);
                }

                historyDislikedArticleViewModel.Pagination = pagination;

                return new JsonResult(historyDislikedArticleViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CommentedArticles(int pageNumber = 1)
        {
            try
            {
                // The response type that will be returned from calling the services.
                var response = new ResponseDto();

                var historyCommentedArticleViewModel = new HistoryCommentedArticleViewModel();

                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                response = await _privateHistoryService.GetCommentedArticlesAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                var allCommentedArticles = response.Map<List<CommentedArticleDto>>();

                int commentedArticlesCount = allCommentedArticles.Count;

                var pagination = new Pagination(commentedArticlesCount, pageNumber, pageSize);

                int skipCommentedArticles = (pageNumber - 1) * pageSize;

                var commentedArticles = allCommentedArticles.Skip(skipCommentedArticles).Take(pagination.PageSize).ToList();

                foreach(var commentedArticle in commentedArticles)
                {
                    response = await _topicArticleService.GetArticleByIdAsync<ResponseDto>(commentedArticle.ArticleId, accessToken);

                    var commentedArticleViewModel = new CommentedArticleViewModel
                    {
                        CommentedArticleId = commentedArticle.CommentedArticleId,
                        Article = response.Map<ArticleDto>(),
                        UserId = commentedArticle.UserId,
                        ArticleComment = commentedArticle.ArticleComment,
                        DateTime = commentedArticle.DateTime,
                        RetentionUntil = commentedArticle.RetentionUntil
                    };

                    historyCommentedArticleViewModel.CommentedArticles.Add(commentedArticleViewModel);
                }

                historyCommentedArticleViewModel.Pagination = pagination;

                return new JsonResult(historyCommentedArticleViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> LikedArticleComments(int pageNumber = 1)
        {
            try
            {
                // The response type that will be returned from calling the services.
                var response = new ResponseDto();

                var historyLikedArticleCommentViewModel = new HistoryLikedArticleCommentViewModel();

                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                response = await _privateHistoryService.GetLikedArticleCommentsAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                var allLikedArticleComments = response.Map<List<LikedArticleCommentDto>>();

                int likedArticleCommentsCount = allLikedArticleComments.Count;

                var pagination = new Pagination(likedArticleCommentsCount, pageNumber, pageSize);

                int skipLikedArticleComments = (pageNumber - 1) * pageSize;

                var likedArticleComments = allLikedArticleComments.Skip(skipLikedArticleComments).Take(pagination.PageSize).ToList();

                foreach (var likedArticleComment in likedArticleComments)
                {
                    response = await _topicArticleService.GetArticleByIdAsync<ResponseDto>(likedArticleComment.ArticleId, accessToken);

                    var likedArticleCommentViewModel = new LikedArticleCommentViewModel
                    {
                        ArticleCommentId = likedArticleComment.ArticleCommentId,
                        Article = response.Map<ArticleDto>(),
                        UserId = likedArticleComment.UserId,
                        DateTime = likedArticleComment.DateTime,
                        RetentionUntil = likedArticleComment.RetentionUntil
                    };

                    historyLikedArticleCommentViewModel.LikedArticleComments.Add(likedArticleCommentViewModel);
                }

                historyLikedArticleCommentViewModel.Pagination = pagination;

                return new JsonResult(historyLikedArticleCommentViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DislikedArticleComments(int pageNumber = 1)
        {
            try
            {
                // The response type that will be returned from calling the services.
                var response = new ResponseDto();

                var historyDislikedArticleCommentViewModel = new HistoryDislikedArticleCommentViewModel();

                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                response = await _privateHistoryService.GetDislikedArticleCommentsAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                var allDislikedArticleComments = response.Map<List<DislikedArticleCommentDto>>();

                int dislikedArticleCommentsCount = allDislikedArticleComments.Count;

                var pagination = new Pagination(dislikedArticleCommentsCount, pageNumber, pageSize);

                int skipDislikedArticleComments = (pageNumber - 1) * pageSize;

                var dislikedArticleComments = allDislikedArticleComments.Skip(skipDislikedArticleComments).Take(pagination.PageSize).ToList();

                foreach (var dislikedArticleComment in dislikedArticleComments)
                {
                    response = await _topicArticleService.GetArticleByIdAsync<ResponseDto>(dislikedArticleComment.ArticleId, accessToken);

                    var dislikedArticleCommentViewModel = new DislikedArticleCommentViewModel
                    {
                        ArticleCommentId = dislikedArticleComment.ArticleCommentId,
                        Article = response.Map<ArticleDto>(),
                        UserId = dislikedArticleComment.UserId,
                        DateTime = dislikedArticleComment.DateTime,
                        RetentionUntil = dislikedArticleComment.RetentionUntil
                    };

                    historyDislikedArticleCommentViewModel.DislikedArticleComments.Add(dislikedArticleCommentViewModel);
                }

                historyDislikedArticleCommentViewModel.Pagination = pagination;

                return new JsonResult(historyDislikedArticleCommentViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchedTopicData(int pageNumber = 1)
        {
            try
            {
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _privateHistoryService.GetSearchedTopicsAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                var allSearchedTopics = response.Map<List<SearchedTopicDataDto>>();

                var tempList = new List<SearchedTopicDataDto>
                {
                    new SearchedTopicDataDto
                    {
                        SearchedTopicDataId = Guid.NewGuid(),
                        TopicId = Guid.NewGuid(),
                        UserId = "111",
                        SearchedData = "...",
                        DateTime = DateTimeOffset.Now
                    }
                };

                int searchedTopicsCount = tempList.Count;

                var pagination = new Pagination(searchedTopicsCount, pageNumber, pageSize);

                int skipSearchedTopics = (pageNumber - 1) * pageSize;

                var searchedTopics = tempList.Skip(skipSearchedTopics).Take(pagination.PageSize).ToList();

                var historySearchedTopicDataViewModel = new HistorySearchedTopicDataViewModel
                {
                    SearchedTopics = searchedTopics,
                    Pagination = pagination
                };

                return new JsonResult(historySearchedTopicDataViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> TopicSubscriptions(int pageNumber = 1)
        {
            try
            {
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _privateHistoryService.GetTopicSubscriptionsAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                var allTopicSubscriptions = response.Map<List<TopicSubscriptionDto>>();

                var tempList = new List<TopicSubscriptionDto>
                {
                    new TopicSubscriptionDto
                    {
                        TopicId = Guid.NewGuid(),
                        UserId = "111",
                        DateTime = DateTimeOffset.Now
                    }
                };

                int topicSubscriptionsCount = tempList.Count;

                var pagination = new Pagination(topicSubscriptionsCount, pageNumber, pageSize);

                int skipTopicSubscriptions = (pageNumber - 1) * pageSize;

                var topicSubscriptions = tempList.Skip(skipTopicSubscriptions).Take(pagination.PageSize).ToList();

                var historyTopicSubscriptionsViewModel = new HistoryTopicSubscriptionsViewModel
                {
                    TopicSubscriptions = topicSubscriptions,
                    Pagination = pagination
                };

                return new JsonResult(historyTopicSubscriptionsViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task SetRetentionPeriod(int retentionDays)
        {
            try
            {
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var userRetentionUntilDto = new UserRetentionUntilDto
                {
                    UserId = userId,
                    DateTime = DateTime.Now.AddDays(retentionDays)
                };

                await _privateHistoryService.SetUserRetentionDateTimeAsync<string>(userRetentionUntilDto, accessToken);
            }
            catch (Exception ex)
            {
                //TODO: Log the exception.
                Console.WriteLine(ex.Message);
            }
        }

        [HttpDelete]
        public async Task RemoveViewedArticle(Guid viewedArticleId)
        {
            //Get the user id.
            //Here the NameIdentifier claim type represents the user id.
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            await _privateHistoryService.RemoveViewedArticleAsync<string>(userId, viewedArticleId, accessToken);
        }

        [HttpDelete]
        public async Task RemoveSearchedArticleData(Guid searchedArticleDataId)
        {
            //Get the user id.
            //Here the NameIdentifier claim type represents the user id.
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            await _privateHistoryService.RemoveSearchedArticleDataAsync<string>(userId, searchedArticleDataId, accessToken);
        }

        [HttpDelete]
        public async Task RemoveCommentedArticle(Guid commentedArticleId)
        {
            //Get the user id.
            //Here the NameIdentifier claim type represents the user id.
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            await _privateHistoryService.RemoveCommentedArticleAsync<string>(userId, commentedArticleId, accessToken);
        }
    }
}
