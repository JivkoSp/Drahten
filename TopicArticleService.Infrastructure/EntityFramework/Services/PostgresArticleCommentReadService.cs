using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.EntityFramework.Services
{
    internal sealed class PostgresArticleCommentReadService : IArticleCommentReadService
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public PostgresArticleCommentReadService(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<ArticleCommentDto> GetArticleCommentByIdAsync(Guid articleCommentId)
        {
            var articleCommentReadModel = await _readDbContext.ArticleComments
                .Include(x => x.Article)
                .Include(x => x.Children)
                .Include(x => x.ArticleCommentLikes)
                .Include(x => x.ArticleCommentDislikes)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ArticleCommentId == articleCommentId);

            return _mapper.Map<ArticleCommentDto>(articleCommentReadModel);
        }
    }
}
