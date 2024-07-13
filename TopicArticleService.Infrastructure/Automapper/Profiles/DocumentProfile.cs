
using AutoMapper;
using TopicArticleService.Application.Dtos.SearchService;

namespace TopicArticleService.Infrastructure.Automapper.Profiles
{
    internal sealed class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<DocumentDto, Document>()
                .ForMember(dest => dest.ArticleId, options => options.MapFrom(source => source.DocumentId));
        }
    }
}
