using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Services.ReadServices;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Services.ReadServices
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
