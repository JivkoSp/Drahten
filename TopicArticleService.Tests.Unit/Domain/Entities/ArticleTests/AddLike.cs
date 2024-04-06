using Shouldly;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.Events;
using TopicArticleService.Domain.Exceptions;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.ValueObjects;
using Xunit;

namespace TopicArticleService.Tests.Unit.Domain.Entities.ArticleTests
{
    public sealed class AddLike
    {
        #region GLOBAL ARRANGE

        private readonly IArticleFactory _articleFactory;
        private readonly ArticleLikeFactory _articleLikeFactory;
        private readonly ArticleDislikeFactory _articleDislikeFactory;

        private Article GetArticle()
        {
            var article = _articleFactory.Create(Guid.NewGuid(), "some title", "main title", "some content", "no date",
                    "no author", "no link", Guid.NewGuid());

            article.ClearEvents();

            return article;
        }

        private ArticleLike GetArticleLike(ArticleID articleId, UserID userId = null)
        {
            userId = userId ?? Guid.NewGuid();

            var articleLike = _articleLikeFactory.Create(articleId, userId, "2021-02-04T00:00:00");

            return articleLike;
        }

        private ArticleDislike GetArticleDislike(ArticleID articleId)
        {
            var articleDislike = _articleDislikeFactory.Create(articleId, Guid.NewGuid(), "2021-02-03T00:00:00");

            return articleDislike;
        }

        public AddLike()
        {
            _articleFactory = new ArticleFactory();
            _articleLikeFactory = new ArticleLikeFactory();
            _articleDislikeFactory = new ArticleDislikeFactory();
        }

        #endregion

        //Should throw ArticleLikeAlreadyExistsException when the following condition is met:
        //There is already article like (ArticleLike value object) with matching UserID.
        [Fact]
        public void ArticleLike_Throws_ArticleLikeAlreadyExistsException()
        {
            //ARRANGE
            var article = GetArticle();

            var articleLike = GetArticleLike(article.Id);

            article.AddLike(articleLike);

            //ACT
            var exception = Record.Exception(() => article.AddLike(articleLike));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ArticleLikeAlreadyExistsException>();
        }

        //Should add article like (ArticleLike value object) to internal collection of ArticleLike value objects and
        //remove article dislike (ArticleDislike value object) from internal collection of ArticleDislike value objects
        //for User with the same UserID.
        //This means that when same user has dislike for particular article and choose to add like, the dislike should be
        //removed.
        //--------------------------------------------------------------------------
        //Should produce the following three domain events:
        // * One ArticleDislikeAdded domain event.
        // * One ArticleDislikeRemoved domain event.
        // * One ArticleLikeAdded domain event.
        //--------------------------------------------------------------------------
        //The ArticleDislikeAdded domain event should contain:
        // 1. The same article entity that the article dislike was added to.
        // 2. The same article dislike value object that was added to the internal collection of ArticleDislike value objects.
        //--------------------------------------------------------------------------
        //The ArticleDislikeRemoved domain event should contain:
        // 1. The same article entity that the article dislike was removed from.
        // 2. The same article dislike value object that was removed from the internal collection of ArticleDislike value objects.
        //--------------------------------------------------------------------------
        //The ArticleLikeAdded domain event should contain:
        // 1. The same article entity that the article like was added to.
        // 2. The same article like value object that was added to the internal collection of ArticleLike value objects.
        //--------------------------------------------------------------------------
        [Fact]
        public void Adds_ArticleLike_For_User_That_Has_ArticleDislike_And_Produces_Domain_Events_On_Success()
        {
            //ARRANGE
            var article = GetArticle();

            var articleDislike = GetArticleDislike(article.Id);

            var articleLike = GetArticleLike(article.Id, articleDislike.UserID);

            article.AddDislike(articleDislike);

            //ACT
            var exception = Record.Exception(() => article.AddLike(articleLike));

            //ASSERT
            exception.ShouldBeNull();

            article.DomainEvents.Count().ShouldBe(3);

            article.ArticleDislikes.Count().ShouldBe(0);

            article.ArticleLikes.Count().ShouldBe(1);

            var articleDislikeAddedEvent = article.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(ArticleDislikeAdded)) as ArticleDislikeAdded;

            var articleDislikeRemovedEvent = article.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(ArticleDislikeRemoved)) as ArticleDislikeRemoved;

            var articleLikeAddedEvent = article.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(ArticleLikeAdded)) as ArticleLikeAdded;

            //ArticleDislikeAdded event check - Start

            articleDislikeAddedEvent.ShouldNotBeNull();

            articleDislikeAddedEvent.Article.ShouldBeSameAs(article);

            articleDislikeAddedEvent.Dislike.ShouldBeSameAs(articleDislike);

            //ArticleDislikeAdded event check - End


            //ArticleDislikeRemoved event check - Start

            articleDislikeRemovedEvent.ShouldNotBeNull();

            articleDislikeRemovedEvent.Article.ShouldBeSameAs(article);

            articleDislikeRemovedEvent.Dislike.ShouldBeSameAs(articleDislike);

            //ArticleDislikeRemoved event check - End


            //ArticleLikeAdded event check - Start

            articleLikeAddedEvent.ShouldNotBeNull();

            articleLikeAddedEvent.Article.ShouldBeSameAs(article);

            articleLikeAddedEvent.Like.ShouldBeSameAs(articleLike);

            //ArticleLikeAdded event check - End
        }

        //Should add article like (ArticleLike value object) to internal collection of ArticleLike value objects
        //and produce ArticleLikeAdded domain event.
        //The ArticleLikeAdded domain event should contain:
        // 1. The same article entity that the article like was added to.
        // 2. The same article like value object that was added to the internal collection of ArticleLike value objects.
        [Fact]
        public void Adds_ArticleLike_And_Produces_ArticleLikeAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var article = GetArticle();

            var articleLike = GetArticleLike(article.Id);

            //ACT
            var exception = Record.Exception(() => article.AddLike(articleLike));

            //ASSERT
            exception.ShouldBeNull();

            article.DomainEvents.Count().ShouldBe(1);

            article.ArticleDislikes.Count().ShouldBe(0);

            article.ArticleLikes.Count().ShouldBe(1);

            var articleLikeAddedEvent = article.DomainEvents.FirstOrDefault() as ArticleLikeAdded;

            articleLikeAddedEvent.ShouldNotBeNull();

            articleLikeAddedEvent.Article.ShouldBeSameAs(article);

            articleLikeAddedEvent.Like.ShouldBeSameAs(articleLike);
        }
    }
}
