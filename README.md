<p align="center">
    <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/logo.PNG" alt="Logo" width="250">
</p>

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

- **Microservice Architecture**: The entire application is built using microservices, with each service running in a separate container and having its own database.
- **User Authentication**: Users are authenticated via an authentication server that operates as an independent service within a container.
- **Reverse Proxy Server**: A reverse proxy server acts as a single entry point for client applications, enforcing authentication and authorization policies for backend services.
- **Data Encryption**: Information transmitted between services and the database is encrypted.
- **Event Logging System**: An event logging system captures events occurring within the application's services, providing a clear overview of the system's activities from a centralized location.

 # 📃Documentation <img src="https://media.giphy.com/media/NFA61GS9qKZ68/giphy.gif" alt="Reading Strategies GIF" height="60">
 * Intro
    - [Capabilities](Docs/intro-capabilities.md)
    - [Purpose](Docs/intro-purpose.md)
    - [Overall design](Docs/intro-design.md)
    - [Technologies](Docs/intro-technologies.md)
* Design
    - [Search Service](Docs/intro-technologies.md)

