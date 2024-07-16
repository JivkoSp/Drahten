This directory contains command dispatcher type that implements ICommandDispatcher interface and serves as the central component responsible for 
routing commands to their respective command handlers.
This directory complements the Commands and Handlers directories and is essential for implementing the Command Query Responsibility Segregation (CQRS). 
-------------------------
What is a Command Dispatcher?
In the context of CQRS, a command dispatcher is a component responsible for receiving commands from the application's command side and 
directing them to the appropriate command handlers for execution. 
Command dispatchers facilitate loose coupling between commands and handlers, allowing for dynamic resolution of command processing logic based 
on the command type.
-------------------------
Characteristics of a Command Dispatcher:
* Routing Logic: Command dispatchers contain the logic required to identify the appropriate command handler based on the type of command received.
* Dynamic Resolution: Command dispatchers dynamically resolve command handlers at runtime, enabling flexibility and extensibility within the application.
* Dependency Injection: Command dispatchers rely on dependency injection to obtain references to the necessary services and components required 
	for command processing.
