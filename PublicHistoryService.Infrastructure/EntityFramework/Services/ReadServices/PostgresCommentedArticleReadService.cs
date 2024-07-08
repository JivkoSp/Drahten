using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Application.Services.ReadServices;
using PublicHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PublicHistoryService.Infrastructure.EntityFramework.Services.ReadServices
{
    internal sealed class PostgresCommentedArticleReadService : ICommentedArticleReadService
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public PostgresCommentedArticleReadService(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<CommentedArticleDto> GetCommentedArticleByIdAsync(Guid commentedArticleId)
        {
            var commentedArticleReadModel = await _readDbContext.CommentedArticles
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.CommentedArticleId == commentedArticleId);

            return _mapper.Map<CommentedArticleDto>(commentedArticleReadModel);
        }
    }
}
