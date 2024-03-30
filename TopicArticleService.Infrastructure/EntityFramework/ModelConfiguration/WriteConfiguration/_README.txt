This directory contains classes that implement IEntityTypeConfiguration<T> and are containing configuration for DOMAIN entities and value objects.
-------------------------
This configuration is slightly different than the configuration contained in the ReadConfiguration directory, but the entities and value objects
are mapped to the same database tables and columns. 
This configuration is needed becouse:
* Command Query Responsibility Segregation (CQRS): Becouse of this rule there are two db contexts (for the read and write side). 
    This is the configuration for the write side. This means that entity framework must know how to map the domain entities and value objects 
    to the database and how to map the database values back to domain entities and value objects.
* Separation of concerns: The domain entities and value objects does not need to have the knowledge of entity framework specific concerns like
    the need of having navigation properties or specifig foreign keys for the table connections. Simply they do not need to be aware of the 
    database schema and need to have only domain specific information. If this rule is not applied then the infrastructure leaks into the domain 
    and that is violation of the clean architecture.
-------------------------
*** Relationships ***
-----------------------------------
Hey why are you not using string-based references in Entity Framework?
-------------------------
For example you could use string-based references to define the following relationships in the Article entity, contained in ArticleConfiguration:
builder.HasMany(typeof(UserArticle), "_userArticles");
builder.HasMany(typeof(ArticleLike), "_articleLikes");
builder.HasMany(typeof(ArticleDislike), "_articleDislikes");
-------------------------
But instead you are defining the relationships like this:
-------------------------
builder.HasMany(p => p.UserArticles);
builder.HasMany(p => p.ArticleLikes);
builder.HasMany(p => p.ArticleDislikes);
-------------------------
And in the queries, you are including related entities like this:
-------------------------
Articles.Include(x => x.UserArticles)
        .Include(x => x.ArticleLikes)
        .Include(x => x.ArticleDislikes)
-------------------------
and not like this:
 Articles.Include("_userArticles")
         .Include("_articleLikes")
         .Include("_articleDislikes")
-------------------------
Reasons:
* Encapsulation: Directly accessing private properties using reflection is violating the encapsulation principle. 
    When accessing private properties using reflection, the encapsulation mechanism provided by the language is bypassed. 
    Reflection allows to examine and manipulate the structure and behavior of types at runtime, including accessing private members that are not 
    intended to be accessed from outside the class. 
    Here's how accessing private properties using reflection violates the encapsulation principle:
    - Breaks Information Hiding: Encapsulation relies on the principle of information hiding, which means that the internal details 
        of an object should be hidden from the outside world. By directly accessing private properties using reflection, the internal state of 
        the object is exposed, violating this principle.
    - Undermines Abstraction: Encapsulation promotes abstraction by hiding the implementation details behind a well-defined interface. 
        When accessing private properties using reflection, this abstraction is undermined by directly manipulating the internal state of the 
        object, which can lead to unexpected behavior and coupling between different parts of the code.
    - Fragile Code: Code that relies on reflection to access private members is inherently fragile because it's tightly coupled to the internal 
        implementation details of the class. Any changes to the private members, such as renaming or refactoring, can break the reflection-based 
        code, leading to maintenance issues and potential bugs.
    - Security Risks: Allowing unrestricted access to private members via reflection can pose security risks, especially in scenarios where 
        sensitive data or critical operations are involved. It opens up the possibility of unauthorized access and manipulation of internal state, 
        which can compromise the integrity and security of the application.
* Readability: Using navigation properties makes the code more readable and understandable. 
    It clearly expresses the relationships between entities in the domain model, making the structure of the data easier for comprehention.
* Maintainability: Navigation properties provide a more robust and maintainable way to define relationships between entities. 
    If the structure of the database changes, there is no need of updating string-based references in multiple places throughout the codebase.
* Compile-time Safety: Navigation properties offer compile-time safety, which means that the compiler can catch errors related to typos or incorrect property
    names at compile time rather than at runtime.
* Refactoring Support: Navigation properties provide better support for refactoring tools. If ntities or properties are renamed, 
    tools like Visual Studio's refactoring tools can automatically update references to those entities or properties.
-------------------------
*** Properties ***
-------------------------
Why are you using string-based properties to configure how entity framework will manage the properties of the domain enties and value objects
despite of the encapsulation problems, related to using reflection?
-------------------------
Reasons:
* Preventing bad logic on top of the domain layer: If for example the private fields for the Article entity were not defined like this:
     -------------------------
         private ArticlePrevTitle _prevTitle;
         private ArticleTitle _title;
         private ArticleContent _content;
         private ArticlePublishingDate _publishingDate;
         private ArticleAuthor _author;
         private ArticleLink _link;
         private TopicId _topicId;
    -------------------------
    but like this:
    -------------------------
        public ArticlePrevTitle prevTitle {get; private set; }
        ....
    -------------------------
    there is nothing that prevents someone of creating some king of logic that says: If ArticleTitle is X and ArticleLink is Y do Z.
    In terms of the collections there is no possible way of creating that kind of logic, becouse, yes the objects inside the collections
    can be reached outside the Article entity, but the objects themselfs are encapsulated and there is public interface that 
    hides the internal details.
* Tradeoffs: In most cases there is some kind of tradeoff when choosing one approach instead of another. In the case of the private fields
    the negative aspects related to using reflection that are mentioned above do apply, but in that way the domain enforces the above layers
    to use it right, with good separation of concerns and preventing bad and messy code on top of the domain. 
    So in this case I think that the domain is more important than the possible performance and other issues related to using reflection.
* Domain-Driven Design (DDD): Eric Evans (the author of "Domain-Driven Design: Tackling Complexity in Software.") talks about persisting objects to 
    a database, and here are a few of the points he made about what the DDD approach should do:
    - Present the client with a simple model for obtaining persistent objects (classes) and managing their life cycle;
    - *** The entity class design should communicate design decisions about object access. *** 
    But WAIT I said that if there is public properties with private setters "there is nothing that prevents someone of creating some king of logic 
    that says: If ArticleTitle is X and ArticleLink is Y do Z" and that is true, but someone can still create bad code if he/she so desires I mean
    there is still reflection, so isn't it better to just have the public properties with private setters?? 
    Eric Evans says "The entity class design should communicate design decisions about object access". In this case the design decision
    is clear: The private properties ARE NOT meant to be accessed outside of the class (yes you can still break this with reflection, but then
    you are idiot, I mean the design decision is clear, you are doing this on purpose).
    There is always a way to mess something if you want it bad enough. 
