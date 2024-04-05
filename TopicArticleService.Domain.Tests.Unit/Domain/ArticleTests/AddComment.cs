
using Shouldly;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.Events;
using TopicArticleService.Domain.Exceptions;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.ValueObjects;
using Xunit;

namespace TopicArticleService.Domain.Tests.Unit.ArticleTests
{
    public sealed class AddComment
    {
        #region GLOBAL ARRANGE

        private readonly IArticleFactory _articleFactory;
        private readonly ArticleCommentFactory _articleCommentFactory;

        private Article GetArticle()
        {
            var article = _articleFactory.Create(Guid.NewGuid(), "some title", "main title", "some content", "no date",
                    "no author", "no link", Guid.NewGuid());

            article.ClearEvents();

            return article;
        }

        private ArticleComment GetArticleCommentWithParentComment(ArticleCommentID parentCommentId)
        {
            var articleComment = _articleCommentFactory.Create(Guid.NewGuid(), "some comment", DateTime.Now, 
                    Guid.NewGuid(), parentCommentId);

            articleComment.ClearEvents();

            return articleComment;
        }

        private ArticleComment GetArticleCommentWithoutParentComment()
        {
            var articleComment = _articleCommentFactory.Create(Guid.NewGuid(), "some comment", DateTime.Now,
                    Guid.NewGuid(), null);

            articleComment.ClearEvents();

            return articleComment;
        }

        public AddComment()
        {
            _articleFactory = new ArticleFactory();
            _articleCommentFactory = new ArticleCommentFactory();
        }

        #endregion

        //Should throw ArticleCommentAlreadyExistsException when the following two conditions are met:
        // 1. There is already article comment (ArticleComment entity) with matching UserID.
        // 2. There is already article comment (ArticleComment entity) that has parent comment (ArticleCommentID that points to parent comment)
        // with the same ArticleCommentID.
        [Fact]
        public void DuplicateArticleComment_With_ParentComment_Throws_ArticleCommentAlreadyExistsException()
        {
            //ARRANGE
            var article = GetArticle();

            var parentArticleComment = GetArticleCommentWithoutParentComment();

            var childArticleComment = GetArticleCommentWithParentComment(parentArticleComment.Id);

            article.AddComment(childArticleComment);

            //ACT
            var exception = Record.Exception(() => article.AddComment(childArticleComment));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ArticleCommentAlreadyExistsException>();
        }

        //Should throw ArticleCommentAlreadyExistsException when the following condition is met:
        //There is already article comment (ArticleComment entity) with matching UserID without parent comment
        //(there is no ArticleCommentID that points to parent comment).
        [Fact]
        public void DuplicateArticleComment_Without_ParentComment_Throws_ArticleCommentAlreadyExistsException()
        {
            //ARRANGE
            var article = GetArticle();

            var articleComment = GetArticleCommentWithoutParentComment();

            article.AddComment(articleComment);

            //ACT
            var exception = Record.Exception(() => article.AddComment(articleComment));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ArticleCommentAlreadyExistsException>();
        }

        //Should add article comment (ArticleComment entity) without parent comment (there is no ArticleCommentID that points to parent comment)
        //to internal collection of ArticleComment entities and produce ArticleCommentAdded domain event.
        //The ArticleCommentAdded domain event should contain:
        // 1. The same article entity that the article comment was added to.
        // 2. The same article comment entity that was added to the internal collection of ArticleComment entities.
        [Fact]
        public void Adds_ArticleComment_Without_ParentComment_And_Produces_ArticleCommentAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var article = GetArticle();

            var articleComment = GetArticleCommentWithoutParentComment();

            //ACT
            var exception = Record.Exception(() => article.AddComment(articleComment));

            //ASSERT
            exception.ShouldBeNull();

            article.DomainEvents.Count().ShouldBe(1);

            article.ArticleComments.Count().ShouldBe(1);

            var articleCommentAddedEvent = article.DomainEvents.FirstOrDefault() as ArticleCommentAdded;

            articleCommentAddedEvent.ShouldNotBeNull();

            articleCommentAddedEvent.Article.ShouldBeSameAs(article);

            articleCommentAddedEvent.ArticleComment.ShouldBeSameAs(articleComment);
        }

        //Should add article comment (ArticleComment entity) with parent comment (there is ArticleCommentID that points to parent comment)
        //to internal collection of ArticleComment entities and produce two domain events:
        // * One ArticleCommentAdded domain event.
        // * One ArticleCommentChildAdded domain event.
        //--------------------------------------------------------------------------
        //The ArticleCommentAdded domain event should contain:
        // 1. The same article entity that the article comment was added to.
        // 2. The same article comment entity that was added to the internal collection of ArticleComment entities.
        //--------------------------------------------------------------------------
        //The ArticleCommentChildAdded domain event should contain:
        // 1. The same parent comment (ArticleComment entity) that was added to the internal collection of ArticleComment entities.
        // 2. The same article child comment that was added to the internal collection of ArticleComment entities.
        //--------------------------------------------------------------------------
        [Fact]
        public void Adds_ArticleComment_With_ParentComment_And_Produces_Domain_Events_On_Success()
        {
            //ARRANGE
            var article = GetArticle();

            var parentArticleComment = GetArticleCommentWithoutParentComment();

            var childArticleComment = GetArticleCommentWithParentComment(parentArticleComment.Id);

            article.AddComment(parentArticleComment);

            //ACT
            var exception = Record.Exception(() => article.AddComment(childArticleComment));

            //ASSERT
            exception.ShouldBeNull();

            article.DomainEvents.Count().ShouldBe(2);

            article.ArticleComments.Count().ShouldBe(2);

            var articleCommentAddedEvent = article.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(ArticleCommentAdded)) as ArticleCommentAdded;

            var articleCommentChildAddedEvent = article.DomainEvents.FirstOrDefault(x =>
                x.GetType() == typeof(ArticleCommentChildAdded)) as ArticleCommentChildAdded;

            //ArticleCommentAdded event check - Start 

            articleCommentAddedEvent.ShouldNotBeNull();

            articleCommentAddedEvent.Article.ShouldBeSameAs(article);

            articleCommentAddedEvent.ArticleComment.ShouldBeSameAs(parentArticleComment);

            //ArticleCommentAdded event check - End


            //ArticleCommentChildAdded event check - Start

            articleCommentChildAddedEvent.ShouldNotBeNull();

            articleCommentChildAddedEvent.ArticleParentComment.ShouldBeSameAs(parentArticleComment);

            articleCommentChildAddedEvent.ArticleChildComment.ShouldBeSameAs(childArticleComment);

            //ArticleCommentChildAdded event check - End
        }
    }
}
