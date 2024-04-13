This directory contains entity types that represent Domain Driven Design (DDD) entities and NOT Entity Framework entities for example.
In the DDD context the entity is just object with identity.
The identity itself can be some identifier like Guid, int, string or some natural identifier (for example email address of user).
---
Important - All entity types must do self validation and keep their internal state valid for the hole live cycle of their existance.
---
This directory also contains AggregateRoot type.
-------------------------
What is an Aggregate Root?
-------------------------
In domain-driven design (DDD), an Aggregate is a cluster of associated objects that are treated as a single unit for the purpose of data changes.
An Aggregate Root is the primary access point to an Aggregate, responsible for ensuring consistency and enforcing invariants within its boundaries.
Characteristics of an Aggregate Root:
* Global Identity: Each Aggregate Root has a globally unique identity, allowing it to be uniquely identifiable within the system.
* Boundary: The Aggregate Root defines the boundary within which all invariants must be maintained. It controls access to its internal entities 
	and enforces consistency.
* Transactional Boundary: All changes to the Aggregate occur within a single transaction, ensuring that either all changes are applied successfully 
	or none at all.
-------------------------
Why Use Aggregate Roots?
-------------------------
* Encapsulation and Consistency: Aggregate Roots encapsulate a cluster of related entities and enforce consistency within their boundaries. 
	By controlling access to internal entities, Aggregate Roots ensure that business rules and invariants are maintained at all times.
* Simplified Interaction: By providing a single entry point to an Aggregate, Aggregate Roots simplify the interaction between different parts 
	of the application. Clients only need to interact with the Aggregate Root, abstracting away the complexities of internal entity management.
* Scalability and Performance: Aggregate Roots define transactional boundaries, allowing for efficient database operations. 
	This ensures that changes to the Aggregate are atomic and consistent, improving scalability and performance, especially in distributed systems.
* Domain Model Clarity: By clearly defining Aggregates and Aggregate Roots, the domain model becomes more expressive and easier to understand. 
	This clarity is leading to better software design and implementation.