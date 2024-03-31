using TopicArticleService.Application.Exceptions;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;

namespace TopicArticleService.Application.Commands.Handlers
{
    public class AddArticleCommentLikeHandler : ICommandHandler<AddArticleCommentLikeCommand>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCommentLikeFactory _articleCommentLikeFactory;

        public AddArticleCommentLikeHandler(IArticleRepository articleRepository, IArticleCommentLikeFactory articleCommentLikeFactory)
        {
            _articleRepository = articleRepository;
            _articleCommentLikeFactory = articleCommentLikeFactory;
        }

        public async Task HandleAsync(AddArticleCommentLikeCommand command)
        {
            var article = await _articleRepository.GetArticleByIdAsync(command.ArticleId);

            if (article == null)
            {
                throw new ArticleNotFoundException(command.ArticleId);
            }

            var articleComment = article.ArticleComments.SingleOrDefault(x => x.Id.Value == command.ArticleCommentId);

            if(articleComment == null)
            {
                throw new ArticleCommentNotFoundException(command.ArticleCommentId);
            }

            var articleCommentLike = _articleCommentLikeFactory.Create(command.ArticleCommentId, command.UserId, command.DateTime);

            articleComment.AddLike(articleCommentLike);

            await _articleRepository.UpdateArticleAsync(article);
        }
    }
}
