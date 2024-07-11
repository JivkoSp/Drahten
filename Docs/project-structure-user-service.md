# User Service Project Structure 

### The project is divided into four main layers:

* **Domain** - Contains the core business logic and rules, including entities, events, exceptions, factories, repositories, and value objects;
* **Application** - Manages service interfaces and application logic, bridging the domain and infrastructure layers.
* **Infrastructure** - Handles data persistence, external services, and other infrastructural concerns.
* **Presentation** - Provides the API controllers and data transfer objects (DTOs) for handling HTTP requests and responses.

This structured approach ensures a clear separation of concerns, making the codebase easier to manage and extend. Below is an outline of the project's directory structure:

   ```js
      UserService
      ├── src
      │   ├── UserService.Domain
      │   │   ├── Entities
      │   │   │   ├── _README.md
      │   │   │   ├── AggregateRoot.cs
      │   │   │   ├── User.cs
      │   │   ├── Events
      │   │   │   ├── _README.md
      │   │   │   ├── BannedUserAdded.cs
      │   │   │   ├── BannedUserRemoved.cs
      │   │   │   ├── ContactRequestAdded.cs
      │   │   │   ├── ContactRequestRemoved.cs
      │   │   │   ├── IDomainEvent.cs
      │   │   │   ├── UserTrackingAuditAdded.cs
      │   │   ├── Exceptions
      │   │   ├── Factories
      │   │   ├── Repositories
      │   │   ├── ValueObjects
      │   ├── UserService.Application
      │   │   ├── _README.md
      │   │   ├── Services
      │   ├── UserService.Infrastructure
      │   │   ├── _README.md
      │   │   ├── Persistence
      │   │   │   ├── UserRepository.cs
      │   │   ├── ExternalServices
      │   ├── UserService.Presentation
      │   │   ├── Controllers
      │   │   ├── DTOs
      ├── tests
      │   ├── UserService.Tests
      │   │   ├── Unit
      │   │   │   ├── Domain
      │   │   │   │   ├── Entities
      │   │   │   │   │   ├── AggregateRootTests.cs
      │   │   │   │   │   ├── UserTests.cs
      │   │   │   │   ├── Events
      │   │   │   │   │   ├── BannedUserAddedTests.cs
      │   │   │   │   │   ├── BannedUserRemovedTests.cs
      │   │   │   │   │   ├── ContactRequestAddedTests.cs
      │   │   │   │   │   ├── ContactRequestRemovedTests.cs
      │   │   │   │   │   ├── UserTrackingAuditAddedTests.cs
      │   │   │   ├── Application
      │   │   │   │   ├── Services
      │   │   │   ├── Infrastructure
      │   │   │   │   ├── Persistence
      │   │   │   │   │   ├── UserRepositoryTests.cs
      ├── .gitignore
      ├── README.md
      └── requirements.txt
   ```
  
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



