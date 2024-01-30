using DrahtenWeb.Dtos;
using DrahtenWeb.Services;
using DrahtenWeb.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace DrahtenWeb.Controllers
{
    [Authorize]
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

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _userService.GetTopics<ResponseDto>(accessToken ?? "");

            if(response != null && response.IsSuccess)
            {
                topics = JsonConvert.DeserializeObject<List<TopicDto>>(Convert.ToString(response.Result));
            }

            return View(topics);
        }

        [HttpPost]
        public async Task<IActionResult> UserTopicSubscription(int topic_id)
        {
            try
            {
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    //throw exception
                }

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _userService.RegisterUserTopic<ResponseDto>(
                    new WriteUserDto { UserId = userId, TopicId = topic_id }, accessToken ?? "");

                return new JsonResult(new { Message = response.IsSuccess });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
