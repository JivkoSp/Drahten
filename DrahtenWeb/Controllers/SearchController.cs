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
        public async Task<IActionResult> CybersecurityNewsEuropeQuery(int topicId, string query)
        {
            try
            {
                var response = new ResponseDto();
                var documents = new List<NLPQueryAnswerDto>();
                var searchedTopicsData = new List<ReadSearchedTopicDataHistoryDto>();

                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                response = await _searchService.GetAllMathingDocumentsNewsCybersecurityEurope<ResponseDto>(query, accessToken ?? "");

                if(response != null && response.IsSuccess)
                {
                    documents = JsonConvert.DeserializeObject<List<NLPQueryAnswerDto>>(Convert.ToString(response.Result));
                }

                //Get all records for user id from SearchedTopicDataPrivateHist table.
                //Check if the current query exist in the records
                //If the current query does not exist register it.

                response = await _userService.GetSearchedTopicsDataPrivateHistory<ResponseDto>(userId ?? "", accessToken ?? "");

                searchedTopicsData = JsonConvert.DeserializeObject<List<ReadSearchedTopicDataHistoryDto>>(Convert.ToString(response.Result));

                var searchedTopicDataList = searchedTopicsData?.Where(x => x.Topic?.TopicId == topicId).ToList();

                var searchedTopicQuery = searchedTopicDataList?.FirstOrDefault(x => x.SearchedData == query);

                if(searchedTopicQuery == null)
                {
                    var writeSearchedTopicDataHistoryDto = new WriteSearchedTopicDataHistoryDto
                    {
                        SearchedData = query,
                        SearchTime = DateTime.Now,
                        TopicId = topicId,
                        HistoryId = userId ?? ""
                    };

                    await _userService.RegisterSearchedTopicsDataPrivateHistory<ResponseDto>(writeSearchedTopicDataHistoryDto, accessToken ?? "");
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
