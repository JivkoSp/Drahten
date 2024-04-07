using NSubstitute;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.ValueObjects;
using Xunit;
using Shouldly;
using System.Reflection;
using TopicArticleService.Domain.Entities;

namespace TopicArticleService.Tests.Unit.Domain.Factories
{
    public class ArticleFactoryTests
    {
        #region GLOBAL ARRANGE

        private ArticleID Id;
        private ArticlePrevTitle _prevTitle;
        private ArticleTitle _title;
        private ArticleContent _content;
        private ArticlePublishingDate _publishingDate;
        private ArticleAuthor _author;
        private ArticleLink _link;
        private TopicId _topicId;
        private HashSet<UserArticle> _userArticles = new HashSet<UserArticle>();
        private List<ArticleComment> _articleComments = new List<ArticleComment>();
        private HashSet<ArticleLike> _articleLikes = new HashSet<ArticleLike>();
        private HashSet<ArticleDislike> _articleDislikes = new HashSet<ArticleDislike>();
        private readonly IArticleFactory _articleConcreteFactory;
        private readonly IArticleFactory _articleMockFactory;

        /// <summary>
        /// Creates an instance of the Article class using reflection, based on the provided parameters.
        /// This method retrieves a reference to the constructor of the Article class that matches the specified parameters,
        /// and then invokes the constructor with the provided values to instantiate an Article object.
        /// </summary>
        /// <param name="articleId">The ID of the article.</param>
        /// <param name="_prevTitle">The previous title of the article.</param>
        /// <param name="_title">The title of the article.</param>
        /// <param name="_content">The content of the article.</param>
        /// <param name="_publishingDate">The publishing date of the article.</param>
        /// <param name="_author">The author of the article.</param>
        /// <param name="_link">The link of the article.</param>
        /// <param name="_topicId">The ID of the topic associated with the article.</param>
        /// <returns>An instance of the Article class created with the specified parameters.</returns>
        private Article CreateArticleWithReflection(ArticleID articleId, ArticlePrevTitle _prevTitle, ArticleTitle _title, 
            ArticleContent _content, ArticlePublishingDate _publishingDate, ArticleAuthor _author, ArticleLink _link, TopicId _topicId)
        {
            //Retrieve a reference to the constructor of the Article class that matches the specified parameters.
            var constructor = typeof(Article).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new[]
                {
                    typeof(ArticleID),
                    typeof(ArticlePrevTitle),
                    typeof(ArticleTitle),
                    typeof(ArticleContent),
                    typeof(ArticlePublishingDate),
                    typeof(ArticleAuthor),
                    typeof(ArticleLink),
                    typeof(TopicId)
                },
                null);

            //Invoke the retrieved constructor with the values that are provided when the CreateArticleWithReflection is called.
            var article = (Article)constructor.Invoke(new object[]
            {
                articleId,
                _prevTitle,
                _title,
                _content,
                _publishingDate,
                _author,
                _link,
                _topicId
            });

            return article;
        }

        public ArticleFactoryTests()
        {
             Id = new ArticleID(Guid.NewGuid());
            _prevTitle = new ArticlePrevTitle("some prev title");
            _title = new ArticleTitle("some title");
            _content = new ArticleContent("some content");
            _publishingDate = new ArticlePublishingDate("2022-08-10T14:38:00");
            _author = new ArticleAuthor("no author");
            _link = new ArticleLink("no link");
            _topicId = new TopicId(Guid.NewGuid());
            _articleConcreteFactory = new ArticleFactory();
            _articleMockFactory = Substitute.For<IArticleFactory>();
        }

        #endregion

        //Should create Article instance from concrete factory implementation. The created Article instance should
        //have the same values as the values provided to the concrete factory. 
        [Fact]
        public void Given_Valid_Article_Parameters_Should_Create_Article_Instance_From_Concrete_Factory()
        {
            //ACT
            var article = _articleConcreteFactory.Create(Id, _prevTitle, _title, _content, _publishingDate, _author, _link, _topicId);

            //ASSERT
            article.ShouldNotBeNull();

            article.Id.ShouldBeSameAs(Id);

            //Comparing the article field values with the provided values for the _articleConcreteFactory - Start
            //--------------
            //This check is necessary because it ensures that the field values of the article object are the same as
            //the values that are provided to the concrete factory. 
            var articleType = article.GetType();
            var fields = articleType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                var fieldName = field.Name;

                var expectedValue = GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);

                var actualValue = field.GetValue(article);

                actualValue.ShouldBe(expectedValue, $"Field {fieldName} should match the expected value!");
            }

            //Comparing the article field values with the provided values for the _articleConcreteFactory - End
        }

        //Should create two article instances with equal values when equal values are given to the concrete factory (_articleConcreteFactory)
        //and to the mock factory (_articleMockFactory).
        //--------------
        //This test ensures that both factories produce equivalent Article instances when provided with the same input parameters,
        //thus validating the correctness of the concrete factory implementation.
        //The mock factory sets the way that article instance should be created. 
        //This is needed becouse the concrete factory may not create article instance as expected (for example it may use different constructor).
        [Fact]
        public void Should_Create_Equal_Article_Instances_From_Concrete_And_Mock_Factories()
        {
            //ACT
            var article = _articleConcreteFactory.Create(Id, _prevTitle, _title, _content, _publishingDate, _author, _link, _topicId);

            _articleMockFactory.Create(Arg.Any<ArticleID>(), Arg.Any<ArticlePrevTitle>(), Arg.Any<ArticleTitle>(), Arg.Any<ArticleContent>(), 
                Arg.Any<ArticlePublishingDate>(), Arg.Any<ArticleAuthor>(), Arg.Any<ArticleLink>(), Arg.Any<TopicId>()).Returns(
                    callInfo =>
                    {
                        var article = CreateArticleWithReflection(callInfo.Arg<ArticleID>(), callInfo.Arg<ArticlePrevTitle>(), 
                            callInfo.Arg<ArticleTitle>(), callInfo.Arg<ArticleContent>(), callInfo.Arg<ArticlePublishingDate>(), 
                            callInfo.Arg<ArticleAuthor>(), callInfo.Arg<ArticleLink>(), callInfo.Arg<TopicId>());

                        return article;
                    });

            var articleFromMockFactory = _articleMockFactory.Create(Id, _prevTitle, _title, _content, _publishingDate, _author, _link, _topicId);

            //ASSERT
            articleFromMockFactory.ShouldNotBeNull();

            //Comparing the values of the article object that is created by _articleConcreteFactory with the values of
            //the articleFromMockFactory object that is created by _articleMockFactory. 
            var articleType = article.GetType();
            var fields = articleType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                var value = field.GetValue(article);

                var returnedValue = field.GetValue(articleFromMockFactory);

                //Assert that the value of the property in the original article is equal to the value of the property
                //in the articleFromMockFactory.
                value.ShouldBe(returnedValue, $"Field {field.Name} should be equal!");
            }
        }
    }
}