using AutoMapper;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class LikedArticleProfile : Profile
    {
        public LikedArticleProfile()
        {
            CreateMap<LikedArticleReadModel, LikedArticleDto>();

            CreateMap<LikedArticleDto, LikedArticle>()
                .ConstructUsing(source =>
                    new LikedArticle(Guid.Parse(source.ArticleId), Guid.Parse(source.UserId), source.DateTime.ToUtc()));
        }
    }
}
