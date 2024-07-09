using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Application.Queries;
using PublicHistoryService.Application.Queries.Handlers;
using PublicHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PublicHistoryService.Infrastructure.Queries.Handlers
{
    internal sealed class GeViewedUsersHandler : IQueryHandler<GeViewedUsersQuery, List<ViewedUserDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GeViewedUsersHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<ViewedUserDto>> HandleAsync(GeViewedUsersQuery query)
        {
            var viewedUserReadModels = await _readDbContext.ViewedUsers
              .Where(x => x.ViewerUserId == query.ViewerUserId.ToString())
              .Include(x => x.ViewedUser)
              .AsNoTracking()
              .ToListAsync();

            return _mapper.Map<List<ViewedUserDto>>(viewedUserReadModels);
        }
    }
}
