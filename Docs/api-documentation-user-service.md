# User Service API Documentation

This REST API is designed to provide a wide range of user functionalities and data related to user activities for the **Drahten** application.

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
  <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/user_service_api.PNG" alt="Allowed Endpoints" width="800">
</p>

## Endpoints description

#### Get user information like username, email etc.
**Endpoint**: GET ``` user_service/users/{UserId} ```

#### Get ban-requests issued by a user.
**Endpoint**: GET ``` user_service/users/{IssuerUserId}/issued-bans-by-user ```

#### Get ban-requests received by a user.
**Endpoint**: GET ``` user_service/users/{ReceiverUserId}/received-bans-by-user ```

#### Get contact-requests issued by a user.
**Endpoint**: GET ``` user_service/users/{IssuerUserId}/issued-contact-requests-by-user ```

#### Get contact-requests received by a user.
**Endpoint**: GET ``` user_service/users/{ReceiverUserId}/issued-contact-requests-by-user ```

#### Register a user.
**Endpoint**: POST ``` user_service/users/ ```

#### Ban a user.
**Endpoint**: POST ``` user_service/users/{IssuerUserId}/banned-users/{ReceiverUserId}/ ```

#### Unban a user (remove banned user).
**Endpoint**: DELETE ``` user_service/users/{IssuerUserId}/banned-users/{ReceiverUserId}/ ```

#### Register a contact request for a user.
**Endpoint**: POST ``` user_service/users/{IssuerUserId}/contact-requests/{ReceiverUserId}/ ```

#### Register user tracking information.
**Endpoint**: POST ``` user_service/users/{UserId}/user-tracking/ ```

#### Partially update a contact request message.
**Endpoint**: PUT ``` user_service/users/{IssuerUserId}/update-contact-request-message/{ReceiverUserId}/ ```

#### Remove an issued contact request.
**Endpoint**: DELETE ``` user_service/users/{IssuerUserId}/issued-contact-requests/{ReceiverUserId}/ ```

#### Remove a received contact request.
**Endpoint**: DELETE ``` user_service/users/{ReceiverUserId}/received-contact-requests/{IssuerUserId}/ ```
