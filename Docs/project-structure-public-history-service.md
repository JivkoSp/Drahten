# Public History Service Project Structure 

### The project is divided into four main layers:

* **Domain** - Contains the core business logic and rules, including entities (Objects that can be identified by an identifier are defined as Entities), events, exceptions, factories, repositories, and value objects (Objects that are compared by their value and do not have an identifier are known as Value Objects). This layer has no external dependencies on other layers or libraries. It defines interfaces that specify the allowed actions;
* **Application** - This layer orchestrates (manages) the work of the domain layer. It has dependencies on the domain layer;
* **Infrastructure** - Here are the implementations of the interfaces provided by the domain layer, application layer and dependencies on libraries such as RabbitMQ, EntityFramework, etc. It has dependencies on the application layer;
* **Presentation** - This layer is the entry point of the service (application) and is implemented as a web API. Provides the API controllers and data transfer objects (DTOs) for handling HTTP requests and responses;

This structured approach ensures a clear separation of concerns, making the codebase easier to manage and extend. Below is an outline of the project's directory structure:

```html
------------------------------------------------------------------
PublicHistoryService
├── src 📦
│   ├── PublicHistoryService.Domain 📂
│   │   ├── Entities 📂
│   │   │   ├── _README.txt
│   │   │   ├── AggregateRoot.cs
│   │   │   ├── User.cs
----------------------------
│   │   ├── Events 📂
│   │   │   ├── _README.txt 
│   │   │   ├── CommentedArticleAdded.cs 
│   │   │   ├── CommentedArticleRemoved.cs 
│   │   │   ├── IDomainEvent.cs 
│   │   │   ├── SearchedArticleDataAdded.cs
│   │   │   ├── SearchedArticleDataRemoved.cs
│   │   │   ├── SearchedTopicDataAdded.cs
│   │   │   ├── SearchedTopicDataRemoved.cs
│   │   │   ├── ViewedArticleAdded.cs
│   │   │   ├── ViewedArticleRemoved.cs
│   │   │   ├── ViewedUserAdded.cs
│   │   │   ├── ViewedUserRemoved.cs
----------------------------
│   │   ├── Exceptions 📂
│   │   │   ├── _README.txt 
│   │   │   ├── <<CustomDomainLayerExceptions>>
----------------------------
│   │   ├── Factories 📂
│   │   │   ├── Interfaces 📂
│   │   │   |   ├── IUserFactory.cs
│   │   │   ├── _README.txt 
│   │   │   ├── UserFactory.cs
----------------------------
│   │   ├── Repositories 📂
│   │   │   ├── _README.txt 
│   │   │   ├── IUserRepository.cs
----------------------------
│   │   ├── ValueObjects 📂
│   │   │   ├── _README.txt 
│   │   │   ├── ArticleComment.cs 
│   │   │   ├── ArticleID.cs 
│   │   │   ├── CommentedArticle.cs 
│   │   │   ├── SearchedArticleData.cs
│   │   │   ├── SearchedData.cs
│   │   │   ├── SearchedTopicData.cs
│   │   │   ├── TopicID.cs
│   │   │   ├── UserID.cs
│   │   │   ├── ViewedArticle.cs
│   │   │   ├── ViewedUser.cs
----------------------------
│   ├── PublicHistoryService.Application 📂
│   │   ├── Commands 📂
│   │   │   ├── Dispatcher 📂
│   │   │   │   ├── ICommandDispatcher.cs 
│   │   │   │   ├── InMemoryCommandDispatcher.cs
│   │   │   ├── Handlers 📂
│   │   │   │   ├── AddCommentedArticleHandler.cs 
│   │   │   │   ├── AddSearchedArticleDataHandler.cs 
│   │   │   │   ├── AddSearchedTopicDataHandler.cs 
│   │   │   │   ├── AddUserHandler.cs
│   │   │   │   ├── AddViewedArticleHandler.cs
│   │   │   │   ├── AddViewedUserHandler.cs
│   │   │   │   ├── ICommandHandler.cs
│   │   │   │   ├── RemoveCommentedArticleHandler.cs
│   │   │   │   ├── RemoveSearchedArticleDataHandler.cs
│   │   │   │   ├── RemoveSearchedTopicDataHandler.cs
│   │   │   │   ├── RemoveViewedArticleHandler.cs
│   │   │   │   ├── RemoveViewedUserHandler.cs
│   │   |   ├── _README.txt 
│   │   |   ├── AddCommentedArticleCommand.cs 
│   │   |   ├── AddSearchedArticleDataCommand.cs 
│   │   |   ├── AddSearchedTopicDataCommand.cs 
│   │   |   ├── AddUserCommand.cs
│   │   |   ├── AddViewedArticleCommand.cs
│   │   |   ├── AddViewedUserCommand.cs
│   │   |   ├── ICommand.cs
│   │   |   ├── RemoveCommentedArticleCommand.cs
│   │   |   ├── RemoveSearchedArticleDataCommand.cs
│   │   |   ├── RemoveSearchedTopicDataCommand.cs
│   │   |   ├── RemoveViewedArticleCommand.cs
│   │   |   ├── RemoveViewedUserCommand.cs
----------------------------
│   │   ├── Dtos 📂
│   │   │   ├── CommentedArticleDto.cs 
│   │   │   ├── SearchedArticleDataDto.cs
│   │   │   ├── SearchedTopicDataDto.cs
│   │   │   ├── ViewedArticleDto.cs
│   │   │   ├── ViewedUserDto.cs
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
│   │   │   │   ├── InMemoryQueryDispatcher.cs 
│   │   │   │   ├── IQueryDispatcher.cs
│   │   |   ├── Handlers 📂
│   │   │   │   ├── IQueryHandler.cs 
│   │   |   ├── GetCommentedArticlesQuery.cs 
│   │   |   ├── GetSearchedArticlesDataQuery.cs
│   │   |   ├── GetSearchedTopicsDataQuery.cs
│   │   |   ├── GetViewedArticlesQuery.cs
│   │   |   ├── GeViewedUsersQuery.cs
│   │   |   ├── IQuery.cs
----------------------------
│   │   ├── Services 📂
│   │   │   ├── ReadServices 📂
│   │   │   │   ├── ICommentedArticleReadService.cs
│   │   │   │   ├── ISearchedArticleDataReadService.cs
│   │   │   │   ├── ISearchedTopicDataReadService.cs
│   │   │   │   ├── IUserReadService.cs
│   │   │   │   ├── IViewedArticleReadService.cs
│   │   │   │   ├── IViewedUserReadService.cs
----------------------------
│   ├── PublicHistoryService.Infrastructure 📂
│   │   ├── Automapper 📂
│   │   │   ├── Profiles 📂
│   │   │   │   ├── CommentedArticleProfile.cs
│   │   │   │   ├── SearchedArticleDataProfile.cs
│   │   │   │   ├── SearchedTopicDataProfile.cs
│   │   │   │   ├── ViewedArticleProfile.cs
│   │   │   │   ├── ViewedUserProfile.cs
----------------------------
│   │   ├── EntityFramework 📂
│   │   │   ├── Contexts 📂
│   │   │   │   ├── _README.txt
│   │   │   │   ├── ReadDbContext.cs
│   │   │   │   ├── WriteDbContext.cs
│   │   │   ├── Encryption 📂
│   │   │   │   ├── EncryptionConverters 📂
│   │   │   │   │   ├── EncryptedArticleCommentConverter.cs
│   │   │   │   │   ├── EncryptedDateTimeOffsetConverter.cs
│   │   │   │   │   ├── EncryptedSearchedDataConverter.cs
│   │   │   │   │   ├── EncryptedStringConverter.cs
│   │   │   │   ├── EncryptionProvider 📂
│   │   │   │   │   ├── IEncryptionProvider.cs
│   │   │   │   │   ├── EncryptionProvider.cs
│   │   │   ├── Initialization 📂
│   │   │   │   ├── _README.txt
│   │   │   │   ├── DbInitializer.cs
│   │   │   ├── Migrations 📂
│   │   │   │   ├── <<EntityFramework database migrations>>
│   │   │   ├── ModelConfiguration 📂
│   │   │   │   ├── ReadConfiguration 📂
│   │   │   │   │   ├── CommentedArticleConfiguration.cs
│   │   │   │   │   ├── SearchedArticleDataConfiguration.cs
│   │   │   │   │   ├── SearchedTopicDataConfiguration.cs
│   │   │   │   │   ├── UserConfiguration.cs
│   │   │   │   │   ├── ViewedArticleConfiguration.cs
│   │   │   │   │   ├── ViewedUserConfiguration.cs
│   │   │   │   ├── WriteConfiguration 📂
│   │   │   │   │   ├── CommentedArticleConfiguration.cs
│   │   │   │   │   ├── SearchedArticleDataConfiguration.cs
│   │   │   │   │   ├── SearchedTopicDataConfiguration.cs
│   │   │   │   │   ├── UserConfiguration.cs
│   │   │   │   │   ├── ViewedArticleConfiguration.cs
│   │   │   │   │   ├── ViewedUserConfiguration.cs
│   │   │   ├── Models 📂
│   │   │   │   ├── CommentedArticleReadModel.cs
│   │   │   │   ├── SearchedArticleDataReadModel.cs
│   │   │   │   ├── SearchedTopicDataReadModel.cs
│   │   │   │   ├── UserReadModel.cs
│   │   │   │   ├── ViewedArticleReadModel.cs
│   │   │   │   ├── ViewedUserReadModel.cs
│   │   │   ├── Options 📂
│   │   │   │   ├── PostgresOptions.cs
│   │   │   ├── Repositories 📂
│   │   │   │   ├── PostgresUserRepository.cs
│   │   │   ├── Services 📂
│   │   │   │   ├── ReadServices 📂
│   │   │   │   |   ├── PostgresCommentedArticleReadService.cs
│   │   │   │   |   ├── PostgresSearchedArticleDataReadService.cs
│   │   │   │   |   ├── PostgresSearchedTopicDataReadService.cs
│   │   │   │   |   ├── PostgresUserReadService.cs
│   │   │   │   |   ├── PostgresViewedArticleReadService.cs
│   │   │   │   |   ├── PostgresViewedUserReadService.cs
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
│   │   │   ├── ConfigurationExtensions.cs
│   │   │   ├── ServiceCollectionExtensions.cs
----------------------------
│   │   ├── Logging 📂
│   │   │   ├── Formatters 📂
│   │   │   │   ├── SerilogJsonFormatter.cs
│   │   │   ├── LoggingCommandHandlerDecorator.cs
----------------------------
│   │   ├── Queries 📂
│   │   │   ├── Handlers 📂
│   │   │   │   ├── GetCommentedArticlesHandler.cs
│   │   │   │   ├── GetSearchedArticlesDataHandler.cs
│   │   │   │   ├── GetSearchedTopicsDataHandler.cs
│   │   │   │   ├── GetViewedArticlesHandler.cs
│   │   │   │   ├── GeViewedUsersHandler.cs
----------------------------
│   ├── PublicHistoryService.Presentation 📂
│   │   ├── Properties 📂
│   │   │   ├── launchSettings.json
│   │   ├── Controllers 📂
│   │   │   ├── UserController.cs
│   │   ├── Dtos 📂
│   │   │   ├── ResponseDto.cs
│   │   ├── Middlewares 📂
│   │   │   ├── ErrorHandlerMiddleware.cs
│   │   │   ├── RateLimitingMiddleware.cs
│   │   ├── appsettings.json
│   │   ├── Program.cs
----------------------------
├── tests 🧪
│   ├── PublicHistoryService.Tests.EndToEnd
│   │   ├── Extensions 📂
│   │   │   ├── ServiceCollectionExtensions.cs
│   │   ├── Factories 📂
│   │   │   ├── PrivateHistoryServiceApplicationFactory.cs
│   │   ├── Sync 📂
│   │   │   ├── AddCommentedArticleTests.cs
│   │   │   ├── AddSearchedArticleDataTests.cs
│   │   │   ├── AddSearchedTopicDataTests.cs
│   │   │   ├── AddViewedArticleTests.cs
│   │   │   ├── AddViewedUserTests.cs
│   │   │   ├── BaseSyncIntegrationTest.cs
│   │   │   ├── RegisterUserTests.cs
│   │   │   ├── RemoveCommentedArticleTests.cs
│   │   │   ├── RemoveSearchedArticleDataTests.cs
│   │   │   ├── RemoveSearchedTopicDataTests.cs
│   │   │   ├── RemoveViewedArticleTests.cs
│   │   │   ├── RemoveViewedUserTests.cs
----------------------------
│   ├── PublicHistoryService.Tests.Unit
│   │   ├── Application 📂
│   │   │   ├── Handlers 📂
│   │   │   │   ├── AddCommentedArticleHandlerTests.cs
│   │   │   │   ├── AddSearchedArticleDataHandlerTests.cs
│   │   │   │   ├── AddSearchedTopicDataHandlerTests.cs
│   │   │   │   ├── AddViewedArticleHandlerTests.cs
│   │   │   │   ├── AddViewedUserHandlerTests.cs
│   │   │   │   ├── RemoveCommentedArticleHandlerTests.cs
│   │   │   │   ├── RemoveSearchedArticleDataHandlerTests.cs
│   │   │   │   ├── RemoveSearchedTopicDataHandlerTests.cs
│   │   │   │   ├── RemoveViewedArticleHandlerTests.cs
│   │   │   │   ├── RemoveViewedUserHandlerTests.cs
│   │   ├── Domain 📂
│   │   │   ├── Entities 📂
│   │   │   │   ├── UserTests 📂
│   │   │   │   |   ├── AddCommentedArticle.cs
│   │   │   │   |   ├── AddSearchedArticleData.cs
│   │   │   │   |   ├── AddSearchedTopicData.cs
│   │   │   │   |   ├── AddViewedArticle.cs
│   │   │   │   |   ├── AddViewedUser.cs
│   │   │   │   |   ├── RemoveCommentedArticle.cs
│   │   │   │   |   ├── RemovedSearchedArticleData.cs
│   │   │   │   |   ├── RemoveSearchedTopicData.cs
│   │   │   │   |   ├── RemoveViewedArticle.cs
│   │   │   │   |   ├── RemoveViewedUser.cs
│   │   │   ├── Factories 📂
│   │   │   │   ├── UserFactoryTests.cs
------------------------------------------------------------------
```

## Directory/File Descriptions

| Layer          | Directory/File                                                                                                        | Description                                                   |
|----------------|-----------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------|
| Domain         | `PrivateHistoryService/src/PrivateHistoryService.Domain/Entities/`                                                        | Contains domain entities representing core business concepts. |
| Domain         | `PrivateHistoryService/src/PrivateHistoryService.Domain/Events/`                                                          | Includes domain events capturing significant changes or actions. |
| Domain         | `PrivateHistoryService/src/PrivateHistoryService.Domain/Exceptions/`                                                      | Houses custom exceptions specific to domain logic.             |
| Domain         | `PrivateHistoryService/src/PrivateHistoryService.Domain/Factories/`                                                       | Provides factories for creating domain entities.               |
| Domain         | `PrivateHistoryService/src/PrivateHistoryService.Domain/Repositories/`                                                    | Defines interfaces or base classes for data access operations. |
| Domain         | `PrivateHistoryService/src/PrivateHistoryService.Domain/ValueObjects/`                                                    | Contains immutable value objects used within the domain.       |
| Application    | `PrivateHistoryService/src/PrivateHistoryService.Application/Commands/`                                                   | Contains command types and serves as the central location for defining and organizing commands. |
| Application    | `PrivateHistoryService/src/PrivateHistoryService.Application/Commands/Dispatcher/`                                        | Contains command dispatcher type and serves as the central component responsible for 
routing commands to their respective command handlers. |
| Application    | `PrivateHistoryService/src/PrivateHistoryService.Application/Commands/Handlers/`                                          | Contains command handler types that implement ICommandHandler<T> interface and plays a crucial role in implementing 
Command Query Responsibility Segregation (CQRS). |
| Application    | `PrivateHistoryService/src/PrivateHistoryService.Application/Dtos/`                                                       | Contains Data Transfer Objects (DTOs). |
| Application    | `PrivateHistoryService/src/PrivateHistoryService.Application/Exceptions/`                                                 | Contains Application layer specific custom exception types. |
| Application    | `PrivateHistoryService/src/PrivateHistoryService.Application/Extensions/`                                                 | Contains custom extension methods. |
| Application    | `PrivateHistoryService/src/PrivateHistoryService.Application/Queries/`                                                    | Contains query types and serves as the central location for defining and organizing queries. |
| Application    | `PrivateHistoryService/src/PrivateHistoryService.Application/Queries/Dispatcher/`                                         | Contains query dispatcher type that implements IQueryDispatcher interface and serves as the central component responsible for 
routing queries to their respective query handlers for execution and data retrieval. |
| Application    | `PrivateHistoryService/src/PrivateHistoryService.Application/Queries/Handlers/`                                           | Contains the definition of the IQueryHandler<TQuery, TResult> interface, which serves as a contract for all query handlers 
responsible for executing queries and retrieving data from the application's read-side or queryable data sources. |
| Application    | `PrivateHistoryService/src/PrivateHistoryService.Application/Services/`                                                   | Implements application services containing business logic.     |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/Automapper/`                                              | Contains types, that are inheriting the Profile type from AutoMapper library. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/EntityFramework/Contexts/`                                | Contains two EntityFramework DbContext classes - ReadDbContext and WriteDbContext. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/EntityFramework/Encryption/`                              | Contains custom EntityFramework encryption converters and encryption provider. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/EntityFramework/Initialization/`                          | Contains DbInitializer class that applies entity framework migrations. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/EntityFramework/Migrations/`                              | Contains EntityFramework migrations. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/EntityFramework/ModelConfiguration/ReadConfiguration/`    | Contains classes that implement IEntityTypeConfiguration<T> and are containing configuration for the database models. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/EntityFramework/ModelConfiguration/WriteConfiguration/`   | Contains classes that implement IEntityTypeConfiguration<T> and are containing configuration for DOMAIN entities and value objects. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/EntityFramework/Models/`                                  | Contains classes for the database models that represent the database tables and the overall database schema. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/EntityFramework/Options/`                                 | Contains class that is used for the OPTIONS pattern. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/EntityFramework/Repositories/`                            | Contains implementations of interfaces for data access operations. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/EntityFramework/Services/`                                | Contains implementations of interfaces for services containing business logic. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/Exceptions/`                                              | Contains Infrastructure layer specific custom exception types. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/Extensions/`                                              | Contains custom extension methods. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/Logging/`                                                 | Contains logging decorators and formatters. The purpose of the decorators is to wrap the behaviour of a Command Handler or other part of the application and to enrich it's capabilities with the ability to log information. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/Queries/`                                                 | Contains query handlers that work with the queries from the Command Query Responsibility Segregation (CQRS) approach. |
| Presentation   | `PrivateHistoryService/src/PrivateHistoryService.Presentation/Properties/`                                                | Contains the application properties related to port and protocol configuration. |
| Presentation   | `PrivateHistoryService/src/PrivateHistoryService.Presentation/Controllers/`                                               | Contains API controllers handling HTTP requests and responses. |
| Presentation   | `PrivateHistoryService/src/PrivateHistoryService.Presentation/DTOs/`                                                      | Provides Data Transfer Objects (DTOs) for API input and output. |
| Presentation   | `PrivateHistoryService/src/PrivateHistoryService.Presentation/Middlewares/`                                               | Contains custom middlewares. |
