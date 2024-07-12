# User Service Project Structure 

### The project is divided into four main layers:

* **Domain** - Contains the core business logic and rules, including entities, events, exceptions, factories, repositories, and value objects;
* **Application** - Manages service interfaces and application logic, bridging the domain and infrastructure layers.
* **Infrastructure** - Handles data persistence, external services, and other infrastructural concerns.
* **Presentation** - Provides the API controllers and data transfer objects (DTOs) for handling HTTP requests and responses.

This structured approach ensures a clear separation of concerns, making the codebase easier to manage and extend. Below is an outline of the project's directory structure:

```plaintext
UserService
├── src 📦
│   ├── UserService.Domain 📂
│   │   ├── Entities 📂
│   │   │   ├── _README.txt 📄
│   │   │   ├── AggregateRoot.cs 📄
│   │   │   ├── User.cs 📄
│   │   ├── Events 📂
│   │   │   ├── _README.txt 📄
│   │   │   ├── BannedUserAdded.cs 📄
│   │   │   ├── BannedUserRemoved.cs 📄
│   │   │   ├── ContactRequestAdded.cs 📄
│   │   │   ├── ContactRequestRemoved.cs 📄
│   │   │   ├── IDomainEvent.cs 📄
│   │   │   ├── UserTrackingAuditAdded.cs 📄
│   │   ├── Exceptions 📂
│   │   │   ├── _README.txt 📄
│   │   │   ├── CustomDomainLayerExceptions.cs 📄
│   │   ├── Factories 📂
│   │   │   ├── Interfaces 📂
│   │   │   │   ├── IUserFactory.cs 📄
│   │   │   ├── _README.txt 📄
│   │   │   ├── UserFactory.cs 📄
│   │   ├── Repositories 📂
│   │   │   ├── _README.txt 📄
│   │   │   ├── IUserRepository.cs 📄
│   │   ├── ValueObjects 📂
│   │   │   ├── _README.txt 📄
│   │   │   ├── BannedUser.cs 📄
│   │   │   ├── ContactRequest.cs 📄
│   │   │   ├── UserEmailAddress.cs 📄
│   │   │   ├── UserFullName.cs 📄
│   │   │   ├── UserID.cs 📄
│   │   │   ├── UserNickName.cs 📄
│   │   │   ├── UserTracking.cs 📄
│   ├── UserService.Application 📂
│   │   ├── AsyncDataServices 📂
│   │   │   ├── IMessageBusPublisher.cs 📄
│   │   ├── Commands 📂
│   │   │   ├── Dispatcher 📂
│   │   │   │   ├── ICommandDispatcher.cs 📄
│   │   │   │   ├── InMemoryCommandDispatcher.cs 📄
│   │   │   ├── Handlers 📂
│   │   │   │   ├── _README.txt 📄
│   │   │   │   ├── AddContactRequestHandler.cs 📄
│   │   │   │   ├── AddToAuditTrailHandler.cs 📄
│   │   │   │   ├── BanUserHandler.cs 📄
│   │   │   │   ├── CreateUserHandler.cs 📄
│   │   │   │   ├── ICommandHandler.cs 📄
│   │   │   │   ├── RemoveIssuedContactRequestHandler.cs 📄
│   │   │   │   ├── RemoveReceivedContactRequestHandler.cs 📄
│   │   │   │   ├── UnbanUserHandler.cs 📄
│   │   │   │   ├── UpdateContactRequestMessageHandler.cs 📄
│   │   ├── _README.txt 📄
│   │   ├── AddContactRequestCommand.cs 📄
│   │   ├── AddToAuditTrailCommand.cs 📄
│   │   ├── BanUserCommand.cs 📄
│   │   ├── CreateUserCommand.cs 📄
│   │   ├── ICommand.cs 📄
│   │   ├── RemoveIssuedContactRequestCommand.cs 📄
│   │   ├── RemoveReceivedContactRequestCommand.cs 📄
│   │   ├── UnbanUserCommand.cs 📄
│   │   ├── UpdateContactRequestMessageCommand.cs 📄
│   │   ├── Dtos 📂
│   │   │   ├── _README.txt 📄
│   │   │   ├── IssuedBanByUserDto.cs 📄
│   │   │   ├── IssuedContactRequestByUserDto.cs 📄
│   │   │   ├── ReceivedBanByUserDto.cs 📄
│   │   │   ├── ReceivedContactRequestByUserDto.cs 📄
│   │   │   ├── UserDto.cs 📄
│   │   │   ├── UserPublishedDto.cs 📄
│   │   ├── Exceptions 📂
│   │   │   ├── _README.txt 📄
│   │   │   ├── CustomApplicationLayerExceptions.cs 📄
│   │   ├── Extensions 📂
│   │   │   ├── ServiceCollectionExtensions.cs 📄
│   │   ├── Queries 📂
│   │   │   ├── Dispatcher 📂
│   │   │   │   ├── _README.txt 📄
│   │   │   │   ├── InMemoryQueryDispatcher.cs 📄
│   │   │   │   ├── IQueryDispatcher.cs 📄
│   │   ├── Handlers 📂
│   │   │   │   ├── _README.txt 📄
│   │   │   │   ├── IQueryHandler.cs 📄
│   │   ├── _README.txt 📄
│   │   ├── GetIssuedBansByUserQuery.cs 📄
│   │   ├── GetIssuedContactRequestsByUserQuery.cs 📄
│   │   ├── GetReceivedBansByUserQuery.cs 📄
│   │   ├── GetReceivedContactRequestByUserQuery.cs 📄
│   │   ├── GetUserQuery.cs 📄
│   │   ├── IQuery.cs 📄
│   │   ├── Services 📂
│   │   │   ├── ReadServices 📂
│   │   │   │   ├── IUserReadService.cs 📄
│   │   ├── _README.txt 📄
│   ├── UserService.Infrastructure 📂
│   │   ├── AsyncDataServices 📂
│   │   │   ├── MessageBusPublisher.cs 📄
│   │   ├── Automapper 📂
│   │   │   ├── Profiles 📂
│   │   │   │   ├── BannedUserProfile.cs 📄
│   │   │   │   ├── ContactRequestProfile.cs 📄
│   │   │   │   ├── UserProfile.cs 📄
│   │   ├── EntityFramework 📂
│   │   │   ├── Contexts 📂
│   │   │   │   ├── ReadDbContext.cs 📄
│   │   │   │   ├── WriteDbContext.cs 📄
│   │   │   ├── Encryption 📂
│   │   │   │   ├── EncryptionConverters 📂
│   │   │   │   │   ├── EncryptedDateTimeOffsetConverter.cs 📄
│   │   │   │   │   ├── EncryptedStringConverter.cs 📄
│   │   │   │   │   ├── EncryptedUserEmailAddressConverter.cs 📄
│   │   │   │   │   ├── EncryptedUserFullNameConverter.cs 📄
│   │   │   │   │   ├── EncryptedUserNickNameConverter.cs 📄
│   │   │   │   ├── EncryptionProvider 📂
│   │   │   │   │   ├── IEncryptionProvider.cs 📄
│   │   │   │   │   ├── EncryptionProvider.cs 📄
│   │   │   ├── Initialization 📂
│   │   │   │   ├── DbInitializer.cs 📄
│   │   │   ├── Migrations 📂
│   │   │   │   ├── EntityFramework database migrations 📄
│   │   │   ├── ModelConfiguration 📂
│   │   │   │   ├── ReadConfiguration 📂
│   │   │   │   │   ├── _README.txt 📄
│   │   │   │   │   ├── BannedUserConfiguration.cs 📄
│   │   │   │   │   ├── ContactRequestConfiguration.cs 📄
│   │   │   │   │   ├── UserConfiguration.cs 📄
│   │   │   │   │   ├── UserTrackingConfiguration.cs 📄
│   │   │   │   ├── WriteConfiguration 📂
│   │   │   │   │   ├── BannedUserConfiguration.cs 📄
│   │   │   │   │   ├── ContactRequestConfiguration.cs 📄
│   │   │   │   │   ├── UserConfiguration.cs 📄
│   │   │   │   │   ├── UserTrackingConfiguration.cs 📄
│   │   │   ├── Models 📂
│   │   │   │   ├── _README.txt 📄
│   │   │   │   ├── BannedUserReadModel.cs 📄
│   │   │   │   ├── ContactRequestReadModel.cs 📄
│   │   │   │   ├── UserReadModel.cs 📄
│   │   │   │   ├── UserTrackingReadModel.cs 📄
│   │   │   ├── Options 📂
│   │   │   │   ├── PostgresOptions.cs 📄
│   │   │   ├── Repositories 📂
│   │   │   │   ├── PostgresUserRepository.cs 📄
│   │   │   ├── Services 📂
│   │   │   │   ├── ReadServices 📂
│   │   │   │   │   ├── PostgresUserReadService.cs 📄
│   │   ├── Exceptions 📂
│   │   │   ├── Interfaces 📂
│   │   │   │   ├── IExceptionToResponseMapper.cs 📄
│   │   │   ├── ExceptionResponse.cs 📄
│   │   │   ├── ExceptionToResponseMapper.cs 📄
│   │   │   ├── InfrastructureException.cs 📄
│   │   │   ├── NullDbContextException.cs 📄
│   │   ├── Extensions 📂
│   │   │   ├── ServiceCollectionExtensions.cs 📄
│   │   │   ├── ConfigurationExtensions.cs 📄
│   │   ├── Logging 📂
│   │   │   ├── Formatters 📂
│   │   │   │   ├── SerilogJsonFormatter.cs 📄
│   │   │   ├── LoggingCommandHandlerDecorator.cs 📄
│   │   ├── Queries 📂
│   │   │   ├── Handlers 📂
│   │   │   │   ├── GetIssuedBansByUserHandler.cs 📄
│   │   │   │   ├── GetIssuedContactRequestsByUserHandler.cs 📄
│   │   │   │   ├── GetReceivedBansByUserHandler.cs 📄
│   │   │   │   ├── GetReceivedContactRequestByUserHandler.cs 📄
│   │   │   │   ├── GetUserHandler.cs
│   │   ├── UserRegistration 📂
│   │   │   ├── IUserSynchronizer.cs 📄
│   │   │   ├── UserSynchronizer.cs 📄
│   ├── UserService.Presentation 📂
│   │   ├── Controllers
│   │   ├── DTOs
├── tests 🧪
│   ├── UserService.Tests.EndToEnd
│   │   ├── Extensions 📂
│   │   │   ├── IServiceCollectionExtensions.cs
│   │   ├── Factories 📂
│   │   │   ├── UserServiceApplicationFactory.cs
│   │   ├── Sync 📂
│   │   │   ├── [_README.txt](tests/UserService.Tests.EndToEnd/Sync/_README.txt)
│   │   │   ├── BaseSyncIntegrationTest.cs
│   │   │   ├── RegisterBannedUserTests.cs
│   │   │   ├── RegisterContactRequestTests.cs
│   │   │   ├── RegisterUserActivityTests.cs
│   │   │   ├── RegisterUserTests.cs
│   │   │   ├── RemoveBannedUserTests.cs
│   │   │   ├── RemoveIssuedContactRequestTests.cs
│   │   │   ├── RemoveReceivedContactRequestTests.cs
│   │   │   ├── UpdateContactRequestMessageTests.cs
│   ├── UserService.Tests.Unit
│   │   ├── Application 📂
│   │   │   ├── Handlers 📂
│   │   │   │   ├── AddContactRequestHandlerTests.cs
│   │   │   │   ├── AddToAuditTrailHandlerTests.cs
│   │   │   │   ├── BanUserHandlerTests.cs
│   │   │   │   ├── CreateUserHandlerTests.cs
│   │   │   │   ├── RemoveIssuedContactRequestHandlerTests.cs
│   │   │   │   ├── RemoveReceivedContactRequestHandlerTests.cs
│   │   │   │   ├── UnbanUserHandlerTests.cs
│   │   │   │   ├── UpdateContactRequestMessageHandlerTests.cs
│   │   ├── Domain 📂
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



