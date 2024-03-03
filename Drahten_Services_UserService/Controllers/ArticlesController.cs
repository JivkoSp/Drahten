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
    [Route("user-service/articles")]
    public class ArticlesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ResponseDto responseDto = new ResponseDto();

        public ArticlesController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet("{articleId}")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult GetArticle(string articleId)
        {
            try
            {
                var articleModel = _appDbContext.Articles?.FirstOrDefault(x => x.ArticleId == articleId);

                if (articleModel == null)
                {
                    responseDto.Result = $"No article with id {articleId} was found.";

                    return NotFound(responseDto);
                }

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<ReadArticleDto>(articleModel);

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

        [HttpPost("article/")]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult RegisterArticle(WriteArticleDto article)
        {
            try
            {
                if(article == null) 
                { 
                    //TODO:
                    //throw custom exception
                }

                var articleModel = _mapper.Map<Article>(article);

                _appDbContext.Articles?.Add(articleModel);

                _appDbContext.SaveChanges();

                responseDto.IsSuccess = true;

                responseDto.Result = article;

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

        [HttpPost("user-article/")]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult RegisterUserArticle(WriteUserArticleDto userArticle)
        {
            try
            {
                if(userArticle == null)
                {
                    //TODO:
                    //throw custom exception
                }

                var userArticleModel = _mapper.Map<UserArticle>(userArticle);

                _appDbContext.UserArticles?.Add(userArticleModel);

                _appDbContext.SaveChanges();

                responseDto.IsSuccess = true;

                responseDto.Result = userArticle;

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

        [HttpGet("{articleId}/likes/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult GetArticleLikes(string articleId)
        {
            try
            {
                var articleLikes = _appDbContext.ArticleLikes?
                    .Where(x => x.ArticleId == articleId)
                    .ToList();

                if(articleLikes == null)
                {
                    responseDto.Result = $"No likes were found for article with id {articleId}.";

                    return NotFound(responseDto);
                }

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<List<ReadArticleLikeDto>>(articleLikes);

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

        [HttpPost("{articleId}/likes/{userId}")]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult RegisterArticleLike(string articleId, string userId)
        {
            try
            {
                var articleLikeModel = new ArticleLike
                {
                    DateTime = DateTime.Now,
                    ArticleId = articleId,
                    UserId = userId
                };

                _appDbContext.ArticleLikes?.Add(articleLikeModel);

                _appDbContext.SaveChanges();

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<ReadArticleLikeDto>(articleLikeModel);

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

        [HttpGet("{articleId}/comments/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult GetArticleComments(string articleId)
        {
            try
            {
                var articleComments = _appDbContext.ArticleComments?
                    .Where(x => x.ArticleId == articleId)
                    .Include(x => x.User)
                    .Include(x => x.ArticleCommentThumbsUp)
                    .Include(x => x.ArticleCommentThumbsDown)
                    .ToList();

                if (articleComments == null)
                {
                    responseDto.Result = $"No comments were found for article with id {articleId}.";

                    return NotFound(responseDto);
                }

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<List<ReadArticleCommentDto>>(articleComments);

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

        [HttpPost("{articleId}/comments/")]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult RegisterArticleComment(WriteArticleCommentDto articleComment)
        {
            try
            {
                var articleCommentModel = _mapper.Map<ArticleComment>(articleComment);

                _appDbContext.ArticleComments?.Add(articleCommentModel);

                _appDbContext.SaveChanges();

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<ReadArticleCommentDto>(articleCommentModel);

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

        [HttpGet("{articleId}/comments/{commentId}/thumbs-up")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult GetArticleCommentThumbsUp(int commentId)
        {
            try
            {
                var articleCommentThumbsUpModelList = _appDbContext.ArticleCommentThumbsUp?
                                                      .Where(p => p.ArticleCommentId == commentId)
                                                      .ToList();

                if(articleCommentThumbsUpModelList == null)
                {
                    responseDto.Result = new List<ReadArticleCommentThumbsUpDto>();

                    return NotFound(responseDto);
                }

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<List<ReadArticleCommentThumbsUpDto>>(articleCommentThumbsUpModelList);

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

        [HttpPost("{articleId}/comments/{commentId}/thumbs-up")]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult RegisterArticleCommentThumbsUp(WriteArticleCommentThumbsUpDto articleCommentThumbsUp)
        {
            try
            {
                var articleCommentThumbsUpModel = _mapper.Map<ArticleCommentThumbsUp>(articleCommentThumbsUp);

                _appDbContext.ArticleCommentThumbsUp?.Add(articleCommentThumbsUpModel);

                _appDbContext.SaveChanges();

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<ReadArticleCommentThumbsUpDto>(articleCommentThumbsUpModel);

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

        [HttpDelete("{articleId}/comments/{commentId}/thumbs-up/{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult DeleteArticleCommentThumbsUp(int commentId, string userId)
        {
            try
            {
                var articleCommentThumbsUpModel = _appDbContext.ArticleCommentThumbsUp?
                   .FirstOrDefault(x => x.ArticleCommentId == commentId && x.UserId == userId);

                if (articleCommentThumbsUpModel == null)
                {
                    return NotFound(responseDto);
                }

                _appDbContext.ArticleCommentThumbsUp?.Remove(articleCommentThumbsUpModel);

                _appDbContext.SaveChanges();

                return NoContent();
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

        [HttpGet("{articleId}/comments/{commentId}/thumbs-down")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult GetArticleCommentThumbsDown(int commentId)
        {
            try
            {
                var articleCommentThumbsDownModelList = _appDbContext.ArticleCommentThumbsDown?
                                                      .Where(p => p.ArticleCommentId == commentId)
                                                      .ToList();

                if (articleCommentThumbsDownModelList == null)
                {
                    responseDto.Result = new List<ReadArticleCommentThumbsDownDto>();

                    return NotFound(responseDto);
                }

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<List<ReadArticleCommentThumbsDownDto>>(articleCommentThumbsDownModelList);

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

        [HttpPost("{articleId}/comments/{commentId}/thumbs-down")]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult RegisterArticleCommentThumbsDown(WriteArticleCommentThumbsDownDto articleCommentThumbsDown)
        {
            try
            {
                var articleCommentThumbsDownModel = _mapper.Map<ArticleCommentThumbsDown>(articleCommentThumbsDown);

                _appDbContext.ArticleCommentThumbsDown?.Add(articleCommentThumbsDownModel);

                _appDbContext.SaveChanges();

                responseDto.IsSuccess = true;

                responseDto.Result = _mapper.Map<ReadArticleCommentThumbsDownDto>(articleCommentThumbsDownModel);

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

        [HttpDelete("{articleId}/comments/{commentId}/thumbs-down/{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        public IActionResult DeleteArticleCommentThumbsDown(int commentId, string userId)
        {
            try
            {
                var articleCommentThumbsDownModel = _appDbContext.ArticleCommentThumbsDown?
                    .FirstOrDefault(x => x.ArticleCommentId == commentId && x.UserId == userId);

                if(articleCommentThumbsDownModel == null)
                {
                    return NotFound(responseDto);
                }

                _appDbContext.ArticleCommentThumbsDown?.Remove(articleCommentThumbsDownModel);

                _appDbContext.SaveChanges();

                return NoContent();
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
