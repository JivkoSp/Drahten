# Public History Service API Documentation

This REST API is designed to provide data about the public history of users who are using the **Drahten** application.

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
  <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/public_history_service_api.PNG" alt="Allowed Endpoints" width="800">
</p>

## Endpoints description

#### Get all commented articles by a user.
**Endpoint**: GET ``` publichistory-service/users/{UserId}/commented-articles/ ```

#### Get all searched articles by a user.
**Endpoint**: GET ``` publichistory-service/users/{UserId}/searched-articles/ ```

#### Get all searched topics by a user.
**Endpoint**: GET ``` publichistory-service/users/{UserId}/searched-topics/ ```

#### Get all viewed articles by a user.
**Endpoint**: GET ``` publichistory-service/users/{UserId}/viewed-articles/ ```

#### Get all viewed users by a user.
**Endpoint**: GET ``` publichistory-service/users/{UserId}/viewed-users/ ```

#### Register a user.
**Endpoint**: POST ``` publichistory-service/users/{UserId}/ ```

#### Register commented article for user.
**Endpoint**: POST ``` publichistory-service/users/{UserId}/commented-articles/{ArticleId}/ ```

#### Register searched article by a user.
**Endpoint**: POST ``` publichistory-service/users/{UserId}/searched-articles/{ArticleId}/ ```

#### Register searched topic by a user.
**Endpoint**: POST ``` publichistory-service/users/{UserId}/searched-topics/{TopicId}/ ```

#### Register viewed article by a user.
**Endpoint**: POST ``` publichistory-service/users/{UserId}/viewed-articles/{ArticleId}/ ```

#### Register viewed user by a user.
**Endpoint**: POST ``` publichistory-service/users/{ViewerUserId}/viewed-users/{ViewedUserId}/ ```

#### Remove viewed user by a user.
**Endpoint**: DELETE ``` publichistory-service/users/{ViewerUserId}/viewed-users/{ViewedUserId}/ ```

#### Remove commented article by a user.
**Endpoint**: DELETE ``` publichistory-service/users/{UserId}/commented-articles/{CommentedArticleId}/ ```

#### Remove searched article data by a user.
**Endpoint**: DELETE ``` publichistory-service/users/{UserId}/searched-articles/{SearchedArticleDataId}/ ```

#### Remove searched topic data by a user.
**Endpoint**: DELETE ``` publichistory-service/users/{UserId}/searched-topics/{SearchedTopicDataId}/ ```

#### Remove viewed article by a user.
**Endpoint**: DELETE ``` publichistory-service/users/{UserId}/viewed-articles/{ViewedArticleId}/ ```
