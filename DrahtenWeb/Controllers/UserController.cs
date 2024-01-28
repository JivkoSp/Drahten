using DrahtenWeb.Dtos;
using DrahtenWeb.Services;
using DrahtenWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DrahtenWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> UserSearchOptions()
        {
            var topics = new List<TopicDto>();
            var response = await _userService.GetUserTopics<ResponseDto>("");

            if(response != null && response.IsSuccess)
            {
                topics = JsonConvert.DeserializeObject<List<TopicDto>>(Convert.ToString(response.Result));
            }

            return View(topics);
        }

        [HttpPost]
        public async Task<IActionResult> UserTopicSubscription()
        {


            return new JsonResult(new { });
        }
    }
}
