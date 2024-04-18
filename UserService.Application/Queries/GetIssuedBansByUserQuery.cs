using UserService.Application.Dtos;

namespace UserService.Application.Queries
{
    public record GetIssuedBansByUserQuery(Guid IssuerUserId) : IQuery<List<IssuedBanByUserDto>>;
}
