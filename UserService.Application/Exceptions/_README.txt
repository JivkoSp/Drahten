This directory contains Application layer specific custom exception types.
-------------------------
The ApplicationException is the root (parent) Application layer exception, that other exceptions in this layer will inherit.
-------------------------
But why?
-------------------------
Throwing generic exceptions like ArgumentNullException or ArgumentOutOfRangeException that are provided by the language is NOT good idea.
Here is why:
* Semantic Clarity: Using custom exceptions allows conveying the specific nature of the error encountered in the application. 
	While generic exceptions provide a broad indication of what went wrong, custom exceptions can offer detailed insights into the exact cause, 
	leading to more informative error messages and easier debugging.
* Granular Error Handling: Custom exceptions enable to differentiate between various error conditions within the application. 
	This granularity facilitates targeted exception handling, allowing implementation of specific recovery or fallback mechanisms based on the 
	type of exception thrown.
* Maintainability and Extensibility: As the application grows, custom exceptions provide a structured and extensible framework for handling errors. 
	They allow encapsulation of application layer specific logic and maintaining a clear separation of concerns, making the codebase more manageable 
	and adaptable to future changes.
* Enhanced Testing: Custom exceptions simplify the testing process by enabling writing of unit focused tests for specific error scenarios. 
	This targeted testing approach ensures comprehensive coverage of the application layer error-handling logic, leading to greater reliability 
	and stability.
* Consistency and Standardization: Establishing a consistent error-handling pattern across the codebase by defining a set of custom exceptions 
	tailored to the application layer. This standardization fosters collaboration among developers and promotes code readability and 
	maintainability.