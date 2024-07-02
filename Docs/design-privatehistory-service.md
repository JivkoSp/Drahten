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

---

## A diagram describing the tables of the Private History Service database, the relationships between them, and the information they represent

<p align="center">
    <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/PrivateHistoryServiceDatabase.PNG" alt="Logo" width="700">
</p>

### Description of the tables
---
* **Table "User"** - The purpose of this table is to establish a link between a user authenticated by the Auth Service and the information in the PrivateHistory Service related to them. It contains the columns: UserId, Version.
    <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/PrivateHistoryServiceDatabaseUserTable.PNG" alt="Logo" width="550">
    </p>

    - Field **UserId** – Primary Key of the table. It serves as a unique identifier for the user. The field type is "text" – it can store text up to 1 GB (gigabyte) in size;
    - Field **Version** – User version. It indicates the current version of the user, for example: 0 – means that the user is new and has not performed any actions; 1, ... N – indicates that actions have been performed. If within a single request (one HTTP request) the user performs more than one action, the version will be incremented only once. This is done to avoid cases where the version increases suddenly, for example from 1 to 4. The field type is "integer" – it can store numbers up to 4 bytes in size.

---

 * **Table "CommentedArticle"** - The purpose of this table is to present information regarding comments made by users on documents (news articles) related to topics to which the users have subscribed. It contains the columns: CommentedArticleId, ArticleId, UserId, ArticleComment, DateTime.
    <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/PrivateHistoryServiceDatabaseCommentedArticleTable.PNG" alt="Logo" width="550">
    </p>

    - Field **CommentedArticleId** – Primary Key of the table. It serves as a unique identifier. The field type is "uuid" – universally unique identifier, storing a 128-bit number. It can be represented as a 32 or 36 character string (without or with hyphens);
    - Field **ArticleId** – Foreign key establishing an indirect 1:N relationship with the Article table from the TopicArticle Service. The field type is "text" – it can store text up to 1 GB (gigabyte) in size;
    - Field **UserId** – Foreign key establishing a 1:N relationship with the User table. The field type is "text" – it can store text up to 1 GB (gigabyte) in size;
    - Field **ArticleComment** – Comment. The field type is "text" – it can store text up to 1 GB (gigabyte) in size;
    - Field **DateTime** – The time when the user commented on the document. The field type is "timestamp with time zone" – it stores the date, time, and time zone information.

---

* **Table "LikedArticle"** - The purpose of this table is to present information regarding approved documents (news, articles) related to topics to which the user has subscribed. It contains the columns: ArticleId, UserId, DateTime.
    <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/PrivateHistoryServiceDatabaseLikedArticleTable.PNG" alt="Logo" width="550">
   </p>

    - Field **ArticleId** - Part of the composite primary key. It serves as a component of the primary key of the table and as a foreign key establishing an indirect 1:N relationship with the Article table from the TopicArticle Service. The field type is "text" – it can store text up to 1 GB (gigabyte) in size;
    - Field **UserId** - Part of the composite primary key. It serves as a component of the primary key of the table and as a foreign key establishing a 1:N relationship with the User table. The field type is "text" – it can store text up to 1 GB (gigabyte) in size;
    - Field **DateTime** - The time when the user approved the document. The field type is "timestamp with time zone" – it stores the date, time, and time zone information.
 
---

* **Table "DislikedArticle"** - The purpose of this table is to present information regarding disliked documents (news articles) related to topics to which the user has subscribed. It contains the columns: ArticleId, UserId, DateTime.
    <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/PrivateHIstoryServiceDatabaseDislikedArticleTable.PNG" alt="Logo" width="550">
    </p>

    - Field **ArticleId** - Part of the composite primary key. It serves as a component of the primary key of the table and as a foreign key establishing an indirect 1:N relationship with the Article table from the TopicArticle Service. The field type is "text" – it can store text up to 1 GB (gigabyte) in size;
    - Field **UserId** - Part of the composite primary key. It serves as a component of the primary key of the table and as a foreign key establishing a 1:N relationship with the User table. The field type is "text" – it can store text up to 1 GB (gigabyte) in size;
    - Field **DateTime** - The time when the user disliked the document. The field type is "timestamp with time zone" – it stores the date, time, and time zone information.

---

* **Table "LikedArticleComment"** - The purpose of this table is to present information regarding liked comments on documents (news, articles) related to topics to which the user has subscribed. It contains the columns: ArticleCommentId, UserId, ArticleId, DateTime.
    <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/PrivateHistoryServiceDatabaseLikedArticleCommentTable.PNG" alt="Logo" width="550">
    </p>

    - Field **ArticleCommentId** - Part of the composite primary key. It serves as a component of the primary key of the table and as a foreign key establishing an indirect 1:N relationship with the ArticleComment table from the TopicArticle Service. The field type is "uuid" – universally unique identifier, storing a 128-bit number. It can be represented as a 32 or 36 character string (without or with hyphens);
    - Field **UserId** - Part of the composite primary key. It serves as a component of the primary key of the table and as a foreign key establishing a 1:N relationship with the User table. The field type is "text" – it can store text up to 1 GB (gigabyte) in size;
    - Field **ArticleId** - Foreign key establishing an indirect 1:N relationship with the Article table from the TopicArticle Service. The field type is "text" – it can store text up to 1 GB (gigabyte) in size;
    - Field **DateTime** - The time when the user liked a comment on a document. The field type is "timestamp with time zone" – it stores the date, time, and time zone information.
 
---

* **Table "DislikedArticleComment"** - The purpose of this table is to present information regarding disliked comments on documents (news, articles) related to topics to which the user has subscribed. It contains the columns: ArticleCommentId, UserId, ArticleId, DateTime.
    <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/PrivateHistoryServiceDatabaseDislikedArticleCommentTable.PNG" alt="Logo" width="550">
    </p>

    - Field **ArticleCommentId** - Part of the composite primary key. It serves as a component of the primary key of the table and as a foreign key establishing an indirect 1:N relationship with the ArticleComment table from the TopicArticle Service. The field type is "uuid" – universally unique identifier, storing a 128-bit number. It can be represented as a 32 or 36 character string (without or with hyphens).
    - Field **UserId** - Part of the composite primary key. It serves as a component of the primary key of the table and as a foreign key establishing a 1:N relationship with the User table. The field type is "text" – it can store text up to 1 GB (gigabyte) in size.
    - Field **ArticleId** - Foreign key establishing an indirect 1:N relationship with the Article table from the TopicArticle Service. The field type is "text" – it can store text up to 1 GB (gigabyte) in size.
    - Field **DateTime** - The time when the user disliked a comment on a document. The field type is "timestamp with time zone" – it stores the date, time, and time zone information.

---

* **Table "SearchedArticleData"** - The purpose of this table is to present information regarding searched data for documents (news, articles) related to topics to which the user has subscribed. It contains the columns: SearchedArticleDataId, ArticleId, UserId, SearchedData, DateTime.
    <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/PrivateHistoryServiceDatabaseSearchedArticleDataTable.PNG" alt="Logo" width="550">
    </p>

    - Field **SearchedArticleDataId** - Primary Key of the table. It serves as a unique identifier. The field type is "uuid" – universally unique identifier, storing a 128-bit number. It can be represented as a 32 or 36 character string (without or with hyphens).
    - Field **ArticleId** - Foreign Key establishing an indirect 1:N relationship with the Article table from the TopicArticle Service. The field type is "text" – it can store text up to 1 GB (gigabyte) in size.
    - Field **UserId** - Foreign Key establishing a 1:N relationship with the User table. The field type is "text" – it can store text up to 1 GB (gigabyte) in size.
    - Field **SearchedData** - Searched data for the document. The field type is "text" – it can store text up to 1 GB (gigabyte) in size.
    - Field **DateTime** - The time when the user searched for information about the document. The field type is "timestamp with time zone" – it stores the date, time, and time zone information.

---

* **Table "SearchedTopicData"** - The purpose of this table is to present information regarding searched data for topics to which the user has subscribed. It contains the columns: SearchedTopicDataId, TopicId, UserId, SearchedData, DateTime.
    <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/PrivateHistoryServiceDatabaseSearchedArticleDataTable.PNG" alt="Logo" width="550">
    </p>

    - Field **SearchedTopicDataId** - Primary key of the table. It serves as a unique identifier. The field type is "uuid" - universally unique identifier, storing a 128-bit number. It can be represented as a 32 or 36-character string (with or without dashes).
    - Field **TopicId** - Foreign key, establishing an indirect 1:N relationship to the Topic table from the TopicArticle Service. The field type is "uuid" - universally unique identifier, storing a 128-bit number. It can be represented as a 32 or 36-character string (with or without dashes).
    - Field **UserId** - Foreign key, establishing a 1:N relationship to the User table. The field type is "text" - capable of storing text up to 1 GB (gigabyte) in size.
    - Field **SearchedData** - Searched data for the topic. The field type is "text" - capable of storing text up to 1 GB (gigabyte) in size.
    - Field **DateTime** - The timestamp when the user searched for information on the topic. The field type is "timestamp with time zone" - storing date, time, and time zone information.
      
