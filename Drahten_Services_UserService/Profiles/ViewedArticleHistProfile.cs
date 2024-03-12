using AutoMapper;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Profiles
{
    public class ViewedArticleHistProfile : Profile
    {
        public ViewedArticleHistProfile()
        {
            CreateMap<WriteViewedArticleHistoryDto, ViewedArticlePrivateHist>()
                .ForMember(dest => dest.PrivateHistoryId, options => options.MapFrom(source => source.HistoryId));

            CreateMap<WriteViewedArticleHistoryDto, ViewedArticlePublicHist>()
                .ForMember(dest => dest.PublicHistoryId, options => options.MapFrom(source => source.HistoryId));

            CreateMap<ViewedArticlePrivateHist, ReadViewedArticleHistoryDto>()
                .ForMember(dest => dest.HistoryId, options => options.MapFrom(source => source.PrivateHistoryId));

            CreateMap<ViewedArticlePublicHist, ReadViewedArticleHistoryDto>()
                .ForMember(dest => dest.HistoryId, options => options.MapFrom(source => source.PublicHistoryId));
        }
    }
}
