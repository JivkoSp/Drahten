# Abstract representation of the project structure

![Architecture Overview](https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/DetailedProjectDiagram.PNG)

## The components of the diagram have the following meaning

* **Auth Server** - Authentication server implemented using Keycloak. User credentials are stored in a PostgreSQL database.
* **Gui** - Graphical User Interface (frontend) that indirectly connects to all services offered by the application. Users access the services through this interface.
* **Api Gateway** - Reverse proxy server (gateway) implemented using YARP. It connects the graphical interface with all the services offered by the application. Additionally, it performs authentication and authorization functions, providing extra security for the services.
* **User Service** - A service responsible for storing information in a PostgreSQL database about general actions and information related to users in the application, such as:
    - Sent/Received requests for contact with other users;
    - Sent requests to block other users;
    - Actions performed by users (e.g., Login/Logout, visited pages within the application);
    - Names, nicknames (aliases), and email addresses of users.
* **TopicArticle Service** - A service responsible for actions and storing information in a PostgreSQL database regarding documents (news/articles) on topics offered by this service. The actions managed by this service include:
    - Subscribing users to receive information on topics provided by the application;
    - Registering new documents on topics provided by the application;
    - Creating comments on documents for subscribed topics;
    - Creating likes/dislikes for documents on subscribed topics;
    - Registering users who have viewed documents on subscribed topics.
* **PrivateHistory Service** - A service responsible for the user's private history, providing information on:
    - Viewed documents (news/articles);
    - Viewed user profiles;
    - Topics the user has subscribed to;
    - Searched information on topics and documents;
    - Commented documents;
    - Liked/Disliked documents.
    - Information is stored for a specified time and deleted after the expiration of that time. Users can remove parts or the entire history before the expiration time.
* **PublicHistory Service** - A service responsible for the user's public history, which can contain parts or the entire private history. The public history is created by selecting parts from the private history. Information is stored indefinitely, and users can remove parts or the entire history at any time.
* **Search Service** - A service responsible for searching information from the internet regarding topics offered by the application. It also handles semantic searches of information offered by the application. The searched information is stored in an Elasticsearch database.
* **Chat Service** - A service providing communication between users through text messages. Text messages are stored in a PostgreSQL database.
* **Message Bus** - Message broker (message bus) implemented using RabbitMQ. It connects the services offered by the application.
* **Log Collection Service** - A service responsible for collecting and recording information about events occurring in the services offered by the application in an Elasticsearch database.

