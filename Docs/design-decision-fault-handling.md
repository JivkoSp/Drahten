# Fault Handling

##  Using Polly for fault handling between services

One of the tasks of the current application is to achieve reliability of the services it offers. Techniques such as running services in containers with independent databases and using a message broker are part of 
the methods for achieving service reliability. Strategies are necessary to ensure higher fault tolerance of the services in the event of temporary accessibility issues by other services.

### Why Choose Polly?

Polly enables the implementation of the following strategies:

* **Retry** - Allows a service to retry executing an operation after a temporary error. Such an operation could be sending messages to another service offered by the application through a message broker
  that is unavailable due to a communication issue;
* **Timeout** - Setting a maximum time for executing an operation. If the operation is not completed within the specified period, it is considered unsuccessful;
* **Fallback** - If the primary operation is unsuccessful, this strategy provides an alternative solution. For example, using a cache with alternative values for a given operation.
