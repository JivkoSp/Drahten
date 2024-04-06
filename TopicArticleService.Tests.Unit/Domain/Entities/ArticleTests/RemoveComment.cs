using Shouldly;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.Events;
using TopicArticleService.Domain.Exceptions;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.ValueObjects;
using Xunit;

namespace TopicArticleService.Tests.Unit.Domain.Entities.ArticleTests
{
    public sealed class RemoveComment
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

        public RemoveComment()
        {
            _articleFactory = new ArticleFactory();
            _articleCommentFactory = new ArticleCommentFactory();
        }

        #endregion

        //Should throw ArticleCommentNotFoundException when the following condition is met:
        //There is no article comment (ArticleComment entity) with matching ArticleCommentID.
        [Fact]
        public void ArticleComment_NotFound_Throws_ArticleCommentNotFoundException()
        {
            //ARRANGE
            var article = GetArticle();

            var articleCommentId = Guid.NewGuid(); //Relates to the ArticleCommentID value object.

            //ACT
            var exception = Record.Exception(() => article.RemoveComment(articleCommentId));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ArticleCommentNotFoundException>();
        }

        //Should remove article comment (ArticleComment entity) without related article comments (child article comments) from
        //internal collection of ArticleComment entities and produce ArticleCommentRemoved domain event.
        //The ArticleCommentRemoved domain event should contain:
        // 1. The same article entity that the article comment was removed from.
        // 2. The same article comment entity that was removed from the internal collection of ArticleComment entities.
        [Fact]
        public void Removes_ArticleComment_Without_ChildrenComments_And_Produces_ArticleCommentRemoved_Domain_Event_On_Success()
        {
            //ARRANGE
            var article = GetArticle();

            var articleComment = GetArticleCommentWithoutParentComment();

            article.AddComment(articleComment);

            //ACT
            var exception = Record.Exception(() => article.RemoveComment(articleComment.Id));

            //ASSERT
            exception.ShouldBeNull();

            article.DomainEvents.Count().ShouldBe(2);

            article.ArticleComments.Count().ShouldBe(0);

            var articleCommentAddedEvent = article.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(ArticleCommentAdded)) as ArticleCommentAdded;

            var articleCommentRemovedEvent = article.DomainEvents.FirstOrDefault(x =>
                x.GetType() == typeof(ArticleCommentRemoved)) as ArticleCommentRemoved;


            articleCommentAddedEvent.ShouldNotBeNull();

            articleCommentAddedEvent.Article.ShouldBeSameAs(article);

            articleCommentAddedEvent.ArticleComment.ShouldBeSameAs(articleComment);

            articleCommentRemovedEvent.ShouldNotBeNull();

            articleCommentRemovedEvent.Article.ShouldBeSameAs(article);

            articleCommentRemovedEvent.ArticleComment.ShouldBeSameAs(articleComment);
        }

        //Should remove article comment (ArticleComment entity) with two related article comments (child article comments) from
        //internal collection of ArticleComment entities and produce the following six domain events:
        // * One ArticleCommentAdded domain event.
        // * Two ArticleCommentChildAdded domain events.
        // * Two ArticleCommentChildRemoved domain events.
        // * One ArticleCommentRemoved domain event.
        //--------------------------------------------------------------------------
        //The ArticleCommentAdded domain event should contain:
        // 1. The same article entity that the article comment was added to.
        // 2. The same article comment entity that was added to the internal collection of ArticleComment entities.
        //--------------------------------------------------------------------------
        //Each of the ArticleCommentChildAdded domain events should contain:
        // 1. The same parent comment (ArticleComment entity) that was added to the internal collection of ArticleComment entities.
        // 2. One of the same article child comment that were added to the internal collection of ArticleComment entities.
        //--------------------------------------------------------------------------
        //Each of the ArticleCommentChildRemoved domain events should contain:
        // 1. The same parent comment (ArticleComment entity) that was added to the internal collection of ArticleComment entities.
        // 2. One of the same article child comments that were removed from the internal collection of ArticleComment entities.
        //--------------------------------------------------------------------------
        //The ArticleCommentRemoved domain event should contain:
        // 1. The same article entity that the article comment was removed from.
        // 2. The same article comment entity that was removed from the internal collection of ArticleComment entities.
        //--------------------------------------------------------------------------
        [Fact]
        public void Removes_ArticleComment_With_ChildrenComments_And_Produces_Domain_Events_On_Success()
        {
            //ARRANGE
            var article = GetArticle();

            var articleComment = GetArticleCommentWithoutParentComment();

            var articleCommentChild1 = GetArticleCommentWithParentComment(articleComment.Id);

            var articleCommentChild2 = GetArticleCommentWithParentComment(articleComment.Id);

            article.AddComment(articleComment);

            article.AddComment(articleCommentChild1);

            article.AddComment(articleCommentChild2);

            //ACT
            var exception = Record.Exception(() => article.RemoveComment(articleComment.Id));

            //ASSERT
            exception.ShouldBeNull();

            article.DomainEvents.Count().ShouldBe(6);

            article.ArticleComments.Count().ShouldBe(0);

            var articleCommentAddedEvent = article.DomainEvents.FirstOrDefault(x =>
                x.GetType() == typeof(ArticleCommentAdded)) as ArticleCommentAdded;

            var articleCommentChildAddedEventList = article.DomainEvents.Where(x => x.GetType() == typeof(ArticleCommentChildAdded))
                .Select(x => x as ArticleCommentChildAdded)
                .ToList();

            var articleCommentRemovedEvent = article.DomainEvents.FirstOrDefault(x =>
                x.GetType() == typeof(ArticleCommentRemoved)) as ArticleCommentRemoved;

            var articleCommentChildRemovedEventList = article.DomainEvents.Where(x => x.GetType() == typeof(ArticleCommentChildRemoved))
                .Select(x => x as ArticleCommentChildRemoved)
                .ToList();

            //ArticleCommentAdded event check - Start 

            articleCommentAddedEvent.ShouldNotBeNull();

            articleCommentAddedEvent.Article.ShouldBeSameAs(article);

            articleCommentAddedEvent.ArticleComment.ShouldBeSameAs(articleComment);

            //ArticleCommentAdded event check - End


            //ArticleCommentChildAdded event check - Start 

            articleCommentChildAddedEventList.ShouldNotBeNull();

            articleCommentChildAddedEventList.ShouldNotBeEmpty();

            articleCommentChildAddedEventList.Count().ShouldBe(2);

            foreach (var articleCommentChildAddedEvent in articleCommentChildAddedEventList)
            {
                articleCommentChildAddedEvent.ArticleParentComment.ShouldBeSameAs(articleComment);

                articleCommentChildAddedEvent.ArticleChildComment.ShouldBeOneOf(new[] { articleCommentChild1, articleCommentChild2 });
            }

            //ArticleCommentChildAdded event check - End


            //ArticleCommentRemoved event check - Start 

            articleCommentRemovedEvent.ShouldNotBeNull();

            articleCommentRemovedEvent.Article.ShouldBeSameAs(article);

            articleCommentRemovedEvent.ArticleComment.ShouldBeSameAs(articleComment);

            //ArticleCommentRemoved event check - End


            //ArticleCommentChildRemoved event check - Start

            articleCommentChildRemovedEventList.ShouldNotBeNull();

            articleCommentChildRemovedEventList.ShouldNotBeEmpty();

            articleCommentChildRemovedEventList.Count().ShouldBe(2);

            foreach (var articleCommentChildRemovedEvent in articleCommentChildRemovedEventList)
            {
                articleCommentChildRemovedEvent.ArticleParentComment.ShouldBeSameAs(articleComment);

                articleCommentChildRemovedEvent.ArticleChildComment.ShouldBeOneOf(new[] { articleCommentChild1, articleCommentChild2 });
            }

            //ArticleCommentChildRemoved event check - End
        }
    }
}
