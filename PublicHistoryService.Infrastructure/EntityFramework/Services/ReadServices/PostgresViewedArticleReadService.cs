using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Application.Services.ReadServices;
using PublicHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PublicHistoryService.Infrastructure.EntityFramework.Services.ReadServices
{
    internal sealed class PostgresViewedArticleReadService : IViewedArticleReadService
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public PostgresViewedArticleReadService(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<ViewedArticleDto> GetViewedArticleByIdAsync(Guid viewedArticleId)
        {
            var viewedArticleReadModel = await _readDbContext.ViewedArticles
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ViewedArticleId == viewedArticleId);

            return _mapper.Map<ViewedArticleDto>(viewedArticleReadModel);
        }
    }
}
