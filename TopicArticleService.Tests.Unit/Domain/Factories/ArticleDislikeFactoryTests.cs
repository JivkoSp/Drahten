using NSubstitute;
using Shouldly;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.ValueObjects;
using Xunit;

namespace TopicArticleService.Tests.Unit.Domain.Factories
{
    public class ArticleDislikeFactoryTests
    {
        #region GLOBAL ARRANGE

        private ArticleID _articleId;
        private UserID _userId;
        private DateTimeOffset _dateTime;
        private readonly IArticleDislikeFactory _articleDislikeConcreteFactory;
        private readonly IArticleDislikeFactory _articleDislikeMockFactory;

        public ArticleDislikeFactoryTests()
        {
            _articleId = new ArticleID(Guid.NewGuid());
            _userId = new UserID(Guid.NewGuid());
            _dateTime = DateTimeOffset.Now;
            
            _articleDislikeConcreteFactory = new ArticleDislikeFactory();
            _articleDislikeMockFactory = Substitute.For<IArticleDislikeFactory>();
        }

        #endregion

        //Should create ArticleDislike instance from concrete factory implementation. The created ArticleDislike instance should
        //have the same values as the values provided to the concrete factory. 
        [Fact]
        public void Given_Valid_ArticleDislike_Parameters_Should_Create_ArticleDislike_Instance_From_Concrete_Factory()
        {
            //ACT
            var articleDislike = _articleDislikeConcreteFactory.Create(_articleId, _userId, _dateTime);

            //ASSERT
            articleDislike.ShouldNotBeNull();

            articleDislike.ArticleID.ShouldBe(_articleId);

            articleDislike.UserID.ShouldBe(_userId);

            articleDislike.DateTime.ShouldBe(_dateTime);
        }

        //Should create two ArticleDislike instances with equal values when equal values are given to the concrete factory
        //(_articleDislikeConcreteFactory) and to the mock factory (_articleDislikeMockFactory).
        //--------------
        //This test ensures that both factories produce equivalent ArticleDislike instances when provided with the same input parameters,
        //thus validating the correctness of the concrete factory implementation.
        //The mock factory sets the way that ArticleDislike instance should be created. 
        //This is needed becouse the concrete factory may not create ArticleDislike instance as expected
        //(for example it may use different constructor).
        [Fact]
        public void Should_Create_Equal_ArticleDislike_Instances_From_Concrete_And_Mock_Factories()
        {
            //ACT
            var articleDislike = _articleDislikeConcreteFactory.Create(_articleId, _userId, _dateTime);

            _articleDislikeMockFactory.Create(Arg.Any<ArticleID>(), Arg.Any<UserID>(), Arg.Any<DateTimeOffset>()).Returns(
                callInfo =>
                {
                    var articleDislike = new ArticleDislike(callInfo.ArgAt<ArticleID>(0),
                        callInfo.ArgAt<UserID>(1), callInfo.ArgAt<DateTimeOffset>(2));

                    return articleDislike;
                });

            var articleLikeFromMockFactory = _articleDislikeMockFactory.Create(_articleId, _userId, _dateTime);

            //ASSERT
            articleLikeFromMockFactory.ShouldNotBeNull();

            //Comparing the values of the articleDislike object that is created by _articleDislikeConcreteFactory with the values of
            //the _articleDislikeMockFactory object that is created by articleLikeFromMockFactory. 

            articleDislike.ArticleID.ShouldBe(articleLikeFromMockFactory.ArticleID);

            articleDislike.UserID.ShouldBe(articleLikeFromMockFactory.UserID);

            articleDislike.DateTime.ShouldBe(articleLikeFromMockFactory.DateTime);
        }
    }
}
