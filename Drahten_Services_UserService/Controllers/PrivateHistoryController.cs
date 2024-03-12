using AutoMapper;
using Drahten_Services_UserService.Data;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Drahten_Services_UserService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("user-service/private-history")]
    public class PrivateHistoryController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ResponseDto responseDto = new ResponseDto();

        public PrivateHistoryController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult GetUserPrivateHistory(string userId)
        {
            try
            {
                var privateHistoryModel = _appDbContext.PrivateHistories?.FirstOrDefault(x => x.UserId == userId);

                if(privateHistoryModel == null)
                {
                    return NotFound(responseDto);
                }

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<ReadPrivateHistoryDto>(privateHistoryModel);

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
        public IActionResult CreateUserPrivateHistory(WritePrivateHistoryDto writePrivateHistoryDto)
        {
            try
            {
                if(writePrivateHistoryDto == null)
                {
                    //TODO:
                    //throw custom exception
                }

                var privateHistoryModel = _mapper.Map<PrivateHistory>(writePrivateHistoryDto);

                _appDbContext.PrivateHistories?.Add(privateHistoryModel);

                _appDbContext.SaveChanges();

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<ReadPrivateHistoryDto>(privateHistoryModel);

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

        [HttpGet("{userId}/viewed_articles/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult GetViewedArticlesPrivateHistory(string userId)
        {
            try
            {
                var viewedArticlesModelList = _appDbContext.ViewedArticlePrivateHists?
                                              .Where(x => x.PrivateHistoryId == userId)
                                              .Include(x => x.Article)
                                              .ToList();

                if(viewedArticlesModelList == null)
                {
                    return NotFound(responseDto);
                }

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<List<ReadViewedArticleHistoryDto>>(viewedArticlesModelList);

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

        [HttpPost("{userId}/viewed_articles/")]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult RegisterViewedArticlePrivateHistory(WriteViewedArticleHistoryDto writeViewedArticleHistoryDto)
        {
            try
            {
                if(writeViewedArticleHistoryDto == null)
                {
                    //TODO:
                    //throw custom exception
                }

                var viewedArticleHistoryModel = _mapper.Map<ViewedArticlePrivateHist>(writeViewedArticleHistoryDto);

                _appDbContext.ViewedArticlePrivateHists?.Add(viewedArticleHistoryModel);

                _appDbContext.SaveChanges();

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<ReadViewedArticleHistoryDto>(viewedArticleHistoryModel);

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

        [HttpGet("{userId}/searched-topics-data/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult GetSearchedTopicsDataPrivateHistory(string userId)
        {
            try
            {
                var searchedTopicsDataModelList = _appDbContext.SearchedTopicDataPrivateHists?
                                                  .Where(x => x.PrivateHistoryId == userId)
                                                  .Include(x => x.Topic)
                                                  .ToList();

                if(searchedTopicsDataModelList == null)
                {
                    return NotFound(responseDto);
                }

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<List<ReadSearchedTopicDataHistoryDto>>(searchedTopicsDataModelList);

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

        [HttpPost("{userId}/searched-topics-data/")]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult RegisterSearchedTopicsDataPrivateHistory(WriteSearchedTopicDataHistoryDto writeSearchedTopicDataHistoryDto)
        {
            try
            {
                if(writeSearchedTopicDataHistoryDto == null)
                {
                    //TODO:
                    //throw custom exception
                }

                var searchedTopicDataHistoryModel = _mapper.Map<SearchedTopicDataPrivateHist>(writeSearchedTopicDataHistoryDto);

                _appDbContext.SearchedTopicDataPrivateHists?.Add(searchedTopicDataHistoryModel);

                _appDbContext.SaveChanges();

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<ReadSearchedTopicDataHistoryDto>(searchedTopicDataHistoryModel);

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

        [HttpGet("{userId}/searched-article-data/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult GetSearchedArticleDataPrivateHistory(string userId)
        {
            try
            {
                var searchedArticleDataModelList = _appDbContext.SearchedArticleDataPrivateHists?
                                                   .Where(x => x.PrivateHistoryId == userId)
                                                   .Include(x => x.Article)
                                                   .ToList();

                if(searchedArticleDataModelList == null)
                {
                    return NotFound(responseDto);
                }

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<List<ReadSearchedArticleDataHistoryDto>>(searchedArticleDataModelList);

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

        [HttpPost("{userId}/searched-article-data/")]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult RegisterSearchedArticleDataPrivateHistory(WriteSearchedArticleDataHistoryDto writeSearchedArticleDataHistoryDto)
        {
            try
            {
                if (writeSearchedArticleDataHistoryDto == null)
                {
                    //TODO:
                    //throw custom exception
                }

                var searchedArticleDataHistoryModel = _mapper.Map<SearchedArticleDataPrivateHist>(writeSearchedArticleDataHistoryDto);

                _appDbContext.SearchedArticleDataPrivateHists?.Add(searchedArticleDataHistoryModel);

                _appDbContext.SaveChanges();

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<ReadSearchedArticleDataHistoryDto>(searchedArticleDataHistoryModel);

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
