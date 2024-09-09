# Private History Service Project Structure 

### The project is divided into four main layers:

* **Domain** - Contains the core business logic and rules, including entities (Objects that can be identified by an identifier are defined as Entities), events, exceptions, factories, repositories, and value objects (Objects that are compared by their value and do not have an identifier are known as Value Objects). This layer has no external dependencies on other layers or libraries. It defines interfaces that specify the allowed actions;
* **Application** - This layer orchestrates (manages) the work of the domain layer. It has dependencies on the domain layer;
* **Infrastructure** - Here are the implementations of the interfaces provided by the domain layer, application layer and dependencies on libraries such as RabbitMQ, EntityFramework, etc. It has dependencies on the application layer;
* **Presentation** - This layer is the entry point of the service (application) and is implemented as a web API. Provides the API controllers and data transfer objects (DTOs) for handling HTTP requests and responses;

This structured approach ensures a clear separation of concerns, making the codebase easier to manage and extend. Below is an outline of the project's directory structure:

```html
------------------------------------------------------------------
PrivateHistoryService
├── src 📦
│   ├── PrivateHistoryService.Domain 📂
│   │   ├── Entities 📂
│   │   │   ├── _README.txt
│   │   │   ├── AggregateRoot.cs
│   │   │   ├── User.cs
----------------------------
│   │   ├── Events 📂
│   │   │   ├── _README.txt 
│   │   │   ├── CommentedArticleAdded.cs 
│   │   │   ├── CommentedArticleRemoved.cs 
│   │   │   ├── DislikedArticleAdded.cs 
│   │   │   ├── DislikedArticleCommentAdded.cs 
│   │   │   ├── IDomainEvent.cs 
│   │   │   ├── LikedArticleAdded.cs
│   │   │   ├── LikedArticleCommentAdded.cs
│   │   │   ├── SearchedArticleDataAdded.cs
│   │   │   ├── SearchedArticleDataRemoved.cs
│   │   │   ├── SearchedTopicDataAdded.cs
│   │   │   ├── SearchedTopicDataRemoved.cs
│   │   │   ├── TopicSubscriptionAdded.cs
│   │   │   ├── TopicSubscriptionRemoved.cs
│   │   │   ├── UserRetentionUntilAdded.cs
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
│   │   │   ├── ArticleCommentID.cs 
│   │   │   ├── ArticleID.cs 
│   │   │   ├── CommentedArticle.cs 
│   │   │   ├── DislikedArticle.cs 
│   │   │   ├── DislikedArticleComment.cs 
│   │   │   ├── LikedArticle.cs
│   │   │   ├── LikedArticleComment.cs
│   │   │   ├── SearchedArticleData.cs
│   │   │   ├── SearchedData.cs
│   │   │   ├── SearchedDataAnswer.cs
│   │   │   ├── SearchedDataAnswerContext.cs
│   │   │   ├── SearchedTopicData.cs
│   │   │   ├── TopicID.cs
│   │   │   ├── TopicSubscription.cs
│   │   │   ├── UserID.cs
│   │   │   ├── UserRetentionUntil.cs
│   │   │   ├── ViewedArticle.cs
│   │   │   ├── ViewedUser.cs
----------------------------
│   ├── PrivateHistoryService.Application 📂
│   │   ├── Commands 📂
│   │   │   ├── Dispatcher 📂
│   │   │   │   ├── _README.txt 
│   │   │   │   ├── ICommandDispatcher.cs 
│   │   │   │   ├── InMemoryCommandDispatcher.cs
│   │   │   ├── Handlers 📂
│   │   │   │   ├── _README.txt 
│   │   │   │   ├── AddCommentedArticleHandler.cs 
│   │   │   │   ├── AddDislikedArticleCommentHandler.cs 
│   │   │   │   ├── AddDislikedArticleHandler.cs 
│   │   │   │   ├── AddLikedArticleCommentHandler.cs 
│   │   │   │   ├── AddLikedArticleHandler.cs 
│   │   │   │   ├── AddSearchedArticleDataHandler.cs 
│   │   │   │   ├── AddSearchedTopicDataHandler.cs 
│   │   │   │   ├── AddTopicSubscriptionHandler.cs 
│   │   │   │   ├── AddUserHandler.cs
│   │   │   │   ├── AddViewedArticleHandler.cs
│   │   │   │   ├── AddViewedUserHandler.cs
│   │   │   │   ├── ICommandHandler.cs
│   │   │   │   ├── RemoveCommentedArticleHandler.cs
│   │   │   │   ├── RemoveSearchedArticleDataHandler.cs
│   │   │   │   ├── RemoveSearchedTopicDataHandler.cs
│   │   │   │   ├── RemoveTopicSubscriptionHandler.cs
│   │   │   │   ├── RemoveViewedArticleHandler.cs
│   │   │   │   ├── RemoveViewedUserHandler.cs
│   │   │   │   ├── SetUserRetentionDateTimeHandler.cs
│   │   |   ├── _README.txt 
│   │   |   ├── AddCommentedArticleCommand.cs 
│   │   |   ├── AddDislikedArticleCommand.cs 
│   │   |   ├── AddDislikedArticleCommentCommand.cs 
│   │   |   ├── AddLikedArticleCommand.cs 
│   │   |   ├── AddLikedArticleCommentCommand.cs 
│   │   |   ├── AddSearchedArticleDataCommand.cs 
│   │   |   ├── AddSearchedTopicDataCommand.cs 
│   │   |   ├── AddTopicSubscriptionCommand.cs 
│   │   |   ├── AddUserCommand.cs
│   │   |   ├── AddViewedArticleCommand.cs
│   │   |   ├── AddViewedUserCommand.cs
│   │   |   ├── ICommand.cs
│   │   |   ├── RemoveCommentedArticleCommand.cs
│   │   |   ├── RemoveSearchedArticleDataCommand.cs
│   │   |   ├── RemoveSearchedTopicDataCommand.cs
│   │   |   ├── RemoveTopicSubscriptionCommand.cs
│   │   |   ├── RemoveViewedArticleCommand.cs
│   │   |   ├── RemoveViewedUserCommand.cs
│   │   |   ├── SetUserRetentionDateTimeCommand.cs
----------------------------
│   │   ├── Dtos 📂
│   │   │   ├── _README.txt 
│   │   │   ├── CommentedArticleDto.cs 
│   │   │   ├── DislikedArticleCommentDto.cs 
│   │   │   ├── DislikedArticleDto.cs 
│   │   │   ├── LikedArticleCommentDto.cs 
│   │   │   ├── LikedArticleDto.cs 
│   │   │   ├── SearchedArticleDataDto.cs
│   │   │   ├── SearchedTopicDataDto.cs
│   │   │   ├── TopicSubscriptionDto.cs
│   │   │   ├── ViewedArticleDto.cs
│   │   │   ├── ViewedUserDto.cs
----------------------------
│   │   ├── Exceptions 📂
│   │   │   ├── _README.txt 
│   │   │   ├── <<CustomApplicationLayerExceptions>>
----------------------------
│   │   ├── Extensions 📂
│   │   │   ├── DateTimeOffsetExtensions.cs
│   │   │   ├── ServiceCollectionExtensions.cs
----------------------------
│   │   ├── Queries 📂
│   │   │   ├── Dispatcher 📂
│   │   │   │   ├── InMemoryQueryDispatcher.cs 
│   │   │   │   ├── IQueryDispatcher.cs
│   │   |   ├── Handlers 📂
│   │   │   │   ├── IQueryHandler.cs 
│   │   |   ├── _README.txt 
│   │   |   ├── GetArticleCommentDislikesQuery.cs 
│   │   |   ├── GetArticleCommentLikesQuery.cs 
│   │   |   ├── GetArticleDislikesQuery.cs 
│   │   |   ├── GetArticleLikesQuery.cs 
│   │   |   ├── GetCommentedArticlesQuery.cs 
│   │   |   ├── GetSearchedArticlesDataQuery.cs
│   │   |   ├── GetSearchedTopicsDataQuery.cs
│   │   |   ├── GetTopicSubscriptionsQuery.cs
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
│   │   │   ├── WriteServices 📂
│   │   │   │   ├── ICommentedArticleWriteService.cs
│   │   │   │   ├── IDislikedArticleCommentWriteService.cs
│   │   │   │   ├── IDislikedArticleWriteService.cs
│   │   │   │   ├── ILikedArticleCommentWriteService.cs
│   │   │   │   ├── ILikedArticleWriteService.cs
│   │   │   │   ├── ISearchedArticleDataWriteService.cs
│   │   │   │   ├── ITopicSubscriptionWriteService.cs
│   │   │   │   ├── IUserWriteService.cs
│   │   │   │   ├── IViewedArticleWriteService.cs
│   │   |   ├── _README.txt
----------------------------
│   ├── PrivateHistoryService.Infrastructure 📂
│   │   ├── AsyncDataServices 📂
│   │   │   ├── MessageBusSubscriber.cs
----------------------------
│   │   ├── Automapper 📂
│   │   │   ├── Profiles 📂
│   │   │   │   ├── _README.txt
│   │   │   │   ├── CommentedArticleProfile.cs
│   │   │   │   ├── DislikedArticleCommentProfile.cs
│   │   │   │   ├── DislikedArticleProfile.cs
│   │   │   │   ├── LikedArticleCommentProfile.cs
│   │   │   │   ├── LikedArticleProfile.cs
│   │   │   │   ├── SearchedArticleDataProfile.cs
│   │   │   │   ├── SearchedTopicDataProfile.cs
│   │   │   │   ├── TopicSubscriptionProfile.cs
│   │   │   │   ├── ViewedArticleProfile.cs
│   │   │   │   ├── ViewedUserProfile.cs
----------------------------
│   │   ├── Dtos 📂
│   │   │   ├── MessageBusEventDto.cs
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
│   │   │   │   │   ├── EncryptedSearchedDataAnswerContextConverter.cs
│   │   │   │   │   ├── EncryptedSearchedDataAnswerConverter.cs
│   │   │   │   │   ├── EncryptedSearchedDataConverter.cs
│   │   │   │   │   ├── EncryptedStringConverter.cs
│   │   │   │   │   ├── EncryptedUserRetentionUntilConverter.cs
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
│   │   │   │   │   ├── DislikedArticleCommentConfiguration.cs
│   │   │   │   │   ├── DislikedArticleConfiguration.cs
│   │   │   │   │   ├── LikedArticleCommentConfiguration.cs
│   │   │   │   │   ├── LikedArticleConfiguration.cs
│   │   │   │   │   ├── SearchedArticleDataConfiguration.cs
│   │   │   │   │   ├── SearchedTopicDataConfiguration.cs
│   │   │   │   │   ├── TopicSubscriptionConfiguration.cs
│   │   │   │   │   ├── UserConfiguration.cs
│   │   │   │   │   ├── ViewedArticleConfiguration.cs
│   │   │   │   │   ├── ViewedUserConfiguration.cs
│   │   │   │   ├── WriteConfiguration 📂
│   │   │   │   │   ├── CommentedArticleConfiguration.cs
│   │   │   │   │   ├── DislikedArticleCommentConfiguration.cs
│   │   │   │   │   ├── DislikedArticleConfiguration.cs
│   │   │   │   │   ├── LikedArticleCommentConfiguration.cs
│   │   │   │   │   ├── LikedArticleConfiguration.cs
│   │   │   │   │   ├── SearchedArticleDataConfiguration.cs
│   │   │   │   │   ├── SearchedTopicDataConfiguration.cs
│   │   │   │   │   ├── TopicSubscriptionConfiguration.cs
│   │   │   │   │   ├── UserConfiguration.cs
│   │   │   │   │   ├── ViewedArticleConfiguration.cs
│   │   │   │   │   ├── ViewedUserConfiguration.cs
│   │   │   ├── Models 📂
│   │   │   │   ├── CommentedArticleReadModel.cs
│   │   │   │   ├── DislikedArticleCommentReadModel.cs
│   │   │   │   ├── DislikedArticleReadModel.cs
│   │   │   │   ├── LikedArticleCommentReadModel.cs
│   │   │   │   ├── LikedArticleReadModel.cs
│   │   │   │   ├── SearchedArticleDataReadModel.cs
│   │   │   │   ├── SearchedTopicDataReadModel.cs
│   │   │   │   ├── TopicSubscriptionReadModel.cs
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
│   │   │   │   ├── WriteServices 📂
│   │   │   │   |   ├── PostgresCommentedArticleWriteService.cs
│   │   │   │   |   ├── PostgresDislikedArticleCommentWriteService.cs
│   │   │   │   |   ├── PostgresDislikedArticleWriteService.cs
│   │   │   │   |   ├── PostgresLikedArticleCommentWriteService.cs
│   │   │   │   |   ├── PostgresLikedArticleWriteService.cs
│   │   │   │   |   ├── PostgresSearchedArticleDataWriteService.cs
│   │   │   │   |   ├── PostgresTopicSubscriptionWriteService.cs
│   │   │   │   |   ├── PostgresUserWriteService.cs
│   │   │   │   |   ├── PostgresViewedArticleWriteService.cs
----------------------------
│   │   ├── EventProcessing 📂
│   │   │   ├── EventProcessor.cs
│   │   │   ├── IEventProcessor.cs
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
│   │   │   │   ├── GetArticleCommentDislikesHandler.cs
│   │   │   │   ├── GetArticleCommentLikesHandler.cs
│   │   │   │   ├── GetArticleDislikesHandler.cs
│   │   │   │   ├── GetArticleLikesHandler.cs
│   │   │   │   ├── GetCommentedArticlesHandler.cs
│   │   │   │   ├── GetSearchedArticlesDataHandler.cs
│   │   │   │   ├── GetSearchedTopicsDataHandler.cs
│   │   │   │   ├── GetTopicSubscriptionsHandler.cs
│   │   │   │   ├── GetViewedArticlesHandler.cs
│   │   │   │   ├── GeViewedUsersHandler.cs
----------------------------
│   │   ├── Schedulers 📂
│   │   │   ├── RetentionJob.cs
----------------------------
│   ├── PrivateHistoryService.Presentation 📂
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
│   ├── PrivateHistoryService.Tests.EndToEnd
│   │   ├── Extensions 📂
│   │   │   ├── ServiceCollectionExtensions.cs
│   │   ├── Factories 📂
│   │   │   ├── PrivateHistoryServiceApplicationFactory.cs
│   │   ├── Sync 📂
│   │   │   ├── AddCommentedArticleTests.cs
│   │   │   ├── AddDislikedArticleCommentTests.cs
│   │   │   ├── AddDislikedArticleTests.cs
│   │   │   ├── AddLikedArticleCommentTests.cs
│   │   │   ├── AddLikedArticleTests.cs
│   │   │   ├── AddSearchedArticleDataTests.cs
│   │   │   ├── AddSearchedTopicDataTests.cs
│   │   │   ├── AddTopicSubscriptionTests.cs
│   │   │   ├── AddViewedArticleTests.cs
│   │   │   ├── AddViewedUserTests.cs
│   │   │   ├── BaseSyncIntegrationTest.cs
│   │   │   ├── RegisterUserTests.cs
│   │   │   ├── RemoveCommentedArticleTests.cs
│   │   │   ├── RemoveSearchedArticleDataTests.cs
│   │   │   ├── RemoveSearchedTopicDataTests.cs
│   │   │   ├── RemoveTopicSubscriptionTests.cs
│   │   │   ├── RemoveViewedArticleTests.cs
│   │   │   ├── RemoveViewedUserTests.cs
----------------------------
│   ├── PrivateHistoryService.Tests.Unit
│   │   ├── Application 📂
│   │   │   ├── Handlers 📂
│   │   │   │   ├── AddCommentedArticleHandlerTests.cs
│   │   │   │   ├── AddDislikedArticleCommentHandlerTests.cs
│   │   │   │   ├── AddDislikedArticleHandlerTests.cs
│   │   │   │   ├── AddLikedArticleCommentHandlerTests.cs
│   │   │   │   ├── AddLikedArticleHandlerTests.cs
│   │   │   │   ├── AddSearchedArticleDataHandlerTests.cs
│   │   │   │   ├── AddSearchedTopicDataHandlerTests.cs
│   │   │   │   ├── AddTopicSubscriptionHandlerTests.cs
│   │   │   │   ├── AddViewedArticleHandlerTests.cs
│   │   │   │   ├── AddViewedUserHandlerTests.cs
│   │   │   │   ├── RemoveCommentedArticleHandlerTests.cs
│   │   │   │   ├── RemoveSearchedArticleDataHandlerTests.cs
│   │   │   │   ├── RemoveSearchedTopicDataHandlerTests.cs
│   │   │   │   ├── RemoveTopicSubscriptionHandlerTests.cs
│   │   │   │   ├── RemoveViewedArticleHandlerTests.cs
│   │   │   │   ├── RemoveViewedUserHandlerTests.cs
│   │   ├── Domain 📂
│   │   │   ├── Entities 📂
│   │   │   │   ├── UserTests 📂
│   │   │   │   |   ├── AddCommentedArticle.cs
│   │   │   │   |   ├── AddDislikedArticle.cs
│   │   │   │   |   ├── AddDislikedArticleComment.cs
│   │   │   │   |   ├── AddLikedArticle.cs
│   │   │   │   |   ├── AddLikedArticleComment.cs
│   │   │   │   |   ├── AddSearchedArticleData.cs
│   │   │   │   |   ├── AddSearchedTopicData.cs
│   │   │   │   |   ├── AddTopicSubscription.cs
│   │   │   │   |   ├── AddTopicSubscription.cs
│   │   │   │   |   ├── AddViewedArticle.cs
│   │   │   │   |   ├── AddViewedUser.cs
│   │   │   │   |   ├── RemoveCommentedArticle.cs
│   │   │   │   |   ├── RemovedSearchedArticleData.cs
│   │   │   │   |   ├── RemoveSearchedTopicData.cs
│   │   │   │   |   ├── RemoveTopicSubscription.cs
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
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/AsyncDataServices/`                                       | Defines implementations of interfaces for asynchronous operations. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/Automapper/`                                              | Contains types, that are inheriting the Profile type from AutoMapper library. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/Dtos/`                                                    | Contains Data Transfer Objects (DTOs). |
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
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/EventProcessing/`                                         | Contains class that is responsible for processing event's that are received from outside the application. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/Exceptions/`                                              | Contains Infrastructure layer specific custom exception types. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/Extensions/`                                              | Contains custom extension methods. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/Logging/`                                                 | Contains logging decorators and formatters. The purpose of the decorators is to wrap the behaviour of a Command Handler or other part of the application and to enrich it's capabilities with the ability to log information. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/Queries/`                                                 | Contains query handlers that work with the queries from the Command Query Responsibility Segregation (CQRS) approach. |
| Infrastructure | `PrivateHistoryService/src/PrivateHistoryService.Infrastructure/Schedulers/`                                              | Contains classes that represent schedulers for various activities. |
| Presentation   | `PrivateHistoryService/src/PrivateHistoryService.Presentation/Properties/`                                                | Contains the application properties related to port and protocol configuration. |
| Presentation   | `PrivateHistoryService/src/PrivateHistoryService.Presentation/Controllers/`                                               | Contains API controllers handling HTTP requests and responses. |
| Presentation   | `PrivateHistoryService/src/PrivateHistoryService.Presentation/DTOs/`                                                      | Provides Data Transfer Objects (DTOs) for API input and output. |
| Presentation   | `PrivateHistoryService/src/PrivateHistoryService.Presentation/Middlewares/`                                               | Contains custom middlewares. |
