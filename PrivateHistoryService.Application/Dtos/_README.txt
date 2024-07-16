This directory contains Data Transfer Objects (DTOs).
The purpose of dtos is transferring data between different parts of an application. They serve as lightweight containers, allowing efficient 
communication and manipulation of data.
-------------------------
Characteristics of DTOs:
* Simplicity: DTOs contain only the necessary data fields required for specific operations or communication between different parts of 
	the application.
* No Business Logic: Unlike domain entities, DTOs do not contain any business logic. They focus solely on data transfer.
* Reduced Overhead: By sending only relevant data, DTOs minimize the amount of information transmitted over the network or passed between 
	the layers.
* Security Enhancement: DTOs limit the exposed data, improving security by preventing sensitive information from being inadvertently revealed.
* Versioning and Compatibility: DTOs can be versioned independently of the underlying data model, making it easier to handle changes 
	without affecting the entire application.
* Performance Optimization: When dealing with large datasets, DTOs allow selective transmission of necessary data fields, improving performance 
	and reducing latency.
-------------------------
Additional benefits of using dtos, related to clean architecture: 
DTOs promote clean separation between layers by encapsulating data and avoiding direct exposure of domain entities or value objects.
