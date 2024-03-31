﻿using AutoMapper;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.Automapper.Profiles
{
    internal class ArticleCommentDislikeProfile : Profile
    {
        public ArticleCommentDislikeProfile()
        {
            CreateMap<ArticleCommentDislikeReadModel, ArticleCommentDislikeDto>();
        }
    }
}
