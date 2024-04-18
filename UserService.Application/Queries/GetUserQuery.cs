using UserService.Application.Dtos;

namespace UserService.Application.Queries
{
    public record GetUserQuery(Guid UserId) : IQuery<UserDto>;
}
