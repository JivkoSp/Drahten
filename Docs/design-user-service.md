# User Service

## Responsible for storing information in a PostgreSQL database regarding more general actions and information related to the user in the application

<p align="center">
    <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/UserService-1.PNG" alt="Logo" width="450">
</p>

### The components of the diagram have the following meaning

* **Actor** - This can be a user or a process from the application. The user or process requests information by sending HTTP requests to the User Service API.
  The diagram provides examples of endpoints that offer access to the following functionalities:
    - Find all issued bans by user. Responds to HTTP GET;
    - Register banned user. Responds to HTTP POST;
    - Update contact request message. Responds to HTTP PUT.
* **User Service API** - This is a .NET web API providing various endpoints. Upon receiving a request for one of the provided endpoints, the request is analyzed by the authentication and authorization block - JWT 
 Authentication & Authorization. If the request meets the necessary authentication and authorization level required by the intended endpoint, the request is executed. A JSON-formatted response is returned to the party/entity that made the request to the User Service API.
* **JWT Authentication & Authorization** - A layer for authentication and authorization, requiring JWT tokens to authenticate the party/entity requesting access to protected resources from the User Service API;
* **PostgreSQL Database** - A database for storing information from the User Service;
* **Logging** - Used for capturing information about various events in the User Service. This information is sent to the Log Collection Service.

## A diagram describing the tables of the User Service database, the relationships between them, and the information they represent.

<p align="center">
    <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/UserServiceDatabase.PNG" alt="Logo" width="700">
</p>

### Description of the tables

* **Table "User"** - The purpose of this table is to link a user authenticated by the Auth Service with the information in the User Service related to them. It contains the columns: UserId, Version, UserFullName, UserNickName, UserEmailAddress.
    <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/UserServiceDatabaseUserTable.PNG" alt="Logo" width="600">
    </p>
