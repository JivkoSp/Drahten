<p align="center">
    <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/logo2.PNG" alt="Logo" width="600">
</p>

[![CodeQL](https://github.com/jivkosp/Drahten/workflows/CodeQL/badge.svg)](https://github.com/jivkosp/Drahten/actions/workflows/codeql.yml)

## ⚠️ Important Notice

**Be aware that this project is under development, has some bugs, and some of the features (The PublicHistory Service) mentioned in the Architecture Overview are not yet finished.**

If you decide to fork it, you will need a machine with parameters similar to these:

- **RAM**: 64GB (The Search Service, in particular, consumes a lot of RAM)
- **GPU**: At least 8GB of RAM
- **CPU**: Currently tested with Ryzen 5 generation

---

## What is Drahten?

Drahten is an open-source project utilizing a **microservices architecture** written in .NET Core 8.0. The project focuses on creating a secure application that encompasses:
- Access control
- Information retrieval
- Data encryption

## Architecture Overview

![Architecture Overview](https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/ArchitectureOverview.PNG)

## Why Security Matters

In the realm of computer systems and networks, defining "absolute security" is impossible. The rapid evolution of information technologies, the reduced timeframes for comprehensive testing of information systems, the increasing capabilities of individual users, and the potential for human errors (such as non-compliance with organizational security policies, configuration mistakes, and missed updates of critical applications and systems) are just some of the challenges that make "absolute security" unattainable. Therefore, the goal is to achieve "sufficient security."

## Achieving Sufficient Security

In this project, "sufficient security" is accomplished through the following measures:

- **Microservice Architecture**: The entire application is built using microservices, with each service running in a separate container and having its own database;
- **User Authentication**: Users are authenticated via an authentication server that operates as an independent service within a container;
- **Reverse Proxy Server**: A reverse proxy server acts as a single entry point for client applications, enforcing authentication and authorization policies for backend services;
- **Data Encryption**: Information transmitted between services and the database is encrypted;
- **Event Logging System**: An event logging system captures events occurring within the application's services, providing a clear overview of the system's activities from a centralized location.

<h2>📖 <a href="https://jivkosp.github.io/Drahten/" target="_blank">Documentation</a></h2>

* **Intro** 📜
    - [Purpose](Docs/intro-purpose.md)
    - [Capabilities](Docs/intro-capabilities.md)
    - [Overall design](Docs/intro-design.md)
    - [Technologies](Docs/intro-technologies.md)
* **Design Decisions** 🧩
    - [Authentication and Authorization](Docs/design-decision-authentication-and-authorization.md)
    - [Reverse Proxy](Docs/design-decision-reverse-proxy.md)
    - [Messaging](Docs/design-decision-messaging.md)
    - [Containerization](Docs/design-decision-containerization.md)
    - [Fault Handling](Docs/design-decision-fault-handling.md)
    - [Data Storage](Docs/design-decision-data-storage.md)
    - [Semantic Search](Docs/design-decision-semantic-search.md)
* **Design** 🛠️
    - [Search Service](Docs/design-search-service.md)
    - [User Service](Docs/design-user-service.md)
    - [Topic Article Service](Docs/design-topicarticle-service.md)
    - [Private History Service](Docs/design-privatehistory-service.md)
    - [Log Collection Service](Docs/design-logcollection-service.md)
* **Project Structure** 📂
    - [User Service Project Structure](Docs/project-structure-user-service.md) --> NOT DONE 
* **Usage** 🚀
   - [How to Run](Docs/usage-how-to-run.md)
   - [API Documentation](Docs/usage-api-documentation.md)
