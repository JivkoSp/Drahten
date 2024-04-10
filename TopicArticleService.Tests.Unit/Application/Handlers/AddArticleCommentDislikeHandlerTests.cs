using NSubstitute;
using TopicArticleService.Application.Commands.Handlers;
using TopicArticleService.Application.Commands;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Domain.ValueObjects;
using Xunit;
using TopicArticleService.Domain.Entities;
using Shouldly;
using TopicArticleService.Application.Exceptions;

namespace TopicArticleService.Tests.Unit.Application.Handlers
{
    public class AddArticleCommentDislikeHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IArticleCommentRepository _articleCommentRepository;
        private readonly IArticleCommentDislikeFactory _articleCommentDislikeMockFactory;
        private readonly IArticleCommentFactory _articleCommentConcreteFactory;
        private readonly ICommandHandler<AddArticleCommentDislikeCommand> _handler;

        private AddArticleCommentDislikeCommand GetAddArticleCommentDislikeCommand(ArticleCommentID articleCommentId)
        {
            var command = new AddArticleCommentDislikeCommand(articleCommentId, DateTimeOffset.Now, Guid.NewGuid());

            return command;
        }

        public AddArticleCommentDislikeHandlerTests()
        {
            _articleCommentRepository = Substitute.For<IArticleCommentRepository>();
            _articleCommentDislikeMockFactory = Substitute.For<IArticleCommentDislikeFactory>();
            _articleCommentConcreteFactory = new ArticleCommentFactory();
            _handler = new AddArticleCommentDislikeHandler(_articleCommentRepository, _articleCommentDislikeMockFactory);
        }

        #endregion

        private Task Act(AddArticleCommentDislikeCommand command)
          => _handler.HandleAsync(command);

        //Should throw ArticleCommentNotFoundException when the following condition is met:
        //There is no ArticleComment returned from the repository.
        //The reason can be that there is no ArticleComment that has ArticleCommentId like the ArticleCommentId from
        //the AddArticleCommentDislikeCommand or any other reason like network problems for example.  
        [Fact]
        public async Task Throws_ArticleCommentNotFoundException_When_ArticleComment_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var articleComment = _articleCommentConcreteFactory.Create(Guid.NewGuid(), "some comment", DateTimeOffset.Now, 
                Guid.NewGuid(), null);

            var addArticleCommentDislikeCommand = GetAddArticleCommentDislikeCommand(articleComment.Id);

            _articleCommentRepository.GetArticleCommentByIdAsync(addArticleCommentDislikeCommand.ArticleCommentId)
                .Returns(default(ArticleComment));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addArticleCommentDislikeCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ArticleCommentNotFoundException>();
        }

        //Should create new ArticleCommentDislike value object if the ArticleCommentId from the AddArticleCommentDislikeCommand
        //is valid Id for existing ArticleComment domain entity.
        //Additionally the created ArticleCommentDislike value object must be added to the ArticleComment domain entity and the repository
        //must be called to update the ArticleComment entity.
        [Fact]
        public async Task Given_Valid_ArticleCommentId_Creates_And_Adds_ArticleCommentDislike_Instance_To_ArticleComment_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var articleComment = _articleCommentConcreteFactory.Create(Guid.NewGuid(), "some comment", DateTimeOffset.Now, 
                Guid.NewGuid(), null);

            var addArticleCommentDislikeCommand = GetAddArticleCommentDislikeCommand(articleComment.Id);

            _articleCommentRepository.GetArticleCommentByIdAsync(addArticleCommentDislikeCommand.ArticleCommentId)
                .Returns(articleComment);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addArticleCommentDislikeCommand));

            //ASSERT
            exception.ShouldBeNull();

            _articleCommentDislikeMockFactory.Received(1).Create(addArticleCommentDislikeCommand.ArticleCommentId,
                addArticleCommentDislikeCommand.UserId, addArticleCommentDislikeCommand.DateTime);

            await _articleCommentRepository.Received(1).UpdateArticleCommentAsync(articleComment);
        }
    }
}
