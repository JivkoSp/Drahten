# Authentication and Authorization

Building a microservice application inherently means that it will be divided into separate components.
This introduces new challenges related to ensuring reliable authentication. 
In many applications, it is still common for the application to be responsible for both authentication and storage of user credentials. 
This means that if such an application is compromised, all other services it offers can also be compromised.

## Best Practices

To mitigate these risks, best practices include using secure protocols (e.g., OAuth, JWT) for authentication, implementing strong encryption for storing credentials, regularly auditing and updating security practices, and ensuring each microservice follows security guidelines and policies.

## Why Keycloak?

Since [Keycloak](https://www.keycloak.org/) implements [OAuth2.0](https://oauth.net/2/), each service/component involved in the microservice application will need to authenticate the user who wants to use it. 
This means that if a [MITM (Man-in-the-Middle)](https://www.fortinet.com/resources/cyberglossary/man-in-the-middle-attack) attack occurs, the attacker will only possess an "authorization code," which cannot be used to impersonate another user because Keycloak must exchange it for an access token or identity token. 
Such tokens can only be issued to pre-registered clients. A client in the context of OAuth2.0 refers to an application that wants access to protected resources. 
Additionally, the issued tokens will have a short lifespan, after which they are considered invalid and must be renewed. The renewal of access tokens or identity tokens is performed automatically by a registered 
client using a refresh token. However, this cannot be done by the attacker, as Keycloak cannot provide tokens to unknown (unregistered) clients.

## Hashing user passwords

The algorithm chosen for hashing user passwords is [pbkdf2-sha512](https://www.ietf.org/rfc/rfc2898.txt). The hashing algorithm pbkdf2-sha512 is a widely used method for securely hashing passwords. 
I selected this algorithm for the following reasons:
  - **Security and Key Reliability** - SHA-512, part of the SHA-2 family, generates a 512-bit hash value (64 bytes) and is used for digital signatures, password hashing, cryptocurrencies, and various cryptographic protocols like TLS, PGP, and SSH. PBKDF2 is specifically designed to make password cracking more difficult, requiring significant computational effort to generate each hash;
  - **Use of Salt (Salted Hashes)** - PBKDF2 includes a salt with each password hash. This ensures that the same password will generate different hashes when different salts are used, preventing malicious actors from using precomputed rainbow tables. This technique is also known as a [rainbow table attack](https://medium.com/@jsquared7/password-cracking-what-is-a-rainbow-table-attack-and-how-to-prevent-it-7904000ffcff).
  - **Iterative Hashing** - PBKDF2 can be configured to use a large number of iterations (also known as rounds). Each iteration requires separate hash computation, significantly slowing down brute-force attacks. 
  - **Compliance with Standards** - PBKDF2 is part of the PKCS #5 standard (also known as [RFC 8018](https://www.rfc-editor.org/rfc/rfc8018)), making it well-documented and widely accepted for password hashing.

This choice of pbkdf2-sha512 ensures robust security measures are in place for handling user passwords within the application, aligning with industry standards and best practices in password security.
