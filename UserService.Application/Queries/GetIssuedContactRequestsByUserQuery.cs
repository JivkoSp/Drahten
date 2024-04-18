using UserService.Application.Dtos;

namespace UserService.Application.Queries
{
    public record GetIssuedContactRequestsByUserQuery(Guid IssuerUserId) : IQuery<List<IssuedContactRequestByUserDto>>;
}
