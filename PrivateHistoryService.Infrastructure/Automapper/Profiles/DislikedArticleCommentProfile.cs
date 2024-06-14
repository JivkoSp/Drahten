using AutoMapper;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class DislikedArticleCommentProfile : Profile
    {
        public DislikedArticleCommentProfile()
        {
            CreateMap<DislikedArticleCommentReadModel, DislikedArticleCommentDto>()
                .ForMember(dest => dest.RetentionUntil, options => options.MapFrom(source => source.User.RetentionUntil));

            CreateMap<DislikedArticleCommentDto, DislikedArticleComment>()
              .ConstructUsing(source =>
                  new DislikedArticleComment(Guid.Parse(source.ArticleId), Guid.Parse(source.UserId), source.ArticleCommentId, source.DateTime.ToUtc()));
        }
    }
}
