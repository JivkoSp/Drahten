using AutoMapper;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Profiles
{
    public class ArticleCommentThumbsUpProfile : Profile
    {
        public ArticleCommentThumbsUpProfile()
        {
            CreateMap<WriteArticleCommentThumbsUpDto, ArticleCommentThumbsUp>();

            CreateMap<ArticleCommentThumbsUp, ReadArticleCommentThumbsUpDto>()
                .ForMember(dest => dest.ArticleId, options => options.MapFrom(source => source.ArticleComment.ArticleId));
        }
    }
}
