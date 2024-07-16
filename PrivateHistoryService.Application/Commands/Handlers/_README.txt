This directory contains command handler types that implement ICommandHandler<T> interface and plays a crucial role in implementing 
Command Query Responsibility Segregation (CQRS). 
The command handlers are responsible for executing commands and orchestrating the corresponding actions within the application's domain.
-------------------------
What is a Command Handler?
In the context of CQRS, a command handler is a component responsible for processing and executing commands received from the command side of 
the application. 
Command handlers encapsulate the logic necessary to interpret, validate, and execute commands, ensuring that the intended actions are performed 
within the domain while adhering to business rules and constraints.
-------------------------
Characteristics of a Command Handler:
* Execution Logic: Command handlers contain the logic required to interpret and execute commands, including validation, authorization and 
	coordination of domain operations.
* Single Responsibility: Each command handler focuses on a specific type or category of command, adhering to the Single Responsibility Principle (SRP) 
	and promoting maintainability and modularity.
* Decoupled from Infrastructure: Command handlers are decoupled from the underlying infrastructure and external dependencies, enabling flexibility,
	testability, and portability.
* Transaction Management: Command handlers manage transactional boundaries, ensuring data consistency and integrity during command execution.
