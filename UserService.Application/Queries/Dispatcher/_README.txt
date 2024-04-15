This directory contains query dispatcher type that implements IQueryDispatcher interface and serves as the central component responsible for 
routing queries to their respective query handlers for execution and data retrieval.
This directory complements the Queries directory and plays a crucial role in implementing the Command Query Responsibility Segregation (CQRS).
-------------------------
What is a Query Dispatcher?
In the context of CQRS, a query dispatcher is a component responsible for receiving queries from the application's query side and directing them 
to the appropriate query handlers for execution and data retrieval. Query dispatchers facilitate loose coupling between queries and handlers, 
enabling dynamic resolution of query processing logic based on the query type and desired result.
-------------------------
Characteristics of a Query Dispatcher:
* Routing Logic: Query dispatchers contain the logic required to identify the appropriate query handler based on the type of query received 
	and the desired result type.
* Dynamic Resolution: Query dispatchers dynamically resolve query handlers at runtime, based on the type of query and the expected result type, 
	enabling flexibility and extensibility within the application.
* Dependency Injection: Query dispatchers rely on dependency injection to obtain references to the necessary services and components required for 
	query processing and handler resolution.