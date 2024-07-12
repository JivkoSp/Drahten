using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Application.Dtos.PrivateHistoryService;
using TopicArticleService.Application.Exceptions;
using TopicArticleService.Application.Extensions;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;

namespace TopicArticleService.Application.Commands.Handlers
{
    internal sealed class AddArticleCommentDislikeHandler : ICommandHandler<AddArticleCommentDislikeCommand>
    {
        private readonly IArticleCommentRepository _articleCommentRepository;
        private readonly IArticleCommentReadService _articleCommentReadService;
        private readonly IArticleCommentDislikeFactory _articleCommentDislikeFactory;
        private readonly IMessageBusPublisher _messageBusPublisher;

        public AddArticleCommentDislikeHandler(IArticleCommentRepository articleCommentRepository, IArticleCommentReadService articleCommentReadService,
                IArticleCommentDislikeFactory articleCommentDislikeFactory, IMessageBusPublisher messageBusPublisher)
        {
            _articleCommentRepository = articleCommentRepository;
            _articleCommentReadService = articleCommentReadService;
            _articleCommentDislikeFactory = articleCommentDislikeFactory;
            _messageBusPublisher = messageBusPublisher;
        }

        public async Task HandleAsync(AddArticleCommentDislikeCommand command)
        {
            var articleComment = await _articleCommentRepository.GetArticleCommentByIdAsync(command.ArticleCommentId);

            if (articleComment == null)
            {
                throw new ArticleCommentNotFoundException(command.ArticleCommentId);
            }

            var articleCommentDto = await _articleCommentReadService.GetArticleCommentByIdAsync(command.ArticleCommentId);

            var dislikedArticleCommentDto = new DislikedArticleCommentDto
            {
                ArticleCommentId = command.ArticleCommentId,
                ArticleId = articleCommentDto.ArticleDto.ArticleId,
                UserId = command.UserId.ToString(),
                DateTime = command.DateTime,
                Event = "DislikedArticleComment"
            };

            //Post message to the message broker about adding dislike for article-comment with ID: ArticleCommentId by user with ID: UserId.
            _messageBusPublisher.PublishDislikedArticleComment(dislikedArticleCommentDto);

            var articleCommentDislike = _articleCommentDislikeFactory.Create(command.ArticleCommentId, 
                command.UserId, command.DateTime.ToUniversalTime());

            articleComment.AddDislike(articleCommentDislike);

            await _articleCommentRepository.UpdateArticleCommentAsync(articleComment);
        }
    }
}
