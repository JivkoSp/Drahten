using AutoMapper;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class DislikedArticleProfile : Profile
    {
        public DislikedArticleProfile()
        {
            CreateMap<DislikedArticleReadModel, DislikedArticleDto>();

            CreateMap<DislikedArticleDto, DislikedArticle>()
                .ConstructUsing(source =>
                    new DislikedArticle(Guid.Parse(source.ArticleId), Guid.Parse(source.UserId), source.DateTime.ToUtc()));
        }
    }
}
