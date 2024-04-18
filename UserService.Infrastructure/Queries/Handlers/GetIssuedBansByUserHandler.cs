using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Dtos;
using UserService.Application.Queries;
using UserService.Application.Queries.Handlers;
using UserService.Infrastructure.EntityFramework.Contexts;

namespace UserService.Infrastructure.Queries.Handlers
{
    internal sealed class GetIssuedBansByUserHandler : IQueryHandler<GetIssuedBansByUserQuery, List<IssuedBanByUserDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetIssuedBansByUserHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<IssuedBanByUserDto>> HandleAsync(GetIssuedBansByUserQuery query)
        {
            var bannedUserReadModels = await _readDbContext.BannedUsers
               .Where(x => x.IssuerUserId == query.IssuerUserId.ToString())
               .Include(x => x.Issuer)
               .AsNoTracking()
               .ToListAsync();

            return _mapper.Map<List<IssuedBanByUserDto>>(bannedUserReadModels);
        }
    }
}
