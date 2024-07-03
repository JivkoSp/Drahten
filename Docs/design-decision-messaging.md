# Messaging

Communication between different services is an old problem. On one hand, there are various protocols that define the means of transport and different participants. Examples of such protocols include **SMTP**, **FTP**, **HTTP**, and **WebSockets**, all based on **TCP/UDP** protocols. Their goal is to create a format for reliably transmitting a message and finding its correct recipient.

On the other hand, communication can be viewed from the perspective of the message itself. A message exists in one system, then gets transferred to another (it is transported), meaning the message has a lifecycle. As the message travels from one system to another, it is important to know where the message is and who "owns" it at any given moment. The communication protocols mentioned above can ensure that the ownership (and "physical" location) of the message is transferred from one system to another. The transmission of a message can be seen as a transaction ("contract") between two parties while both are present. Most of the time, this active exchange is desirable, for instance, querying the status of a service and expecting a timely and accurate response.

However, there are cases when a response from the recipient is not needed; the recipient just needs to temporarily take ownership of the message and ensure that the message reaches its location. When a monolithic system is broken down into separate subsystems, one of the biggest problems to solve is which communication technology to use.

---

## Direct Communication with HTTP

**HTTP** can be given as an example of direct communication for connecting individual services. Although this protocol is well-supported and a solid choice, it has some drawbacks:

- **Service Discovery**: 
  - Service discovery is not solved. A possible solution is using DNS. 
  - As the system scales and grows, the complexity of finding and load balancing also increases.
  - RabbitMQ can mitigate the increased complexity of the solution.

- **Ephemeral Communication**: 
  - Communication is ephemeral (short-lived). 
  - Messages may be lost or duplicated.
  - If a service is temporarily unavailable, message delivery fails and the message is lost (i.e., the message cannot be received after the service is restored).

---

## Using RabbitMQ for Reliable Communication

**RabbitMQ** can help in both cases by using message queues as a means of transport:

- The services provided by the application can publish and consume messages, decentralizing the "end-to-end" message delivery from the availability of the final recipient (a service that needs to receive messages).
- If a message recipient is temporarily unavailable, unlike HTTP, the message is safely buffered and stored in RabbitMQ, and the message is delivered when the recipient becomes available again.
- The discoverability of messages by the recipient is also simplified. All that needs to be known is the location of RabbitMQ and the name of the queue from which messages should be retrieved. This means that the queue name serves as the address of the service for receiving messages.
- Each queue can serve multiple recipients and balance the load.

Reliable communication between the services provided by the application is also one of the tasks of the current thesis. To achieve service reliability, communication between them must also be reliable.
