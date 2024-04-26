using DrahtenWeb.Dtos;
using DrahtenWeb.Extensions;
using DrahtenWeb.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrahtenWeb.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IUserService _userService;

        public SearchController(ISearchService searchService, IUserService userService)
        {
            _searchService = searchService;
            _userService = userService;
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
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _searchService.GetAllMathingDocumentsNewsCybersecurityEurope<ResponseDto>(query, accessToken);

                var documents = response.Map<List<NLPQueryAnswerDto>>();

                return new JsonResult(documents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
