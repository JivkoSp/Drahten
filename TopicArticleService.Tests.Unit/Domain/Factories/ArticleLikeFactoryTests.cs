using NSubstitute;
using Shouldly;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.ValueObjects;
using Xunit;

namespace TopicArticleService.Tests.Unit.Domain.Factories
{
    public class ArticleLikeFactoryTests
    {
        #region GLOBAL ARRANGE

        private ArticleID _articleId;
        private UserID _userId;
        private DateTimeOffset _dateTime;
        private readonly IArticleLikeFactory _articleLikeConcreteFactory;
        private readonly IArticleLikeFactory _articleLikeMockFactory;

        public ArticleLikeFactoryTests()
        {
            _articleId = new ArticleID(Guid.NewGuid());
            _userId = new UserID(Guid.NewGuid());
            _dateTime = DateTimeOffset.Now;
            _articleLikeConcreteFactory = new ArticleLikeFactory();
            _articleLikeMockFactory = Substitute.For<IArticleLikeFactory>();
        }

        #endregion

        //Should create ArticleLike instance from concrete factory implementation. The created ArticleLike instance should
        //have the same values as the values provided to the concrete factory. 
        [Fact]
        public void Given_Valid_ArticleLike_Parameters_Should_Create_ArticleLike_Instance_From_Concrete_Factory()
        {
            //ACT
            var articleLike = _articleLikeConcreteFactory.Create(_articleId, _userId, _dateTime);

            //ASSERT
            articleLike.ShouldNotBeNull();

            articleLike.ArticleID.ShouldBe(_articleId);

            articleLike.UserID.ShouldBe(_userId);

            articleLike.DateTime.ShouldBe(_dateTime);
        }

        //Should create two ArticleLike instances with equal values when equal values are given to the concrete factory
        //(_articleLikeConcreteFactory) and to the mock factory (_articleLikeMockFactory).
        //--------------
        //This test ensures that both factories produce equivalent ArticleLike instances when provided with the same input parameters,
        //thus validating the correctness of the concrete factory implementation.
        //The mock factory sets the way that ArticleLike instance should be created. 
        //This is needed becouse the concrete factory may not create ArticleLike instance as expected
        //(for example it may use different constructor).
        [Fact]
        public void Should_Create_Equal_ArticleLike_Instances_From_Concrete_And_Mock_Factories()
        {
            //ACT
            var articleLike = _articleLikeConcreteFactory.Create(_articleId, _userId, _dateTime);

            _articleLikeMockFactory.Create(Arg.Any<ArticleID>(), Arg.Any<UserID>(), Arg.Any<DateTimeOffset>()).Returns(
                callInfo =>
                {
                    var articleLike = new ArticleLike(callInfo.ArgAt<ArticleID>(0),
                        callInfo.ArgAt<UserID>(1), callInfo.ArgAt<DateTimeOffset>(2));

                    return articleLike;
                });

            var articleLikeFromMockFactory = _articleLikeMockFactory.Create(_articleId, _userId, _dateTime);

            //ASSERT
            articleLikeFromMockFactory.ShouldNotBeNull();

            //Comparing the values of the articleLike object that is created by _articleLikeConcreteFactory with the values of
            //the articleLikeFromMockFactory object that is created by _articleLikeMockFactory. 

            articleLike.ArticleID.ShouldBe(articleLikeFromMockFactory.ArticleID);

            articleLike.UserID.ShouldBe(articleLikeFromMockFactory.UserID);

            articleLike.DateTime.ShouldBe(articleLikeFromMockFactory.DateTime);
        }
    }
}
