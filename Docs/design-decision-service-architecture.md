# Service Architecture

<p align="center">
    <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/CleanArchitecture.PNG" alt="Logo" width="450">
</p>

## I have chosen Clean Architecture because of the principles it focuses on

* **Independence of framework components** - The architecture should be independent of any specific external framework or component. This allows for easier portability and adaptability to changes in the technologies
  or components used in the system;
* **Testability** - The architecture should facilitate the testing of business rules and the system's logic, enabling automated testing of modules (unit testing) and the integration of different components of the
  system (integration testing);
* **Independence from the user interface** - The user interface should be separated from the business logic and the domain model of the system. This allows flexibility in designing different user interfaces without
  affecting the core business logic;
* **Independence from the database** - The implementation of the database should be separate from the core business logic. This allows for easy replacement or migration to different database systems;

Each service (component) of the application is as independent as possible from every other component, which complements the principles of microservices architecture and makes the process of developing the
  overall system, as well as its testing and expansion, easier.
