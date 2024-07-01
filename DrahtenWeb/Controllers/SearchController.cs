using DrahtenWeb.Dtos;
using DrahtenWeb.Dtos.TopicArticleService;
using DrahtenWeb.Extensions;
using DrahtenWeb.Services;
using DrahtenWeb.Services.IServices;
using DrahtenWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrahtenWeb.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly ITopicArticleService _topicArticleService;

        public SearchController(ISearchService searchService, ITopicArticleService topicArticleService)
        {
            _searchService = searchService;
            _topicArticleService = topicArticleService;
        }

        [HttpGet]
        public async Task<IActionResult> CybersecurityNewsEurope()
        {
            try
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _searchService.GetAllDocumentsNewsCybersecurityEurope<ResponseDto>(accessToken);

                var documents = response.Map<List<QueryAnswerDto>>();

                return new JsonResult(documents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CybersecurityNewsEuropeQuery(string query)
        {
            try
            {
                var semanticSearchViewModel = new SemanticSearchViewModel();

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _searchService.GetAllMathingDocumentsNewsCybersecurityEurope<ResponseDto>(query, accessToken);

                var nLPQueryAnswerDtos = response.Map<List<NLPQueryAnswerDto>>();

                foreach (var nLPQueryAnswerDto in nLPQueryAnswerDtos)
                {
                    var articleResponseDto = await _topicArticleService.GetArticleByIdAsync<ResponseDto>(nLPQueryAnswerDto.DocumentId, accessToken);

                    var articleCommentsResponseDto = await _topicArticleService.GetArticleCommentsAsync<ResponseDto>(nLPQueryAnswerDto.DocumentId, accessToken);

                    var usersRelatedToArticleResponseDto = await _topicArticleService.GetUsersRelatedToArticleAsync<ResponseDto>(nLPQueryAnswerDto.DocumentId, accessToken);

                    semanticSearchViewModel.Articles.Add(articleResponseDto.Map<ArticleDto>());

                    semanticSearchViewModel.ArticleComments[nLPQueryAnswerDto.DocumentId] = articleCommentsResponseDto.Map<List<ReadArticleCommentDto>>();

                    semanticSearchViewModel.UsersRelatedToArticle[nLPQueryAnswerDto.DocumentId] = usersRelatedToArticleResponseDto.Map<List<ReadUserArticleDto>>();
                }

                return new JsonResult(semanticSearchViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
