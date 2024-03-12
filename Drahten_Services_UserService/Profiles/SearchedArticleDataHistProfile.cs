using AutoMapper;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Profiles
{
    public class SearchedArticleDataHistProfile : Profile
    {
        public SearchedArticleDataHistProfile()
        {
            CreateMap<WriteSearchedArticleDataHistoryDto, SearchedArticleDataPrivateHist>()
                .ForMember(dest => dest.PrivateHistoryId, options => options.MapFrom(source => source.HistoryId));
            
            CreateMap<WriteSearchedArticleDataHistoryDto, SearchedArticleDataPublicHist>()
                .ForMember(dest => dest.PublicHistoryId, options => options.MapFrom(source => source.HistoryId));

            CreateMap<SearchedArticleDataPrivateHist, ReadSearchedArticleDataHistoryDto>()
                .ForMember(dest => dest.HistoryId, options => options.MapFrom(source => source.PrivateHistoryId));

            CreateMap<SearchedArticleDataPublicHist, ReadSearchedArticleDataHistoryDto>()
                .ForMember(dest => dest.HistoryId, options => options.MapFrom(source => source.PublicHistoryId));
        }
    }
}
