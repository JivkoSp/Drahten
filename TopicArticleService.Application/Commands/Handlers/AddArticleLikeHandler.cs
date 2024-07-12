using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Application.Dtos.PrivateHistoryService;
using TopicArticleService.Application.Exceptions;
using TopicArticleService.Application.Extensions;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;

namespace TopicArticleService.Application.Commands.Handlers
{
    internal sealed class AddArticleLikeHandler : ICommandHandler<AddArticleLikeCommand>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleLikeFactory _articleLikeFactory;
        private readonly IMessageBusPublisher _messageBusPublisher;

        public AddArticleLikeHandler(IArticleRepository articleRepository, IArticleLikeFactory articleLikeFactory,
            IMessageBusPublisher messageBusPublisher)
        {
            _articleRepository = articleRepository;
            _articleLikeFactory = articleLikeFactory;
            _messageBusPublisher = messageBusPublisher;
        }

        public async Task HandleAsync(AddArticleLikeCommand command)
        {
            var article = await _articleRepository.GetArticleByIdAsync(command.ArticleId);

            if (article == null)
            {
                throw new ArticleNotFoundException(command.ArticleId);
            }

            var likedArticleDto = new LikedArticleDto
            {
                ArticleId = command.ArticleId.ToString(),
                UserId = command.UserId.ToString(),
                DateTime = command.DateTime,
                Event = "LikedArticle"
            };

            //Post message to the message broker about adding like for article with ID: ArticleId by user with ID: UserId.
            _messageBusPublisher.PublishLikedArticle(likedArticleDto);

            var articleLike = _articleLikeFactory.Create(command.ArticleId, command.UserId, command.DateTime.ToUniversalTime());

            article.AddLike(articleLike);

            await _articleRepository.UpdateArticleAsync(article);
        }
    }
}
