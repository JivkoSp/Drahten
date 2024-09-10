# User Service API Documentation

This REST API is designed to provide more broad user functionalities and data related to user activities to the **Drahten** application.

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
  <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/user_service_api.PNG" alt="Allowed Endpoints" width="800">
</p>

## Endpoints description

#### Get user information like username, email etc.
**Endpoint**: GET ``` user_service/users/{UserId} ```

#### Get issued ban-requests by a user.
**Endpoint**: GET ``` user_service/users/{IssuerUserId}/issued-bans-by-user ```

#### Get received ban-requests for a user.
**Endpoint**: GET ``` user_service/users/{ReceiverUserId}/received-bans-by-user ```

#### Get issued contact-requests by a user.
**Endpoint**: GET ``` user_service/users/{IssuerUserId}/issued-contact-requests-by-user ```

#### Get received contact-requests for a user.
**Endpoint**: GET ``` user_service/users/{ReceiverUserId}/issued-contact-requests-by-user ```

#### Register user.
**Endpoint**: POST ``` user_service/users/ ```

#### Ban a user.
**Endpoint**: POST ``` user_service/users/{IssuerUserId}/banned-users/{ReceiverUserId}/ ```

#### Remove banned user (unbann the user).
**Endpoint**: DELETE ``` user_service/users/{IssuerUserId}/banned-users/{ReceiverUserId}/ ```

#### Register contact request to a user.
**Endpoint**: POST ``` user_service/users/{IssuerUserId}/contact-requests/{ReceiverUserId}/ ```

#### Register user tracking information.
**Endpoint**: POST ``` user_service/users/{UserId}/user-tracking/ ```

#### Partially update a contact-request message.
**Endpoint**: PUT ``` user_service/users/{IssuerUserId}/update-contact-request-message/{ReceiverUserId}/ ```

#### Remove issued contact-request.
**Endpoint**: DELETE ``` user_service/users/{IssuerUserId}/issued-contact-requests/{ReceiverUserId}/ ```

#### Remove received contact-request.
**Endpoint**: DELETE ``` user_service/users/{ReceiverUserId}/received-contact-requests/{IssuerUserId}/ ```
