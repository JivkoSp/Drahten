using Xunit;
using Shouldly;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Domain.Exceptions;
using TopicArticleService.Domain.Events;

namespace TopicArticleService.Domain.Tests.Unit.ArticleTests
{
    public sealed class AddUserArticle
    {
        #region GLOBAL ARRANGE

        private readonly IArticleFactory _articleFactory;
        private readonly UserArticleFactory _userArticleFactory;

        private Article GetArticle()
        {
            var article = _articleFactory.Create(Guid.NewGuid(), "some title", "main title", "some content", "no date", 
                    "no author", "no link", Guid.NewGuid());

            article.ClearEvents();

            return article;
        }

        private UserArticle GetUserArticle()
        {
            var userArticle = _userArticleFactory.Create(Guid.NewGuid(), Guid.NewGuid());

            return userArticle;
        }

        public AddUserArticle()
        {
            _articleFactory = new ArticleFactory();
            _userArticleFactory = new UserArticleFactory();
        }

        #endregion

        //Should throw UserArticleAlreadyExistsException when the following condition is met:
        //There is already user article (UserArticle value object) with matching UserID and ArticleID.
        [Fact]
        public void UserArticle_Throws_UserArticleAlreadyExistsException()
        {
            //ARRANGE
            var article = GetArticle();

            var userArticle = GetUserArticle();

            article.AddUserArticle(userArticle);

            //ACT
            var exception = Record.Exception(() => article.AddUserArticle(userArticle));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserArticleAlreadyExistsException>();
        }

        //Should add user article (UserArticle value object) to internal collection of UserArticle value objects
        //and produce UserArticleAdded domain event.
        //The UserArticleAdded domain event should contain:
        // 1. The same article entity that the user article was added to.
        // 2. The same user article value object that was added to the internal collection of UserArticle value objects.
        [Fact]
        public void Adds_UserArticle_And_Produces_UserArticleAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var article = GetArticle();

            var userArticle = GetUserArticle();

            //ACT
            var exception = Record.Exception(() => article.AddUserArticle(userArticle));

            //ASSERT
            exception.ShouldBeNull();

            article.DomainEvents.Count().ShouldBe(1);

            article.UserArticles.Count().ShouldBe(1);

            var userArticleAddedEvent = article.DomainEvents.FirstOrDefault() as UserArticleAdded;

            userArticleAddedEvent.ShouldNotBeNull();

            userArticleAddedEvent.Article.ShouldBeSameAs(article);

            userArticleAddedEvent.UserArticle.ShouldBeSameAs(userArticle);
        }
    }
}
