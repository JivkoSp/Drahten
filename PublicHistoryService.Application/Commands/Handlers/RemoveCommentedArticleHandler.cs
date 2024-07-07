using PublicHistoryService.Application.Exceptions;
using PublicHistoryService.Application.Services.ReadServices;
using PublicHistoryService.Domain.Repositories;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Application.Commands.Handlers
{
    internal sealed class RemoveCommentedArticleHandler : ICommandHandler<RemoveCommentedArticleCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICommentedArticleReadService _commentedArticleReadService;

        public RemoveCommentedArticleHandler(IUserRepository userRepository, ICommentedArticleReadService commentedArticleReadService)
        {
            _userRepository = userRepository;
            _commentedArticleReadService = commentedArticleReadService;
        }

        public async Task HandleAsync(RemoveCommentedArticleCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var commentedArticleDto = await _commentedArticleReadService.GetCommentedArticleByIdAsync(command.CommentedArticleId);

            if (commentedArticleDto == null)
            {
                throw new CommentedArticleNotFoundException(command.CommentedArticleId);
            }

            var commentedArticle = new CommentedArticle(Guid.Parse(commentedArticleDto.ArticleId),
                Guid.Parse(commentedArticleDto.UserId), commentedArticleDto.ArticleComment, commentedArticleDto.DateTime.ToUniversalTime());

            user.RemoveCommentedArticle(commentedArticle);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
