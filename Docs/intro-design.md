# Abstract representation of the project structure

![Architecture Overview](https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/DetailedProjectDiagram.PNG)

## The components of the diagram have the following meaning

* Auth Server - Authentication server implemented using Keycloak. User credentials are stored in a PostgreSQL database.
* Gui - Graphical User Interface (frontend) that indirectly connects to all services offered by the application. Users access the services through this interface.
* Api Gateway - Reverse proxy server (gateway) implemented using YARP. It connects the graphical interface with all the services offered by the application. Additionally, it performs authentication and authorization functions, providing extra security for the services.
* User Service - A service responsible for storing information in a PostgreSQL database about general actions and information related to users in the application, such as:
    - Sent/Received requests for contact with other users;
    - Sent requests to block other users;
    - Actions performed by users (e.g., Login/Logout, visited pages within the application);
    - Names, nicknames (aliases), and email addresses of users.
* TopicArticle Service - A service responsible for actions and storing information in a PostgreSQL database regarding documents (news/articles) on topics offered by this service. The actions managed by this service include:
    - Subscribing users to receive information on topics provided by the application;
    - Registering new documents on topics provided by the application;
    - Creating comments on documents for subscribed topics;
    - Creating likes/dislikes for documents on subscribed topics;
    - Registering users who have viewed documents on subscribed topics.
* PrivateHistory Service - A service responsible for the user's private history, providing information on:
    - Viewed documents (news/articles);
    - Viewed user profiles;
    - Topics the user has subscribed to;
    - Searched information on topics and documents;
    - Commented documents;
    - Liked/Disliked documents.
    - Information is stored for a specified time and deleted after the expiration of that time. Users can remove parts or the entire history before the expiration time.
* PublicHistory Service - A service responsible for the user's public history, which can contain parts or the entire private history. The public history is created by selecting parts from the private history. Information is stored indefinitely, and users can remove parts or the entire history at any time.
* Search Service - A service responsible for searching information from the internet regarding topics offered by the application. It also handles semantic searches of information offered by the application. The searched information is stored in an Elasticsearch database.
* Chat Service - A service providing communication between users through text messages. Text messages are stored in a PostgreSQL database.
* Message Bus - Message broker (message bus) implemented using RabbitMQ. It connects the services offered by the application.
* Log Collection Service - A service responsible for collecting and recording information about events occurring in the services offered by the application in an Elasticsearch database.


# Search Service - Responsible for actions related to searching information from the internet on topics offered by the application

<p align="center">
    <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/SearchService-1.PNG" alt="Logo" width="350">
</p>

## The components of the diagram have the following meaning

* Actor - This can be a user or a process from the application. The user or process requests information. In the diagram, an example request for "Cybersecurity news from Europe and America" is shown. The request is forwarded to the Search Service API, which then returns a response containing the appropriate HTTP code and information (if found).  
* Search Service API - A web API providing various endpoints. The diagram shows two such endpoints: "Find news for Europe" and "Find news for America," responding to HTTP GET requests for information requested by third parties/users. When a request is made to one of the provided endpoints, it is forwarded to Haystack, where it is processed and sent to ElasticSearch. ElasticSearch returns a response in JSON format to the party/user who made the request to the Search Service API.
   
* JWT Authentication & Authorization - A layer for authentication and authorization, requiring JWT tokens to authenticate the party/user requesting access to protected resources from the Search Service API.
   
* Web Crawler - An internet bot that systematically browses web pages to extract information.
   
* ElasticSearch - Used for searching and storing information extracted by Web Crawler processes.
   
* Haystack - Acts as a wrapper for ElasticSearch to build capabilities for semantic search. Together, Haystack and ElasticSearch form a search engine for semantic information retrieval. The information passed to Haystack by Web Crawler processes is processed using techniques such as removing unnecessary white spaces, tokenization (e.g., splitting text by words or sentences), etc. After processing, the information is stored in ElasticSearch. Information to be retrieved from ElasticSearch through an HTTP request by third parties/users to Haystack is either forwarded directly to ElasticSearch (if the request does not require semantic information retrieval) or processed by Haystack using techniques such as retrieving information from ElasticSearch that matches the request using the TF-IDF algorithm. The result is then passed to a transformer model that provides the final result for the request. The result is returned in JSON format to the party/user who made the request to the Search Service API.
   
* Scrapy - Used for extracting information from websites through Web Crawler processes.
   
* Celery Scheduler - Used to execute Scrapy at predefined intervals.
   
* Web sites - Web pages from which the Web Crawler extracts information.
    
* Logging - Used for recording information about various events in the Search service. This information is forwarded to the Log Collection Service.

