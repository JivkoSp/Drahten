using TopicArticleService.Application.Exceptions;
using TopicArticleService.Domain.Repositories;

namespace TopicArticleService.Application.Commands.Handlers
{
    internal sealed class RemoveArticleCommentHandler : ICommandHandler<RemoveArticleCommentCommand>
    {
        private IArticleRepository _articleRepository;

        internal RemoveArticleCommentHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task HandleAsync(RemoveArticleCommentCommand command)
        {
            var article = await _articleRepository.GetArticleByIdAsync(command.ArticleId);

            if (article == null)
            {
                throw new ArticleNotFoundException(command.ArticleId);
            }

            article.RemoveComment(command.ArticleCommentId, command.ParentArticleCommentId);

            await _articleRepository.UpdateArticleAsync(article);
        }
    }
}
