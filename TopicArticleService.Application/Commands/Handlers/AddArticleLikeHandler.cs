using TopicArticleService.Application.Exceptions;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;

namespace TopicArticleService.Application.Commands.Handlers
{
    public class AddArticleLikeHandler : ICommandHandler<AddArticleLikeCommand>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleLikeFactory _articleLikeFactory;

        public AddArticleLikeHandler(IArticleRepository articleRepository, IArticleLikeFactory articleLikeFactory)
        {
            _articleRepository = articleRepository;
            _articleLikeFactory = articleLikeFactory;
        }

        public async Task HandleAsync(AddArticleLikeCommand command)
        {
            var article = await _articleRepository.GetArticleByIdAsync(command.ArticleId);

            if (article == null)
            {
                throw new ArticleNotFoundException(command.ArticleId);
            }

            var articleLike = _articleLikeFactory.Create(command.ArticleId, command.UserId, command.DateTime);

            article.AddLike(articleLike);

            await _articleRepository.UpdateArticleAsync(article);
        }
    }
}
