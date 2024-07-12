using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Application.Dtos.PrivateHistoryService;
using TopicArticleService.Application.Exceptions;
using TopicArticleService.Application.Extensions;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;

namespace TopicArticleService.Application.Commands.Handlers
{
    internal sealed class AddArticleDislikeHandler : ICommandHandler<AddArticleDislikeCommand>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleDislikeFactory _articleDislikeFactory;
        private readonly IMessageBusPublisher _messageBusPublisher;

        public AddArticleDislikeHandler(IArticleRepository articleRepository, IArticleDislikeFactory articleDislikeFactory,
            IMessageBusPublisher messageBusPublisher)
        {
            _articleRepository = articleRepository;
            _articleDislikeFactory = articleDislikeFactory;
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
            _messageBusPublisher.PublishDislikedArticle(dislikedArticleDto);

            var articleDislike = _articleDislikeFactory.Create(command.ArticleId, command.UserId, command.DateTime.ToUniversalTime());

            article.AddDislike(articleDislike);

            await _articleRepository.UpdateArticleAsync(article);
        }
    }
}
