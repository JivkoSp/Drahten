using AutoMapper;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Profiles
{
    public class SearchedTopicDataHistProfile : Profile
    {
        public SearchedTopicDataHistProfile()
        {
            CreateMap<WriteSearchedTopicDataHistoryDto, SearchedTopicDataPrivateHist>()
                .ForMember(dest => dest.PrivateHistoryId, options => options.MapFrom(source => source.HistoryId));

            CreateMap<WriteSearchedTopicDataHistoryDto, SearchedTopicDataPublicHist>()
                .ForMember(dest => dest.PublicHistoryId, options => options.MapFrom(source => source.HistoryId));

            CreateMap<SearchedTopicDataPrivateHist, ReadSearchedTopicDataHistoryDto>()
                 .ForMember(dest => dest.HistoryId, options => options.MapFrom(source => source.PrivateHistoryId));

            CreateMap<SearchedTopicDataPublicHist, ReadSearchedTopicDataHistoryDto>()
                .ForMember(dest => dest.HistoryId, options => options.MapFrom(source => source.PublicHistoryId));
        }
    }
}
