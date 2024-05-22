using AutoMapper;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class TopicSubscriptionProfile : Profile
    {
        internal TopicSubscriptionProfile()
        {
            CreateMap<TopicSubscriptionReadModel, TopicSubscriptionDto>();
        }
    }
}
