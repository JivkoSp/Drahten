This directory contains essential components related to a factory pattern implementation.
---
Why the factory pattern is used?
The Factory Design Pattern offers several benefits:
* Encapsulation and Abstraction:
	The factory pattern encapsulates the object creation process. Clients interact with the factory method without needing to know the intricate details 
	of how objects are constructed.
	It abstracts away the complexities of instantiation, promoting cleaner and more maintainable code.
* Flexibility and Extensibility:
	As the application evolves, it might introduce new types of objects. The factory pattern allows to add new concrete 
	factories without modifying existing client code.
	The factory pattern can be extended to create variations of objects (e.g., specialized users) without affecting other parts of the system.
* Decoupling Dependencies:
	By using a factory, the client code is decoupled from the specific classes it creates. The client depends only on the factory interface, 
	not on concrete implementations.
	Changes to the User entity (e.g., constructor parameters, additional logic) won’t ripple through the entire application.
* Centralized Object Creation Logic:
	The factory centralizes the creation logic. If there is need to apply common logic (e.g., logging, caching, validation) during object creation, 
	it can be done within the factory method.
	This avoids duplicating the same logic across different parts of the codebase.
* Testing and Mocking:
	Factories facilitate unit testing. The factory can be easily mocked to isolate client code during testing.
	Mocking the factory allows simulation of different scenarios without instantiating real objects.
---
The IUserFactory interface defines the contract for creating instances of the User entity. It serves as the blueprint for any concrete factory that 
produces users. Key points:
* Purpose: The IUserFactory interface abstracts the creation process, allowing clients to create instance of the User entity without knowing the details.
* Method: The Create method in the interface accepts parameters necessary for constructing an instance of the User entity.
		  Important: These parameters align with the User entity constructor.
Responsibility: Concrete factories implementing this interface will provide specific logic for creating instances of the user entity,
				based on the input parameters.
---
The UserFactory class is an implementation of the IUserFactory interface. It handles the actual creation of User entities.
* Purpose: The UserFactory encapsulates the logic for creating User entities.
* Constructor: The UserFactory class itself doesn’t have a constructor. Instead, it relies on the Create method.
* Create Method: The Create method accepts parameters (such as userFullName, userNickName) and returns a fully constructed User entity.
-------------------------
Similar to IUserFactory the other interfaces are defining the contract for creating instances of other entity types or value objects.
-------------------------
Why are you using the factory pattern for creating simple value objects?
-------------------------
The creating of the value object is not complex, but am using the factory pattern for the following reasons:
* Consistency: Using the factory pattern for creating both entity types and value objects provides consistency for the codebase.
	Consistency can make the codebase easier to understand for developers who are already familiar with the project's design patterns,
	conventions and align well with the principles of Clean Architecture, Domain-Driven Design.
* Separation of Concerns: As mentioned earlier, separating the responsibility of creating objects from the command execution logic 
	can help maintain a clear separation of concerns in the application. This separation can improve code readability, maintainability, 
	and scalability.
* Testing: Using a factory can facilitate easier unit testing by allowing mocking the factory interface in the unit tests. 
	This isolation can make writing focused and targeted tests simpler, without needing to worry about the details of object creation.
* Flexibility and Extensibility: Adapting the code to future changes or requirements can be easier by encapsulating the object creation process 
	within a factory. If the creation process for value objects needs to evolve or become more complex in the future, I can modify the factory 
	implementation without impacting other parts of the application.