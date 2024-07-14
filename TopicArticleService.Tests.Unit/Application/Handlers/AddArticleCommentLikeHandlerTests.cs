using NSubstitute;
using TopicArticleService.Application.Commands.Handlers;
using TopicArticleService.Application.Commands;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Application.Exceptions;
using Xunit;
using TopicArticleService.Domain.Entities;
using Shouldly;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Tests.Unit.Application.Handlers
{
    public class AddArticleCommentLikeHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IArticleCommentRepository _articleCommentRepository;
        private readonly IArticleCommentReadService _articleCommentReadService;
        private readonly IMessageBusPublisher _messageBusPublisher;
        private readonly IArticleCommentFactory _articleCommentConcreteFactory;
        private readonly ICommandHandler<AddArticleCommentLikeCommand> _handler;

        private AddArticleCommentLikeCommand GetAddArticleCommentLikeCommand(ArticleCommentID articleCommentId)
        {
            var command = new AddArticleCommentLikeCommand(articleCommentId, DateTimeOffset.Now, Guid.NewGuid());

            return command;
        }

        public AddArticleCommentLikeHandlerTests()
        {
            _articleCommentRepository = Substitute.For<IArticleCommentRepository>();
            _articleCommentReadService = Substitute.For<IArticleCommentReadService>();
            _messageBusPublisher = Substitute.For<IMessageBusPublisher>();  
            _articleCommentConcreteFactory = new ArticleCommentFactory();
            _handler = new AddArticleCommentLikeHandler(_articleCommentRepository, _articleCommentReadService, _messageBusPublisher);
        }

        #endregion

        private Task Act(AddArticleCommentLikeCommand command)
          => _handler.HandleAsync(command);

        //Should throw ArticleCommentNotFoundException when the following condition is met:
        //There is no ArticleComment returned from the repository.
        //The reason can be that there is no ArticleComment that has ArticleCommentId like the ArticleCommentId from
        //the AddArticleCommentLikeCommand or any other reason like network problems for example.  
        [Fact]
        public async Task Throws_ArticleCommentNotFoundException_When_ArticleComment_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var articleComment = _articleCommentConcreteFactory.Create(Guid.NewGuid(), "some comment", DateTimeOffset.Now, 
                Guid.NewGuid(), null);

            

            var addArticleCommentLikeCommand = GetAddArticleCommentLikeCommand(articleComment.Id);

            _articleCommentRepository.GetArticleCommentByIdAsync(addArticleCommentLikeCommand.ArticleCommentId)
                .Returns(default(ArticleComment));

             //ACT
             var exception = await Record.ExceptionAsync(async () => await Act(addArticleCommentLikeCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ArticleCommentNotFoundException>();
        }

        //Should create new ArticleCommentLike value object if the ArticleCommentId from the AddArticleCommentLikeCommand
        //is valid Id for existing ArticleComment domain entity.
        //Additionally the created ArticleCommentLike value object must be added to the ArticleComment domain entity and the repository
        //must be called to update the ArticleComment entity.
        [Fact]
        public async Task Given_Valid_ArticleCommentId_Creates_And_Adds_ArticleCommentLike_Instance_To_ArticleComment_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var articleComment = _articleCommentConcreteFactory.Create(Guid.NewGuid(), "some comment", DateTimeOffset.Now, 
                Guid.NewGuid(), null);

            var articleCommentDto = new ArticleCommentDto
            {
                ArticleDto = new ArticleDto { ArticleId = Guid.NewGuid().ToString() }
            };

            var addArticleCommentLikeCommand = GetAddArticleCommentLikeCommand(articleComment.Id);

            _articleCommentRepository.GetArticleCommentByIdAsync(addArticleCommentLikeCommand.ArticleCommentId)
                .Returns(articleComment);

            _articleCommentReadService.GetArticleCommentByIdAsync(addArticleCommentLikeCommand.ArticleCommentId)
               .Returns(articleCommentDto);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addArticleCommentLikeCommand));

            //ASSERT
            exception.ShouldBeNull();

            _messageBusPublisher.Received(1);

            await _articleCommentRepository.Received(1).UpdateArticleCommentAsync(articleComment);
        }
    }
}
