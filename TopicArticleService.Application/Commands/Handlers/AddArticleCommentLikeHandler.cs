using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Application.Dtos.PrivateHistoryService;
using TopicArticleService.Application.Exceptions;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Application.Commands.Handlers
{
    internal sealed class AddArticleCommentLikeHandler : ICommandHandler<AddArticleCommentLikeCommand>
    {
        private readonly IArticleCommentRepository _articleCommentRepository;
        private readonly IArticleCommentReadService _articleCommentReadService;
        private readonly IMessageBusPublisher _messageBusPublisher;

        public AddArticleCommentLikeHandler(IArticleCommentRepository articleCommentRepository, 
            IArticleCommentReadService articleCommentReadService, IMessageBusPublisher messageBusPublisher)
        {
            _articleCommentRepository = articleCommentRepository;
            _articleCommentReadService = articleCommentReadService;
            _messageBusPublisher = messageBusPublisher;
        }

        public async Task HandleAsync(AddArticleCommentLikeCommand command)
        {
            var articleComment = await _articleCommentRepository.GetArticleCommentByIdAsync(command.ArticleCommentId);

            if(articleComment == null)
            {
                throw new ArticleCommentNotFoundException(command.ArticleCommentId);
            }

            var articleCommentDto = await _articleCommentReadService.GetArticleCommentByIdAsync(command.ArticleCommentId);

            var likedArticleCommentDto = new LikedArticleCommentDto
            {
                ArticleCommentId = command.ArticleCommentId,
                ArticleId = articleCommentDto.ArticleDto.ArticleId,
                UserId = command.UserId.ToString(),
                DateTime = command.DateTime,
                Event = "LikedArticleComment"
            };

            //Post message to the message broker about adding like for article-comment with ID: ArticleCommentId by user with ID: UserId.
            await _messageBusPublisher.PublishLikedArticleCommentAsync(likedArticleCommentDto);

            var articleCommentLike = new ArticleCommentLike(command.ArticleCommentId, command.UserId,
                command.DateTime.ToUniversalTime());
                
            articleComment.AddLike(articleCommentLike);

            await _articleCommentRepository.UpdateArticleCommentAsync(articleComment);
        }
    }
}
