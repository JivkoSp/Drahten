using UserService.Application.Dtos;

namespace UserService.Application.Queries
{
    public record GetUserQuery : IQuery<UserDto>
    {
        public string UserId { get; set; }
    }
}
