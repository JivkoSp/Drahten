 Run locally

* The whole system contains a lot of components, despite that everything can be ran locally on a machine powerful enough, mainly one having lots of RAM
* For local development it is more convenient to use the Docker deployment with a selection of components from the integration, data storage and observability categories, while the .NET services could be ran in the IDE

# Certificates

Security is a part of the modern development with an ever-growing importance. All .NET services are using TLS certificates.  

Security considerations:

* Adding to git the certificates which are self-signed and should be trusted is a security vulnerability;
* The certificates are git-ignored and should be generated after the initial clone of the repo;
* Everyone should generate their own certificates.
