This directory contains custom domain event types that implement IDomainEvent. This interface is marker interface for generic constraint purposes
and does not have any methods.
-------------------------
The domain events represent meaningful state changes within the application's domain. They capture significant occurrences or interactions 
between domain entities. In this context, these events are immutable, lightweight objects that carry essential information about what happened 
in the system.
-------------------------
Why I am using domain events?
-------------------------
Reasons:
* Decoupling and Separation of Concerns: Achieving a high level of decoupling between different parts of the application. 
	This separation of concerns is vital for maintaining a clean architecture, as it allows the domain logic to remain agnostic of specific 
	implementation details.
* Explicit Communication: Domain events serve as an explicit means of communication between various components of the application. 
	Rather than relying on direct method calls or shared state, events enable a more transparent and loosely-coupled interaction model.
* Audit Trail and History: Domain events provide a built-in audit trail of important state changes within the system. 
	This history can be invaluable for debugging, auditing, or even reconstructing the sequence of events leading to a particular state.
* Extensibility and Scalability: New functionalities or business requirements can often be implemented by introducing additional domain events 
	and handling them appropriately within the architecture.
-------------------------
Anatomy of a Domain Event
-------------------------
Each domain event in this directory follows a common structure:
--| Name: Descriptive name indicating the occurrence being represented (e.g., ArticleCommentAdded).
--| Properties: Data associated with the event, providing context and details about the state change.
--| Immutability: Once created, domain events are immutable to ensure consistency and prevent unintended modifications.
--| IDomainEvent Interface: All domain events implement the IDomainEvent interface to maintain consistency and enable generic 
	handling within the architecture.