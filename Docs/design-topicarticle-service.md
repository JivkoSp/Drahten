# Topic Article Service

## Responsible for actions related to documents (news/articles) on topics offered by this service

<p align="center">
    <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/TopicArticleService-1.PNG" alt="Logo" width="450">
</p>

### The components of the diagram have the following meaning

* **Actor** - This can be a user or a process from the application. The user or process requests information by sending HTTP requests to the TopicArticle Service API. The diagram shows an example of endpoints that provide access to the following functionalities:
  - Find all user articles. Responds to HTTP GET;
  - Register article. Responds to HTTP POST;
  - Register article comment. Responds to HTTP POST.
* **TopicArticle Service API** - This is a .NET web API providing various endpoints. When a request is made to one of the provided endpoints, the request is analyzed by the authentication and authorization block - JWT Authentication & Authorization. If the request meets the necessary level of authentication and authorization required by the endpoint it is intended for, the request is executed.
A JSON response is returned to the entity/person that made the request to the TopicArticle Service API;
* **JWT Authentication & Authorization** - A layer for authentication and authorization, requiring JWT tokens to authenticate the entity/person seeking access to protected resources from the TopicArticle Service API;
* **Postgresql Database** – A database for storing information from the TopicArticle Service;
* **Logging** - Serves to track information regarding various events in the TopicArticle Service. This information is forwarded to the Log Collection Service.

## A diagram describing the tables of the Topic Article Service database, the relationships between them, and the information they represent.

