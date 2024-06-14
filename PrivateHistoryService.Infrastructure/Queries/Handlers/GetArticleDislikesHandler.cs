using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Queries;
using PrivateHistoryService.Application.Queries.Handlers;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.Queries.Handlers
{
    internal sealed class GetArticleDislikesHandler : IQueryHandler<GetArticleDislikesQuery, List<DislikedArticleDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetArticleDislikesHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<DislikedArticleDto>> HandleAsync(GetArticleDislikesQuery query)
        {
            var dislikedArticleReadModels = await _readDbContext.DislikedArticles
               .Where(x => x.UserId == query.UserId.ToString())
               .Include(x => x.User)
               .AsNoTracking()
               .ToListAsync();

            return _mapper.Map<List<DislikedArticleDto>>(dislikedArticleReadModels);
        }
    }
}
