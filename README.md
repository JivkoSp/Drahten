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

* Auth Server - Authentication server implemented using Keycloak. User credentials are stored in a PostgreSQL database.
* Gui - Graphical User Interface (frontend) that indirectly connects to all services offered by the application. Users access the services through this interface.
* Api Gateway - Reverse proxy server (gateway) implemented using YARP. It connects the graphical interface with all the services offered by the application. Additionally, it performs authentication and authorization functions, providing extra security for the services.
* User Service - A service responsible for storing information in a PostgreSQL database about general actions and information related to users in the application, such as:
1. Sent/Received requests for contact with other users;
2.  Sent requests to block other users;
3.   ctions performed by users (e.g., Login/Logout, visited pages within the application);
4. Names, nicknames (aliases), and email addresses of users.
* TopicArticle Service - A service responsible for actions and storing information in a PostgreSQL database regarding documents (news/articles) on topics offered by this service. The actions managed by this service include:
1. Subscribing users to receive information on topics provided by the application;
2. Registering new documents on topics provided by the application;
3. Creating comments on documents for subscribed topics;
4. Creating likes/dislikes for documents on subscribed topics;
5. Registering users who have viewed documents on subscribed topics.
* PrivateHistory Service - A service responsible for the user's private history, providing information on:
1. Viewed documents (news/articles);
2. Viewed user profiles;
3. Topics the user has subscribed to;
4. Searched information on topics and documents;
5. Commented documents;
6. Liked/Disliked documents.
7. Information is stored for a specified time and deleted after the expiration of that time. Users can remove parts or the entire history before the expiration time.
* PublicHistory Service - A service responsible for the user's public history, which can contain parts or the entire private history. The public history is created by selecting parts from the private history. Information is stored indefinitely, and users can remove parts or the entire history at any time.
* Search Service - A service responsible for searching information from the internet regarding topics offered by the application. It also handles semantic searches of information offered by the application. The searched information is stored in an Elasticsearch database.
* Chat Service - A service providing communication between users through text messages. Text messages are stored in a PostgreSQL database.
* Message Bus - Message broker (message bus) implemented using RabbitMQ. It connects the services offered by the application.
* Log Collection Service - A service responsible for collecting and recording information about events occurring in the services offered by the application in an Elasticsearch database.

