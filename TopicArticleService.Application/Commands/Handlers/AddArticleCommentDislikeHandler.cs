using TopicArticleService.Application.Exceptions;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;

namespace TopicArticleService.Application.Commands.Handlers
{
    public class AddArticleCommentDislikeHandler : ICommandHandler<AddArticleCommentDislikeCommand>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCommentDislikeFactory _articleCommentDislikeFactory;

        public AddArticleCommentDislikeHandler(IArticleRepository articleRepository, IArticleCommentDislikeFactory articleCommentDislikeFactory)
        {
            _articleRepository = articleRepository;
            _articleCommentDislikeFactory = articleCommentDislikeFactory;
        }

        public async Task HandleAsync(AddArticleCommentDislikeCommand command)
        {
            var article = await _articleRepository.GetArticleByIdAsync(command.ArticleId);

            if (article == null)
            {
                throw new ArticleNotFoundException(command.ArticleId);
            }

            var articleComment = article.ArticleComments.SingleOrDefault(x => x.Id.Value == command.ArticleCommentId);

            if (articleComment == null)
            {
                throw new ArticleCommentNotFoundException(command.ArticleCommentId);
            }

            var articleCommentDislike = _articleCommentDislikeFactory.Create(command.ArticleCommentId, command.UserId, command.DateTime);

            articleComment.AddDislike(articleCommentDislike);

            await _articleRepository.UpdateArticleAsync(article);
        }
    }
}
