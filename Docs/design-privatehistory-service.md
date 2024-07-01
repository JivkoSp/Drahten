# Private History Service

## Responsible for the user's private history

<p align="center">
    <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/PrivateHistoryService-1.PNG" alt="Logo" width="450">
</p>

### The components of the diagram have the following meaning

* **Actor** - Can be a user or an application process. The user or process requests information by sending HTTP requests to the PrivateHistory Service API. In the diagram are given examples of endpoints providing access to the following functionalities:
  - Find all commented articles from user. Responds to HTTP GET;
  - Find all searched article data from user. Responds to HTTP GET;
  - Register commented article from user. Responds to HTTP POST.
* **PrivateHistory Service API** - Represents a .NET web API providing various endpoints. Upon receiving a request for one of the provided endpoints, the request is analyzed by the authentication and authorization block - JWT Authentication & Authorization. If the request meets the required level of authentication and authorization for the intended endpoint, the request is executed. A response in JSON format is returned to the party/entity that made the request to the PrivateHistory Service API.
* **JWT Authentication & Authorization** - Layer for authentication and authorization, requiring JWT tokens to authenticate the party/entity requesting access to protected resources from the PrivateHistory Service API.
* **PostgreSQL Database** - A database for storing information from the PrivateHistory Service.
* **Logging** - Serves to detect information regarding various events in the PrivateHistory Service. This information is forwarded to the Log Collection Service.
