# Topic Article Service Project Structure 

### The project is divided into four main layers:

* **Domain** - Contains the core business logic and rules, including entities (Objects that can be identified by an identifier are defined as Entities), events, exceptions, factories, repositories, and value objects (Objects that are compared by their value and do not have an identifier are known as Value Objects). This layer has no external dependencies on other layers or libraries. It defines interfaces that specify the allowed actions;
* **Application** - This layer orchestrates (manages) the work of the domain layer. It has dependencies on the domain layer;
* **Infrastructure** - Here are the implementations of the interfaces provided by the domain layer, application layer and dependencies on libraries such as RabbitMQ, EntityFramework, etc. It has dependencies on the application layer;
* **Presentation** - This layer is the entry point of the service (application) and is implemented as a web API. Provides the API controllers and data transfer objects (DTOs) for handling HTTP requests and responses;

This structured approach ensures a clear separation of concerns, making the codebase easier to manage and extend. Below is an outline of the project's directory structure:

```html
------------------------------------------------------------------
TopicArticleService
├── src 📦
│   ├── TopicArticleService.Domain 📂
│   │   ├── Entities 📂
│   │   │   ├── _README.txt
│   │   │   ├── AggregateRoot.cs
│   │   │   ├── Article.cs
│   │   │   ├── ArticleComment.cs
│   │   │   ├── Topic.cs
│   │   │   ├── User.cs
----------------------------
│   │   ├── Events 📂
│   │   │   ├── _README.txt 
│   │   │   ├── ArticleCommentAdded.cs 
│   │   │   ├── ArticleCommentChildAdded.cs 
│   │   │   ├── ArticleCommentChildRemoved.cs 
│   │   │   ├── ArticleCommentDislikeAdded.cs 
│   │   │   ├── ArticleCommentDislikeRemoved.cs 
│   │   │   ├── ArticleCommentLikeAdded.cs
│   │   │   ├── ArticleCommentLikeRemoved.cs
│   │   │   ├── ArticleCommentRemoved.cs
│   │   │   ├── ArticleDislikeAdded.cs
│   │   │   ├── ArticleDislikeRemoved.cs
│   │   │   ├── ArticleLikeAdded.cs
│   │   │   ├── ArticleLikeRemoved.cs
│   │   │   ├── IDomainEvent.cs
│   │   │   ├── TopicAdded.cs
│   │   │   ├── TopicChildAdded.cs
│   │   │   ├── UserArticleAdded.cs
│   │   │   ├── UserTopicAdded.cs
----------------------------
│   │   ├── Exceptions 📂
│   │   │   ├── _README.txt 
│   │   │   ├── <<CustomDomainLayerExceptions>>
----------------------------
│   │   ├── Factories 📂
│   │   │   ├── _README.txt 
│   │   │   ├── ArticleCommentFactory.cs
│   │   │   ├── ArticleFactory.cs
│   │   │   ├── TopicFactory.cs
│   │   │   ├── UserFactory.cs
│   │   │   ├── IArticleCommentFactory.cs
│   │   │   ├── IArticleFactory.cs
│   │   │   ├── ITopicFactory.cs
│   │   │   ├── IUserFactory.cs
----------------------------
│   │   ├── Repositories 📂
│   │   │   ├── _README.txt 
│   │   │   ├── IArticleCommentRepository.cs
│   │   │   ├── IArticleRepository.cs
│   │   │   ├── ITopicRepository.cs
│   │   │   ├── IUserRepository.cs
----------------------------
│   │   ├── ValueObjects 📂
│   │   │   ├── _README.txt 
│   │   │   ├── ArticleAuthor.cs 
│   │   │   ├── ArticleCommentDateTime.cs 
│   │   │   ├── ArticleCommentDislike.cs 
│   │   │   ├── ArticleCommentID.cs 
│   │   │   ├── ArticleCommentLike.cs 
│   │   │   ├── ArticleContent.cs 
│   │   │   ├── ArticleDislike.cs
│   │   │   ├── ArticleID.cs
│   │   │   ├── ArticleLike.cs
│   │   │   ├── ArticleLink.cs
│   │   │   ├── ArticleListId.cs
│   │   │   ├── ArticlePrevTitle.cs
│   │   │   ├── ArticlePublishingDate.cs
│   │   │   ├── ArticleTitle.cs
│   │   │   ├── TopicFullName.cs
│   │   │   ├── TopicId.cs
│   │   │   ├── TopicName.cs
│   │   │   ├── UserArticle.cs
│   │   │   ├── UserID.cs
│   │   │   ├── UserTopic.cs
----------------------------
│   ├── TopicArticleService.Application 📂
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
│   │   │   │   ├── AddArticleCommentDislikeHandler.cs 
│   │   │   │   ├── AddArticleCommentHandler.cs 
│   │   │   │   ├── AddArticleCommentLikeHandler.cs 
│   │   │   │   ├── AddArticleDislikeHandler.cs 
│   │   │   │   ├── AddArticleLikeHandler.cs 
│   │   │   │   ├── CreateArticleHandler.cs 
│   │   │   │   ├── ICommandHandler.cs 
│   │   │   │   ├── RegisterUserArticleHandler.cs 
│   │   │   │   ├── RegisterUserHandler.cs
│   │   │   │   ├── RegisterUserTopicHandler.cs
│   │   │   │   ├── RemoveArticleCommentHandler.cs
│   │   |   ├── _README.txt 
│   │   |   ├── AddArticleCommentCommand.cs 
│   │   |   ├── AddArticleCommentDislikeCommand.cs 
│   │   |   ├── AddArticleCommentLikeCommand.cs 
│   │   |   ├── AddArticleDislikeCommand.cs 
│   │   |   ├── AddArticleLikeCommand.cs 
│   │   |   ├── CreateArticleCommand.cs 
│   │   |   ├── ICommand.cs 
│   │   |   ├── RegisterUserArticleCommand.cs 
│   │   |   ├── RegisterUserCommand.cs
│   │   |   ├── RegisterUserTopicCommand.cs
│   │   |   ├── RemoveArticleCommentCommand.cs
----------------------------
│   │   ├── Dtos 📂
│   │   |   ├── PrivateHistoryService 📂
│   │   │   |   ├── CommentedArticleDto.cs
│   │   │   |   ├── DislikedArticleCommentDto.cs
│   │   │   |   ├── DislikedArticleDto.cs
│   │   │   |   ├── LikedArticleCommentDto.cs
│   │   │   |   ├── LikedArticleDto.cs
│   │   │   |   ├── TopicSubscriptionDto.cs
│   │   │   |   ├── ViewedArticleDto.cs
│   │   |   ├── SearchService 📂
│   │   │   |   ├── DocumentDto.cs
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
│   │   |   ├── Handlers 📂
│   │   │   │   ├── _README.txt 
│   │   │   │   ├── IQueryHandler.cs 
│   │   |   ├── _README.txt 
│   │   |   ├── GetArticleCommentsQuery.cs 
│   │   |   ├── GetArticleDislikesQuery.cs 
│   │   |   ├── GetArticleLikesQuery.cs 
│   │   |   ├── GetArticleQuery.cs 
│   │   |   ├── GetArticlesQuery.cs 
│   │   |   ├── GetParentTopicWithChildrenQuery.cs
│   │   |   ├── GetTopicsQuery.cs
│   │   |   ├── GetTopicsRelatedToUserQuery.cs
│   │   |   ├── GetTopicSubscriptionsQuery.cs
│   │   |   ├── GetUserArticlesQuery.cs
│   │   |   ├── GetUsersRelatedToArticleQuery.cs
│   │   |   ├── IQuery.cs
----------------------------
│   │   ├── Services 📂
│   │   │   ├── ReadServices 📂
│   │   │   │   ├── IArticleCommentReadService.cs
│   │   │   │   ├── IArticleReadService.cs
│   │   │   │   ├── ITopicReadService.cs
│   │   │   │   ├── IUserReadService.cs
│   │   │   ├── WriteServices 📂
│   │   │   │   ├── IUserWriteService.cs
│   │   |   ├── _README.txt
----------------------------
│   ├── TopicArticleService.Infrastructure 📂
│   │   ├── AsyncDataServices 📂
│   │   │   ├── MessageBusPublisher.cs
│   │   │   ├── MessageBusSubscriber.cs
│   │   │   ├── MessageDescriptor.cs
----------------------------
│   │   ├── Automapper 📂
│   │   │   ├── Profiles 📂
│   │   │   │   ├── _README.txt
│   │   │   │   ├── ArticleCommentDislikeProfile.cs
│   │   │   │   ├── ArticleCommentLikeProfile.cs
│   │   │   │   ├── ArticleCommentProfile.cs
│   │   │   │   ├── ArticleDislikeProfile.cs
│   │   │   │   ├── ArticleLikeProfile.cs
│   │   │   │   ├── ArticleProfile.cs
│   │   │   │   ├── DocumentProfile.cs
│   │   │   │   ├── TopicProfile.cs
│   │   │   │   ├── UserArticleProfile.cs
│   │   │   │   ├── UserProfile.cs
│   │   │   │   ├── UserTopicProfile.cs
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
│   │   │   │   │   ├── EncryptedArticleCommentDateTimeConverter.cs
│   │   │   │   │   ├── EncryptedArticleCommentValueConverter.cs
│   │   │   │   │   ├── EncryptedDateTimeOffsetConverter.cs
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
│   │   │   │   │   ├── _README.txt
│   │   │   │   │   ├── ArticleCommentConfiguration.cs
│   │   │   │   │   ├── ArticleCommentDislikeConfiguration.cs
│   │   │   │   │   ├── ArticleCommentLikeConfiguration.cs
│   │   │   │   │   ├── ArticleConfiguration.cs
│   │   │   │   │   ├── ArticleDislikeConfiguration.cs
│   │   │   │   │   ├── ArticleLikeConfiguration.cs
│   │   │   │   │   ├── TopicConfiguration.cs
│   │   │   │   │   ├── UserArticleConfiguration.cs
│   │   │   │   │   ├── UserConfiguration.cs
│   │   │   │   │   ├── UserTopicConfiguration.cs
│   │   │   │   ├── WriteConfiguration 📂
│   │   │   │   │   ├── _README.txt
│   │   │   │   │   ├── ArticleCommentConfiguration.cs
│   │   │   │   │   ├── ArticleCommentDislikeConfiguration.cs
│   │   │   │   │   ├── ArticleCommentLikeConfiguration.cs
│   │   │   │   │   ├── ArticleConfiguration.cs
│   │   │   │   │   ├── ArticleDislikeConfiguration.cs
│   │   │   │   │   ├── ArticleLikeConfiguration.cs
│   │   │   │   │   ├── TopicConfiguration.cs
│   │   │   │   │   ├── UserArticleConfiguration.cs
│   │   │   │   │   ├── UserConfiguration.cs
│   │   │   │   │   ├── UserTopicConfiguration.cs
│   │   │   ├── Models 📂
│   │   │   │   ├── _README.txt
│   │   │   │   ├── ArticleCommentDislikeReadModel.cs
│   │   │   │   ├── ArticleCommentLikeReadModel.cs
│   │   │   │   ├── ArticleCommentReadModel.cs
│   │   │   │   ├── ArticleDislikeReadModel.cs
│   │   │   │   ├── ArticleLikeReadModel.cs
│   │   │   │   ├── ArticleReadModel.cs
│   │   │   │   ├── TopicReadModel.cs
│   │   │   │   ├── UserArticleReadModel.cs
│   │   │   │   ├── UserReadModel.cs
│   │   │   │   ├── UserTopicReadModel.cs
│   │   │   ├── Options 📂
│   │   │   │   ├── PostgresOptions.cs
│   │   │   ├── PrepareDatabase 📂
│   │   │   │   ├── DbPrepper.cs
│   │   │   ├── Repositories 📂
│   │   │   │   ├── PostgresArticleCommentRepository.cs
│   │   │   │   ├── PostgresArticleRepository.cs
│   │   │   │   ├── PostgresTopicRepository.cs
│   │   │   │   ├── PostgresUserRepository.cs
│   │   │   ├── Services 📂
│   │   │   │   ├── PostgresArticleCommentReadService.cs
│   │   │   │   ├── PostgresArticleReadService.cs
│   │   │   │   ├── PostgresUserReadService.cs
│   │   │   │   ├── PostgresUserWriteService.cs
│   │   │   │   ├── PostgreTopicReadServices.cs
----------------------------
│   │   ├── EventProcessing 📂
│   │   │   ├── EventProcessor.cs
│   │   │   ├── IEventProcessor.cs
----------------------------
│   │   ├── Exceptions 📂
│   │   │   ├── Interfaces 📂
│   │   │   │   ├── IExceptionToResponseMapper.cs
│   │   │   ├── _README.txt
│   │   │   ├── EmptyMessageDescriptorExchangeException.cs 
│   │   │   ├── EmptyMessageDescriptorMessageException.cs
│   │   │   ├── EmptyMessageDescriptorRoutingKeyException.cs
│   │   │   ├── ExceptionResponse.cs
│   │   │   ├── ExceptionToResponseMapper.cs
│   │   │   ├── InfrastructureException.cs
│   │   │   ├── NullDbContextException.cs
│   │   │   ├── RabbitMqInitializationException.cs
----------------------------
│   │   ├── Extensions 📂
│   │   │   ├── ServiceCollectionExtensions.cs
│   │   │   ├── ConfigurationExtensions.cs
│   │   │   ├── ModelBuilderExtensions.cs
│   │   │   ├── StringExtensions.cs
----------------------------
│   │   ├── Logging 📂
│   │   │   ├── Formatters 📂
│   │   │   │   ├── SerilogJsonFormatter.cs
│   │   │   ├── LoggingCommandHandlerDecorator.cs
----------------------------
│   │   ├── Protos 📂
│   │   │   ├── greeter.proto
----------------------------
│   │   ├── Queries 📂
│   │   │   ├── Handlers 📂
│   │   │   │   ├── _README.txt
│   │   │   │   ├── GetArticleCommentsHandler.cs
│   │   │   │   ├── GetArticleDislikesHandler.cs
│   │   │   │   ├── GetArticleHandler.cs
│   │   │   │   ├── GetArticleLikesHandler.cs
│   │   │   │   ├── GetArticlesHandler.cs
│   │   │   │   ├── GetParentTopicWithChildrenHandler.cs
│   │   │   │   ├── GetTopicsHandler.cs
│   │   │   │   ├── GetTopicsRelatedToUserHandler.cs
│   │   │   │   ├── GetTopicSubscriptionsHandler.cs
│   │   │   │   ├── GetUserArticlesHandler.cs
│   │   │   │   ├── GetUsersRelatedToArticleHandler.cs
----------------------------
│   │   ├── SyncDataServices 📂
│   │   │   ├── Grpc 📂
│   │   │   │   ├── ISearchServiceDataClient.cs
│   │   │   │   ├── SearchServiceDataClient.cs
----------------------------
│   │   ├── UserRegistration 📂
│   │   │   ├── IUserSynchronizer.cs
│   │   │   ├── UserSynchronizer.cs
----------------------------
│   ├── TopicArticleService.Presentation 📂
│   │   ├── Properties 📂
│   │   │   ├── launchSettings.json
│   │   ├── Controllers 📂
│   │   │   ├── ArticlesController.cs
│   │   │   ├── TopicController.cs
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
│   ├── TopicArticleService.Tests.EndToEnd
│   │   ├── Events 📂
│   │   │   ├── ITestEvent.cs
│   │   │   ├── ViewedArticleAdded.cs
│   │   ├── Extensions 📂
│   │   │   ├── ServiceCollectionExtensions.cs
│   │   ├── Factories 📂
│   │   │   ├── DrahtenApplicationFactory.cs
│   │   ├── Services 📂
│   │   │   ├── MessageDescriptor.cs
│   │   │   ├── RabbitMqMessageBusPublisher.cs
│   │   │   ├── RabbitMqMessageBusSubscriber.cs
│   │   ├── Sync 📂
│   │   │   ├── _README.txt
│   │   │   ├── BaseSyncIntegrationTest.cs
│   │   │   ├── RegisterArticleCommentDislikeTests.cs
│   │   │   ├── RegisterArticleCommentLikeTests.cs
│   │   │   ├── RegisterArticleCommentTests.cs
│   │   │   ├── RegisterArticleDislikeTests.cs
│   │   │   ├── RegisterArticleLikeTests.cs
│   │   │   ├── RegisterArticleTests.cs
│   │   │   ├── RegisterUserArticleTests.cs
│   │   │   ├── RegisterUserTests.cs
│   │   │   ├── RegisterUserTopicTests.cs
│   │   │   ├── RemoveArticleCommentTests.cs
----------------------------
│   ├── TopicArticleService.Tests.Integration
│   │   ├── Async 📂
│   │   │   ├── _README.txt
│   │   │   ├── PublishCommentedArticleTests.cs
│   │   │   ├── PublishDislikedArticleCommentTests.cs
│   │   │   ├── PublishDislikedArticleTests.cs
│   │   │   ├── PublishLikedArticleComment.cs
│   │   │   ├── PublishLikedArticleTests.cs
│   │   │   ├── PublishTopicSubscriptionTests.cs
│   │   │   ├── PublishViewedArticleTests.cs
│   │   ├── EventProcessing 📂
│   │   │   ├── EventProcessor.cs
│   │   │   ├── IEventProcessor.cs
│   │   ├── Events 📂
│   │   │   ├── CommentedArticleAdded.cs
│   │   │   ├── DislikedArticleAdded.cs
│   │   │   ├── DislikedArticleCommentAdded.cs
│   │   │   ├── ITestEvent.cs
│   │   │   ├── LikedArticleAdded.cs
│   │   │   ├── LikedArticleCommentAdded.cs
│   │   │   ├── TopicSubscriptionAdded.cs
│   │   │   ├── ViewedArticleAdded.cs
│   │   ├── Extensions 📂
│   │   │   ├── ServiceCollectionExtensions.cs
│   │   ├── Factories 📂
│   │   │   ├── DrahtenApplicationFactory.cs
│   │   ├── Services 📂
│   │   │   ├── ConsoleOutput.cs
│   │   │   ├── MessageDescriptor.cs
│   │   │   ├── RabbitMqMessageBusPublisher.cs
│   │   │   ├── RabbitMqMessageBusSubscriber.cs
----------------------------
│   ├── TopicArticleService.Tests.Unit
│   │   ├── Application 📂
│   │   │   ├── Handlers 📂
│   │   │   │   ├── _README.txt
│   │   │   │   ├── AddArticleCommentDislikeHandlerTests.cs
│   │   │   │   ├── AddArticleCommentHandlerTests.cs
│   │   │   │   ├── AddArticleCommentLikeHandlerTests.cs
│   │   │   │   ├── AddArticleDislikeHandlerTests.cs
│   │   │   │   ├── AddArticleLikeHandlerTests.cs
│   │   │   │   ├── CreateArticleHandlerTests.cs
│   │   │   │   ├── RegisterUserArticleHandlerTests.cs
│   │   │   │   ├── RegisterUserHandlerTests.cs
│   │   │   │   ├── RegisterUserTopicHandlerTests.cs
│   │   │   │   ├── RemoveArticleCommentHandlerTests.cs
│   │   ├── Domain 📂
│   │   │   ├── Entities 📂
│   │   │   │   ├── ArticleCommentTests 📂
│   │   │   │   |   ├── _README.txt
│   │   │   │   |   ├── AddDislike.cs
│   │   │   │   |   ├── AddLike.cs
│   │   │   │   ├── ArticleTests 📂
│   │   │   │   |   ├── _README.txt
│   │   │   │   |   ├── AddComment.cs
│   │   │   │   |   ├── AddDislike.cs
│   │   │   │   |   ├── AddLike.cs
│   │   │   │   |   ├── AddUserArticle.cs
│   │   │   │   |   ├── RemoveComment.cs
│   │   │   │   ├── UserTests 📂
│   │   │   │   |   ├── SubscribeToTopic.cs
│   │   │   ├── Factories 📂
│   │   │   │   ├── _README.txt
│   │   │   │   ├── ArticleCommentFactoryTests.cs
│   │   │   │   ├── ArticleFactoryTests.cs
------------------------------------------------------------------
```

## Directory/File Descriptions

| Layer          | Directory/File                                                                                        | Description                                                   |
|----------------|-------------------------------------------------------------------------------------------------------|---------------------------------------------------------------|
| Domain         | `TopicArticleService/src/TopicArticleService.Domain/Entities/`                                                        | Contains domain entities representing core business concepts. |
| Domain         | `TopicArticleService/src/TopicArticleService.Domain/Events/`                                                          | Includes domain events capturing significant changes or actions. |
| Domain         | `TopicArticleService/src/TopicArticleService.Domain/Exceptions/`                                                      | Houses custom exceptions specific to domain logic.             |
| Domain         | `TopicArticleService/src/TopicArticleService.Domain/Factories/`                                                       | Provides factories for creating domain entities.               |
| Domain         | `TopicArticleService/src/TopicArticleService.Domain/Repositories/`                                                    | Defines interfaces or base classes for data access operations. |
| Domain         | `TopicArticleService/src/TopicArticleService.Domain/ValueObjects/`                                                    | Contains immutable value objects used within the domain.       |
| Application    | `TopicArticleService/src/TopicArticleService.Application/AsyncDataServices/`                                          | Defines interfaces for asynchronous operations. |
| Application    | `TopicArticleService/src/TopicArticleService.Application/Commands/`                                                   | Contains command types and serves as the central location for defining and organizing commands. |
| Application    | `TopicArticleService/src/TopicArticleService.Application/Commands/Dispatcher/`                                        | Contains command dispatcher type and serves as the central component responsible for 
routing commands to their respective command handlers. |
| Application    | `TopicArticleService/src/TopicArticleService.Application/Commands/Handlers/`                                          | Contains command handler types that implement ICommandHandler<T> interface and plays a crucial role in implementing 
Command Query Responsibility Segregation (CQRS). |
| Application    | `TopicArticleService/src/TopicArticleService.Application/Dtos/`                                                       | Contains Data Transfer Objects (DTOs). |
| Application    | `TopicArticleService/src/TopicArticleService.Application/Exceptions/`                                                 | Contains Application layer specific custom exception types. |
| Application    | `TopicArticleService/src/TopicArticleService.Application/Extensions/`                                                 | Contains custom extension methods. |
| Application    | `TopicArticleService/src/TopicArticleService.Application/Queries/`                                                    | Contains query types and serves as the central location for defining and organizing queries. |
| Application    | `TopicArticleService/src/TopicArticleService.Application/Queries/Dispatcher/`                                       | Contains query dispatcher type that implements IQueryDispatcher interface and serves as the central component responsible for 
routing queries to their respective query handlers for execution and data retrieval. |
| Application    | `TopicArticleService/src/TopicArticleService.Application/Queries/Handlers/`                                           | Contains the definition of the IQueryHandler<TQuery, TResult> interface, which serves as a contract for all query handlers 
responsible for executing queries and retrieving data from the application's read-side or queryable data sources. |
| Application    | `TopicArticleService/src/TopicArticleService.Application/Services/`                                                   | Implements application services containing business logic.     |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/AsyncDataServices/`                                       | Defines implementations of interfaces for asynchronous operations. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/Automapper/`                                              | Contains types, that are inheriting the Profile type from AutoMapper library. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/Dtos/`                                                    | Contains Data Transfer Objects (DTOs). |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/EntityFramework/Contexts/`                                | Contains two EntityFramework DbContext classes - ReadDbContext and WriteDbContext. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/EntityFramework/Encryption/`                              | Contains custom EntityFramework encryption converters and encryption provider. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/EntityFramework/Initialization/`                          | Contains DbInitializer class that applies entity framework migrations. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/EntityFramework/Migrations/`                              | Contains EntityFramework migrations. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/EntityFramework/ModelConfiguration/ReadConfiguration/`    | Contains classes that implement IEntityTypeConfiguration<T> and are containing configuration for the database models. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/EntityFramework/ModelConfiguration/WriteConfiguration/`   | Contains classes that implement IEntityTypeConfiguration<T> and are containing configuration for DOMAIN entities and value objects. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/EntityFramework/Models/`                                  | Contains classes for the database models that represent the database tables and the overall database schema. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/EntityFramework/Options/`                                 | Contains class that is used for the OPTIONS pattern. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/EntityFramework/PrepareDatabase/`                         | Contains class that is responsible for preparing and seeding the database with initial data. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/EntityFramework/Repositories/`                            | Contains implementations of interfaces for data access operations. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/EntityFramework/Services/`                                | Contains implementations of interfaces for services containing business logic. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/EventProcessing/`                                         | Contains class that is responsible for processing event's that are received from outside the application. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/Exceptions/`                                              | Contains Infrastructure layer specific custom exception types. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/Extensions/`                                              | Contains custom extension methods. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/Logging/`                                                 | Contains logging decorators and formatters. The purpose of the decorators is to wrap the behaviour of a Command Handler or other part of the application and to enrich it's capabilities with the ability to log information. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/Queries/`                                                 | Contains query handlers that work with the queries from the Command Query Responsibility Segregation (CQRS) approach. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/SyncDataServices/`                                        | Contains services that are related to synchronous operations. |
| Infrastructure | `TopicArticleService/src/TopicArticleService.Infrastructure/UserRegistration/`                                        | Contains UserSynchronizer that synchronizes a user with this service. |
| Presentation   | `TopicArticleService/src/TopicArticleService.Presentation/Properties/`                                                | Contains the application properties related to port and protocol configuration. |
| Presentation   | `TopicArticleService/src/TopicArticleService.Presentation/Controllers/`                                               | Contains API controllers handling HTTP requests and responses. |
| Presentation   | `TopicArticleService/src/TopicArticleService.Presentation/DTOs/`                                                      | Provides Data Transfer Objects (DTOs) for API input and output. |
| Presentation   | `TopicArticleService/src/TopicArticleService.Presentation/Middlewares/`                                               | Contains custom middlewares. |
