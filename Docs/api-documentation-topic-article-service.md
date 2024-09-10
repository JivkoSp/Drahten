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
**Endpoint**: GET ``` topic_article_service/articles/ ```




