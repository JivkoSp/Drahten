using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Factories.Interfaces
{
    public interface IUserFactory
    {
        User Create(UserID userId);
    }
}
