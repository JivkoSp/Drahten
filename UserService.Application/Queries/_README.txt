This directory contains query types that implement IQuery<T> interface and serves as the central location for defining and organizing queries.
This direcotry complements the Commands directory and plays a crucial role in implementing the Command Query Responsibility Segregation (CQRS).
-------------------------
What is a Query?
In the context of CQRS, a query represents a request for data retrieval or read-only access to the application's state. 
Unlike commands, which initiate modifications or state changes within the domain, queries are focused on retrieving information from the 
system without affecting its state. Queries encapsulate these data retrieval operations in a structured format, making them explicit and actionable.
-------------------------
Characteristics of a Query:
* Data Retrieval: Queries are responsible for retrieving data from the application's read-side or queryable data sources, such as databases, 
	caches, or external APIs.
* Read-Only Operations: Queries do not modify the application's state and are strictly focused on retrieving information for consumption or 
	display purposes.
* Data Projection: Queries often include data projection logic to transform raw data from the underlying data sources into a format suitable for 
	presentation or further processing.
* Parameterization: Queries may accept parameters or criteria to customize the data retrieval process, enabling dynamic filtering, sorting, 
	or pagination of query results.