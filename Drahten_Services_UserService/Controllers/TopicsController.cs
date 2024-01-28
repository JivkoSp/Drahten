using AutoMapper;
using Drahten_Services_UserService.Data;
using Drahten_Services_UserService.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Drahten_Services_UserService.Controllers
{
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
                Console.WriteLine(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
