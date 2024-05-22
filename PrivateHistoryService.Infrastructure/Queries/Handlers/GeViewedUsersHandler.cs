using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Queries;
using PrivateHistoryService.Application.Queries.Handlers;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.Queries.Handlers
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
              .AsNoTracking()
              .ToListAsync();

            return _mapper.Map<List<ViewedUserDto>>(viewedUserReadModels);
        }
    }
}
