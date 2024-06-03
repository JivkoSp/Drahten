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
    }
}
