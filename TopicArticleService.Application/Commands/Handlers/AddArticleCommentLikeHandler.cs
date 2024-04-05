using TopicArticleService.Application.Exceptions;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;

namespace TopicArticleService.Application.Commands.Handlers
{
    internal sealed class AddArticleCommentLikeHandler : ICommandHandler<AddArticleCommentLikeCommand>
    {
        private readonly IArticleCommentRepository _articleCommentRepository;
        private readonly IArticleCommentLikeFactory _articleCommentLikeFactory;

        internal AddArticleCommentLikeHandler(IArticleCommentRepository articleCommentRepository,
                IArticleCommentLikeFactory articleCommentLikeFactory)
        {
            _articleCommentRepository = articleCommentRepository;
            _articleCommentLikeFactory = articleCommentLikeFactory;
        }

        public async Task HandleAsync(AddArticleCommentLikeCommand command)
        {
            var articleComment = await _articleCommentRepository.GetArticleCommentByIdAsync(command.ArticleCommentId);

            if(articleComment == null)
            {
                throw new ArticleCommentNotFoundException(command.ArticleCommentId);
            }

            var articleCommentLike = _articleCommentLikeFactory.Create(command.ArticleCommentId, command.UserId, command.DateTime);

            articleComment.AddLike(articleCommentLike);

            await _articleCommentRepository.UpdateArticleCommentAsync(articleComment);
        }
    }
}
