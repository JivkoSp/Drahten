# User Service

## Introduction
The User Service is designed using Clean Architecture and Domain-Driven Design principles to ensure scalability, maintainability, and testability. This service is responsible for managing user-related data and operations, including authentication, authorization, and interaction with a PostgreSQL database.

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

---

## A diagram describing the tables of the User Service database, the relationships between them, and the information they represent.

<p align="center">
    <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/UserServiceDatabase.PNG" alt="Logo" width="700">
</p>

### Description of the tables
---
* **Table "User"** - The purpose of this table is to link a user authenticated by the Auth Service with the information in the User Service related to them. It contains the columns: UserId, Version, UserFullName, UserNickName, UserEmailAddress.
    <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/UserServiceDatabaseUserTable.PNG" alt="Logo" width="550">
    </p>

    - Field **UserId** - Primary key of the table. Serves as a unique identifier for the user. The field type is "text" - capable of storing text up to 1 GB in size.
    - Field **Version** - User version. Indicates the current version of the user, for example: 0 - indicates that the user is new and has not performed any actions; 1, ... N - indicates that actions have been performed. If within one request (one HTTP request), the user performs more than one action, the version will only increment once. This is done to prevent cases where the version increases suddenly, for example, from 1 to 4. The field type is "integer" - capable of storing numbers up to 4 bytes in size.
    - Field **UserFullName** - Full name of the user. The field type is "text" - capable of storing text up to 1 GB in size.
    - Field **UserNickName** - User nickname. The field type is "text" - capable of storing text up to 1 GB in size.
    - Field **UserEmailAddress** - User's email address. The field type is "text" - capable of storing text up to 1 GB in size.

---

* **Table "BannedUser"** - The purpose of this table is to represent information about banned users. It contains the columns: IssuerUserId, ReceiverUserId, DateTime.
    <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/UserServiceDatabaseBannedUserTable.PNG" alt="Logo" width="550">
    </p>
    
    - Field **IssuerUserId** - Part of a composite primary key. It serves as a component of the primary key of the table and also as a foreign key, establishing a 1:N
    relationship with the User table. The field type is "text" - capable of storing text up to 1 GB in size.
    - Field **ReceiverUserId** - Part of a composite primary key. It serves as a component of the primary key of the table and also as a foreign key, establishing a 1:N
    relationship with the User table. The field type is "text" - capable of storing text up to 1 GB in size.
    - Field **DateTime** - Date and time of the user being banned. The field type is "timestamp with time zone" - storing date, time, and timezone information.
 
---

* **Table "ContactRequest"** - The purpose of this table is to represent information regarding contact requests with users. It contains the columns: IssuerUserId, ReceiverUserId, Message, DateTime.
   <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/UserServiceDatabaseContactRequestTable.PNG" alt="Logo" width="550">
   </p>

    - Field **IssuerUserId** - Part of a composite primary key. It serves as a component of the primary key of the table and also as a foreign key, establishing a 1:N
    relationship with the User table. The field type is "text" - capable of storing text up to 1 GB in size.
    - Field **ReceiverUserId** - Part of a composite primary key. It serves as a component of the primary key of the table and also as a foreign key, establishing a 1:N
    relationship with the User table. The field type is "text" - capable of storing text up to 1 GB in size.
    - Field **Message** - Message regarding a contact request with the user. The field type is "text" - capable of storing text up to 1 GB in size.
    - Field **DateTime** - Date and time of the sent contact request. The field type is "timestamp with time zone" - storing date, time, and timezone information.

 ---

* **Table "UserTracking"** - The purpose of this table is to represent information about actions performed by users (e.g., Login/Logout, Visited page (URL) in the application, etc.). It contains the columns: UserTrackingId, Action, DateTime, Referrer, UserId.
  <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/UserServiceDatabaseUserTrackingTable.PNG" alt="Logo" width="550">
  </p>

    - Field **UserTrackingId** - Primary key of the table. Serves as a unique identifier. The field type is "uuid" - universally unique identifier, storing a 128-bit number. It can be represented as a 32 or 36 character string (with or without dashes).
    - **Field Action** - Action performed by the user. The field type is "text" - capable of storing text up to 1 GB in size.
    - Field **DateTime** - Date and time when the action was performed. The field type is "timestamp with time zone" - storing date, time, and timezone information.
    - Field **Referrer** - Referrer that directed the user to the current action (e.g., URL of a website). The field type is "text" - capable of storing text up to 1 GB in size.
    - Field **UserId** - Foreign key establishing a 1:N relationship with the User table. The field type is "text" - capable of storing text up to 1 GB in size.
