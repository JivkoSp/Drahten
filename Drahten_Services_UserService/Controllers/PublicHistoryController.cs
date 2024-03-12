using AutoMapper;
using Drahten_Services_UserService.Data;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Drahten_Services_UserService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("user-service/public-history")]
    public class PublicHistoryController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ResponseDto responseDto = new ResponseDto();

        public PublicHistoryController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult GetUserPublicHistory(string userId)
        {
            try
            {
                var publicHistoryModel = _appDbContext.PublicHistories?.FirstOrDefault(x => x.UserId == userId);

                if (publicHistoryModel == null)
                {
                    return NotFound(responseDto);
                }

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<ReadPublicHistoryDto>(publicHistoryModel);

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
        public IActionResult CreateUserPublicHistory(WritePublicHistoryDto writePublicHistoryDto)
        {
            try
            {
                if (writePublicHistoryDto == null)
                {
                    //TODO:
                    //throw custom exception
                }

                var publicHistoryModel = _mapper.Map<PublicHistory>(writePublicHistoryDto);

                _appDbContext.PublicHistories?.Add(publicHistoryModel);

                _appDbContext.SaveChanges();

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<ReadPublicHistoryDto>(publicHistoryModel);

                return Created(HttpContext.Request.Path, responseDto);
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
    }
}
