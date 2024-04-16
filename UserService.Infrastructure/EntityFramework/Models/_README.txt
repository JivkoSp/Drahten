This directory contains classes for the database models that represent the database tables and the overall database schema.
This models are different than the domain entities and value objects becouse they need to have the knowledge of entity framework specific 
concerns like the need of having navigation properties or specifig foreign keys for the table connections.
-------------------------
The models naming convention is: <name of domain enity or value object>ReadModel. This naming convention is used in order of having
clear separation of the domain entities and value objects, and the database models. Further to make the intention of the models more clear - 
the models are for READING purposes (i.e queries in the context of CQRS). 