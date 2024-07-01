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

---

## A diagram describing the tables of the Topic Article Service database, the relationships between them, and the information they represent.

<p align="center">
    <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/TopicArticleServiceDatabase.PNG" alt="Logo" width="700">
</p>

### Description of the tables
---
* **Table "User"** - The purpose of this table is to link a user, who is authenticated by the Auth Service, with the information in the TopicArticle Service that pertains to them. It contains the columns: UserId, Version.
    <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/TopicArticleServiceDatabaseUserTable.PNG" alt="Logo" width="550">
    </p>

    - Field **UserId** – The primary key of the table. It serves as a unique identifier for a user. The field type is "text" – it can store text up to 1 GB (gigabyte) in size.
    - Field **Version** – The user version. It indicates the current version of the user, for example: 0 – means the user is new and has not performed any actions; 1, ... N – indicates that actions have been performed. If within a single request (one HTTP request) the user performs more than one action, the version will increment only once. This is done to avoid cases where the version jumps suddenly, for example from 1 to 4. The field type is "integer" – it can store numbers up to 4 bytes in size.

 ---
      
* **Table "Topic"** - The purpose of this table is to present information regarding topics that a user can subscribe to. It contains the columns: TopicId, Version, TopicName, TopicFullName, ParentTopicId.
   <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/TopicArticleServiceDatabaseTopicTable.PNG" alt="Logo" width="550">
   </p>

    - Field **TopicId** - The primary key of the table. It serves as a unique identifier. The field type is "uuid" – a universally unique identifier that stores a 128-bit number. It can be represented as a 32 or 36 character string (without or with hyphens).
    - Field **Version** - The version of the topic. It indicates the current version of the topic, for example: 0 – means there are no changes in the topic. 1, ... N – indicates that changes have been made. If more than one change is made within a single request (one HTTP request), the version will increment only once. This is done to avoid cases where the version jumps suddenly, for example from 1 to 4. The field type is "integer" – it can store numbers up to 4 bytes in size.
    - Field **TopicName** – The name of the topic. The field type is "text" – it can store text up to 1 GB (gigabyte) in size.
    - Field **TopicFullName** – The full (entire) name of the topic. For example: There are topics A and B. Topic B is a subtopic of topic A. The full name of topic B is: AB. The field type is "text" – it can store text up to 1 GB (gigabyte) in size.
    - Field **ParentTopicId** - A foreign key establishing a 1:N relationship with the same table. The field type is "uuid" – a universally unique identifier that stores a 128-bit number. It can be represented as a 32 or 36 character string (without or with hyphens).
 
 ---
  
* **Table "UserTopic"** - The purpose of this table is to serve as a linking table between the **User** and **Topic** tables, as these two tables are related to each other with an N:N (many-to-many) relationship. It contains the columns: UserId, TopicId, SubscriptionTime.
  <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/TopicArticleServiceDatabaseUserTopicTable.PNG" alt="Logo" width="550">
  </p>

    - Field **UserId** - Part of a composite primary key. It serves as part of the primary key of the table and as a foreign key establishing a 1:N relationship with the User table. The field type is "text" – it can store text up to 1 GB (gigabyte).
    - Field **TopicId** - Part of a composite primary key. It serves as part of the primary key of the table and as a foreign key establishing a 1:N relationship with the Topic table. The field type is "uuid" – a universally unique identifier that stores a 128-bit number. It can be represented as a 32 or 36-character string (with or without hyphens).
    - Field **SubscriptionTime** - The time at which a user subscribes to a topic from the **"Topic"** table. The field type is "timestamp with time zone" – it stores the date, time, and time zone information.
 
  ---
  
* Table **"Article"** - The purpose of this table is to represent information about documents (news) related to the topics to which a user has subscribed. It contains the columns: ArticleId, Version, PrevTitle, Title, Content, PublishingDate, Author, Link, TopicId.
  <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/TopicArticleServiceDatabaseArticleTable.PNG" alt="Logo" width="550">
  </p>

    - Field **ArticleId** - The primary key of the table. It serves as a unique identifier. The field type is "text" – it can store text up to 1 GB (gigabyte).
    - Field **Version** - Version of the document. It shows the current version of the document, for example: 0 – indicates no changes in the document. 1, ... N – indicates changes have been made. If more than one change is made within a single request (one HTTP request), the version will be incremented only once. This is done to avoid cases where the version jumps, for example, from 1 to 4. The field type is "integer" – it can store numbers up to 4 bytes in size.
    - Field **PrevTitle** – Previous title of the document. The field type is "text" – it can store text up to 1 GB (gigabyte).
    - Field **Title** - Title of the document. The field type is "text" – it can store text up to 1 GB (gigabyte).
    - Field **Content** - Content of the document. The field type is "text" – it can store text up to 1 GB (gigabyte).
    - Field **PublishingDate** - Date and time of publishing the document. The field type is "text" – it can store text up to 1 GB (gigabyte). The reason this field is of type "text" instead of "timestamp with time zone" is that the documents are retrieved from the internet. Different websites may use different formats for presenting the date and time. Using text ensures there are no potential issues with converting from one format to another.
    - Field **Author** - Author of the document. The field type is "text" – it can store text up to 1 GB (gigabyte).
    - Field **Link** – The address from where the document was retrieved. The field type is "text" – it can store text up to 1 GB (gigabyte).
    - Field **TopicId** - Foreign key establishing a 1:N relationship with the Topic table. The field type is "uuid" – a universally unique identifier that stores a 128-bit number. It can be represented as a 32 or 36-character string (with or without hyphens).
 
 ---
      
* Table **"UserArticle"** - The purpose of this table is to serve as a linking table between the **"User"** and **"Article"** tables, as these two tables are related to each other with an N:N (many-to-many) relationship. It contains the columns: UserId, ArticleId.
  <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/TopicArticleServiceDatabaseUserArticleTable.PNG" alt="Logo" width="550">
  </p>

    - Field **UserId** - Part of a composite primary key. It serves as part of the primary key of the table and as a foreign key establishing a 1:N
    relationship with the User table. The field type is "text" – it can store text up to 1 GB (gigabyte).
    - Field **ArticleId** - Part of a composite primary key. It serves as part of the primary key of the table and as a foreign key establishing a 1:N relationship with the Article table. The field type is "text" – it can store text up to 1 GB (gigabyte).
 
 ---
    
* Table **"ArticleLike"** - The purpose of this table is to represent information about likes (approvals) of a document (news) related to a topic to which a user has subscribed. It contains the columns: ArticleId, UserId, DateTime.
   <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/TopicArticleServiceDatabaseArticleLikeTable.PNG" alt="Logo" width="550">
  </p>

    - Field **ArticleId** - Part of a composite primary key. It serves as part of the primary key of the table and as a foreign key establishing a 1:N relationship with the Article table. The field type is "text" – it can store text up to 1 GB (gigabyte).
    - Field **UserId** - Part of a composite primary key. It serves as part of the primary key of the table and as a foreign key establishing a 1:N relationship with the User table. The field type is "text" – it can store text up to 1 GB (gigabyte).
    - Field **DateTime** - The time when the user approved the document. The field type is "timestamp with time zone" – it stores the date, time, and time zone information.

 ---

* Table **"ArticleDislike"** - The purpose of this table is to represent information about dislikes (disapprovals) of a document (news) related to a topic to which a user has subscribed. It contains the columns: ArticleId, UserId, DateTime.
  <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/TopicArticleServiceDatabaseArticleDislikeTable.PNG" alt="Logo" width="550">
  </p>

    - Field **ArticleId** - Part of a composite primary key. It serves as part of the primary key of the table and as a foreign key establishing a 1:N relationship with the Article table. The field type is "text" – it can store text up to 1 GB (gigabyte).
    - Field **UserId** - Part of a composite primary key. It serves as part of the primary key of the table and as a foreign key establishing a 1:N relationship with the User table. The field type is "text" – it can store text up to 1 GB (gigabyte).
    - Field **DateTime** - The time when the user disliked the document. The field type is "timestamp with time zone" – it stores the date, time, and time zone information.

 ---

* Table **"ArticleComment"** - The purpose of this table is to represent information about comments on a document (news) related to a topic to which a user has subscribed. It contains the columns: ArticleCommentId, Version, Comment, DateTime, ParentArticleCommentId, ArticleId, UserId.
  <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/TopicArticleServiceDatabaseArticleCommentTable.PNG" alt="Logo" width="550">
  </p>

    - Field **ArticleCommentId** - Primary key of the table. It serves as a unique identifier. The field type is "uuid" – a universally unique identifier that stores a 128-bit number. It can be represented as a 32 or 36-character string (with or without hyphens).
    - Field **Version** - Version of the comment. It indicates the current version of the comment, for example: 0 – means the comment has no changes. 1, ... N – indicates changes have been made. If more than one change is made within a single request (HTTP request), the version will be incremented only once. This is done to avoid cases where the version jumps suddenly, for example, from 1 to 4. The field type is "integer" – it can store numbers up to 4 bytes in size.
    - Field **Comment** - The field type is "text" – it can store text up to 1 GB (gigabyte).
    - Field **DateTime** - The time when the user commented on the document. The field type is "timestamp with time zone" – it stores the date, time, and time zone information.
    - Field **ParentArticleCommentId** - Foreign key establishing a 1:N relationship within the same table. The field type is "uuid" – a universally unique identifier that stores a 128-bit number. It can be represented as a 32 or 36-character string (with or without hyphens).
    - Field **ArticleId** - Foreign key establishing a 1:N relationship with the Article table. The field type is "text" – it can store text up to 1 GB (gigabyte).
    - Field **UserId** - Foreign key establishing a 1:N relationship with the User table. The field type is "text" – it can store text up to 1 GB (gigabyte).
  
 ---

* Table **"ArticleCommentLike"** - The purpose of this table is to represent information about likes (approvals) on a comment for a document (news) related to a topic to which a user has subscribed. It contains the columns: ArticleCommentId, UserId, DateTime.
  <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/TopicArticleServiceDatabaseArticleCommentLikeTable.PNG" alt="Logo" width="550">
  </p>

    - Field **ArticleCommentId** - Part of a composite primary key. It serves as part of the primary key of the table and as a foreign key establishing a 1:N relationship with the ArticleComment table. The field type is "uuid" – a universally unique identifier that stores a 128-bit number. It can be represented as a 32 or 36-character string (with or without hyphens).
    - Field **UserId** - Part of a composite primary key. It serves as part of the primary key of the table and as a foreign key establishing a 1:N relationship with the User table. The field type is "text" – it can store text up to 1 GB (gigabyte).
    - Field **DateTime** - The time when the user approved the comment for the document. The field type is "timestamp with time zone" – it stores the date, time, and time zone information.
 
 ---

 * Table **"ArticleCommentDislike"** - The purpose of this table is to represent information about dislikes (disapprovals) of a comment on a document (news) related to a topic to which a user has subscribed. It contains the columns: ArticleCommentId, UserId, DateTime.
   <p align="center">
        <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/TopicArticleServiceDatabaseArticleCommentDislikeTable.PNG" alt="Logo" width="550">
   </p>

    - Field **ArticleCommentId** - Part of a composite primary key. It serves as part of the primary key of the table and as a foreign key establishing a 1:N relationship with the ArticleComment table. The field type is "uuid" – a universally unique identifier that stores a 128-bit number. It can be represented as a 32 or 36-character string (with or without hyphens).
    - Field **UserId** - Part of a composite primary key. It serves as part of the primary key of the table and as a foreign key establishing a 1:N relationship with the User table. The field type is "text" – it can store text up to 1 GB (gigabyte).
    - Field **DateTime** - The time when the user disliked (disapproved) the comment on the document. The field type is "timestamp with time zone" – it stores the date, time, and time zone information.
  
