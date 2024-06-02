using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Application.Services.ReadServices;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class RemoveViewedArticleHandler : ICommandHandler<RemoveViewedArticleCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IViewedArticleReadService _viewedArticleReadService;

        public RemoveViewedArticleHandler(IUserRepository userRepository, IViewedArticleReadService viewedArticleReadService)
        {
            _userRepository = userRepository;
            _viewedArticleReadService = viewedArticleReadService;
        }

        public async Task HandleAsync(RemoveViewedArticleCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var viewedArticleDto = await _viewedArticleReadService.GetViewedArticleByIdAsync(command.ViewedArticleId);

            if (viewedArticleDto == null)
            {
                throw new ViewedArticleNotFoundException(command.ViewedArticleId);
            }

            var viewedArticle = new ViewedArticle(Guid.Parse(viewedArticleDto.ArticleId),
                Guid.Parse(viewedArticleDto.UserId), viewedArticleDto.DateTime);

            user.RemoveViewedArticle(viewedArticle);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
