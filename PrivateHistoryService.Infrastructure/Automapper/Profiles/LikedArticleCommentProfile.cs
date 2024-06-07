using AutoMapper;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class LikedArticleCommentProfile : Profile
    {
        public LikedArticleCommentProfile()
        {
            CreateMap<LikedArticleCommentReadModel, LikedArticleCommentDto>();

            CreateMap<LikedArticleCommentDto, LikedArticleComment>()
               .ConstructUsing(source =>
                   new LikedArticleComment(Guid.Parse(source.ArticleId), Guid.Parse(source.UserId), source.ArticleCommentId, source.DateTime.ToUtc()));
        }
    }
}
