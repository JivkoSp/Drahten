<p align="center">
    <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/logo.PNG" alt="Logo" width="250">
</p>

## Table of Contents
1. [What is Drahten?](#what-is-drahten)
2. [Architecture Overview](#architecture-overview)
3. [Will This Product Solve My Problem?](#will-this-product-solve-my-problem)
4. [What features are available?](#what-features-are-available)
5. [Detailed technical project explanation](#detailed-technical-project-explanation)

## What is Drahten?

Drahten is an open-source project utilizing a **microservices architecture** written in .NET Core 8.0. The project focuses on creating a secure application that encompasses:
- Access control
- Information retrieval
- Data encryption

## Architecture Overview

![Architecture Overview](https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/ArchitectureOverview.PNG)

## Why Security Matters

In the realm of computer systems and networks, defining "absolute security" is impossible. The rapid evolution of information technologies, the reduced timeframes for comprehensive testing of information systems, the increasing capabilities of individual users, and the potential for human errors (such as non-compliance with organizational security policies, configuration mistakes, and missed updates of critical applications and systems) are just some of the challenges that make "absolute security" unattainable. Therefore, the goal is to achieve "sufficient security."

## Achieving Sufficient Security

In this project, "sufficient security" is accomplished through the following measures:

- **Microservice Architecture**: The entire application is built using microservices, with each service running in a separate container and having its own database.
- **User Authentication**: Users are authenticated via an authentication server that operates as an independent service within a container.
- **Reverse Proxy Server**: A reverse proxy server acts as a single entry point for client applications, enforcing authentication and authorization policies for backend services.
- **Data Encryption**: Information transmitted between services and the database is encrypted.
- **Event Logging System**: An event logging system captures events occurring within the application's services, providing a clear overview of the system's activities from a centralized location.

## Will This Product Solve My Problem?

If you are looking for a secure application framework that provides robust access control, reliable information retrieval, and strong data encryption, Drahten is designed to meet those needs. By leveraging a microservices architecture, Drahten ensures that each service is isolated and secure, while the use of an authentication server and reverse proxy server provides centralized user authentication and authorization. Additionally, the encryption of data in transit and the comprehensive logging system contribute to the overall security and transparency of the application.

Moreover, Drahten offers advanced capabilities for scraping information from various websites, performing semantic searches on the retrieved information, and generating questions based on the semantic meaning of the data. This is achieved through a custom search engine implemented by the project. These features enable efficient data retrieval and analysis, making Drahten an excellent choice for applications requiring intelligent data processing and query generation.

While no system can guarantee "absolute security," Drahten aims to provide "sufficient security" to protect your data and maintain the integrity of your application.


## What features are available?

### The application offers the following services:

1. Automatic information retrieval through the application on topics that the user has requested to receive information about in the field of:
   * Laws and news related to cybersecurity;
   * New threats related to cybersecurity;
   * Users.
   
2. Semantic search of information from a specific document (news article) related to a topic that the user has requested to receive information about.
   * Searching for information by providing a question from the user;
   * Automatic generation of questions by the application based on the semantics of the document (news article), with the ability to provide an answer and indicate the context from which the answer was extracted;
   * Automatic summarization of the document (news article).
   
3. Creation of private and public histories related to the user's work within the system.
   * The private history should provide information about viewed documents (news articles), users, and searched information on topics. Information should be stored based on specified time and deleted after expiration of that time;
   * The public history should be created by selecting parts from the private history.

### The application achieves security and reliability of the offered services through:

1. Security:
   * Access control in the application through an authentication server, operating as an independent service. The authentication server must implement the following internet protocols: OpenID and OAuth 2.0;
   * Use of a gateway to ensure that the services offered by the application are as independent as possible from the application's interface. This means that the application's interface should only know the location of the gateway, and the gateway should forward requests to the respective services;
   * Event logging system for events occurring within the services offered by the application;
   * Encryption of transmitted information from each service to and from the database;
   * Operation of the services offered by the application in separate containers.

2. Reliability:
   * The application must use microservices architecture, thereby dividing the system into individual components. The goal is to make the system more reliable so that it remains functional in case of service failure;
   * Each service (component) of the system must have a separate database to ensure that in case of a breach in the system, the information from the system is not located in one place.


## Detailed technical project explanation

### Abstract representation of the project structure

![Architecture Overview](https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/DetailedProjectDiagram.PNG)

#### The components of the diagram have the following meaning

1. Auth Server - Authentication server implemented using Keycloak. User credentials are stored in a PostgreSQL database.
2. Gui - Graphical User Interface (frontend) that indirectly connects to all services offered by the application. Users access the services through this interface.
3. Api Gateway - Reverse proxy server (gateway) implemented using YARP. It connects the graphical interface with all the services offered by the application. Additionally, it performs authentication and authorization functions, providing extra security for the services.
4. User Service - A service responsible for storing information in a PostgreSQL database about general actions and information related to users in the application, such as:
    * Sent/Received requests for contact with other users;
    * Sent requests to block other users;
    * Actions performed by users (e.g., Login/Logout, visited pages within the application);
    * Names, nicknames (aliases), and email addresses of users.
5. TopicArticle Service - A service responsible for actions and storing information in a PostgreSQL database regarding documents (news/articles) on topics offered by this service. The actions managed by this service include:
    * Subscribing users to receive information on topics provided by the application;
    * Registering new documents on topics provided by the application;
    * Creating comments on documents for subscribed topics;
    * Creating likes/dislikes for documents on subscribed topics;
    * Registering users who have viewed documents on subscribed topics.
6. PrivateHistory Service - A service responsible for the user's private history, providing information on:
    * Viewed documents (news/articles);
    * Viewed user profiles;
    * Topics the user has subscribed to;
    * Searched information on topics and documents;
    * Commented documents;
    * Liked/Disliked documents.
    * Information is stored for a specified time and deleted after the expiration of that time. Users can remove parts or the entire history before the expiration time.
7. PublicHistory Service - A service responsible for the user's public history, which can contain parts or the entire private history. The public history is created by selecting parts from the private history. Information is stored indefinitely, and users can remove parts or the entire history at any time.
8. Search Service - A service responsible for searching information from the internet regarding topics offered by the application. It also handles semantic searches of information offered by the application. The searched information is stored in an Elasticsearch database.
9. Chat Service - A service providing communication between users through text messages. Text messages are stored in a PostgreSQL database.
10. Message Bus - Message broker (message bus) implemented using RabbitMQ. It connects the services offered by the application.
11. Log Collection Service - A service responsible for collecting and recording information about events occurring in the services offered by the application in an Elasticsearch database.


### Search Service - Responsible for actions related to searching information from the internet on topics offered by the application

![Architecture Overview](https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/SearchService-1.PNG)

#### The components of the diagram have the following meaning

1. Actor - This can be a user or a process from the application. The user or process requests information. In the diagram, an example request for "Cybersecurity news from Europe and America" is shown. The request is forwarded to the Search Service API, which then returns a response containing the appropriate HTTP code and information (if found).
   
2. Search Service API - A web API providing various endpoints. The diagram shows two such endpoints: "Find news for Europe" and "Find news for America," responding to HTTP GET requests for information requested by third parties/users. When a request is made to one of the provided endpoints, it is forwarded to Haystack, where it is processed and sent to ElasticSearch. ElasticSearch returns a response in JSON format to the party/user who made the request to the Search Service API.
   
3. JWT Authentication & Authorization - A layer for authentication and authorization, requiring JWT tokens to authenticate the party/user requesting access to protected resources from the Search Service API.
5. Web Crawler - An internet bot that systematically browses web pages to extract information.
6. ElasticSearch - Used for searching and storing information extracted by Web Crawler processes.
7. Haystack - Acts as a wrapper for ElasticSearch to build capabilities for semantic search. Together, Haystack and ElasticSearch form a search engine for semantic information retrieval. The information passed to Haystack by Web Crawler processes is processed using techniques such as removing unnecessary white spaces, tokenization (e.g., splitting text by words or sentences), etc. After processing, the information is stored in ElasticSearch. Information to be retrieved from ElasticSearch through an HTTP request by third parties/users to Haystack is either forwarded directly to ElasticSearch (if the request does not require semantic information retrieval) or processed by Haystack using techniques such as retrieving information from ElasticSearch that matches the request using the TF-IDF algorithm. The result is then passed to a transformer model that provides the final result for the request. The result is returned in JSON format to the party/user who made the request to the Search Service API.
8. Scrapy - Used for extracting information from websites through Web Crawler processes.
9. Celery Scheduler - Used to execute Scrapy at predefined intervals.
10. Web sites - Web pages from which the Web Crawler extracts information.
11. Logging - Used for recording information about various events in the Search service. This information is forwarded to the Log Collection Service.





