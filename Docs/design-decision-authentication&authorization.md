# Authentication and Authorization

Building a microservice application inherently means that it will be divided into separate components.
This introduces new challenges related to ensuring reliable authentication. 
In many applications, it is still common for the application to be responsible for both authentication and storage of user credentials. 
This means that if such an application is compromised, all other services it offers can also be compromised.

## Best Practices

To mitigate these risks, best practices include using secure protocols (e.g., OAuth, JWT) for authentication, implementing strong encryption for storing credentials, regularly auditing and updating security practices, and ensuring each microservice follows security guidelines and policies.

## Why Keycloak?

Since Keycloak implements OAuth2.0, each service/component involved in the microservice application will need to authenticate the user who wants to use it. 
This means that if a MITM (Man-in-the-Middle) attack occurs, the attacker will only possess an "authorization code," which cannot be used to impersonate another user because Keycloak must exchange it 
for an access token or identity token. Such tokens can only be issued to pre-registered clients. A client in the context of OAuth2.0 refers to an application that wants access to protected resources. 
Additionally, the issued tokens will have a short lifespan, after which they are considered invalid and must be renewed. The renewal of access tokens or identity tokens is performed automatically by a registered 
client using a refresh token. However, this cannot be done by the attacker, as Keycloak cannot provide tokens to unknown (unregistered) clients.
