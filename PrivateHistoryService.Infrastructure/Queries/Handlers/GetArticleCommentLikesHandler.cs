using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Queries;
using PrivateHistoryService.Application.Queries.Handlers;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.Queries.Handlers
{
    internal sealed class GetArticleCommentLikesHandler : IQueryHandler<GetArticleCommentLikesQuery, List<LikedArticleCommentDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetArticleCommentLikesHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<LikedArticleCommentDto>> HandleAsync(GetArticleCommentLikesQuery query)
        {
            var likedArticleCommentReadModels = await _readDbContext.LikedArticleComments
               .Where(x => x.UserId == query.UserId.ToString())
               .Include(x => x.User)
               .AsNoTracking()
               .ToListAsync();

            return _mapper.Map<List<LikedArticleCommentDto>>(likedArticleCommentReadModels);
        }
    }
}
