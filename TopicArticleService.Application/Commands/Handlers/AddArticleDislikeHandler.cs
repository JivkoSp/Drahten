﻿using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Application.Dtos.PrivateHistoryService;
using TopicArticleService.Application.Exceptions;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Application.Commands.Handlers
{
    internal sealed class AddArticleDislikeHandler : ICommandHandler<AddArticleDislikeCommand>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMessageBusPublisher _messageBusPublisher;

        public AddArticleDislikeHandler(IArticleRepository articleRepository, IMessageBusPublisher messageBusPublisher)
        {
            _articleRepository = articleRepository;
            _messageBusPublisher = messageBusPublisher;
        }

        public async Task HandleAsync(AddArticleDislikeCommand command)
        {
            var article = await _articleRepository.GetArticleByIdAsync(command.ArticleId);

            if (article == null)
            {
                throw new ArticleNotFoundException(command.ArticleId);
            }

            var dislikedArticleDto = new DislikedArticleDto
            {
                ArticleId = command.ArticleId.ToString(),
                UserId = command.UserId.ToString(),
                DateTime = command.DateTime,
                Event = "DislikedArticle"
            };

            //Post message to the message broker about adding dislike for article with ID: ArticleId by user with ID: UserId.
            await _messageBusPublisher.PublishDislikedArticleAsync(dislikedArticleDto);

            var articleDislike = new ArticleDislike(command.ArticleId, command.UserId, command.DateTime.ToUniversalTime());

            article.AddDislike(articleDislike);

            await _articleRepository.UpdateArticleAsync(article);
        }
    }
}
