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
        [Fact]
        public void Should_Create_Equal_Article_Instances_From_Concrete_And_Mock_Factories()
        {
            //ACT
            var article = _articleConcreteFactory.Create(Id, _prevTitle, _title, _content, _publishingDate, _author, _link, _topicId);

            _articleMockFactory.Create(Id, _prevTitle, _title, _content, _publishingDate, _author, _link, _topicId).Returns(article);

            var returnedArticle = _articleMockFactory.Create(Id, _prevTitle, _title, _content, _publishingDate, _author, _link, _topicId);

            //ASSERT
            returnedArticle.ShouldNotBeNull();

            //Comparing the values of the article object that is created by _articleConcreteFactory with the values of
            //the returnedArticle object that is created by _articleMockFactory. 
            var articleType = article.GetType();
            var fields = articleType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                var value = field.GetValue(article);

                var returnedValue = field.GetValue(returnedArticle);

                //Assert that the value of the property in the original article is equal to the value of the property in the returned article.
                value.ShouldBe(returnedValue, $"Field {field.Name} should be equal!");
            }
        }

        //Should call the mock factory _articleMockFactory only once and ensure that it is called with the the specified arguments.
        [Fact]
        public void Given_Valid_Article_Parameters_Calls_Mock_Factory_And_Ensures_That_The_Call_Is_Made_With_The_Provided_Parameters()
        {
            //ACT
            var article = _articleConcreteFactory.Create(Id, _prevTitle, _title, _content, _publishingDate, _author, _link, _topicId);

            _articleMockFactory.Create(Id, _prevTitle, _title, _content, _publishingDate, _author, _link, _topicId).Returns(article);

            _articleMockFactory.Create(Id, _prevTitle, _title, _content, _publishingDate, _author, _link, _topicId);

            //ASSERT

            //Verify interaction with the mock factory and ensure that it is called with the the specified arguments.
            _articleMockFactory.Received(1).Create(
                Arg.Is<ArticleID>(id => id.Value == Id.Value),
                Arg.Is<ArticlePrevTitle>(prevTitle => prevTitle.Value == _prevTitle.Value),
                Arg.Is<ArticleTitle>(title => title.Value == _title.Value),
                Arg.Is<ArticleContent>(content => content.Value == _content.Value),
                Arg.Is<ArticlePublishingDate>(date => date.Value == _publishingDate.Value),
                Arg.Is<ArticleAuthor>(author => author.Value == _author.Value),
                Arg.Is<ArticleLink>(link => link.Value == _link.Value),
                Arg.Is<TopicId>(id => id.Value == _topicId.Value)
            );
        }
    }
}