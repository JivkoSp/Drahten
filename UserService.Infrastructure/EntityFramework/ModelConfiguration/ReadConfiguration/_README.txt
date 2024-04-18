This directory contains classes that implement IEntityTypeConfiguration<T> and are containing configuration for the database models.
-------------------------
This configuration is different than the configuration contained in the WriteConfiguration directory, but the entities and value objects
are mapped to the same database tables and columns. Command Query Responsibility Segregation (CQRS): Becouse of this rule there are two 
db contexts (for the read and write side). This is the configuration for the read side. Becouse entity framework must know
what is the database schema, the models (that represent the database tables and the overall database schema) need to have the
knowledge of entity framework specific concerns like the need of having navigation properties or specifig foreign keys for the table connections.
On the other hand the WriteConfiguration directory contains the configuration for the write side with domain entities and value objects and they 
(the domain entities and value objects) does not need to have entity framework specific knowledge.