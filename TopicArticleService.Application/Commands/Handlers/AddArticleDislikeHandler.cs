using TopicArticleService.Application.Exceptions;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;

namespace TopicArticleService.Application.Commands.Handlers
{
    internal sealed class AddArticleDislikeHandler : ICommandHandler<AddArticleDislikeCommand>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleDislikeFactory _articleDislikeFactory;

        internal AddArticleDislikeHandler(IArticleRepository articleRepository, IArticleDislikeFactory articleDislikeFactory)
        {
            _articleRepository = articleRepository;
            _articleDislikeFactory = articleDislikeFactory;
        }

        public async Task HandleAsync(AddArticleDislikeCommand command)
        {
            var article = await _articleRepository.GetArticleByIdAsync(command.ArticleId);

            if (article == null)
            {
                throw new ArticleNotFoundException(command.ArticleId);
            }

            var articleDislike = _articleDislikeFactory.Create(command.ArticleId, command.UserId, command.DateTime);

            article.AddDislike(articleDislike);

            await _articleRepository.UpdateArticleAsync(article);
        }
    }
}
