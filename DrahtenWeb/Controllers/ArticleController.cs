using DrahtenWeb.Dtos;
using DrahtenWeb.Services;
using DrahtenWeb.Services.IServices;
using DrahtenWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Xml.Linq;

namespace DrahtenWeb.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IUserService _userService;

        public ArticleController(ISearchService searchService, IUserService userService)
        {
            _searchService = searchService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> ArticleInfo(string articleId)
        {
            try
            {
                var response = new ResponseDto();
                var articleLikes = new List<ReadArticleLikeDto>();
                var articleComments = new List<ReadArticleCommentDto>();
                var userArticleList = new List<ReadUserArticleDto>();

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                response = await _userService.GetArticleComments<ResponseDto>(articleId, accessToken ?? "");

                articleComments = JsonConvert.DeserializeObject<List<ReadArticleCommentDto>>(Convert.ToString(response.Result));

                response = await _userService.GetUsersRelatedToArticle<ResponseDto>(articleId, accessToken ?? "");

                userArticleList = JsonConvert.DeserializeObject<List<ReadUserArticleDto>>(Convert.ToString(response.Result));

                response = await _userService.GetArticleLikes<ResponseDto>(articleId, accessToken ?? "");

                articleLikes = JsonConvert.DeserializeObject<List<ReadArticleLikeDto>>(Convert.ToString(response.Result));

                var articleInfoViewModel = new ArticleInfoViewModel
                {
                    Comments = articleComments?.Count == null ? 0 : articleComments.Count,
                    Views = userArticleList?.Count == null ? 0 : userArticleList.Count,
                    Likes = articleLikes?.Count == null ? 0 : articleLikes.Count,
                    DisLikes = 0
                };

                return new JsonResult(articleInfoViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ViewArticle(int document_topic_id, string document_id, DocumentDto document)
        {
            var response = new ResponseDto();
            var articleLikes = new List<ReadArticleLikeDto>();
            var articleComments = new List<ReadArticleCommentDto>();
            var userArticleList = new List<ReadUserArticleDto>();

            //Get the user id.
            //Here the NameIdentifier claim type represents the user id.
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            //Find article with id: document_id from UserService. 
            response = await _userService.GetArticle<ResponseDto>(document_id, accessToken ?? "");

            //Check if article with id: document_id exists.
            if(response != null && response.IsSuccess == false) 
            {
                //Article with id: document_id does NOT exist.

                //Create dto for writing article to the UserService database. 
                var writeArticleDto = new WriteArticleDto
                {
                    ArticleId = document_id,
                    PrevTitle = document.article_prev_title ?? "",
                    Title = document.article_title ?? "",
                    Data = document.article_data ?? "",
                    Date = document.article_published_date ?? "",
                    Author = document.article_author ?? "",
                    Link = document.article_link ?? "",
                    TopicId = document_topic_id
                };

                //Write the article with id: document_id to the UserService database. 
                response = await _userService.RegisterArticle<ResponseDto>(writeArticleDto, accessToken ?? "");

                if (response == null || response.IsSuccess == false)
                {
                    //TODO:
                    //throw custom exception
                }
            }

            response = await _userService.GetUsersRelatedToArticle<ResponseDto>(document_id, accessToken ?? "");

            userArticleList = JsonConvert.DeserializeObject<List<ReadUserArticleDto>>(Convert.ToString(response.Result));

            var userArticle = userArticleList?.FirstOrDefault(x => x.UserDto?.UserId == userId);

            if(userArticle == null)
            {
                var writeUserArticleDto = new WriteUserArticleDto
                {
                    UserId = userId ?? "",
                    ArticleId = document_id,
                };

                response = await _userService.RegisterUserArticle<ResponseDto>(writeUserArticleDto, accessToken ?? "");

                if (response == null || response.IsSuccess == false)
                {
                    //TODO:
                    //throw custom exception
                }
            }

            //Get all likes for article with id: document_id.
            response = await _userService.GetArticleLikes<ResponseDto>(document_id, accessToken ?? "");

            if (response == null || response.IsSuccess == false)
            {
                //TODO
                //throw custom exception
            }

            articleLikes = JsonConvert.DeserializeObject<List<ReadArticleLikeDto>>(Convert.ToString(response.Result));

            //Get all comments for article with id: document_id.
            response = await _userService.GetArticleComments<ResponseDto>(document_id, accessToken ?? "");

            if (response == null || response.IsSuccess == false)
            {
                //TODO
                //throw custom exception
            }

            articleComments = JsonConvert.DeserializeObject<List<ReadArticleCommentDto>>(Convert.ToString(response.Result));

            var articleViewModel = new ArticleViewModel
            { 
                DocumentId = document_id,
                Document = document,
                ArticleLikes = articleLikes,
                ArticleComments = articleComments
            };

            return View(articleViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ArticleSummary(string articleId)
        {
            try
            {
                var documentSummaryDto = new DocumentSummaryDto();

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _searchService.GetDocumentSummarizationNewsCybersecurityEurope<ResponseDto>(articleId, accessToken ?? "");

                if(response != null && response.IsSuccess)
                {
                    documentSummaryDto = JsonConvert.DeserializeObject<DocumentSummaryDto>(Convert.ToString(response.Result));
                }

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
                var documentQuestions = new List<string>();

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _searchService.GetDocumentQuestionsNewsCybersecurityEurope<ResponseDto>(articleId, accessToken ?? "");

                if (response != null && response.IsSuccess)
                {
                    documentQuestions = JsonConvert.DeserializeObject<List<string>>(Convert.ToString(response.Result));
                }

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
                var answerDto = new NLPQueryAnswerDto();

                var documentQuestionDto = new DocumentQuestionDto
                { 
                    document_id = articleId,
                    query = question
                };

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _searchService.SemanticSearchDocumentNewsCybersecurityEurope<ResponseDto>(documentQuestionDto, accessToken ?? "");

                if (response != null && response.IsSuccess)
                {
                    answerDto = JsonConvert.DeserializeObject<NLPQueryAnswerDto>(Convert.ToString(response.Result));
                }

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
                var response = new ResponseDto();
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                response = await _userService.GetArticleLikes<ResponseDto>(articleId, accessToken ?? "");

                if(response == null || response.IsSuccess == false)
                {
                    //TODO
                    //throw custom exception
                }

                var articleLikes = new List<ReadArticleLikeDto>();
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                articleLikes = JsonConvert.DeserializeObject<List<ReadArticleLikeDto>>(Convert.ToString(response.Result));

                var userArticleLike = articleLikes?.Where(x => x.UserId == userId).FirstOrDefault();

                if(userArticleLike == null) 
                {
                    var writeArticleLikeDto = new WriteArticleLikeDto
                    {
                        ArticleId = articleId,
                        UserId = userId ?? ""
                    };

                    response = await _userService.RegisterArticleLike<ResponseDto>(writeArticleLikeDto, accessToken ?? "");

                    if (response == null || response.IsSuccess == false)
                    {
                        //TODO
                        //throw custom exception
                    }
                }

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
                var response = new ResponseDto();
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                response = await _userService.GetArticleComments<ResponseDto>(articleId, accessToken ?? "");

                if (response == null || response.IsSuccess == false)
                {
                    //TODO
                    //throw custom exception
                }

                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var articleComments = new List<ReadArticleCommentDto>();

                articleComments = JsonConvert.DeserializeObject<List<ReadArticleCommentDto>>(Convert.ToString(response.Result));

                var userArticleComment = articleComments?.Where(x => x.UserDto?.UserId == userId).FirstOrDefault();

                if(userArticleComment == null)
                {
                    var writeArticleCommentDto = new WriteArticleCommentDto
                    {
                        Comment = comment,
                        DateTime = DateTime.Now,
                        ArticleId = articleId,
                        UserId = userId ?? ""
                    };

                    response = await _userService.RegisterArticleComment<ResponseDto>(writeArticleCommentDto, accessToken ?? "");

                    if (response == null || response.IsSuccess == false)
                    {
                        //TODO
                        //throw custom exception
                    }
                }

                return new JsonResult(new { });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ArticleChildComment(string articleId, int articleCommentId, string comment)
        {
            try
            {
                var response = new ResponseDto();
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                response = await _userService.GetArticleComments<ResponseDto>(articleId, accessToken ?? "");

                if (response == null || response.IsSuccess == false)
                {
                    //TODO
                    //throw custom exception
                }

                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var articleComments = new List<ReadArticleCommentDto>();

                articleComments = JsonConvert.DeserializeObject<List<ReadArticleCommentDto>>(Convert.ToString(response.Result));

                var articleComment = articleComments?
                    .Where(x => x.ArticleCommentId == articleCommentId).FirstOrDefault();

                var userArticleComment = articleComment?.Children?.Where(x => x.UserDto?.UserId == userId).FirstOrDefault();

                if (articleComment?.UserDto?.UserId != userId && userArticleComment == null)
                {
                    var writeArticleCommentDto = new WriteArticleCommentDto
                    {
                        Comment = comment,
                        DateTime = DateTime.Now,
                        ArticleId = articleId,
                        UserId = userId ?? "",
                        ParentArticleCommentId = articleCommentId
                    };

                    response = await _userService.RegisterArticleComment<ResponseDto>(writeArticleCommentDto, accessToken ?? "");

                    if (response == null || response.IsSuccess == false)
                    {
                        //TODO
                        //throw custom exception
                    }
                }

                return new JsonResult(new { });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ArticleCommentThumbsUp(string articleId, int articleCommentId)
        {
            try
            {
                //Check if the user has entry in table - ArticleCommentThumbsUp
                //If the user has entry - do nothing
                //If the user does not has entry check if the user has entry in table - ArticleCommentThumbsDown
                //If the user has entry in table - ArticleCommentThumbsDown delete this entry 
                //Insert new entry for the user in table - ArticleCommentThumbsUp

                var response = new ResponseDto();
                var articleCommentThumbsUpList = new List<ReadArticleCommentThumbsUpDto>();
                var articleCommentThumbsDownList = new List<ReadArticleCommentThumbsDownDto>();

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                response = await _userService.GetArticleCommentThumbsUp<ResponseDto>(articleId, articleCommentId, accessToken ?? "");

                articleCommentThumbsUpList = JsonConvert.DeserializeObject<List<ReadArticleCommentThumbsUpDto>>(Convert.ToString(response.Result));

                var userArticleCommentThumbsUp = articleCommentThumbsUpList?.FirstOrDefault(x => x.UserId == userId);

                if(userArticleCommentThumbsUp != null)
                {
                    return new JsonResult(new { });
                }

                response = await _userService.GetArticleCommentThumbsDown<ResponseDto>(articleId, articleCommentId, accessToken ?? "");

                articleCommentThumbsDownList = JsonConvert.DeserializeObject<List<ReadArticleCommentThumbsDownDto>>(Convert.ToString(response.Result));

                var userArticleCommentThumbsDown = articleCommentThumbsDownList?.FirstOrDefault(x => x.UserId == userId);

                if( userArticleCommentThumbsDown != null)
                {
                    await _userService.DeleteArticleCommentThumbsDown<ResponseDto>(articleId, articleCommentId, userId ?? "", accessToken ?? "");
                }

                var writeArticleCommentThumbsUpDto = new WriteArticleCommentThumbsUpDto
                {
                    ArticleCommentId = articleCommentId,
                    UserId = userId ?? "",
                    ArticleId = articleId
                };

                await _userService.RegisterArticleCommentThumbsUp<ResponseDto>(writeArticleCommentThumbsUpDto, accessToken ?? "");

                return new JsonResult(new { });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ArticleCommentThumbsDown(string articleId, int articleCommentId)
        {
            try
            {
                //Check if the user has entry in table - ArticleCommentThumbsDown
                //If the user has entry - do nothing
                //If the user does not has entry check if the user has entry in table - ArticleCommentThumbsUp
                //If the user has entry in table - ArticleCommentThumbsUp delete this entry 
                //Insert new entry for the user in table - ArticleCommentThumbsDown

                var response = new ResponseDto();
                var articleCommentThumbsUpList = new List<ReadArticleCommentThumbsUpDto>();
                var articleCommentThumbsDownList = new List<ReadArticleCommentThumbsDownDto>();

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                response = await _userService.GetArticleCommentThumbsDown<ResponseDto>(articleId, articleCommentId, accessToken ?? "");

                articleCommentThumbsDownList = JsonConvert.DeserializeObject<List<ReadArticleCommentThumbsDownDto>>(Convert.ToString(response.Result));

                var userArticleCommentThumbsDown = articleCommentThumbsDownList?.FirstOrDefault(x => x.UserId == userId);

                if (userArticleCommentThumbsDown != null)
                {
                    return new JsonResult(new { });
                }

                response = await _userService.GetArticleCommentThumbsUp<ResponseDto>(articleId, articleCommentId, accessToken ?? "");

                articleCommentThumbsUpList = JsonConvert.DeserializeObject<List<ReadArticleCommentThumbsUpDto>>(Convert.ToString(response.Result));

                var userArticleCommentThumbsUp = articleCommentThumbsUpList?.FirstOrDefault(x => x.UserId == userId);

                if (userArticleCommentThumbsUp != null)
                {
                    await _userService.DeleteArticleCommentThumbsUp<ResponseDto>(articleId, articleCommentId, userId ?? "", accessToken ?? "");
                }

                var writeArticleCommentThumbsDownDto = new WriteArticleCommentThumbsDownDto
                {
                    ArticleCommentId = articleCommentId,
                    UserId = userId ?? "",
                    ArticleId = articleId
                };

                await _userService.RegisterArticleCommentThumbsDown<ResponseDto>(writeArticleCommentThumbsDownDto, accessToken ?? "");

                return new JsonResult(new { });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
