using NSubstitute;
using Shouldly;
using System.Reflection;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.ValueObjects;
using Xunit;

namespace TopicArticleService.Tests.Unit.Domain.Factories
{
    public class UserArticleFactoryTests
    {
        #region GLOBAL ARRANGE

        private UserID _userId;
        private ArticleID _articleId;
        private readonly IUserArticleFactory _userArticleConcreteFactory;
        private readonly IUserArticleFactory _userArticleMockFactory;

        public UserArticleFactoryTests()
        {
            _userId = new UserID(Guid.NewGuid());
            _articleId = new ArticleID(Guid.NewGuid());
            _userArticleConcreteFactory = new UserArticleFactory();
            _userArticleMockFactory = Substitute.For<IUserArticleFactory>();
        }

        #endregion

        //Should create UserArticle instance from concrete factory implementation. The created UserArticle instance should
        //have the same values as the values provided to the concrete factory. 
        [Fact]
        public void Given_Valid_UserArticle_Parameters_Should_Create_UserArticle_Instance_From_Concrete_Factory()
        {
            //ACT
            var userArticle = _userArticleConcreteFactory.Create(_userId, _articleId);

            //ASSERT
            userArticle.ShouldNotBeNull();

            userArticle.UserID.ShouldBeSameAs(_userId);

            userArticle.ArticleId.ShouldBeSameAs(_articleId);
        }

        //Should create two UserArticle instances with equal values when equal values are given to the concrete factory
        //(_userArticleConcreteFactory) and to the mock factory (_userArticleMockFactory).
        [Fact]
        public void Should_Create_Equal_UserArticle_Instances_From_Concrete_And_Mock_Factories()
        {
            //ACT
            var userArticle = _userArticleConcreteFactory.Create(_userId, _articleId);

            _userArticleMockFactory.Create(_userId, _articleId).Returns(userArticle);

            var userArticleFromMockFactory = _userArticleMockFactory.Create(_userId, _articleId);

            //ASSERT
            userArticleFromMockFactory.ShouldNotBeNull();

            //Comparing the values of the userArticle object that is created by _userArticleConcreteFactory with the values of
            //the userArticleFromMockFactory object that is created by _userArticleMockFactory. 
            var userArticleType = userArticle.GetType();
            var fields = userArticleType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                var value = field.GetValue(userArticle);

                var returnedValue = field.GetValue(userArticleFromMockFactory);

                //Assert that the value of the property in the original userArticle is equal to the value of the property
                //in the userArticleFromMockFactory.
                value.ShouldBe(returnedValue, $"Field {field.Name} should be equal!");
            }
        }

        //Should call the mock factory _userArticleMockFactory only once and ensure that it is called with the the specified arguments.
        [Fact]
        public void Given_Valid_UserArticle_Parameters_Calls_Mock_Factory_And_Ensures_That_The_Call_Is_Made_With_The_Provided_Parameters()
        {
            //ACT
            var userArticle = _userArticleConcreteFactory.Create(_userId, _articleId);

            _userArticleMockFactory.Create(_userId, _articleId).Returns(userArticle);

            _userArticleMockFactory.Create(_userId, _articleId);

            //ASSERT

            //Verify interaction with the mock factory and ensure that it is called with the the specified arguments.
            _userArticleMockFactory.Received(1).Create(
                Arg.Is<UserID>(id => id == _userId),
                Arg.Is<ArticleID>(id => id == _articleId)
            );
        }
    }
}
