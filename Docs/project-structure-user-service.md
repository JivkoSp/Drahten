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
│   │   │   ├── _README.txt
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

| Layer          | Directory/File                                                                                        | Description                                                   |
|----------------|-------------------------------------------------------------------------------------------------------|---------------------------------------------------------------|
| Domain         | `UserService/src/UserService.Domain/Entities/`                                                        | Contains domain entities representing core business concepts. |
| Domain         | `UserService/src/UserService.Domain/Events/`                                                          | Includes domain events capturing significant changes or actions. |
| Domain         | `UserService/src/UserService.Domain/Exceptions/`                                                      | Houses custom exceptions specific to domain logic.             |
| Domain         | `UserService/src/UserService.Domain/Factories/`                                                       | Provides factories for creating domain entities.               |
| Domain         | `UserService/src/UserService.Domain/Repositories/`                                                    | Defines interfaces or base classes for data access operations. |
| Domain         | `UserService/src/UserService.Domain/ValueObjects/`                                                    | Contains immutable value objects used within the domain.       |
| Application    | `UserService/src/UserService.Application/AsyncDataServices/`                                          | Defines interfaces for asynchronous operations. |
| Application    | `UserService/src/UserService.Application/Commands/`                                                   | Contains command types and serves as the central location for defining and organizing commands. |
| Application    | `UserService/src/UserService.Application/Commands/Dispatcher/`                                        | Contains command dispatcher type and serves as the central component responsible for 
routing commands to their respective command handlers. |
| Application    | `UserService/src/UserService.Application/Commands/Handlers/`                                          | Contains command handler types that implement ICommandHandler<T> interface and plays a crucial role in implementing 
Command Query Responsibility Segregation (CQRS). |
| Application    | `UserService/src/UserService.Application/Dtos/`                                                       | Contains Data Transfer Objects (DTOs). |
| Application    | `UserService/src/UserService.Application/Exceptions/`                                                 | Contains Application layer specific custom exception types. |
| Application    | `UserService/src/UserService.Application/Extensions/`                                                 | Contains custom extension methods. |
| Application    | `UserService/src/UserService.Application/Queries/`                                                    | Contains query types and serves as the central location for defining and organizing queries. |
  | Application    | `UserService/src/UserService.Application/Queries/Dispatcher/`                                       | Contains query dispatcher type that implements IQueryDispatcher interface and serves as the central component responsible for 
routing queries to their respective query handlers for execution and data retrieval. |
| Application    | `UserService/src/UserService.Application/Queries/Handlers/`                                           | Contains the definition of the IQueryHandler<TQuery, TResult> interface, which serves as a contract for all query handlers 
responsible for executing queries and retrieving data from the application's read-side or queryable data sources. |
| Application    | `UserService/src/UserService.Application/Services/`                                                   | Implements application services containing business logic.     |
| Infrastructure | `UserService/src/UserService.Infrastructure/AsyncDataServices/`                                       | Defines implementations of interfaces for asynchronous operations. |
| Infrastructure | `UserService/src/UserService.Infrastructure/Automapper/`                                              | Contains types, that are inheriting the Profile type from AutoMapper library. |
| Infrastructure | `UserService/src/UserService.Infrastructure/EntityFramework/Contexts/`                                | Contains two EntityFramework DbContext classes - ReadDbContext and WriteDbContext. |
| Infrastructure | `UserService/src/UserService.Infrastructure/EntityFramework/Encryption/`                              | Contains custom EntityFramework encryption converters and encryption provider. |
| Infrastructure | `UserService/src/UserService.Infrastructure/EntityFramework/Initialization/`                          | Contains DbInitializer class that applies entity framework migrations. |
| Infrastructure | `UserService/src/UserService.Infrastructure/EntityFramework/Migrations/`                              | Contains EntityFramework migrations. |
| Infrastructure | `UserService/src/UserService.Infrastructure/EntityFramework/ModelConfiguration/ReadConfiguration/`    | Contains classes that implement IEntityTypeConfiguration<T> and are containing configuration for the database models. |
| Infrastructure | `UserService/src/UserService.Infrastructure/EntityFramework/ModelConfiguration/WriteConfiguration/`   | Contains classes that implement IEntityTypeConfiguration<T> and are containing configuration for DOMAIN entities and value objects. |
| Infrastructure | `UserService/src/UserService.Infrastructure/EntityFramework/Models/`                                  | Contains classes for the database models that represent the database tables and the overall database schema. |
| Infrastructure | `UserService/src/UserService.Infrastructure/EntityFramework/Options/`                                 | Contains class that is used for the OPTIONS pattern. |
| Infrastructure | `UserService/src/UserService.Infrastructure/EntityFramework/Repositories/`                            | Contains implementations of interfaces for data access operations. |
| Infrastructure | `UserService/src/UserService.Infrastructure/EntityFramework/Services/`                                | Contains implementations of interfaces for services containing business logic. |
| Infrastructure | `UserService/src/UserService.Infrastructure/Exceptions/`                                              | Contains Infrastructure layer specific custom exception types. |
| Infrastructure | `UserService/src/UserService.Infrastructure/Extensions/`                                              | Contains custom extension methods. |
| Infrastructure | `UserService/src/UserService.Infrastructure/Logging/`                                                 | Contains logging decorators and formatters. The purpose of the decorators is to wrap the behaviour of a Command Handler or other part of the application and to enrich it's capabilities with the ability to log information. |
| Infrastructure | `UserService/src/UserService.Infrastructure/Queries/`                                                 | Contains query handlers that work with the queries from the Command Query Responsibility Segregation (CQRS) approach. |
| Infrastructure | `UserService/src/UserService.Infrastructure/UserRegistration/`                                        | Contains UserSynchronizer that synchronizes a user with this service. |
| Presentation   | `UserService/src/UserService.Presentation/Properties/`                                                | Contains the application properties related to port and protocol configuration. |
| Presentation   | `UserService/src/UserService.Presentation/Controllers/`                                               | Contains API controllers handling HTTP requests and responses. |
| Presentation   | `UserService/src/UserService.Presentation/DTOs/`                                                      | Provides Data Transfer Objects (DTOs) for API input and output. |
| Presentation   | `UserService/src/UserService.Presentation/Middlewares/`                                               | Contains custom middlewares. |




