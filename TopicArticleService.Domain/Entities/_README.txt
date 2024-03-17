This directory contains entity types that represent Domain Driven Design (DDD) entities NOT Entity Framework entities for example.
In the DDD context the entity is just object with identity.
The identity itself can be some identifier like Guid, int, string or some natural identifier (for example email address of user).
---
Important - All entity types must do self validation and keep their internal state valid for the hole live cycle of their existance.
---
This directory also contains AggregateRoot type ....