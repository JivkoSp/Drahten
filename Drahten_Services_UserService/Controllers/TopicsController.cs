using AutoMapper;
using Drahten_Services_UserService.Data;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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

        [HttpGet("root-topic/{topicId}")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult GetRootTopicWithChildren(int topicId)
        {
            try
            {
                //Find all parents for topic with id: topicId and select ONLY the root parent topic.
                //This is recursive function, that will be executed in the database.
                //The table and column names are written with double quotes, becouse there names must be interpreted by postgresql literally.
                //CAUTION: If the table and column names are written without double quotes there names will be lower cased by postgresql and the query will NOT be valid.
                var topic = _appDbContext.Topics?
                            .FromSqlRaw("WITH RECURSIVE ancestors AS (" +
                                        "   SELECT \"Topic\".\"TopicId\", \"Topic\".\"TopicName\", \"Topic\".\"ParentTopicId\"\t\n" +
                                        "   FROM \"Topic\"" +
                                        "   WHERE \"Topic\".\"TopicId\" = @topicId" +
                                        "   UNION ALL" +
                                        "   SELECT t.\"TopicId\", t.\"TopicName\", t.\"ParentTopicId\"" +
                                        "   FROM \"Topic\" AS t" +
                                        "   JOIN ancestors ON t.\"TopicId\" = ancestors.\"ParentTopicId\"" +
                                        "   )" +
                                        "   SELECT * FROM ancestors",
                             new NpgsqlParameter("@topicId", topicId)) //Include the topicId. The documentation specifies that this is safer than $"{}".
                            .Where(x => x.Parent == null) //Include ONLY the root parent topic.
                            .FirstOrDefault(); 

                //Find all topics and include there children.
                var alltopics = _appDbContext.Topics?
                        .Include(x => x.Children)
                        .ToList();

                //Find the topic, that has id equal to the id of the root parent topic.
                //The effect is that the topic variable will hold the root parent topic of the child topic with id: topicId
                //and will include all of its children topics (including the child topic with id: topicId).
                topic = alltopics?.Where(x => x.TopicId == topic.TopicId).FirstOrDefault();


                //Check if the variable topic has null value.
                if (topic != null)
                {
                    //The variable topic is NOT null.
                    responseDto.IsSuccess = true;
                    responseDto.Result = _mapper.Map<ReadTopicDto>(topic);
                }
                else
                {
                    //The variable topic IS null.
                    responseDto.Result = $"No topic with id: {topicId} was found.";

                    return BadRequest(responseDto);
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

        [HttpGet("user-topics/{userId}")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult GetAllUserTopics(string userId)
        {
            try
            {
                //This query performs two inner joins between three tables, who are in many-to-many relationship.
                //The UserTopic table is join table between the User and Topic tables.
                //The User table is joined with the UserTopic table on the condition that UserId from the User table
                //is equal to the UserId from the UserTopic table.
                //The Topic table is then joined on the condition that the TopicId from the UserTopic table
                //is equal to the TopicId from the Topic table.
                var userTopics = _appDbContext.Users?
                    .Join(_appDbContext.UserTopics,
                          left_side => left_side.UserId,
                          right_side => right_side.UserId,
                          (left_side, right_side) => new { UserId = left_side.UserId, 
                                                           TopicId = right_side.TopicId, 
                                                           SubscriptionTime = right_side.SubscriptionTime 
                     }).Where(x => x.UserId == userId)
                       .Join(_appDbContext.Topics,
                             left_side => left_side.TopicId,
                             right_side => right_side.TopicId,
                             (left_side, right_side) => new { UserId = left_side.UserId, 
                                                              TopicId = left_side.TopicId, 
                                                              SubscriptionTime = left_side.SubscriptionTime, 
                                                              TopicName = right_side.TopicName 
                       }).Select(x => new ReadUserTopicDto { UserId = x.UserId, 
                                                             TopicId = x.TopicId, 
                                                             TopicName = x.TopicName, 
                                                             SubscriptionTime = x.SubscriptionTime }).ToList();


                if(userTopics != null) 
                { 
                    responseDto.IsSuccess = true;

                    responseDto.Result = userTopics;
                }
                else
                {
                    responseDto.Result = $"No topics were found for user with id: {userId}";

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
        public IActionResult RegisterUserTopic(WriteUserTopicDto user)
        {
            try
            {
                var userTopicModel = _mapper.Map<UserTopic>(user);

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
