using Shouldly;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.Events;
using TopicArticleService.Domain.Exceptions;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.ValueObjects;
using Xunit;

namespace TopicArticleService.Domain.Tests.Unit.ArticleCommentTests
{
    public sealed class AddDislike
    {
        #region GLOBAL ARRANGE

        private readonly ArticleCommentFactory _articleCommentFactory;
        private readonly ArticleCommentLikeFactory _articleCommentLikeFactory;
        private readonly ArticleCommentDislikeFactory _articleCommentDislikeFactory;

        private ArticleComment GetArticleComment()
        {
            var articleComment = _articleCommentFactory.Create(Guid.NewGuid(), "some comment", DateTime.Now,
                    Guid.NewGuid(), null);

            articleComment.ClearEvents();

            return articleComment;
        }

        private ArticleCommentLike GetArticleCommentLike(ArticleCommentID articleCommentId, UserID userId = null)
        {
            userId = userId ?? Guid.NewGuid();

            var articleCommentLike = _articleCommentLikeFactory.Create(articleCommentId, userId, "2022-08-10T14:38:00");

            return articleCommentLike;
        }

        private ArticleCommentDislike GetArticleCommentDislike(ArticleCommentID articleCommentId, UserID userId = null)
        {
            userId = userId ?? Guid.NewGuid();

            var articleCommentDislike = _articleCommentDislikeFactory.Create(articleCommentId, userId, "2022-08-10T14:39:00");

            return articleCommentDislike;
        }

        public AddDislike()
        {
            _articleCommentFactory = new ArticleCommentFactory();
            _articleCommentLikeFactory = new ArticleCommentLikeFactory();
            _articleCommentDislikeFactory = new ArticleCommentDislikeFactory();
        }

        #endregion

        //Should throw ArticleCommentDisLikeAlreadyExistsException when the following condition is met:
        //There is already dislike for this article comment (ArticleCommentDislike value object) with matching UserID.
        [Fact]
        public void ArticleCommentDislike_Throws_ArticleCommentDisLikeAlreadyExistsException()
        {
            //ARRANGE
            var articleComment = GetArticleComment();

            var articleCommentDislike = GetArticleCommentDislike(articleComment.Id);

            articleComment.AddDislike(articleCommentDislike);

            //ACT
            var exception = Record.Exception(() => articleComment.AddDislike(articleCommentDislike));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ArticleCommentDisLikeAlreadyExistsException>();
        }

        //Should add dislike for article comment (ArticleCommentDislike value object) to internal collection of ArticleCommentDislike
        //value objects and remove like for the same article comment (ArticleCommentLike value object) from internal collection
        //of ArticleCommentLike value objects for User with the same UserID.
        //This means that when same user has like for particular article comment and choose to add dislike to that comment, the like
        //should be removed.
        //--------------------------------------------------------------------------
        //Should produce the following three domain events:
        // * One ArticleCommentLikeAdded domain event.
        // * One ArticleCommentLikeRemoved domain event.
        // * One ArticleCommentDislikeAdded domain event.
        //--------------------------------------------------------------------------
        //The ArticleCommentLikeAdded domain event should contain:
        // 1. The same article comment entity that the article comment like (the ArticleCommentLike value object) was added to.
        // 2. The same article comment like (ArticleCommentLike) value object that was added to the internal collection
        // of ArticleCommentLike value objects.
        //--------------------------------------------------------------------------
        //The ArticleCommentLikeRemoved domain event should contain:
        // 1. The same article comment entity that the article comment like (the ArticleCommentLike value object) was removed from.
        // 2. The same article comment like (ArticleCommentLike) value object that was removed from the internal collection
        // of ArticleCommentLike value objects.
        //--------------------------------------------------------------------------
        //The ArticleCommentDislikeAdded domain event should contain:
        // 1. The same article comment entity that the article comment dislike (the ArticleCommentDislike value object) was added to.
        // 2. The same article comment dislike (ArticleCommentDislike) value object that was added to the internal collection
        // of ArticleCommentDislike value objects.
        //--------------------------------------------------------------------------
        [Fact]
        public void Adds_ArticleCommentDislike_For_User_That_Has_ArticleCommentLike_And_Produces_Domain_Events_On_Success()
        {
            //ARRANGE
            var articleComment = GetArticleComment();

            var articleCommentLike = GetArticleCommentLike(articleComment.Id);

            var articleCommentDislike = GetArticleCommentDislike(articleComment.Id, articleCommentLike.UserID);

            articleComment.AddLike(articleCommentLike);

            //ACT
            var exception = Record.Exception(() => articleComment.AddDislike(articleCommentDislike));

            //ASSERT
            exception.ShouldBeNull();

            articleComment.DomainEvents.Count().ShouldBe(3);

            articleComment.ArticleCommentDislikes.Count().ShouldBe(1);

            articleComment.ArticleCommentLikes.Count().ShouldBe(0);

            var articleCommentLikeAddedEvent = articleComment.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(ArticleCommentLikeAdded)) as ArticleCommentLikeAdded;

            var articleCommentLikeRemovedEvent = articleComment.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(ArticleCommentLikeRemoved)) as ArticleCommentLikeRemoved;

            var articleCommentDislikeAddedEvent = articleComment.DomainEvents.FirstOrDefault(x =>
                x.GetType() == typeof(ArticleCommentDislikeAdded)) as ArticleCommentDislikeAdded;

            //ArticleCommentLikeAdded event check - Start

            articleCommentLikeAddedEvent.ShouldNotBeNull();

            articleCommentLikeAddedEvent.ArticleComment.ShouldBeSameAs(articleComment);

            articleCommentLikeAddedEvent.ArticleCommentLike.ShouldBeSameAs(articleCommentLike);

            //ArticleCommentLikeAdded event check - End


            //ArticleCommentLikeRemoved event check - Start

            articleCommentLikeRemovedEvent.ShouldNotBeNull();

            articleCommentLikeRemovedEvent.ArticleComment.ShouldBeSameAs(articleComment);

            articleCommentLikeRemovedEvent.ArticleCommentLike.ShouldBeSameAs(articleCommentLike);

            //ArticleCommentLikeRemoved event check - End


            //ArticleCommentDislikeAdded event check - Start

            articleCommentDislikeAddedEvent.ShouldNotBeNull();

            articleCommentDislikeAddedEvent.ArticleComment.ShouldBeSameAs(articleComment);

            articleCommentDislikeAddedEvent.ArticleCommentDislike.ShouldBeSameAs(articleCommentDislike);

            //ArticleCommentDislikeAdded event check - End
        }

        //Should add dislike for article comment (ArticleCommentDislike value object) to internal collection of ArticleCommentDislike
        //value objects and produce ArticleCommentDislikeAdded domain event.
        //The ArticleCommentDislikeAdded domain event should contain:
        // 1. The same article comment entity that the article comment dislike (the ArticleCommentDislike value object) was added to.
        // 2. The same article comment dislike (ArticleCommentDislike) value object that was added to the internal collection
        // of ArticleCommentDislike value objects.
        [Fact]
        public void Adds_ArticleCommentDislike_And_Produces_ArticleCommentDislikeAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var articleComment = GetArticleComment();

            var articleCommentDislike = GetArticleCommentDislike(articleComment.Id);

            //ACT
            var exception = Record.Exception(() => articleComment.AddDislike(articleCommentDislike));

            //ASSERT
            exception.ShouldBeNull();

            articleComment.DomainEvents.Count().ShouldBe(1);

            articleComment.ArticleCommentDislikes.Count().ShouldBe(1);

            articleComment.ArticleCommentLikes.Count().ShouldBe(0);

            var articleCommentDislikeAddedEvent = articleComment.DomainEvents.FirstOrDefault() as ArticleCommentDislikeAdded;

            articleCommentDislikeAddedEvent.ShouldNotBeNull();

            articleCommentDislikeAddedEvent.ArticleComment.ShouldBeSameAs(articleComment);

            articleCommentDislikeAddedEvent.ArticleCommentDislike.ShouldBeSameAs(articleCommentDislike);
        }
    }
}
