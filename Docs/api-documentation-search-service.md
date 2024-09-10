# Search Service API Documentation

This REST API is designed to provide search functionality and semantic search capabilities, utilized in the **Drahten** application.

## Authentication

This API uses JWT token provided from the **Authentication Service** for authentication and authorization. The token must be included in every request routed to this API.
The request must have structure similar to the following:

```json
{
  "Data": {},
  "Token": "jwtToken"
}
```

## Allowed endpoints:

<p align="center">
  <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/search_service_api.PNG" alt="Allowed Endpoints" width="800">
</p>

## Endpoints description

#### Get all news related to cybersecurity from Europe
Endpoint: GET search_service/news/cybersecurity/europe/

#### Get sumarization of a document related to news to cybersecurity from Europe
Endpoint: GET search_service/news/cybersecurity/europe/sumarization/documents/{document_id}

#### Get questions generated from the semantic meaning of a document related to news to cybersecurity from Europe
Endpoint: GET search_service/news/cybersecurity/europe/questions/documents/{document_id}

#### Retrieves information with SEMANTIC query from document related to news to cybersecurity from Europe, stored in the Elasticsearch instance that the API is using.
Endpoint: GET search_service/news/cybersecurity/europe/semantic_search/documents/data/{'document_id': '', 'query': ''}
