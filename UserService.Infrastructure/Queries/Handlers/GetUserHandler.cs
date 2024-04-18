using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Dtos;
using UserService.Application.Queries;
using UserService.Application.Queries.Handlers;
using UserService.Infrastructure.EntityFramework.Contexts;

namespace UserService.Infrastructure.Queries.Handlers
{
    internal sealed class GetUserHandler : IQueryHandler<GetUserQuery, UserDto>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetUserHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<UserDto> HandleAsync(GetUserQuery query)
        {
            var userReadModel = await _readDbContext.Users
                .Where(x => x.UserId == query.UserId.ToString())
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return _mapper.Map<UserDto>(userReadModel);
        }
    }
}
