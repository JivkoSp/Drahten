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
        //--------------
        //This test ensures that both factories produce equivalent UserArticle instances when provided with the same input parameters,
        //thus validating the correctness of the concrete factory implementation.
        //The mock factory sets the way that UserArticle instance should be created. 
        //This is needed becouse the concrete factory may not create UserArticle instance as expected
        //(for example it may use different constructor).
        [Fact]
        public void Should_Create_Equal_UserArticle_Instances_From_Concrete_And_Mock_Factories()
        {
            //ACT
            var userArticle = _userArticleConcreteFactory.Create(_userId, _articleId);

            _userArticleMockFactory.Create(Arg.Any<UserID>(), Arg.Any<ArticleID>()).Returns(
                callInfo =>
                {
                    var userArticle = new UserArticle(callInfo.Arg<UserID>(), callInfo.Arg<ArticleID>());

                    return userArticle;
                });

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
    }
}
