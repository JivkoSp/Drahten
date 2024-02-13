using DrahtenWeb.Dtos;
using DrahtenWeb.Services;
using DrahtenWeb.Services.IServices;
using DrahtenWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DrahtenWeb.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ISearchService _searchService;

        public ArticleController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        public IActionResult ViewArticle(string document_id, DocumentDto document)
        {
            var articleViewModel = new ArticleViewModel
            { 
                DocumentId = document_id,
                Document = document
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
    }
}
