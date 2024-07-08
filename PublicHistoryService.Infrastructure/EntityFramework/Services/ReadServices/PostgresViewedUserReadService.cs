using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Application.Services.ReadServices;
using PublicHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PublicHistoryService.Infrastructure.EntityFramework.Services.ReadServices
{
    internal sealed class PostgresViewedUserReadService : IViewedUserReadService
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public PostgresViewedUserReadService(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<ViewedUserDto> GetViewedUserByIdAsync(Guid viewedUserId)
        {
            var viewedUserReadModel = await _readDbContext.ViewedUsers
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ViewedUserReadModelId == viewedUserId);

            return _mapper.Map<ViewedUserDto>(viewedUserReadModel);
        }
    }
}
