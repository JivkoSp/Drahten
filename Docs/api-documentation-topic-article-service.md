# Topic Article Service API Documentation

This REST API is designed to provide functionalities related to articles (documents) and topics, provided by the **Drahten** application.

## Authentication

This API uses a JWT token provided by the **Authentication Service** for authentication and authorization. The token must be included in every request routed to this API, typically in the ``` Authorization ``` header.
The request must have structure similar to the following:

```json
{
  "Data": {},
  "Token": "jwtToken"
}
```
OR in the headers:

```json
{
  "Authorization": "Bearer <jwtToken>"
}
```

## Allowed endpoints:

<p align="center">
  <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/topic_article_service_api_1.PNG" alt="Allowed Endpoints" width="800">
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/topic_article_service_api_2.PNG" alt="Allowed Endpoints" width="800">
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/topic_article_service_api_3.PNG" alt="Allowed Endpoints" width="800">
</p>

## Endpoints description

#### Get all articles (documents).
**Endpoint**: GET ``` topic-article_service/articles/ ```

#### Register article (document).
**Endpoint**: POST ``` topic-article-service/articles/ ```

#### Get article (document).
**Endpoint**: GET ``` topic-article-service/articles/{ArticleId}/ ```

#### Get all articles (documents) that are viewed by users.
**Endpoint**: GET ``` topic-article-service/user-articles/{UserId}/ ```

#### Get article (document) likes.
**Endpoint**: GET ``` topic-article-service/articles/{ArticleId}/likes/ ```

#### Register article (document) like.
**Endpoint**: POST ``` topic-article-service/articles/{ArticleId}/likes/ ```

#### Get article (document) dislikes.
**Endpoint**: GET ``` topic-article-service/articles/{ArticleId}/dislikes/ ```

#### Register article (document) dislike.
**Endpoint**: POST ``` topic-article-service/articles/{ArticleId}/dislikes/ ```

#### Get article (document) comments.
**Endpoint**: GET ``` topic-article-service/articles/{ArticleId}/comments/ ```

#### Register article (document) comment.
**Endpoint**: POST ``` topic-article-service/articles/{ArticleId}/comments/ ```

#### Register article (document) comment like.
**Endpoint**: POST ``` topic-article-service/comments/{ArticleCommentId}/likes/ ```

#### Register article (document) comment dislike.
**Endpoint**: POST ``` topic-article-service/comments/{ArticleCommentId}/dislikes/ ```

#### Remove comment about article (document).
**Endpoint**: DELETE ``` topic-article-service/articles/{ArticleId}/comments/{ArticleCommentId}/ ```

#### Get all topics.
**Endpoint**: GET ``` topic-article-service/topics/ ```

#### Get topic subscriptions.
**Endpoint**: GET ``` topic-article-service/topics/{TopicId}/subscriptions/ ```

#### Get the parent topic of a topic.
**Endpoint**: GET ``` topic-article-service/topics/{TopicId}/parent-topic/ ```

#### Get all topics related to a user (the user subscribed topics).
**Endpoint**: GET ``` topic-article-service/topics/{UserId}/user-topics/ ```

#### Get all users related to a article (users that are visited the article).
**Endpoint**: GET ``` topic-article-service/users/articles/{ArticleId}/ ```

#### Register article that is visited by a user.
**Endpoint**: POST ``` topic-article-service/users/{UserId}/articles/ ```

#### Register topic that is subscribed by a user.
**Endpoint**: POST ``` topic-article-service/users/{UserId}/topics/ ```

#### Register user.
**Endpoint**: POST ``` topic-article-service/users/ ```
