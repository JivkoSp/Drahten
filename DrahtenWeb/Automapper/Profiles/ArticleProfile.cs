using AutoMapper;
using DrahtenWeb.Dtos.TopicArticleService;

namespace DrahtenWeb.Automapper.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleDto, WriteArticleDto>()
                .ForMember(dest => dest.ArticleId, options => options.MapFrom(source => Guid.Parse(source.ArticleId)));
        }
    }
}
