This directory contains unit tests for the api endpoints in the presentation layer.
--------------------------------------------------------
				**** PostgreSQL ****
--------------------------------------------------------
PostgreSQL preserves microsecond-level precision when storing a DateTimeOffset value but does not retain 
nanosecond-level precision. As a result, any nanoseconds present in the original DateTimeOffset value would be rounded or truncated 
to microseconds when stored in the database. Therefore, there might be instances where the time of two objects that were previously 
initialized with equal times is not exactly equal.
Due to this reason, the time comparison is done with a tolerance of 1 millisecond.
--------------------------------------------------------
				**** PostgreSQL ****
--------------------------------------------------------


--------------------------------------------------------
*** Special features for RegisterArticleCommentTests *** 
--------------------------------------------------------
About the Register_ArticleComment_Endpoint_Should_Add_ArticleComment_With_Given_ArticleCommentId_To_The_Database(...) method:
------------
The time comaprison of the objects addArticleCommentCommand and articleCommentDto is NOT done like this: 
articleCommentDto.DateTime.ShouldBe(addArticleCommentCommand.DateTime);
This is because of the reasons mentioned in the section - **** PostgreSQL **** in this document.
--------------------------------------------------------
*** Special features for RegisterArticleCommentTests *** 
--------------------------------------------------------


---------------------------------------------------------------
*** Special features for RegisterArticleCommentLikeTests *** 
---------------------------------------------------------------
About the Register_ArticleCommentLike_Endpoint_Should_Add_ArticleCommentLike_With_Given_ArticleCommentId_And_UserId_To_The_Database(...) method:
------------
The time comaprison of the objects addArticleCommentLikeCommand and articleCommentLike is NOT done like this:
articleCommentLike.DateTime.ShouldBe(addArticleCommentLikeCommand.DateTime);
This is because of the reasons mentioned in the section - **** PostgreSQL **** in this document.
---------------------------------------------------------------
*** Special features for RegisterArticleCommentLikeTests *** 
---------------------------------------------------------------


---------------------------------------------------------------
*** Special features for RegisterArticleCommentDislikeTests *** 
---------------------------------------------------------------
About the Register_ArticleCommentDislike_Endpoint_Should_Add_ArticleCommentDislike_With_Given_ArticleCommentId_And_UserId_To_The_Database(...) method:
------------
The time comaprison of the objects addArticleCommentDislikeCommand and articleCommentDislike is NOT done like this:
articleCommentDislike.DateTime.ShouldBe(addArticleCommentDislikeCommand.DateTime);
This is because of the reasons mentioned in the section - **** PostgreSQL **** in this document.
---------------------------------------------------------------
*** Special features for RegisterArticleCommentDislikeTests *** 
---------------------------------------------------------------


--------------------------------------------------------
*** Special features for RegisterArticleLikeTests *** 
--------------------------------------------------------
About the Register_ArticleLike_Endpoint_Should_Add_ArticleLike_With_Given_ArticleId_And_UserId_To_The_Database(...) method:
------------
The time comaprison of the objects addArticleLikeCommand and articleLike is NOT done like this: 
articleLike.DateTime.ShouldBe(addArticleLikeCommand.DateTime);
This is because of the reasons mentioned in the section - **** PostgreSQL **** in this document.
--------------------------------------------------------
*** Special features for RegisterArticleLikeTests *** 
--------------------------------------------------------


--------------------------------------------------------
*** Special features for RegisterArticleDislikeTests *** 
--------------------------------------------------------
About the Register_ArticleDislike_Endpoint_Should_Add_ArticleDislike_With_Given_ArticleId_And_UserId_To_The_Database(...) method:
------------
The time comaprison of the objects addArticleDislikeCommand and articleDislike is NOT done like this: 
articleDislike.DateTime.ShouldBe(addArticleDislikeCommand.DateTime);
This is because of the reasons mentioned in the section - **** PostgreSQL **** in this document.
--------------------------------------------------------
*** Special features for RegisterArticleDislikeTests *** 
--------------------------------------------------------