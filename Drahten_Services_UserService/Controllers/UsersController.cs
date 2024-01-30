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
    [Route("user-service/users")]
    public class UsersController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ResponseDto responseDto = new ResponseDto();

        public UsersController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _appDbContext.Users?.ToList();

                if(users != null)
                {
                    responseDto.IsSuccess = true;

                    responseDto.Result = _mapper.Map<List<ReadUserDto>>(users);
                }
                else
                {
                    responseDto.Result = "No users found.";

                    return NotFound(responseDto);
                }

                return Ok(responseDto);
            }
            catch(Exception ex) 
            {
                responseDto.ErrorMessages = new List<string>
                {
                    ex.ToString()
                };

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult GetUserById(string userId)
        {
            try
            {
                var user = _appDbContext.Users?.FirstOrDefault(x => x.UserId == userId);

                if(user != null)
                {
                    responseDto.IsSuccess= true;

                    responseDto.Result = _mapper.Map<ReadUserDto>(user);
                }
                else
                {
                    responseDto.Result = $"No user with id {userId} was found.";

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

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult RegisterUser(WriteUserDto user)
        {
            try
            {
                var userModel = _mapper.Map<User>(user);

                _appDbContext.Users?.Add(userModel);

                _appDbContext.SaveChanges();

                responseDto.IsSuccess = true;

                responseDto.Result = user;
               
                return Created(HttpContext.Request.Path, responseDto);
            }
            catch (Exception ex)
            {
                responseDto.ErrorMessages = new List<string>
                {
                    ex.ToString()
                };

                return BadRequest(ex.Message);
            }
        }
    }
}
