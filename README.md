# School System API

> A production-oriented School Management System REST API built with **ASP.NET Core** using **Clean Architecture**, **Domain-Driven Design (DDD)**, **CQRS**, **MediatR**, and **Entity Framework Core**.

This project demonstrates how to build a scalable, maintainable, and testable backend application by separating business rules from infrastructure concerns while following modern backend engineering practices and SOLID principles.

---

# Tech Stack

## Backend

- ASP.NET Core Web API
- C#
- Entity Framework Core
- PostgreSQL

## Authentication & Security

- JWT Authentication
- Role-Based Authorization
- Password Hashing
- Rate Limiting

## Architecture & Patterns

- Clean Architecture
- Domain-Driven Design (DDD)
- CQRS
- MediatR
- Repository Pattern
- Unit of Work
- Dependency Injection
- SOLID Principles

## Logging

- Serilog

---

# Key Features

## Authentication

- JWT Authentication
- Login
- Current Logged-in User
- Role-Based Authorization

## User Management

- Create Admin
- Create Teacher
- Create Student
- Update User
- Soft Delete User
- View Active Users
- View Deleted Users
- Get User by Id
- User Profile Management
- Profile Picture Upload (Cloudinary)

## Class Management

- Create Class
- Update Class
- Delete Class
- Assign Teacher
- Remove Teacher
- Teacher Schedule Conflict Validation
- Student Capacity Management

## Enrollment Management

### Student

- Request Enrollment
- Cancel Pending Enrollment
- View My Classes
- Drop Class

### Administrator

- View Pending Enrollments
- Approve Enrollment
- Reject Enrollment

---

# Architecture

> <img width="1536" height="1024" alt="Clean_Arch" src="https://github.com/user-attachments/assets/b12e3354-8607-47ac-b083-54901c110c8d" />


## Clean Architecture

Separates the application into independent layers to keep business rules isolated from frameworks, databases, and external services.

### API (Presentation)

**Responsibility**

- RESTful API endpoints
- Controllers
- Authentication & Authorization
- Request / Response handling
- Middleware
- Structured Logging
- Exception Handling

| Responsibility | Description |
|----------------|-------------|
| Commands | Write operations that modify state |
| Queries | Read operations that retrieve data |
| Handlers | Process commands and queries |
| Services | Application-specific business logic |
| DTOs | Data transfer objects |
| Interfaces | Abstractions for infrastructure |
| Mappers | Entity to DTO conversions |

#### Domain Layer

**Responsibility**

- Use Cases
- MediatR Commands & Queries
- DTOs
- Business Workflow Coordination
- Application Services
- Interfaces (Abstractions)

---
## Design Patterns

### Domain

**Responsibility**

- Core Business Logic
- Entities
- Aggregate Roots
- Value Objects
- Domain Events
- Domain Exceptions
- Business Rules

---

### Infrastructure

**Responsibility**

- Entity Framework Core
- PostgreSQL Persistence
- Repository Implementations
- Unit of Work
- JWT Generation
- Password Hashing
- Cloudinary Integration
- External Service Integrations

---

# Architectural Patterns

## Clean Architecture

Separates the application into Presentation, Application, Domain, and Infrastructure layers, ensuring that business rules remain independent from frameworks and external services.

---

## Domain-Driven Design (DDD)

Models the business domain using rich entities, aggregate roots, value objects, and domain events to enforce business rules and maintain consistency across the system.

---

## CQRS

Separates write operations (Commands) from read operations (Queries), allowing each side to evolve independently while keeping responsibilities focused.

---

## MediatR

Implements a decoupled request pipeline where controllers communicate with Commands and Queries instead of directly depending on business logic.

---

## Repository Pattern

Abstracts data persistence behind interfaces, allowing the Application layer to remain independent of Entity Framework Core.

---

## Unit of Work

Coordinates multiple repository operations into a single transaction, ensuring data consistency during business operations.

---

## Dependency Injection

Uses ASP.NET Core's built-in dependency injection container to manage repositories, services, handlers, logging, and infrastructure dependencies.

### Log Categories

## Global Exception Handling

Centralizes exception handling through middleware to provide consistent API error responses and simplify error management.


## Structured Logging

Uses Serilog for structured application logging to improve monitoring, diagnostics, and troubleshooting.

---

# Swagger

> <img width="1901" height="856" alt="Screenshot 2026-08-02 182000" src="https://github.com/user-attachments/assets/3f3549b1-5618-41f2-9b40-a829329a6039" />


---

# Getting Started

## Clone the Repository

```bash
git clone https://github.com/ashura2002/SchoolSystem.git

cd SchoolSystem
```

---

## Configure Application Settings

Configure the following:

- PostgreSQL Connection String
- JWT Settings
- Cloudinary Settings

For local development, sensitive values can be stored using **.NET User Secrets** instead of committing them to source control.

---

## Apply Database Migrations

```bash
dotnet ef database update
```

---

## Run the Application

```bash
dotnet run
```

Swagger will be available at:

```
https://localhost:xxxx/swagger
```

## Architectural Decisions

This project was intentionally designed with maintainability, scalability, and separation of concerns in mind.

- FluentValidation
- Refresh Token Rotation
- Unit Testing
- Integration Testing
- Docker
- Redis Caching
- API Versioning
- Health Checks
- OpenTelemetry
- CI/CD (GitHub Actions)

---

## Learning Objectives

This project was built to strengthen practical experience with:

- ASP.NET Core Web API
- Clean Architecture
- Domain-Driven Design (DDD)
- CQRS
- MediatR
- Entity Framework Core
- PostgreSQL
- Repository Pattern
- Unit of Work
- JWT Authentication
- SOLID Principles
- RESTful API Design
