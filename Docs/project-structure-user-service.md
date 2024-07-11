# User Service Project Structure 

## Responsible for storing information in a PostgreSQL database regarding more general actions and information related to the user in the application

<p align="center">
    <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/UserService-1.PNG" alt="User Service Diagram" width="450">
</p>

### Overview

The User Service manages user-related actions and information within the application. It utilizes a PostgreSQL database to store data and provides endpoints via a .NET web API for interacting with user data securely.

### Architecture Components

The diagram illustrates the following components:

- **Actor**: Users or processes that interact with the User Service API.
- **User Service API**: Provides various endpoints secured by JWT authentication and authorization.
- **JWT Authentication & Authorization**: Ensures secure access to protected resources.
- **PostgreSQL Database**: Stores user-related information.
- **Logging**: Captures events within the User Service, sending them to the Log Collection Service.

---

## Database Structure

A detailed look at the tables within the User Service database and their relationships:

### Table "User"

![User Table](https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/UserServiceDatabaseUserTable.PNG)

- **UserId**: Primary key, uniquely identifies users.
- **Version**: Tracks the user's version to manage updates.
- **UserFullName, UserNickName, UserEmailAddress**: Store user details.

### Table "BannedUser"

![Banned User Table](https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/UserServiceDatabaseBannedUserTable.PNG)

- **IssuerUserId, ReceiverUserId**: Composite primary key, linked to User table.
- **DateTime**: Date and time of user ban.

### Table "ContactRequest"

![Contact Request Table](https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/UserServiceDatabaseContactRequestTable.PNG)

- **IssuerUserId, ReceiverUserId**: Composite primary key, linked to User table.
- **Message, DateTime**: Details and timestamp of contact requests.

### Table "UserTracking"

![User Tracking Table](https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/UserServiceDatabaseUserTrackingTable.PNG)

- **UserTrackingId**: Unique identifier for user actions.
- **Action, DateTime, Referrer, UserId**: Record user activities and details.

---

- **UserService**
  - **src**
    - **UserService.Domain**
      - **Entities**
        - [_README.md](UserService/src/UserService.Domain/Entities/_README.md)
        - AggregateRoot.cs
        - User.cs
      - **Events**
        - [_README.md](UserService/src/UserService.Domain/Events/_README.md)
        - BannedUserAdded.cs
        - BannedUserRemoved.cs
        - ContactRequestAdded.cs
        - ContactRequestRemoved.cs
        - IDomainEvent.cs
        - UserTrackingAuditAdded.cs
      - **Exceptions**
      - **Factories**
      - **Repositories**
      - **ValueObjects**
    - **UserService.Application**
      - [_README.md](UserService/src/UserService.Application/_README.md)
      - **Services**
    - **UserService.Infrastructure**
      - [_README.md](UserService/src/UserService.Infrastructure/_README.md)
      - **Persistence**
        - UserRepository.cs
      - **ExternalServices**
    - **UserService.Presentation**
      - **Controllers**
      - **DTOs**
  - **tests**
    - **UserService.Tests**
      - **Unit**
        - **Domain**
          - **Entities**
            - AggregateRootTests.cs
            - UserTests.cs
          - **Events**
            - BannedUserAddedTests.cs
            - BannedUserRemovedTests.cs
            - ContactRequestAddedTests.cs
            - ContactRequestRemovedTests.cs
            - UserTrackingAuditAddedTests.cs
        - **Application**
          - **Services**
        - **Infrastructure**
          - **Persistence**
            - UserRepositoryTests.cs
- [.gitignore](UserService/.gitignore)
- [README.md](UserService/README.md)
- [requirements.txt](UserService/requirements.txt)
  
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



