using DrahtenWeb.Dtos;
using DrahtenWeb.Services;
using DrahtenWeb.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace DrahtenWeb.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IActionResult> CybersecurityNewsEurope()
        {
            try
            {
                var documents = new List<QueryAnswerDto>();

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _searchService.GetAllDocumentsNewsCybersecurityEurope<ResponseDto>(accessToken ?? "");

                if (response != null && response.IsSuccess)
                {
                    documents = JsonConvert.DeserializeObject<List<QueryAnswerDto>>(Convert.ToString(response.Result));
                }

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
                var documents = new List<NLPQueryAnswerDto>();

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _searchService.GetAllMathingDocumentsNewsCybersecurityEurope<ResponseDto>(query, accessToken ?? "");

                if(response != null && response.IsSuccess)
                {
                    documents = JsonConvert.DeserializeObject<List<NLPQueryAnswerDto>>(Convert.ToString(response.Result));
                }

                return new JsonResult(documents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
