using AutoMapper;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Infrastructure.EntityFramework.Models;

namespace PublicHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class SearchedTopicDataProfile : Profile
    {
        public SearchedTopicDataProfile()
        {
            CreateMap<SearchedTopicDataReadModel, SearchedTopicDataDto>();
        }
    }
}
