This directory contains unit tests for the api endpoints in the presentation layer.
-------------------------
*** Special features for RegisterArticleCommentTests *** 
--------------------------------------------------------
About the Register_ArticleComment_Endpoint_Should_Add_ArticleComment_With_Given_ArticleCommentId_To_The_Database(...) method:
------------
The time comaprison of the objects addArticleCommentCommand and articleCommentDto is NOT done like this: 
articleCommentDto.DateTime.ShouldBe(addArticleCommentCommand.DateTime);
This is because PostgreSQL preserves microsecond-level precision when storing a DateTimeOffset value but does not retain 
nanosecond-level precision. As a result, any nanoseconds present in the original DateTimeOffset value would be rounded or truncated 
to microseconds when stored in the database. Therefore, there might be instances where the articleCommentDto time is not exactly equal 
to the addArticleCommentCommand time.
Due to this reason, the time comparison is done with a tolerance of 1 millisecond.
--------------------------------------------------------
*** Special features for RegisterArticleCommentTests *** 
--------------------------------------------------------