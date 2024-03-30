This directory contains the definition of the IQueryHandler<TQuery, TResult> interface, which serves as a contract for all query handlers 
responsible for executing queries and retrieving data from the application's read-side or queryable data sources.
-------------------------
Responsibilities of the Query Handler Interface:
* Execution Contract: The Query handler interface defines the contract for executing queries and retrieving data, specifying the input parameters 
	and return types expected by query handlers.
* Abstraction Layer: The Query handler interface abstracts away the implementation details of data retrieval logic, enabling decoupling between 
	query definitions and their execution.
* Single Responsibility: The Query handler interface is adhering to the Single Responsibility Principle (SRP), focusing on a specific aspect of 
	application functionality—data retrieval.