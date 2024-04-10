using NSubstitute;
using Shouldly;
using System.Reflection;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.ValueObjects;
using Xunit;

namespace TopicArticleService.Tests.Unit.Domain.Factories
{
    public class ArticleCommentFactoryTests
    {
        #region GLOBAL ARRANGE

        private ArticleCommentID _articleCommentId;
        private ArticleCommentValue _commentValue;
        private ArticleCommentDateTime _dateTime;
        private UserID _userId;
        private ArticleCommentID _parentArticleCommentId;
        private HashSet<ArticleCommentLike> _articleCommentLikes = new HashSet<ArticleCommentLike>();
        private HashSet<ArticleCommentDislike> _articleCommentDislikes = new HashSet<ArticleCommentDislike>();
        private readonly IArticleCommentFactory _articleCommentConcreteFactory;
        private readonly IArticleCommentFactory _articleCommentMockFactory;

        /// <summary>
        /// Creates an instance of the ArticleComment class using reflection, based on the provided parameters.
        /// This method retrieves a reference to the constructor of the ArticleComment class that matches the specified parameters,
        /// and then invokes the constructor with the provided values to instantiate an ArticleComment object.
        /// </summary>
        /// <param name="articleCommentId">The ID of the ArticleComment.</param>
        /// <param name="articleCommentValue">The value that represents the comment itself.</param>
        /// <param name="articleCommentDateTime">The time that the ArticleComment was created.</param>
        /// <param name="userID">The ID of the User that created the ArticleComment.</param>
        /// <param name="parentArticleCommentId">The ID of the parent ArticleComment (if any).</param>
        /// <returns>An instance of the ArticleComment class created with the specified parameters.</returns>
        private ArticleComment CreateArticleCommentWithReflection(ArticleCommentID articleCommentId, 
            ArticleCommentValue articleCommentValue, ArticleCommentDateTime articleCommentDateTime, UserID userID, 
            ArticleCommentID parentArticleCommentId)
        {
            //Retrieve a reference to the constructor of the ArticleComment class that matches the specified parameters.
            var constructor = typeof(ArticleComment).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new[]
                {
                    typeof(ArticleCommentID),
                    typeof(ArticleCommentValue),
                    typeof(ArticleCommentDateTime),
                    typeof(UserID),
                    typeof(ArticleCommentID)
                },
                null);

            //Invoke the retrieved constructor with the values that are provided when the CreateArticleCommentWithReflection is called.
            var articleComment = (ArticleComment)constructor.Invoke(new object[]
            {
               articleCommentId,
               articleCommentValue,
               articleCommentDateTime,
               userID,
               parentArticleCommentId
            });

            return articleComment;
        }

        public ArticleCommentFactoryTests()
        {
            _articleCommentId = new ArticleCommentID(Guid.NewGuid());
            _commentValue = new ArticleCommentValue("some comment");
            _dateTime = new ArticleCommentDateTime(DateTimeOffset.Now);
            _userId = new UserID(Guid.NewGuid());
            _parentArticleCommentId = null;

            _articleCommentConcreteFactory = new ArticleCommentFactory();
            _articleCommentMockFactory = Substitute.For<IArticleCommentFactory>();
        }

        #endregion

        //Should create ArticleComment instance from concrete factory implementation. The created ArticleComment instance should
        //have the same values as the values provided to the concrete factory. 
        [Fact]
        public void Given_Valid_ArticleComment_Parameters_Should_Create_ArticleComment_Instance_From_Concrete_Factory()
        {
            //ACT
            var articleComment = _articleCommentConcreteFactory.Create(_articleCommentId, _commentValue, _dateTime, 
                _userId, _parentArticleCommentId);

            //ASSERT
            articleComment.ShouldNotBeNull();

            articleComment.Id.ShouldBeSameAs(_articleCommentId);

            //Comparing the articleComment field values with the provided values for the _articleCommentConcreteFactory - Start
            //--------------
            //This check is necessary because it ensures that the field values of the articleComment object are the same as
            //the values that are provided to the concrete factory. 
            var articleType = articleComment.GetType();
            var fields = articleType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                var fieldName = field.Name;

                var expectedValue = GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);

                var actualValue = field.GetValue(articleComment);

                actualValue.ShouldBe(expectedValue, $"Field {fieldName} should match the expected value!");
            }

            //Comparing the articleComment field values with the provided values for the _articleCommentConcreteFactory - End
        }

        //Should create two ArticleComment instances with equal values when equal values are given to the concrete factory
        //(_articleCommentConcreteFactory) and to the mock factory (_articleCommentMockFactory).
        //--------------
        //This test ensures that both factories produce equivalent ArticleComment instances when provided with the same input parameters,
        //thus validating the correctness of the concrete factory implementation.
        //The mock factory sets the way that ArticleComment instance should be created. 
        //This is needed becouse the concrete factory may not create ArticleComment instance as expected
        //(for example it may use different constructor).
        [Fact]
        public void Should_Create_Equal_Article_Instances_From_Concrete_And_Mock_Factories()
        {
            //ACT
            var articleComment = _articleCommentConcreteFactory.Create(_articleCommentId, _commentValue, _dateTime,
                _userId, _parentArticleCommentId);

            _articleCommentMockFactory.Create(Arg.Any<ArticleCommentID>(), Arg.Any<ArticleCommentValue>(), 
                Arg.Any<ArticleCommentDateTime>(), Arg.Any<UserID>(), Arg.Any<ArticleCommentID>()).Returns(
                    callInfo =>
                    {
                        var articleComment = CreateArticleCommentWithReflection(callInfo.ArgAt<ArticleCommentID>(0), 
                            callInfo.ArgAt<ArticleCommentValue>(1), callInfo.ArgAt<ArticleCommentDateTime>(2), 
                            callInfo.ArgAt<UserID>(3), callInfo.ArgAt<ArticleCommentID>(4));

                        return articleComment;
                    });

            var articleCommentFromMockFactory = _articleCommentMockFactory.Create(_articleCommentId, _commentValue, _dateTime,
                _userId, _parentArticleCommentId);

            //ASSERT
            articleCommentFromMockFactory.ShouldNotBeNull();

            //Comparing the values of the ArticleComment object that is created by _articleCommentConcreteFactory with the values of
            //the articleCommentFromMockFactory object that is created by _articleCommentMockFactory. 
            var articleCommentType = articleComment.GetType();
            var fields = articleCommentType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                var valueFromArticleCommentConcreteImplementation = field.GetValue(articleComment);

                var valueFromArticleCommentMockImplementation = field.GetValue(articleCommentFromMockFactory);

                //Assert that the value of the property in the original articleComment is equal to the value of the property
                //in the articleCommentFromMockFactory.
                valueFromArticleCommentConcreteImplementation
                    .ShouldBe(valueFromArticleCommentMockImplementation, $"Field {field.Name} should be equal!");
            }
        }
    }
}
