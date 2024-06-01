using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Services.ReadServices;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Services.ReadServices
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
