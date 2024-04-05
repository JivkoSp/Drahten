using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;

namespace TopicArticleService.Application.Commands.Handlers
{
    internal sealed class RegisterUserArticleHandler : ICommandHandler<RegisterUserArticleCommand>
    {
        private IArticleRepository _articleRepository;
        private IUserArticleFactory _userArticleFactory;

        internal RegisterUserArticleHandler(IArticleRepository articleRepository, IUserArticleFactory userArticleFactory)
        {
            _articleRepository = articleRepository;
            _userArticleFactory = userArticleFactory;
        }

        public async Task HandleAsync(RegisterUserArticleCommand command)
        {
            var article = await _articleRepository.GetArticleByIdAsync(command.ArticleId);

            if(article == null)
            {
                //TODO: throw exception
            }

            var userArticle = _userArticleFactory.Create(command.UserId, command.ArticleId);

            article.AddUserArticle(userArticle);

            await _articleRepository.UpdateArticleAsync(article);
        }
    }
}
