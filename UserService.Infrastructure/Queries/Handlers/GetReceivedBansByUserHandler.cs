using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Dtos;
using UserService.Application.Queries;
using UserService.Application.Queries.Handlers;
using UserService.Infrastructure.EntityFramework.Contexts;

namespace UserService.Infrastructure.Queries.Handlers
{
    internal sealed class GetReceivedBansByUserHandler : IQueryHandler<GetReceivedBansByUserQuery, List<ReceivedBanByUserDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetReceivedBansByUserHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<ReceivedBanByUserDto>> HandleAsync(GetReceivedBansByUserQuery query)
        {
            var bannedUserReadModels = await _readDbContext.BannedUsers
               .Where(x => x.ReceiverUserId == query.ReceiverUserId.ToString())
               .Include(x => x.Receiver)
               .AsNoTracking()
               .ToListAsync();

            return _mapper.Map<List<ReceivedBanByUserDto>>(bannedUserReadModels);
        }
    }
}
