using Shouldly;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.Events;
using TopicArticleService.Domain.Exceptions;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.ValueObjects;
using Xunit;

namespace TopicArticleService.Tests.Unit.Domain.ArticleCommentTests
{
    public sealed class AddLike
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

        private ArticleCommentDislike GetArticleCommentDislike(ArticleCommentID articleCommentId)
        {
            var articleCommentDislike = _articleCommentDislikeFactory.Create(articleCommentId, Guid.NewGuid(), "2022-08-10T14:39:00");

            return articleCommentDislike;
        }

        public AddLike()
        {
            _articleCommentFactory = new ArticleCommentFactory();
            _articleCommentLikeFactory = new ArticleCommentLikeFactory();
            _articleCommentDislikeFactory = new ArticleCommentDislikeFactory();
        }

        #endregion

        //Should throw ArticleCommentLikeAlreadyExistsException when the following condition is met:
        //There is already like for this article comment (ArticleCommentLike value object) with matching UserID.
        [Fact]
        public void ArticleCommentLike_Throws_ArticleCommentLikeAlreadyExistsException()
        {
            //ARRANGE
            var articleComment = GetArticleComment();

            var articleCommentLike = GetArticleCommentLike(articleComment.Id);

            articleComment.AddLike(articleCommentLike);

            //ACT
            var exception = Record.Exception(() => articleComment.AddLike(articleCommentLike));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ArticleCommentLikeAlreadyExistsException>();
        }

        //Should add like for article comment (ArticleCommentLike value object) to internal collection of ArticleCommentLike value objects and
        //remove dislike for the same article comment (ArticleCommentDislike value object) from internal collection of ArticleCommentDislike
        //value objects for User with the same UserID.
        //This means that when same user has dislike for particular article comment and choose to add like to that comment, the dislike
        //should be removed.
        //--------------------------------------------------------------------------
        //Should produce the following three domain events:
        // * One ArticleCommentDislikeAdded domain event.
        // * One ArticleCommentDislikeRemoved domain event.
        // * One ArticleCommentLikeAdded domain event.
        //--------------------------------------------------------------------------
        //The ArticleCommentDislikeAdded domain event should contain:
        // 1. The same article comment entity that the article comment dislike (the ArticleCommentDislike value object) was added to.
        // 2. The same article comment dislike (ArticleCommentDislike) value object that was added to the internal collection
        // of ArticleCommentDislike value objects.
        //--------------------------------------------------------------------------
        //The ArticleCommentDislikeRemoved domain event should contain:
        // 1. The same article comment entity that the article comment dislike (the ArticleCommentDislike value object) was removed from.
        // 2. The same article comment dislike (ArticleCommentDislike) value object that was removed from the internal collection
        // of ArticleCommentDislike value objects.
        //--------------------------------------------------------------------------
        //The ArticleCommentLikeAdded domain event should contain:
        // 1. The same article comment entity that the article comment like (the ArticleCommentLike value object) was added to.
        // 2. The same article comment like (ArticleCommentLike) value object that was added to the internal collection
        // of ArticleCommentLike value objects.
        //--------------------------------------------------------------------------
        [Fact]
        public void Adds_ArticleCommentLike_For_User_That_Has_ArticleCommentDislike_And_Produces_Domain_Events_On_Success()
        {
            //ARRANGE
            var articleComment = GetArticleComment();

            var articleCommentDislike = GetArticleCommentDislike(articleComment.Id);

            var articleCommentLike = GetArticleCommentLike(articleComment.Id, articleCommentDislike.UserID);

            articleComment.AddDislike(articleCommentDislike);

            //ACT
            var exception = Record.Exception(() => articleComment.AddLike(articleCommentLike));

            //ASSERT
            exception.ShouldBeNull();

            articleComment.DomainEvents.Count().ShouldBe(3);

            articleComment.ArticleCommentDislikes.Count().ShouldBe(0);

            articleComment.ArticleCommentLikes.Count().ShouldBe(1);

            var articleCommentDislikeAddedEvent = articleComment.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(ArticleCommentDislikeAdded)) as ArticleCommentDislikeAdded;

            var articleCommentDislikeRemovedEvent = articleComment.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(ArticleCommentDislikeRemoved)) as ArticleCommentDislikeRemoved;

            var articleCommentLikeAddedEvent = articleComment.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(ArticleCommentLikeAdded)) as ArticleCommentLikeAdded;

            //ArticleCommentDislikeAdded event check - Start

            articleCommentDislikeAddedEvent.ShouldNotBeNull();

            articleCommentDislikeAddedEvent.ArticleComment.ShouldBeSameAs(articleComment);

            articleCommentDislikeAddedEvent.ArticleCommentDislike.ShouldBeSameAs(articleCommentDislike);

            //ArticleCommentDislikeAdded event check - End


            //ArticleCommentDislikeRemoved event check - Start

            articleCommentDislikeRemovedEvent.ShouldNotBeNull();

            articleCommentDislikeRemovedEvent.ArticleComment.ShouldBeSameAs(articleComment);

            articleCommentDislikeRemovedEvent.ArticleCommentDislike.ShouldBeSameAs(articleCommentDislike);

            //ArticleCommentDislikeRemoved event check - End


            //ArticleCommentLikeAdded event check - Start

            articleCommentLikeAddedEvent.ShouldNotBeNull();

            articleCommentLikeAddedEvent.ArticleComment.ShouldBeSameAs(articleComment);

            articleCommentLikeAddedEvent.ArticleCommentLike.ShouldBeSameAs(articleCommentLike);

            //ArticleCommentLikeAdded event check - End
        }

        //Should add like for article comment (ArticleCommentLike value object) to internal collection of ArticleCommentLike value objects
        //and produce ArticleCommentLikeAdded domain event.
        //The ArticleCommentLikeAdded domain event should contain:
        // 1. The same article comment entity that the article comment like (the ArticleCommentLike value object) was added to.
        // 2. The same article comment like (ArticleCommentLike) value object that was added to the internal collection
        // of ArticleCommentLike value objects.
        [Fact]
        public void Adds_ArticleCommentLike_And_Produces_ArticleCommentLikeAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var articleComment = GetArticleComment();

            var articleCommentLike = GetArticleCommentLike(articleComment.Id);

            //ACT
            var exception = Record.Exception(() => articleComment.AddLike(articleCommentLike));

            //ASSERT
            exception.ShouldBeNull();

            articleComment.DomainEvents.Count().ShouldBe(1);

            articleComment.ArticleCommentDislikes.Count().ShouldBe(0);

            articleComment.ArticleCommentLikes.Count().ShouldBe(1);

            var articleCommentLikeAddedEvent = articleComment.DomainEvents.FirstOrDefault() as ArticleCommentLikeAdded;

            articleCommentLikeAddedEvent.ShouldNotBeNull();

            articleCommentLikeAddedEvent.ArticleComment.ShouldBeSameAs(articleComment);

            articleCommentLikeAddedEvent.ArticleCommentLike.ShouldBeSameAs(articleCommentLike);
        }
    }
}
