# User Service Project Structure 

### The project is divided into four main layers:

* **Domain** - Contains the core business logic and rules, including entities (Objects that can be identified by an identifier are defined as Entities), events, exceptions, factories, repositories, and value objects (Objects that are compared by their value and do not have an identifier are known as Value Objects). This layer has no external dependencies on other layers or libraries. It defines interfaces that specify the allowed actions;
* **Application** - This layer orchestrates (manages) the work of the domain layer. It has dependencies on the domain layer;
* **Infrastructure** - Here are the implementations of the interfaces provided by the domain layer, application layer and dependencies on libraries such as RabbitMQ, EntityFramework, etc. It has dependencies on the application layer;
* **Presentation** - This layer is the entry point of the service (application) and is implemented as a web API. Provides the API controllers and data transfer objects (DTOs) for handling HTTP requests and responses;

This structured approach ensures a clear separation of concerns, making the codebase easier to manage and extend. Below is an outline of the project's directory structure:

```html
------------------------------------------------------------------
UserService
├── src 📦
│   ├── UserService.Domain 📂
│   │   ├── Entities 📂
│   │   │   ├── _README.txt <<https://github.com/JivkoSp/Drahten/edit/master/Docs/project-structure-user-service.md>>
│   │   │   ├── AggregateRoot.cs 
│   │   │   ├── User.cs
----------------------------
│   │   ├── Events 📂
│   │   │   ├── _README.txt 
│   │   │   ├── BannedUserAdded.cs 
│   │   │   ├── BannedUserRemoved.cs 
│   │   │   ├── ContactRequestAdded.cs 
│   │   │   ├── ContactRequestRemoved.cs 
│   │   │   ├── IDomainEvent.cs 
│   │   │   ├── UserTrackingAuditAdded.cs
----------------------------
│   │   ├── Exceptions 📂
│   │   │   ├── _README.txt 
│   │   │   ├── <<CustomDomainLayerExceptions>>
----------------------------
│   │   ├── Factories 📂
│   │   │   ├── Interfaces 📂
│   │   │   │   ├── IUserFactory.cs 
│   │   │   ├── _README.txt 
│   │   │   ├── UserFactory.cs
----------------------------
│   │   ├── Repositories 📂
│   │   │   ├── _README.txt 
│   │   │   ├── IUserRepository.cs
----------------------------
│   │   ├── ValueObjects 📂
│   │   │   ├── _README.txt 
│   │   │   ├── BannedUser.cs 
│   │   │   ├── ContactRequest.cs 
│   │   │   ├── UserEmailAddress.cs 
│   │   │   ├── UserFullName.cs 
│   │   │   ├── UserID.cs 
│   │   │   ├── UserNickName.cs 
│   │   │   ├── UserTracking.cs
----------------------------
│   ├── UserService.Application 📂
│   │   ├── AsyncDataServices 📂
│   │   │   ├── IMessageBusPublisher.cs
----------------------------
│   │   ├── Commands 📂
│   │   │   ├── Dispatcher 📂
│   │   │   │   ├── _README.txt 
│   │   │   │   ├── ICommandDispatcher.cs 
│   │   │   │   ├── InMemoryCommandDispatcher.cs
│   │   │   ├── Handlers 📂
│   │   │   │   ├── _README.txt 
│   │   │   │   ├── AddContactRequestHandler.cs 
│   │   │   │   ├── AddToAuditTrailHandler.cs 
│   │   │   │   ├── BanUserHandler.cs 
│   │   │   │   ├── CreateUserHandler.cs 
│   │   │   │   ├── ICommandHandler.cs 
│   │   │   │   ├── RemoveIssuedContactRequestHandler.cs 
│   │   │   │   ├── RemoveReceivedContactRequestHandler.cs 
│   │   │   │   ├── UnbanUserHandler.cs 
│   │   │   │   ├── UpdateContactRequestMessageHandler.cs 
│   │   ├── _README.txt 
│   │   ├── AddContactRequestCommand.cs 
│   │   ├── AddToAuditTrailCommand.cs 
│   │   ├── BanUserCommand.cs 
│   │   ├── CreateUserCommand.cs 
│   │   ├── ICommand.cs 
│   │   ├── RemoveIssuedContactRequestCommand.cs 
│   │   ├── RemoveReceivedContactRequestCommand.cs 
│   │   ├── UnbanUserCommand.cs 
│   │   ├── UpdateContactRequestMessageCommand.cs
----------------------------
│   │   ├── Dtos 📂
│   │   │   ├── _README.txt 
│   │   │   ├── IssuedBanByUserDto.cs 
│   │   │   ├── IssuedContactRequestByUserDto.cs 
│   │   │   ├── ReceivedBanByUserDto.cs 
│   │   │   ├── ReceivedContactRequestByUserDto.cs 
│   │   │   ├── UserDto.cs 
│   │   │   ├── UserPublishedDto.cs
----------------------------
│   │   ├── Exceptions 📂
│   │   │   ├── _README.txt 
│   │   │   ├── <<CustomApplicationLayerExceptions>>
----------------------------
│   │   ├── Extensions 📂
│   │   │   ├── ServiceCollectionExtensions.cs
----------------------------
│   │   ├── Queries 📂
│   │   │   ├── Dispatcher 📂
│   │   │   │   ├── _README.txt 
│   │   │   │   ├── InMemoryQueryDispatcher.cs 
│   │   │   │   ├── IQueryDispatcher.cs
----------------------------
│   │   |   ├── Handlers 📂
│   │   │   │   ├── _README.txt 
│   │   │   │   ├── IQueryHandler.cs 
│   │   ├── _README.txt 
│   │   ├── GetIssuedBansByUserQuery.cs 
│   │   ├── GetIssuedContactRequestsByUserQuery.cs 
│   │   ├── GetReceivedBansByUserQuery.cs 
│   │   ├── GetReceivedContactRequestByUserQuery.cs 
│   │   ├── GetUserQuery.cs 
│   │   ├── IQuery.cs
----------------------------
│   │   ├── Services 📂
│   │   │   ├── ReadServices 📂
│   │   │   │   ├── IUserReadService.cs
│   │   |   ├── _README.txt
----------------------------
│   ├── UserService.Infrastructure 📂
│   │   ├── AsyncDataServices 📂
│   │   │   ├── MessageBusPublisher.cs
----------------------------
│   │   ├── Automapper 📂
│   │   │   ├── Profiles 📂
│   │   │   │   ├── BannedUserProfile.cs
│   │   │   │   ├── ContactRequestProfile.cs
│   │   │   │   ├── UserProfile.cs
----------------------------
│   │   ├── EntityFramework 📂
│   │   │   ├── Contexts 📂
│   │   │   │   ├── ReadDbContext.cs
│   │   │   │   ├── WriteDbContext.cs
│   │   │   ├── Encryption 📂
│   │   │   │   ├── EncryptionConverters 📂
│   │   │   │   │   ├── EncryptedDateTimeOffsetConverter.cs
│   │   │   │   │   ├── EncryptedStringConverter.cs
│   │   │   │   │   ├── EncryptedUserEmailAddressConverter.cs
│   │   │   │   │   ├── EncryptedUserFullNameConverter.cs
│   │   │   │   │   ├── EncryptedUserNickNameConverter.cs
│   │   │   │   ├── EncryptionProvider 📂
│   │   │   │   │   ├── IEncryptionProvider.cs
│   │   │   │   │   ├── EncryptionProvider.cs
│   │   │   ├── Initialization 📂
│   │   │   │   ├── DbInitializer.cs
│   │   │   ├── Migrations 📂
│   │   │   │   ├── <<EntityFramework database migrations>>
│   │   │   ├── ModelConfiguration 📂
│   │   │   │   ├── ReadConfiguration 📂
│   │   │   │   │   ├── _README.txt
│   │   │   │   │   ├── BannedUserConfiguration.cs
│   │   │   │   │   ├── ContactRequestConfiguration.cs
│   │   │   │   │   ├── UserConfiguration.cs
│   │   │   │   │   ├── UserTrackingConfiguration.cs
│   │   │   │   ├── WriteConfiguration 📂
│   │   │   │   │   ├── BannedUserConfiguration.cs
│   │   │   │   │   ├── ContactRequestConfiguration.cs
│   │   │   │   │   ├── UserConfiguration.cs
│   │   │   │   │   ├── UserTrackingConfiguration.cs
│   │   │   ├── Models 📂
│   │   │   │   ├── _README.txt
│   │   │   │   ├── BannedUserReadModel.cs
│   │   │   │   ├── ContactRequestReadModel.cs
│   │   │   │   ├── UserReadModel.cs
│   │   │   │   ├── UserTrackingReadModel.cs
│   │   │   ├── Options 📂
│   │   │   │   ├── PostgresOptions.cs
│   │   │   ├── Repositories 📂
│   │   │   │   ├── PostgresUserRepository.cs
│   │   │   ├── Services 📂
│   │   │   │   ├── ReadServices 📂
│   │   │   │   │   ├── PostgresUserReadService.cs
----------------------------
│   │   ├── Exceptions 📂
│   │   │   ├── Interfaces 📂
│   │   │   │   ├── IExceptionToResponseMapper.cs
│   │   │   ├── ExceptionResponse.cs 
│   │   │   ├── ExceptionToResponseMapper.cs
│   │   │   ├── InfrastructureException.cs
│   │   │   ├── NullDbContextException.cs
----------------------------
│   │   ├── Extensions 📂
│   │   │   ├── ServiceCollectionExtensions.cs
│   │   │   ├── ConfigurationExtensions.cs
----------------------------
│   │   ├── Logging 📂
│   │   │   ├── Formatters 📂
│   │   │   │   ├── SerilogJsonFormatter.cs
│   │   │   ├── LoggingCommandHandlerDecorator.cs
----------------------------
│   │   ├── Queries 📂
│   │   │   ├── Handlers 📂
│   │   │   │   ├── GetIssuedBansByUserHandler.cs
│   │   │   │   ├── GetIssuedContactRequestsByUserHandler.cs
│   │   │   │   ├── GetReceivedBansByUserHandler.cs
│   │   │   │   ├── GetReceivedContactRequestByUserHandler.cs
│   │   │   │   ├── GetUserHandler.cs
----------------------------
│   │   ├── UserRegistration 📂
│   │   │   ├── IUserSynchronizer.cs
│   │   │   ├── UserSynchronizer.cs
----------------------------
│   ├── UserService.Presentation 📂
│   │   ├── Properties 📂
│   │   │   ├── launchSettings.json
│   │   ├── Controllers 📂
│   │   │   ├── UserController.cs
│   │   ├── Dtos 📂
│   │   │   ├── ResponseDto.cs
│   │   ├── Middlewares 📂
│   │   │   ├── ErrorHandlerMiddleware.cs
│   │   │   ├── RateLimitingMiddleware.cs
│   │   │   ├── UserRegistrationMiddleware.cs
│   │   ├── appsettings.json
│   │   ├── Program.cs
----------------------------
├── tests 🧪
│   ├── UserService.Tests.EndToEnd
│   │   ├── Extensions 📂
│   │   │   ├── IServiceCollectionExtensions.cs
│   │   ├── Factories 📂
│   │   │   ├── UserServiceApplicationFactory.cs
│   │   ├── Sync 📂
│   │   │   ├── _README.txt
│   │   │   ├── BaseSyncIntegrationTest.cs
│   │   │   ├── RegisterBannedUserTests.cs
│   │   │   ├── RegisterContactRequestTests.cs
│   │   │   ├── RegisterUserActivityTests.cs
│   │   │   ├── RegisterUserTests.cs
│   │   │   ├── RemoveBannedUserTests.cs
│   │   │   ├── RemoveIssuedContactRequestTests.cs
│   │   │   ├── RemoveReceivedContactRequestTests.cs
│   │   │   ├── UpdateContactRequestMessageTests.cs
----------------------------
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
│   │   │   ├── Entities 📂
│   │   │   │   ├── UserTests 📂
│   │   │   │   |   ├── _README.txt
│   │   │   │   |   ├── AddToAuditTrail.cs
│   │   │   │   |   ├── BanUser.cs
│   │   │   │   |   ├── IssueContactRequest.cs
│   │   │   │   |   ├── ReceiveContactRequest.cs
│   │   │   │   |   ├── RemoveReceivedContactRequest.cs
│   │   │   │   |   ├── UnbanUser.cs
│   │   │   ├── Factories 📂
│   │   │   │   ├── _README.txt
│   │   │   │   ├── UserFactoryTests.cs
------------------------------------------------------------------
```

## Directory/File Descriptions

| Directory/File                                              | Description                                                   |
|-------------------------------------------------------------|---------------------------------------------------------------|
| `UserService/src/UserService.Domain/Entities/`              | Contains domain entities representing core business concepts. |
| `UserService/src/UserService.Domain/Events/`                | Includes domain events capturing significant changes or actions. |
| `UserService/src/UserService.Domain/Exceptions/`            | Houses custom exceptions specific to domain logic.             |
| `UserService/src/UserService.Domain/Factories/`             | Provides factories for creating domain entities.               |
| `UserService/src/UserService.Domain/Repositories/`          | Defines interfaces or base classes for data access operations. |
| `UserService/src/UserService.Domain/ValueObjects/`          | Contains immutable value objects used within the domain.       |
| `UserService/src/UserService.Application/AsyncDataServices/`         | TODO     |
| `UserService/src/UserService.Application/Commands/`         | TODO     |
| `UserService/src/UserService.Application/Dtos/`         | TODO     |
| `UserService/src/UserService.Application/Exceptions/`         | TODO     |
| `UserService/src/UserService.Application/Extensions/`         | TODO     |
| `UserService/src/UserService.Application/Queries/`         | TODO     |
| `UserService/src/UserService.Application/Services/`         | Implements application services containing business logic.     |
| `UserService/src/UserService.Infrastructure/AsyncDataServices/`    | TODO |
| `UserService/src/UserService.Presentation/Controllers/`      | Contains API controllers handling HTTP requests and responses. |
| `UserService/src/UserService.Presentation/DTOs/`            | Provides Data Transfer Objects (DTOs) for API input and output. |
| `UserService/tests/UserService.Tests/Unit/Domain/Entities/`  | Houses unit tests for domain entities and aggregate roots.     |
| `UserService/tests/UserService.Tests/Unit/Domain/Events/`    | Includes unit tests for domain events and event handlers.      |
| `UserService/tests/UserService.Tests/Unit/Application/Services/` | Contains unit tests for application layer services.        |
| `UserService/tests/UserService.Tests/Unit/Infrastructure/Persistence/` | Includes unit tests for repository implementations.  |




