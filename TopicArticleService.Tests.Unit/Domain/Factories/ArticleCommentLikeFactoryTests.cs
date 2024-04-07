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
        private string _dateTime;
        private readonly IArticleCommentLikeFactory _articleCommentLikeConcreteFactory;
        private readonly IArticleCommentLikeFactory _articleCommentLikeMockFactory;

        public ArticleCommentLikeFactoryTests()
        {
            _articleCommentId = new ArticleCommentID(Guid.NewGuid());
            _userId = new UserID(Guid.NewGuid());
            _dateTime = DateTime.Now.ToString();
            
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
            var articleCommentLike = _articleCommentLikeConcreteFactory.Create(_articleCommentId, _userId, DateTime.Now.ToString());

            //ASSERT
            articleCommentLike.ShouldNotBeNull();

            articleCommentLike.ArticleCommentID.ShouldBeSameAs(_articleCommentId);

            articleCommentLike.UserID.ShouldBeSameAs(_userId);
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
            var articleCommentLike = _articleCommentLikeConcreteFactory.Create(_articleCommentId, _userId, DateTime.Now.ToString());

            _articleCommentLikeMockFactory.Create(_articleCommentId, _userId, DateTime.Now.ToString()).Returns(
                callInfo =>
                {
                    var articleCommentLike = new ArticleCommentLike(_articleCommentId, _userId, DateTime.Now.ToString());

                    return articleCommentLike;
                });

            var articleCommentLikeFromMockFactory = _articleCommentLikeMockFactory
                .Create(_articleCommentId, _userId, DateTime.Now.ToString());

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
