using Shouldly;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.Events;
using TopicArticleService.Domain.Exceptions;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.ValueObjects;
using Xunit;

namespace TopicArticleService.Tests.Unit.Domain.Entities.ArticleTests
{
    public sealed class AddDislike
    {
        #region GLOBAL ARRANGE

        private readonly IArticleFactory _articleFactory;

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

            var articleLike = new ArticleLike(articleId, userId, DateTimeOffset.Now);
                
            return articleLike;
        }

        private ArticleDislike GetArticleDislike(ArticleID articleId, UserID userId = null)
        {
            userId = userId ?? Guid.NewGuid();

            var articleDislike = new ArticleDislike(articleId, userId, DateTimeOffset.Now);
                
            return articleDislike;
        }

        public AddDislike()
        {
            _articleFactory = new ArticleFactory();
        }

        #endregion

        //Should throw ArticleDislikeAlreadyExistsException when the following condition is met:
        //There is already article dislike (ArticleDislike value object) with matching UserID.
        [Fact]
        public void ArticleDislike_Throws_ArticleDislikeAlreadyExistsException()
        {
            //ARRANGE
            var article = GetArticle();

            var articleDislike = GetArticleDislike(article.Id);

            article.AddDislike(articleDislike);

            //ACT
            var exception = Record.Exception(() => article.AddDislike(articleDislike));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ArticleDislikeAlreadyExistsException>();
        }

        //Should add article dislike (ArticleDislike value object) to internal collection of ArticleDislike value objects and
        //remove article like (ArticleLike value object) from internal collection of ArticleLike value objects
        //for User with the same UserID.
        //This means that when same user has like for particular article and choose to add dislike, the like should be
        //removed.
        //--------------------------------------------------------------------------
        //Should produce the following three domain events:
        // * One ArticleLikeAdded domain event.
        // * One ArticleLikeRemoved domain event.
        // * One ArticleDislikeAdded domain event.
        //--------------------------------------------------------------------------
        //The ArticleLikeAdded domain event should contain:
        // 1. The same article entity that the article like was added to.
        // 2. The same article like value object that was added to the internal collection of ArticleLike value objects.
        //--------------------------------------------------------------------------
        //The ArticleLikeRemoved domain event should contain:
        // 1. The same article entity that the article like was removed from.
        // 2. The same article like value object that was removed from the internal collection of ArticleLike value objects.
        //--------------------------------------------------------------------------
        //The ArticleDislikeAdded domain event should contain:
        // 1. The same article entity that the article dislike was added to.
        // 2. The same article dislike value object that was added to the internal collection of ArticleDislike value objects.
        //--------------------------------------------------------------------------
        [Fact]
        public void Adds_ArticleDislike_For_User_That_Has_ArticleLike_And_Produces_Domain_Events_On_Success()
        {
            //ARRANGE
            var article = GetArticle();

            var articleLike = GetArticleLike(article.Id);

            var articleDislike = GetArticleDislike(article.Id, articleLike.UserID);

            article.AddLike(articleLike);

            //ACT
            var exception = Record.Exception(() => article.AddDislike(articleDislike));

            //ASSERT
            exception.ShouldBeNull();

            article.DomainEvents.Count().ShouldBe(3);

            article.ArticleDislikes.Count().ShouldBe(1);

            article.ArticleLikes.Count().ShouldBe(0);

            var articleLikeAddedEvent = article.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(ArticleLikeAdded)) as ArticleLikeAdded;

            var articleLikeRemovedEvent = article.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(ArticleLikeRemoved)) as ArticleLikeRemoved;

            var articleDislikeAddedEvent = article.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(ArticleDislikeAdded)) as ArticleDislikeAdded;

            //ArticleLikeAdded event check - Start

            articleLikeAddedEvent.ShouldNotBeNull();

            articleLikeAddedEvent.Article.ShouldBeSameAs(article);

            articleLikeAddedEvent.Like.ShouldBeSameAs(articleLike);

            //ArticleLikeAdded event check - End


            //ArticleLikeRemoved event check - Start

            articleLikeRemovedEvent.ShouldNotBeNull();

            articleLikeRemovedEvent.Article.ShouldBeSameAs(article);

            articleLikeRemovedEvent.Like.ShouldBeSameAs(articleLike);

            //ArticleLikeRemoved event check - Start


            //ArticleDislikeAdded event check - Start

            articleDislikeAddedEvent.ShouldNotBeNull();

            articleDislikeAddedEvent.Article.ShouldBeSameAs(article);

            articleDislikeAddedEvent.Dislike.ShouldBeSameAs(articleDislike);

            //ArticleDislikeAdded event check - End
        }

        //Should add article dislike (ArticleDislike value object) to internal collection of ArticleDislike value objects
        //and produce ArticleDislikeAdded domain event.
        //The ArticleDislikeAdded domain event should contain:
        // 1. The same article entity that the article dislike was added to.
        // 2. The same article dislike value object that was added to the internal collection of ArticleDislike value objects.
        [Fact]
        public void Adds_ArticleDislike_And_Produces_ArticleDislikeAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var article = GetArticle();

            var articleDislike = GetArticleDislike(article.Id);

            //ACT
            var exception = Record.Exception(() => article.AddDislike(articleDislike));

            //ASSERT
            exception.ShouldBeNull();

            article.DomainEvents.Count().ShouldBe(1);

            article.ArticleDislikes.Count().ShouldBe(1);

            article.ArticleLikes.Count().ShouldBe(0);

            var articleDislikeAddedEvent = article.DomainEvents.FirstOrDefault() as ArticleDislikeAdded;

            articleDislikeAddedEvent.ShouldNotBeNull();

            articleDislikeAddedEvent.Article.ShouldBeSameAs(article);

            articleDislikeAddedEvent.Dislike.ShouldBeSameAs(articleDislike);
        }
    }
}
