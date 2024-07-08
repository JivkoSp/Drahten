using AutoMapper;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Domain.ValueObjects;
using PublicHistoryService.Infrastructure.EntityFramework.Models;

namespace PublicHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class CommentedArticleProfile : Profile
    {
        public CommentedArticleProfile()
        {
            CreateMap<CommentedArticleReadModel, CommentedArticleDto>();

            CreateMap<CommentedArticleDto, CommentedArticle>()
             .ConstructUsing(source =>
                 new CommentedArticle(Guid.Parse(source.ArticleId), Guid.Parse(source.UserId), source.ArticleComment, source.DateTime.ToUniversalTime()));
        }
    }
}
