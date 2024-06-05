using DrahtenWeb.Dtos;
using DrahtenWeb.Dtos.PrivateHistoryService;
using DrahtenWeb.Extensions;
using DrahtenWeb.Models;
using DrahtenWeb.Services.IServices;
using DrahtenWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DrahtenWeb.Controllers
{
    [Authorize]
    public class PrivateHistoryController : Controller
    {
        private readonly IPrivateHistoryService _privateHistoryService;

        public PrivateHistoryController(IPrivateHistoryService privateHistoryService)
        {
            _privateHistoryService = privateHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> ViewedArticles(int pageNumber = 1)
        {
            try
            {
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _privateHistoryService.GetViewedArticlesAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                var allArticles = response.Map<List<ViewedArticleDto>>();

                var tempList = new List<ViewedArticleDto>
                {
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    }
                };

                int articlesCount = tempList.Count;

                var pagination = new Pagination(articlesCount, pageNumber, pageSize);

                int skipArticles = (pageNumber - 1) * pageSize;

                var articles = tempList.Skip(skipArticles).Take(pagination.PageSize).ToList();

                var historyArticleViewModel = new HistoryArticleViewModel
                {
                    Articles = articles,
                    Pagination = pagination
                };


                //int articlesCount = allArticles.Count;

                //var pagination = new Pagination(articlesCount, pageNumber, pageSize);

                //int skipArticles = (pageNumber - 1) * pageSize;

                //var articles = allArticles.Skip(skipArticles).Take(pagination.PageSize).ToList();

                //var historyArticleViewModel = new HistoryArticleViewModel
                //{
                //    Articles = articles,
                //    Pagination = pagination
                //};

                return new JsonResult(historyArticleViewModel);
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
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _privateHistoryService.GetViewedUsersAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                var allViewedUsers = response.Map<List<ViewedUserDto>>();

                var tempList = new List<ViewedUserDto>
                {
                   new ViewedUserDto
                   {
                       ViewedUserReadModelId = Guid.NewGuid(),
                       ViewerUserId = "111",
                       ViewedUserId = "122",
                       DateTime = DateTimeOffset.Now
                   },
                   new ViewedUserDto
                   {
                       ViewedUserReadModelId = Guid.NewGuid(),
                       ViewerUserId = "121",
                       ViewedUserId = "123",
                       DateTime = DateTimeOffset.Now
                   }
                };

                int viewedUsersCount = tempList.Count;

                var pagination = new Pagination(viewedUsersCount, pageNumber, pageSize);

                int skipUsers = (pageNumber - 1) * pageSize;

                var viewedUsers = tempList.Skip(skipUsers).Take(pagination.PageSize).ToList();

                var historyUserViewModel = new HistoryUserViewModel
                {
                    ViewedUsers = viewedUsers,
                    Pagination = pagination,
                };

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
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _privateHistoryService.GetSearchedArticlesAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                var allSearchedArticles = response.Map<List<SearchedArticleDataDto>>();

                var tempList = new List<SearchedArticleDataDto>
                {
                   new SearchedArticleDataDto
                   {
                       SearchedArticleDataId = Guid.NewGuid(),
                       ArticleId = "122",
                       UserId = "111",
                       SearchedData = "...",
                       DateTime = DateTimeOffset.Now
                   },
                   new SearchedArticleDataDto
                   {
                       SearchedArticleDataId = Guid.NewGuid(),
                       ArticleId = "122",
                       UserId = "111",
                       SearchedData = "...",
                       DateTime = DateTimeOffset.Now
                   },
                   new SearchedArticleDataDto
                   {
                       SearchedArticleDataId = Guid.NewGuid(),
                       ArticleId = "122",
                       UserId = "111",
                       SearchedData = "...",
                       DateTime = DateTimeOffset.Now
                   }
                };

                int searchedArticlesCount = tempList.Count;

                var pagination = new Pagination(searchedArticlesCount, pageNumber, pageSize);

                int skipSearchedArticles = (pageNumber - 1) * pageSize;

                var searchedArticles = tempList.Skip(skipSearchedArticles).Take(pagination.PageSize).ToList();

                var historySeachedArticleDataViewModel = new HistorySeachedArticleDataViewModel
                {
                    SearchedArticles = searchedArticles,
                    Pagination = pagination
                };

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
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _privateHistoryService.GetLikedArticlesAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;
                
                var allLikedArticles = response.Map<List<LikedArticleDto>>();

                var tempList = new List<LikedArticleDto>
                {
                    new LikedArticleDto
                    {
                        ArticleId = "123",
                        UserId = "121",
                        DateTime = DateTimeOffset.Now
                    },
                    new LikedArticleDto
                    {
                        ArticleId = "123",
                        UserId = "121",
                        DateTime = DateTimeOffset.Now
                    }
                };

                int likedArticlesCount = tempList.Count;

                var pagination = new Pagination(likedArticlesCount, pageNumber, pageSize);

                int skipLikedArticles = (pageNumber - 1) * pageSize;

                var likedArticles = tempList.Skip(skipLikedArticles).Take(pagination.PageSize).ToList();

                var historyLikedArticleViewModel = new HistoryLikedArticleViewModel
                {
                    LikedArticles = likedArticles,
                    Pagination = pagination
                };

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
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _privateHistoryService.GetDislikedArticlesAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                var allDislikedArticles = response.Map<List<DislikedArticleDto>>();

                var tempList = new List<DislikedArticleDto>
                {
                    new DislikedArticleDto
                    {
                        ArticleId = "123",
                        UserId = "121",
                        DateTime = DateTimeOffset.Now
                    },
                    new DislikedArticleDto
                    {
                        ArticleId = "123",
                        UserId = "121",
                        DateTime = DateTimeOffset.Now
                    }
                };

                int dislikedArticlesCount = tempList.Count;

                var pagination = new Pagination(dislikedArticlesCount, pageNumber, pageSize);

                int skipDislikedArticles = (pageNumber - 1) * pageSize;

                var dislikedArticles = tempList.Skip(skipDislikedArticles).Take(pagination.PageSize).ToList();

                var historyDislikedArticleViewModel = new HistoryDislikedArticleViewModel
                {
                    DislikedArticles = dislikedArticles,
                    Pagination = pagination,
                };

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
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _privateHistoryService.GetCommentedArticlesAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                var allCommentedArticles = response.Map<List<CommentedArticleDto>>();

                var tempList = new List<CommentedArticleDto>
                {
                    new CommentedArticleDto
                    {
                        CommentedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "111",
                        ArticleComment = "...",
                        DateTime = DateTimeOffset.Now
                    }
                };

                int commentedArticlesCount = tempList.Count;

                var pagination = new Pagination(commentedArticlesCount, pageNumber, pageSize);

                int skipCommentedArticles = (pageNumber - 1) * pageSize;

                var commentedArticles = tempList.Skip(skipCommentedArticles).Take(pagination.PageSize).ToList();

                var historyCommentedArticleViewModel = new HistoryCommentedArticleViewModel
                {
                    CommentedArticles = commentedArticles,
                    Pagination = pagination
                };

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
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _privateHistoryService.GetLikedArticleCommentsAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                var allLikedArticleComments = response.Map<List<LikedArticleCommentDto>>();

                var tempList = new List<LikedArticleCommentDto>
                {
                    new LikedArticleCommentDto
                    {
                        ArticleCommentId = Guid.NewGuid(),
                        ArticleId = "121",
                        UserId = "111",
                        DateTime = DateTimeOffset.Now
                    }
                };

                int likedArticleCommentsCount = tempList.Count;

                var pagination = new Pagination(likedArticleCommentsCount, pageNumber, pageSize);

                int skipLikedArticleComments = (pageNumber - 1) * pageSize;

                var likedArticleComments = tempList.Skip(skipLikedArticleComments).Take(pagination.PageSize).ToList();

                var historyLikedArticleCommentViewModel = new HistoryLikedArticleCommentViewModel
                {
                    LikedArticleComments = likedArticleComments,
                    Pagination = pagination
                };

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
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _privateHistoryService.GetDislikedArticleCommentsAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                var allDislikedArticleComments = response.Map<List<DislikedArticleCommentDto>>();

                var tempList = new List<DislikedArticleCommentDto>
                {
                    new DislikedArticleCommentDto
                    {
                        ArticleCommentId = Guid.NewGuid(),
                        ArticleId = "112",
                        UserId = "111",
                        DateTime = DateTimeOffset.Now
                    }
                };

                int dislikedArticleCommentsCount = tempList.Count;

                var pagination = new Pagination(dislikedArticleCommentsCount, pageNumber, pageSize);

                int skipDislikedArticleComments = (pageNumber - 1) * pageSize;

                var dislikedArticleComments = tempList.Skip(skipDislikedArticleComments).Take(pagination.PageSize).ToList();

                var historyDislikedArticleCommentViewModel = new HistoryDislikedArticleCommentViewModel
                {
                    DislikedArticleComments = dislikedArticleComments,
                    Pagination = pagination
                };

                return new JsonResult(historyDislikedArticleCommentViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
