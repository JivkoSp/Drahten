using DrahtenWeb.Dtos;
using DrahtenWeb.Dtos.TopicArticleService;
using DrahtenWeb.Extensions;
using DrahtenWeb.Services.IServices;
using DrahtenWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DrahtenWeb.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ITopicArticleService _topicArticleService;

        public UserController(ITopicArticleService topicArticleService)
        {
            _topicArticleService = topicArticleService;
        }

        [HttpGet]
        public async Task<IActionResult> UserSearchOptions()
        {
            var userSearchOptionsViewModel = new UserSearchOptionsViewModel();

            //Get the user id.
            //Here the NameIdentifier claim type represents the user id.
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _topicArticleService.GetTopicsAsync<ResponseDto>(accessToken);

            userSearchOptionsViewModel.Topics = response.Map<List<TopicDto>>();

            response = await _topicArticleService.GetTopicsRelatedToUserAsync<ResponseDto>(userId, accessToken);

            userSearchOptionsViewModel.UserTopics = response.Map<List<UserTopicDto>>();

            return View(userSearchOptionsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserTopicSubscription(Guid topicId)
        {
            try
            {
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var userTopicDto = new WriteUserTopicDto
                {
                    UserId = userId,
                    TopicId = topicId,
                    DateTime = DateTimeOffset.Now
                };

                await _topicArticleService.RegisterUserTopicAsync<object>(userTopicDto, accessToken);

                return new JsonResult(new { });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
