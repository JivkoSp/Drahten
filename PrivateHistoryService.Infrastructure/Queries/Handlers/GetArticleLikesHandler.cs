using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Queries;
using PrivateHistoryService.Application.Queries.Handlers;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.Queries.Handlers
{
    internal sealed class GetArticleLikesHandler : IQueryHandler<GetArticleLikesQuery, List<LikedArticleDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetArticleLikesHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<LikedArticleDto>> HandleAsync(GetArticleLikesQuery query)
        {
            var likedArticleReadModels = await _readDbContext.LikedArticles
              .Where(x => x.UserId == query.UserId.ToString())
              .AsNoTracking()
              .ToListAsync();

            return _mapper.Map<List<LikedArticleDto>>(likedArticleReadModels);
        }
    }
}
