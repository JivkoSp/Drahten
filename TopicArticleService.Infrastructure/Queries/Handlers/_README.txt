This directory contains query handlers that work with the queries from the Command Query Responsibility Segregation (CQRS) approach.
-------------------------
The queries work directly with entity framework (i.e the database) and that is why they are in the Infrastructure layer.
-------------------------
But why? I mean the query handlers can be placed in the Application layer as the queries themselfs right?
-------------------------
Yes, but then there must be interface abstraction that encapsulates the entity framework specific queries to the tabase and that introduces
two new potentially bad things:
* Too many methods and large interfaces: There can be potentially many methods, encapsulating specific entity framework queries,
	for example: GroupByName(...), SearchByTitle(...), SearchByPhrase(...) and so on, leading to very large interfaces that can be 
	confusing hard to understand.
* Too large method names: In addition some method names can become very large, becouse the names must be descriptive of what the method
	is ment to do. For example: If there is search functionality that can search articles by title, author and publication date:
	SearchArticleByTitleAndAuthorAndPublicationDate(...) this name becomes too large and looks ridiculous.
