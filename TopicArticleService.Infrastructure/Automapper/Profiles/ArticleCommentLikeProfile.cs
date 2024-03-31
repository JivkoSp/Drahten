﻿using AutoMapper;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.Automapper.Profiles
{
    internal class ArticleCommentLikeProfile : Profile
    {
        public ArticleCommentLikeProfile()
        {
            CreateMap<ArticleCommentLikeReadModel, ArticleCommentLikeDto>();
        }
    }
}
