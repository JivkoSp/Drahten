# Search Service API Documentation

This REST API is designed to provide search functionality and semantic search capabilities, utilized in the *Drahten* application.

## Authentication

This API uses JWT token provided from the *Authentication Service* for authentication and authorization. The token must be included in every request routed to this API.
The request must have structure similar to the following:

```json
{
  "Data": {},
  "Token": "jwtToken"
}
```

## Allowed endpoints:

<div style="text-align: center;">
  <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/search_service_api.PNG" alt="Allowed Endpoints" width="800">
</div>
