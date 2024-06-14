using AutoMapper;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class CommentedArticleProfile : Profile
    {
        public CommentedArticleProfile()
        {
            CreateMap<CommentedArticleReadModel, CommentedArticleDto>()
                .ForMember(dest => dest.RetentionUntil, options => options.MapFrom(source => source.User.RetentionUntil));

            CreateMap<CommentedArticleDto, CommentedArticle>()
             .ConstructUsing(source =>
                 new CommentedArticle(Guid.Parse(source.ArticleId), Guid.Parse(source.UserId), source.ArticleComment, source.DateTime.ToUtc()));
        }
    }
}
