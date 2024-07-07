using NSubstitute;
using PublicHistoryService.Application.Commands;
using PublicHistoryService.Application.Commands.Handlers;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Application.Exceptions;
using PublicHistoryService.Application.Services.ReadServices;
using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.Factories;
using PublicHistoryService.Domain.Factories.Interfaces;
using PublicHistoryService.Domain.Repositories;
using PublicHistoryService.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace PublicHistoryService.Tests.Unit.Application.Handlers
{
    public sealed class RemoveViewedArticleHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly IViewedArticleReadService _viewedArticleReadService;
        private readonly ICommandHandler<RemoveViewedArticleCommand> _handler;

        private RemoveViewedArticleCommand GetRemoveViewedArticleCommand()
        {
            var command = new RemoveViewedArticleCommand(UserId: Guid.NewGuid(), ViewedArticleId: Guid.NewGuid());

            return command;
        }

        private ViewedArticle GetViewedArticle()
        {
            var viewedArticle = new ViewedArticle(articleId: Guid.NewGuid(), userId: Guid.NewGuid(), dateTime: DateTimeOffset.Now);

            return viewedArticle;
        }

        private ViewedArticleDto GetViewedArticleDto(ViewedArticle viewedArticle)
        {
            var viewedArticleDto = new ViewedArticleDto
            {
                ViewedArticleId = Guid.NewGuid(),
                ArticleId = viewedArticle.ArticleID.Value.ToString(),
                UserId = viewedArticle.UserID.Value.ToString(),
                DateTime = viewedArticle.DateTime
            };

            return viewedArticleDto;
        }

        public RemoveViewedArticleHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _viewedArticleReadService = Substitute.For<IViewedArticleReadService>();
            _handler = new RemoveViewedArticleHandler(_userRepository, _viewedArticleReadService);
        }

        #endregion

        private Task Act(RemoveViewedArticleCommand command)
           => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the UserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_UserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var removeViewedArticleCommand = GetRemoveViewedArticleCommand();

            _userRepository.GetUserByIdAsync(removeViewedArticleCommand.UserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeViewedArticleCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should throw ViewedArticleNotFoundException when the following condition is met:
        //There is no ViewedArticle returned from the repository that corresponds to the ViewedArticleId from the RemoveViewedArticleCommand.
        [Fact]
        public async Task Throws_ViewedArticleNotFoundException_When_ViewedArticle_With_Given_ViewedArticleId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var removeViewedArticleCommand = GetRemoveViewedArticleCommand();

            _userRepository.GetUserByIdAsync(removeViewedArticleCommand.UserId).Returns(user);

            _viewedArticleReadService.GetViewedArticleByIdAsync(removeViewedArticleCommand.ViewedArticleId)
               .Returns(default(ViewedArticleDto));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeViewedArticleCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ViewedArticleNotFoundException>();
        }

        //Should remove ViewedArticle value object if the UserId and ViewedArticleId from the RemoveViewedArticleCommand
        //are valid Id's for an existing User domain entity and ViewedArticle value object.
        //The repository must be called to update the User.
        [Fact]
        public async Task Given_Valid_UserId_And_ViewedArticleId_Removes_ViewedArticle_Instance_From_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var viewedArticle = GetViewedArticle();

            var viewedArticleDto = GetViewedArticleDto(viewedArticle);

            var removeViewedArticleCommand = GetRemoveViewedArticleCommand();

            user.AddViewedArticle(viewedArticle);

            _userRepository.GetUserByIdAsync(removeViewedArticleCommand.UserId).Returns(user);

            _viewedArticleReadService.GetViewedArticleByIdAsync(removeViewedArticleCommand.ViewedArticleId)
                .Returns(viewedArticleDto);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeViewedArticleCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(user);
        }
    }
}
