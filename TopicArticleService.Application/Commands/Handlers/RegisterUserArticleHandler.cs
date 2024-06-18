using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Application.Dtos.PrivateHistoryService;
using TopicArticleService.Application.Exceptions;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;

namespace TopicArticleService.Application.Commands.Handlers
{
    internal sealed class RegisterUserArticleHandler : ICommandHandler<RegisterUserArticleCommand>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserArticleFactory _userArticleFactory;
        private readonly IMessageBusPublisher _messageBusPublisher;

        public RegisterUserArticleHandler(IArticleRepository articleRepository, IUserArticleFactory userArticleFactory,
            IMessageBusPublisher messageBusPublisher)
        {
            _articleRepository = articleRepository;
            _userArticleFactory = userArticleFactory;
            _messageBusPublisher = messageBusPublisher;
        }

        public async Task HandleAsync(RegisterUserArticleCommand command)
        {
            var article = await _articleRepository.GetArticleByIdAsync(command.ArticleId);

            if(article == null)
            {
                throw new ArticleNotFoundException(command.ArticleId);
            }

            var viewedArticleDto = new ViewedArticleDto
            {
                ArticleId = command.ArticleId.ToString(),
                UserId = command.UserId.ToString(),
                DateTime = DateTimeOffset.Now,
                Event = "ViewedArticle"
            };

            //Post message to the message broker about visiting the article with ID: ArticleId.
            await _messageBusPublisher.PublishViewedArticleAsync(viewedArticleDto);

            var userArticle = _userArticleFactory.Create(command.UserId, command.ArticleId);

            article.AddUserArticle(userArticle);

            await _articleRepository.UpdateArticleAsync(article);
        }
    }
}
