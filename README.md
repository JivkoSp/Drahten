#Drahten
<p align="center">
    <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/logo.PNG" alt="Logo" width="250">
</p>

## Table of Contents
1. [What is Drahten?](#what-is-drahten)
2. [Why Security Matters](#why-security-matters)
3. [Achieving Sufficient Security](#achieving-sufficient-security)
4. [Will This Product Solve My Problem?](#will-this-product-solve-my-problem)
5. [Architecture Overview](#architecture-overview)

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

## Will This Product Solve My Problem?

If you are looking for a secure application framework that provides robust access control, reliable information retrieval, and strong data encryption, Drahten is designed to meet those needs. By leveraging a microservices architecture, Drahten ensures that each service is isolated and secure, while the use of an authentication server and reverse proxy server provides centralized user authentication and authorization. Additionally, the encryption of data in transit and the comprehensive logging system contribute to the overall security and transparency of the application.

Moreover, Drahten offers advanced capabilities for scraping information from various websites, performing semantic searches on the retrieved information, and generating questions based on the semantic meaning of the data. This is achieved through a custom search engine implemented by the project. These features enable efficient data retrieval and analysis, making Drahten an excellent choice for applications requiring intelligent data processing and query generation.

While no system can guarantee "absolute security," Drahten aims to provide "sufficient security" to protect your data and maintain the integrity of your application.
