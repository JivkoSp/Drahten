# User Service Project Structure 

## Introduction
The User Service is designed using Clean Architecture and Domain-Driven Design principles to ensure scalability, maintainability, and testability. This service is responsible for managing user-related data and operations, including authentication, authorization, and interaction with a PostgreSQL database.

### The project is divided into four main layers:

* **Domain** - Contains the core business logic and rules, including entities, events, exceptions, factories, repositories, and value objects;
* **Application** - Manages service interfaces and application logic, bridging the domain and infrastructure layers.
* **Infrastructure** - Handles data persistence, external services, and other infrastructural concerns.
* **Presentation** - Provides the API controllers and data transfer objects (DTOs) for handling HTTP requests and responses.

This structured approach ensures a clear separation of concerns, making the codebase easier to manage and extend. Below is an outline of the project's directory structure:

<details>
  <summary><b>UserService</b></summary>
      <details>
          <summary><b>src</b></summary>
            <details>
                <summary><b>UserService.Domain</b></summary>
                  <details>
                      <summary><b>Entities</b></summary>
                      <ul>
                        <li><a href="UserService/src/UserService.Domain/Entities/_README.md">_README.md</a></li>
                        <li>AggregateRoot.cs</li>
                        <li>User.cs</li>
                      </ul>
                  </details>
              <details>
                    <summary><b>Events</b></summary>
        <ul>
          <li><a href="UserService/src/UserService.Domain/Events/_README.md">_README.md</a></li>
          <li>BannedUserAdded.cs</li>
          <li>BannedUserRemoved.cs</li>
          <li>ContactRequestAdded.cs</li>
          <li>ContactRequestRemoved.cs</li>
          <li>IDomainEvent.cs</li>
          <li>UserTrackingAuditAdded.cs</li>
        </ul>
      </details>
      <details><summary>Exceptions</summary></details>
      <details><summary>Factories</summary></details>
      <details><summary>Repositories</summary></details>
      <details><summary>ValueObjects</summary></details>
    </details>
    <details>
      <summary><b>UserService.Application</b></summary>
      <ul>
        <li><a href="UserService/src/UserService.Application/_README.md">_README.md</a></li>
        <details><summary>Services</summary></details>
      </ul>
    </details>
    <details>
      <summary><b>UserService.Infrastructure</b></summary>
      <ul>
        <li><a href="UserService/src/UserService.Infrastructure/_README.md">_README.md</a></li>
        <details>
          <summary>Persistence</summary>
          <ul>
            <li>UserRepository.cs</li>
          </ul>
        </details>
        <details><summary>ExternalServices</summary></details>
      </ul>
    </details>
    <details>
      <summary><b>UserService.Presentation</b></summary>
      <details><summary>Controllers</summary></details>
      <details><summary>DTOs</summary></details>
    </details>
  </details>
  <details>
    <summary><b>tests</b></summary>
    <details>
      <summary>UserService.Tests</summary>
      <details>
        <summary>Unit</summary>
        <details>
          <summary>Domain</summary>
          <details>
            <summary>Entities</summary>
            <ul>
              <li>AggregateRootTests.cs</li>
              <li>UserTests.cs</li>
            </ul>
          </details>
          <details>
            <summary>Events</summary>
            <ul>
              <li>BannedUserAddedTests.cs</li>
              <li>BannedUserRemovedTests.cs</li>
              <li>ContactRequestAddedTests.cs</li>
              <li>ContactRequestRemovedTests.cs</li>
              <li>UserTrackingAuditAddedTests.cs</li>
            </ul>
          </details>
        </details>
        <details><summary>Application</summary></details>
        <details>
          <summary>Infrastructure</summary>
          <details>
            <summary>Persistence</summary>
            <ul>
              <li>UserRepositoryTests.cs</li>
            </ul>
          </details>
        </details>
      </details>
    </details>
  </details>
  <ul>
    <li><a href="UserService/.gitignore">.gitignore</a></li>
    <li><a href="UserService/README.md">README.md</a></li>
    <li><a href="UserService/requirements.txt">requirements.txt</a></li>
  </ul>
</details>
  
## Directory/File Descriptions

| Directory/File                          | Description                                                   |
|-----------------------------------------|---------------------------------------------------------------|
| `UserService/src/UserService.Domain/Entities/`     | Domain entities representing core business concepts.          |
| `UserService/src/UserService.Domain/Events/`       | Domain events capturing significant changes or actions.       |
| `UserService/src/UserService.Domain/Exceptions/`   | Custom exceptions specific to domain logic.                    |
| `UserService/src/UserService.Domain/Factories/`    | Factories for creating domain objects.                         |
| `UserService/src/UserService.Domain/Repositories/` | Interfaces or base classes for data access operations.         |
| `UserService/src/UserService.Domain/ValueObjects/`  | Immutable value objects used within the domain.                |
| `UserService/src/UserService.Application/Services/` | Application services implementing business logic.             |
| `UserService/src/UserService.Infrastructure/Persistence/` | Data access logic, including repository implementations.   |
| `UserService/src/UserService.Infrastructure/ExternalServices/` | Integration with external services or APIs.             |
| `UserService/src/UserService.Presentation/Controllers/` | API controllers handling HTTP requests and responses.       |
| `UserService/src/UserService.Presentation/DTOs/`      | Data Transfer Objects for API input and output.              |
| `UserService/tests/UserService.Tests/Unit/Domain/Entities/` | Unit tests for domain entities and aggregate roots.    |
| `UserService/tests/UserService.Tests/Unit/Domain/Events/` | Unit tests for domain events and event handlers.       |
| `UserService/tests/UserService.Tests/Unit/Application/Services/` | Unit tests for application layer services.         |
| `UserService/tests/UserService.Tests/Unit/Infrastructure/Persistence/` | Unit tests for repository implementations.   |
| `UserService/.gitignore`                  | Specifies files and directories to ignore in version control. |
| `UserService/README.md`                   | Project documentation providing an overview and instructions. |
| `UserService/requirements.txt`            | Lists dependencies required for the project.                   |



