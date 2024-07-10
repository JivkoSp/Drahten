using DrahtenWeb.Dtos;
using DrahtenWeb.Dtos.PublicHistoryService;
using DrahtenWeb.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrahtenWeb.Controllers
{
    [Authorize]
    public class PublicHistoryController : Controller
    {
        private readonly IPublicHistoryService _publicHistoryService;

        public PublicHistoryController(IPublicHistoryService publicHistoryService)
        {
            _publicHistoryService = publicHistoryService;
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
    }
}
