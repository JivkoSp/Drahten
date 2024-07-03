# Reverse Proxy

## There are several reasons why using a reverse proxy server is beneficial for microservices applications

* **Reduced Number of Client Requests** - Multiple requests from client applications can be consolidated into a single request, reducing network latency.
  For example, a client application needing data from three different services can make a single request to Yarp, which then makes three requests to the respective services and returns a consolidated response
  to the client. This approach can also simplify client-side logic;
* **Hiding Service Details** - A reverse proxy server can hide backend service details from client applications, such as service location, used protocol, protocol version, and communication port.
  Thus, client applications only need to know the reverse proxy server address and the API protocol of the desired service, while the reverse proxy server handles routing and transporting requests and responses;
* **Enhanced Security** - Acting as a single entry point for client applications, the reverse proxy server can enforce authentication and authorization policies for backend services.
  This helps prevent unauthorized clients from discovering the service structure and endpoints by filtering unwanted requests. The reverse proxy server can also perform SSL termination, meaning it decrypts
  incoming HTTPS requests and encrypts outgoing HTTPS responses;
* **Improved Reliability** - The reverse proxy server can enhance the reliability of backend services by implementing load balancing mechanisms.
  Incoming requests can be distributed among multiple instances of the same service, balancing the service load. It can also limit the number and speed of incoming requests, preventing service overload.

## Limiting HTTP Requests in the Reverse Proxy Server

Limiting requests aligns with the [principle of layered security](https://www.cloudflare.com/learning/security/glossary/what-is-defense-in-depth/), where each layer provides an additional checkpoint, thereby enhancing the overall security of the application. The gateway (reverse proxy server) enforces a more general policy for request limiting, while individual services can have more specific policies. Distributing the responsibilities for traffic limiting also helps in better handling large volumes of traffic.

### The rate limiting policy in the gateway is shown in the figure below

<p align="center">
    <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/YarpPolicy.PNG" alt="Logo" width="350">
</p>
