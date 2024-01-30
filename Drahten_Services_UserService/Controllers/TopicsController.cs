using AutoMapper;
using Drahten_Services_UserService.Data;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Drahten_Services_UserService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("user-service/topics")]
    public class TopicsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ResponseDto responseDto = new ResponseDto();

        public TopicsController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult GetAllTopics()
        {
            try
            {
                var topics = _appDbContext.Topics?
                    .Include(p => p.Children)
                    .ToList();

                if(topics != null)
                {
                    responseDto.IsSuccess = true;

                    responseDto.Result = _mapper.Map<List<ReadTopicDto>>(topics);
                }
                else
                {
                    responseDto.Result = "No topics found.";

                    return NotFound(responseDto);
                }

                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                responseDto.ErrorMessages = new List<string>
                {
                    ex.ToString()
                };

                return BadRequest(responseDto);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult RegisterUserTopic(WriteUserDto user)
        {
            try
            {
                var userTopicModel = _mapper.Map<UserTopic>(user);

                userTopicModel.SubscriptionTime = DateTime.Now;

                _appDbContext.UserTopics?.Add(userTopicModel);

                _appDbContext.SaveChanges();

                responseDto.IsSuccess = true;

                responseDto.Result = user;

                return Created(HttpContext.Request.Path, responseDto);
            }
            catch(Exception ex)
            {
                responseDto.ErrorMessages = new List<string>
                {
                    ex.ToString()
                };

                return BadRequest(responseDto);
            }
        }
    }
}
