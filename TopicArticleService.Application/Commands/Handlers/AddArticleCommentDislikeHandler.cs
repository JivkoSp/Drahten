using TopicArticleService.Application.Exceptions;
using TopicArticleService.Application.Extensions;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;

namespace TopicArticleService.Application.Commands.Handlers
{
    internal sealed class AddArticleCommentDislikeHandler : ICommandHandler<AddArticleCommentDislikeCommand>
    {
        private readonly IArticleCommentRepository _articleCommentRepository;
        private readonly IArticleCommentDislikeFactory _articleCommentDislikeFactory;

        public AddArticleCommentDislikeHandler(IArticleCommentRepository articleCommentRepository, 
                IArticleCommentDislikeFactory articleCommentDislikeFactory)
        {
            _articleCommentRepository = articleCommentRepository;
            _articleCommentDislikeFactory = articleCommentDislikeFactory;
        }

        public async Task HandleAsync(AddArticleCommentDislikeCommand command)
        {
            var articleComment = await _articleCommentRepository.GetArticleCommentByIdAsync(command.ArticleCommentId);

            if (articleComment == null)
            {
                throw new ArticleCommentNotFoundException(command.ArticleCommentId);
            }

            var articleCommentDislike = _articleCommentDislikeFactory.Create(command.ArticleCommentId, 
                command.UserId, command.DateTime.ToUtc());

            articleComment.AddDislike(articleCommentDislike);

            await _articleCommentRepository.UpdateArticleCommentAsync(articleComment);
        }
    }
}
