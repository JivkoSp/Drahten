using AutoMapper;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Profiles
{
    public class ArticleCommentThumbsDownProfile : Profile
    {
        public ArticleCommentThumbsDownProfile()
        {
            CreateMap<WriteArticleCommentThumbsDownDto, ArticleCommentThumbsDown>();

            CreateMap<ArticleCommentThumbsDown, ReadArticleCommentThumbsDownDto>()
                .ForMember(dest => dest.ArticleId, options => options.MapFrom(source => source.ArticleComment.ArticleId));
        }
    }
}
