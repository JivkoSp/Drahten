using AutoMapper;
using DrahtenWeb.Dtos;
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
    public class ArticleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISearchService _searchService;
        private readonly ITopicArticleService _topicArticleService;
        private readonly IUserService _userService;

        public ArticleController(IMapper mapper, ISearchService searchService, ITopicArticleService topicArticleService, IUserService userService)
        {
            _mapper = mapper;
            _searchService = searchService;
            _topicArticleService = topicArticleService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> ViewArticle(ArticleDto articleDto, string articleComments, string usersRelatedToArticle)
        {
            var articleViewModel = new ArticleViewModel
            {
                Article = articleDto,
                UserArticles = JsonConvert.DeserializeObject<List<ReadUserArticleDto>>(usersRelatedToArticle),
                ArticleComments = JsonConvert.DeserializeObject<List<ReadArticleCommentDto>>(articleComments)
            };

            try
            {
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                foreach (var articleComment in articleViewModel.ArticleComments)
                {
                    var response = await _userService.GetUserByIdAsync<ResponseDto>(Guid.Parse(articleComment.UserId), accessToken);

                    if (response != null && response.IsSuccess)
                    {
                        var userDto = JsonConvert.DeserializeObject<Dtos.UserService.UserDto>(Convert.ToString(response.Result));

                        articleViewModel.Users.Add(userDto);
                    }
                }

                var writeUserArticleDto = new WriteUserArticleDto
                {
                    UserId = userId,
                    ArticleId = Guid.Parse(articleDto.ArticleId),
                };

                await _topicArticleService.RegisterUserArticleAsync<string>(writeUserArticleDto, accessToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(articleViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ArticleSummary(string articleId)
        {
            try
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _searchService.GetDocumentSummarizationNewsCybersecurityEurope<ResponseDto>(articleId, accessToken);

                var documentSummaryDto = response.Map<DocumentSummaryDto>();

                return new JsonResult(documentSummaryDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ArticleQuestions(string articleId)
        {
            try
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _searchService.GetDocumentQuestionsNewsCybersecurityEurope<ResponseDto>(articleId, accessToken);

                var documentQuestions = response.Map<List<string>>();

                return new JsonResult(documentQuestions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ArticleSemanticSearch(string articleId, string question)
        {
            try
            {
                var documentQuestionDto = new DocumentQuestionDto
                { 
                    document_id = articleId,
                    query = question
                };

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _searchService.SemanticSearchDocumentNewsCybersecurityEurope<ResponseDto>(documentQuestionDto, accessToken);

                var answerDto = response.Map<NLPQueryAnswerDto>();

                return new JsonResult(answerDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ArticleLike(string articleId)
        {
            try
            {
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var articleLikeDto = new ArticleLikeDto
                {
                    ArticleId = Guid.Parse(articleId),
                    DateTime = DateTimeOffset.Now,
                    UserId = userId
                };

                await _topicArticleService.RegisterArticleLikeAsync<string>(articleLikeDto, accessToken);

                return new JsonResult(new { });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ArticleComment(string articleId, string comment)
        {
            try
            {
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var writeArticleCommentDto = new WriteArticleCommentDto
                {
                    ArticleId = Guid.Parse(articleId),
                    ArticleCommentId = Guid.NewGuid(),
                    CommentValue = comment,
                    DateTime = DateTimeOffset.Now,
                    UserId = userId
                };

                await _topicArticleService.RegisterArticleCommentAsync<string>(writeArticleCommentDto, accessToken);

                return new JsonResult(new { });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ArticleChildComment(string articleId, Guid parentArticleCommentId, string comment)
        {
            try
            {
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var writeArticleCommentDto = new WriteArticleCommentDto
                {
                    ArticleId = Guid.Parse(articleId),
                    ArticleCommentId = Guid.NewGuid(),
                    CommentValue = comment,
                    DateTime = DateTimeOffset.Now,
                    UserId = userId,
                    ParentArticleCommentId = parentArticleCommentId
                };

                await _topicArticleService.RegisterArticleCommentAsync<string>(writeArticleCommentDto, accessToken);

                return new JsonResult(new { });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ArticleCommentLike(Guid articleCommentId)
        {
            try
            {
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var articleCommentLikeDto = new ArticleCommentLikeDto
                {
                    ArticleCommentId = articleCommentId,
                    DateTime = DateTimeOffset.Now,
                    UserId = userId
                };

                await _topicArticleService.RegisterArticleCommentLikeAsync<string>(articleCommentLikeDto, accessToken);

                return new JsonResult(new { });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ArticleCommentDislike(Guid articleCommentId)
        {
            try
            {
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var articleCommentDislikeDto = new ArticleCommentDislikeDto
                {
                    ArticleCommentId = articleCommentId,
                    DateTime = DateTimeOffset.Now,
                    UserId = userId
                };

                await _topicArticleService.RegisterArticleCommentDislikeAsync<string>(articleCommentDislikeDto, accessToken);

                return new JsonResult(new { });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
