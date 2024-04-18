using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Dtos;
using UserService.Application.Queries;
using UserService.Application.Queries.Handlers;
using UserService.Infrastructure.EntityFramework.Contexts;

namespace UserService.Infrastructure.Queries.Handlers
{
    internal sealed class GetIssuedContactRequestsByUserHandler 
        : IQueryHandler<GetIssuedContactRequestsByUserQuery, List<IssuedContactRequestByUserDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetIssuedContactRequestsByUserHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<IssuedContactRequestByUserDto>> HandleAsync(GetIssuedContactRequestsByUserQuery query)
        {
            var contactRequestReadModels = await _readDbContext.ContactRequests
               .Where(x => x.IssuerUserId == query.IssuerUserId.ToString())
               .Include(x => x.Issuer)
               .AsNoTracking()
               .ToListAsync();

            return _mapper.Map<List<IssuedContactRequestByUserDto>>(contactRequestReadModels);
        }
    }
}
