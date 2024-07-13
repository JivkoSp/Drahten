using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Application.Dtos.PrivateHistoryService;
using TopicArticleService.Application.Exceptions;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Application.Commands.Handlers
{
    internal sealed class AddArticleLikeHandler : ICommandHandler<AddArticleLikeCommand>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMessageBusPublisher _messageBusPublisher;

        public AddArticleLikeHandler(IArticleRepository articleRepository, IMessageBusPublisher messageBusPublisher)
        {
            _articleRepository = articleRepository;
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
            await _messageBusPublisher.PublishLikedArticleAsync(likedArticleDto);

            var articleLike = new ArticleLike(command.ArticleId, command.UserId, command.DateTime.ToUniversalTime());
                
            article.AddLike(articleLike);

            await _articleRepository.UpdateArticleAsync(article);
        }
    }
}
