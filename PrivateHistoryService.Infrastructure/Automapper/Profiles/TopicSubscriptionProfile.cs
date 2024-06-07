using AutoMapper;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class TopicSubscriptionProfile : Profile
    {
        public TopicSubscriptionProfile()
        {
            CreateMap<TopicSubscriptionReadModel, TopicSubscriptionDto>();

            CreateMap<TopicSubscriptionDto, TopicSubscription>()
                .ConstructUsing(source =>
                    new TopicSubscription(source.TopicId, Guid.Parse(source.UserId), source.DateTime.ToUtc()));
        }
    }
}
