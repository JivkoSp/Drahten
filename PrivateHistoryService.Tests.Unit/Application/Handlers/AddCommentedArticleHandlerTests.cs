using NSubstitute;
using PrivateHistoryService.Application.Commands;
using PrivateHistoryService.Application.Commands.Handlers;
using PrivateHistoryService.Domain.Factories;
using PrivateHistoryService.Domain.Factories.Interfaces;
using PrivateHistoryService.Domain.Repositories;

namespace PrivateHistoryService.Tests.Unit.Application.Handlers
{
    public sealed class AddCommentedArticleHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly ICommandHandler<AddCommentedArticleCommand> _handler;

        private AddCommentedArticleCommand GetAddCommentedArticleCommand()
        {
            var command = new AddCommentedArticleCommand(ArticleId: Guid.NewGuid(), UserId: Guid.NewGuid(), 
                ArticleComment: "", DateTime: DateTimeOffset.Now);

            return command;
        }

        public AddCommentedArticleHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new AddCommentedArticleHandler(_userRepository);
        }

        #endregion
    }
}
