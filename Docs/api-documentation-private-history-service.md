# Private History Service API Documentation

This REST API is designed to provide data about the private history of users who are using the **Drahten** application.

## Authentication

This API uses a JWT token provided by the **Authentication Service** for authentication and authorization. The token must be included in every request routed to this API, typically in the ``` Authorization ``` header.
The request must have the headers:

```json
{
  "Authorization": "Bearer <jwtToken>"
}
```

## Allowed endpoints:

<p align="center">
  <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/private_history_service_api_1.PNG" alt="Allowed Endpoints" width="800">
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/private_history_service_api_2.PNG" alt="Allowed Endpoints" width="800">
</p>

## Endpoints description

#### Get all commented articles by a user.
**Endpoint**: GET ``` privatehistory-service/users/{UserId}/commented-articles/ ```

#### Get all liked articles by a user.
**Endpoint**: GET ``` privatehistory-service/users/{UserId}/liked-articles/ ```

#### Get all disliked articles by a user.
**Endpoint**: GET ``` privatehistory-service/users/{UserId}/disliked-articles/ ```

#### Get all liked article comments by a user.
**Endpoint**: GET ``` privatehistory-service/users/{UserId}/liked-article-comments/ ```

#### Get all disliked article comments by a user.
**Endpoint**: GET ``` privatehistory-service/users/{UserId}/disliked-article-comments/ ```

#### Get all searched articles by a user.
**Endpoint**: GET ``` privatehistory-service/users/{UserId}/searched-articles/ ```

#### Get all searched topics by a user.
**Endpoint**: GET ``` privatehistory-service/users/{UserId}/searched-topics/ ```

#### Get all topic subscriptions for a user.
**Endpoint**: GET ``` privatehistory-service/users/{UserId}/topic-subscriptions/ ```

#### Get all viewed articles by a user.
**Endpoint**: GET ``` privatehistory-service/users/{UserId}/viewed-articles/ ```

#### Get all viewed users by a user.
**Endpoint**: GET ``` privatehistory-service/users/{UserId}/viewed-users/ ```

#### Register a user.
**Endpoint**: POST ``` privatehistory-service/users/{UserId}/ ```

#### Register retention time for a user.
**Endpoint**: POST ``` privatehistory-service/users/{UserId}/retention-datetime/ ```

#### Register commented article for user.
**Endpoint**: POST ``` privatehistory-service/users/{UserId}/commented-articles/{ArticleId}/ ```

#### Register liked article comment by a user.
**Endpoint**: POST ``` privatehistory-service/users/{UserId}/liked-article-comments/{ArticleCommentId}/ ```

#### Register disliked article comment by a user.
**Endpoint**: POST ``` privatehistory-service/users/{UserId}/disliked-article-comments/{ArticleCommentId}/ ```

#### Register searched article by a user.
**Endpoint**: POST ``` privatehistory-service/users/{UserId}/searched-articles/{ArticleId}/ ```

#### Register searched topic by a user.
**Endpoint**: POST ``` privatehistory-service/users/{UserId}/searched-topics/{TopicId}/ ```

#### Register topic subscription for a user.
**Endpoint**: POST ``` privatehistory-service/users/{UserId}/topic-subscriptions/{TopicId}/ ```

#### Delete topic subscription for a user.
**Endpoint**: DELETE ``` privatehistory-service/users/{UserId}/topic-subscriptions/{TopicId}/ ```

#### Register viewed article by a user.
**Endpoint**: POST ``` privatehistory-service/users/{UserId}/viewed-articles/{ArticleId}/ ```

#### Register viewed user by a user.
**Endpoint**: POST ``` privatehistory-service/users/{ViewerUserId}/viewed-users/{ViewedUserId}/ ```

#### Remove viewed user by a user.
**Endpoint**: DELETE ``` privatehistory-service/users/{ViewerUserId}/viewed-users/{ViewedUserId}/ ```

#### Remove commented article by a user.
**Endpoint**: DELETE ``` privatehistory-service/users/{UserId}/commented-articles/{CommentedArticleId}/ ```

#### Remove searched article data by a user.
**Endpoint**: DELETE ``` privatehistory-service/users/{UserId}/searched-articles/{SearchedArticleDataId}/ ```

#### Remove searched topic data by a user.
**Endpoint**: DELETE ``` privatehistory-service/users/{UserId}/searched-topics/{SearchedTopicDataId}/ ```

#### Remove viewed article by a user.
**Endpoint**: DELETE ``` privatehistory-service/users/{UserId}/viewed-articles/{ViewedArticleId}/ ```
