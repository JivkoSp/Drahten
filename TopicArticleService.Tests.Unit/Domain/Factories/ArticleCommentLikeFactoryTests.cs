using NSubstitute;
using Shouldly;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.ValueObjects;
using Xunit;

namespace TopicArticleService.Tests.Unit.Domain.Factories
{
    public class ArticleCommentLikeFactoryTests
    {
        #region GLOBAL ARRANGE

        private ArticleCommentID _articleCommentId;
        private UserID _userId;
        private DateTimeOffset _dateTime;
        private readonly IArticleCommentLikeFactory _articleCommentLikeConcreteFactory;
        private readonly IArticleCommentLikeFactory _articleCommentLikeMockFactory;

        public ArticleCommentLikeFactoryTests()
        {
            _articleCommentId = new ArticleCommentID(Guid.NewGuid());
            _userId = new UserID(Guid.NewGuid());
            _dateTime = DateTimeOffset.Now;
            
            _articleCommentLikeConcreteFactory = new ArticleCommentLikeFactory();
            _articleCommentLikeMockFactory = Substitute.For<IArticleCommentLikeFactory>();
        }

        #endregion

        //Should create ArticleCommentLike instance from concrete factory implementation. The created ArticleCommentLike instance should
        //have the same values as the values provided to the concrete factory. 
        [Fact]
        public void Given_Valid_ArticleCommentLike_Parameters_Should_Create_ArticleCommentLike_Instance_From_Concrete_Factory()
        {
            //ACT
            var articleCommentLike = _articleCommentLikeConcreteFactory.Create(_articleCommentId, _userId, _dateTime);

            //ASSERT
            articleCommentLike.ShouldNotBeNull();

            articleCommentLike.ArticleCommentID.ShouldBe(_articleCommentId);

            articleCommentLike.UserID.ShouldBe(_userId);

            articleCommentLike.DateTime.ShouldBe(_dateTime);
        }

        //Should create two ArticleCommentLike instances with equal values when equal values are given to the concrete factory
        //(_articleCommentLikeConcreteFactory) and to the mock factory (_articleCommentLikeMockFactory).
        //--------------
        //This test ensures that both factories produce equivalent ArticleCommentLike instances when provided with the same input parameters,
        //thus validating the correctness of the concrete factory implementation.
        //The mock factory sets the way that ArticleCommentLike instance should be created. 
        //This is needed becouse the concrete factory may not create ArticleCommentLike instance as expected
        //(for example it may use different constructor).
        [Fact]
        public void Should_Create_Equal_ArticleCommentLike_Instances_From_Concrete_And_Mock_Factories()
        {
            //ACT
            var articleCommentLike = _articleCommentLikeConcreteFactory.Create(_articleCommentId, _userId, _dateTime);

            _articleCommentLikeMockFactory.Create(Arg.Any<ArticleCommentID>(), Arg.Any<UserID>(), Arg.Any<DateTimeOffset>()).Returns(
                callInfo =>
                {
                    var articleCommentLike = new ArticleCommentLike(callInfo.ArgAt<ArticleCommentID>(0),
                        callInfo.ArgAt<UserID>(1), callInfo.ArgAt<DateTimeOffset>(2));

                    return articleCommentLike;
                });

            var articleCommentLikeFromMockFactory = _articleCommentLikeMockFactory.Create(_articleCommentId, _userId, _dateTime);

            //ASSERT
            articleCommentLikeFromMockFactory.ShouldNotBeNull();

            //Comparing the values of the articleCommentLike object that is created by _articleCommentLikeConcreteFactory with the values of
            //the articleCommentLikeFromMockFactory object that is created by _articleCommentLikeMockFactory. 

            articleCommentLike.ArticleCommentID.ShouldBe(articleCommentLikeFromMockFactory.ArticleCommentID);

            articleCommentLike.UserID.ShouldBe(articleCommentLikeFromMockFactory.UserID);

            articleCommentLike.DateTime.ShouldBe(articleCommentLikeFromMockFactory.DateTime);
        }
    }
}
