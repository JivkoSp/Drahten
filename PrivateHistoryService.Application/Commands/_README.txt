This directory contains command types that implement ICommand interface and serves as the central location for defining and organizing commands.
-------------------------
What is a Command?
In the context of Command Query Responsibility Segregation (CQRS), a command represents an intention to perform a specific action or change within 
the application's domain. Unlike queries, which retrieve data from the system, commands are responsible for initiating modifications or state 
changes. Commands encapsulate these intentions in a structured format, making them explicit and actionable.
-------------------------
Characteristics of a Command:
* Intentional: Commands express clear intentions or actions to be performed within the domain.
* Actionable: Each command corresponds to a specific operation or task that the system should execute.
* Data-Driven: Commands typically encapsulate relevant data or parameters required to perform the intended action.
* Single Responsibility: Commands adhere to the Single Responsibility Principle (SRP), focusing on a single task or action.
