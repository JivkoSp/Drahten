# Capabilities

The current product and technical features

# Product

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
