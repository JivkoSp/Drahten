using TopicArticleService.Application.Exceptions;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;

namespace TopicArticleService.Application.Commands.Handlers
{
    public sealed class AddArticleCommentHandler : ICommandHandler<AddArticleCommentCommand>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCommentFactory _articleCommentFactory;

        public AddArticleCommentHandler(IArticleRepository articleRepository, IArticleCommentFactory articleCommentFactory)
        {
            _articleRepository = articleRepository;
            _articleCommentFactory = articleCommentFactory;
        }

        public async Task HandleAsync(AddArticleCommentCommand command)
        {
            var article = await _articleRepository.GetArticleByIdAsync(command.ArticleId);

            if (article == null)
            {
                throw new ArticleNotFoundException(command.ArticleId);
            }

            var articleComment = _articleCommentFactory.Create(command.ArticleCommentId, command.CommentValue, command.DateTime, 
                    command.UserId, command.ParentArticleCommentId);

            article.AddComment(articleComment);

            await _articleRepository.UpdateArticleAsync(article);
        }
    }
}
