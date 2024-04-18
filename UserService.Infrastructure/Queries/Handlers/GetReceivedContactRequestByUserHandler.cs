using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Dtos;
using UserService.Application.Queries;
using UserService.Application.Queries.Handlers;
using UserService.Infrastructure.EntityFramework.Contexts;

namespace UserService.Infrastructure.Queries.Handlers
{
    internal sealed class GetReceivedContactRequestByUserHandler
        : IQueryHandler<GetReceivedContactRequestByUserQuery, List<ReceivedContactRequestByUserDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetReceivedContactRequestByUserHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<ReceivedContactRequestByUserDto>> HandleAsync(GetReceivedContactRequestByUserQuery query)
        {
            var contactRequestReadModels = await _readDbContext.ContactRequests
               .Where(x => x.ReceiverUserId == query.ReceiverUserId.ToString())
               .Include(x => x.Receiver)
               .AsNoTracking()
               .ToListAsync();

            return _mapper.Map<List<ReceivedContactRequestByUserDto>>(contactRequestReadModels);
        }
    }
}
