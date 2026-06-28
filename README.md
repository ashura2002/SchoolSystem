# School Management System API

A RESTful School Management System built with **ASP.NET Core** following **Clean Architecture**, **Domain-Driven Design (DDD)**,
and **CQRS** principles.

The project demonstrates how to build scalable, maintainable, and testable backend applications by separating business rules from 
infrastructure concerns while applying SOLID principles and modern architectural patterns.

---

# Table of Contents

- Features
- Technologies
- Architecture
- Project Structure
- Design Patterns
- Domain-Driven Design
- CQRS
- Dependency Injection
- Logging
- Error Handling
- Security
- Pagination
- API Overview
- Architectural Decisions
- Future Improvements
- Learning Objectives
- Author

---

# Features

## Authentication

- JWT Authentication
- Role-based Authorization
- Login
- Get Current Logged-in User

## User Management

- Create Admin
- Create Teacher
- Create Student
- Update User
- Soft Delete User
- View Active Users
- View Deleted Users
- Get User by Id

## Class Management

- Create Class
- Update Class
- Delete Class
- Assign Teacher
- Remove Teacher
- View All Classes
- View Classes Without Teacher
- View Teacher's Classes

## Enrollment

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

# Technologies

### Backend

- ASP.NET Core Web API
- C#
- Entity Framework Core
- PostgreSQL

### Authentication & Security

- JWT Authentication
- Role-based Authorization
- Password Hashing
- Rate Limiting

### Architecture

- Clean Architecture
- Domain-Driven Design (DDD)
- CQRS
- Repository Pattern
- Dependency Injection

### Logging

- Serilog

---

# Architecture

The project follows **Clean Architecture**, ensuring that business rules remain independent of frameworks, databases, and 
external libraries.

```
Presentation (WebAPI)
        │
        ▼
Application
        │
        ▼
Domain
        ▲
        │
Infrastructure
```

## Layer Responsibilities

### Presentation (WebAPI)

Responsible for:

- Controllers
- Authentication
- Authorization
- Middleware
- API Requests & Responses
- Dependency Injection

---

### Application

Responsible for:

- Commands
- Queries
- Handlers
- Services
- DTOs
- Interfaces
- Mappers

The Application layer contains application business logic and depends only on abstractions.

---

### Domain

Responsible for:

- Entities
- Value Objects
- Domain Exceptions
- Domain Enums

The Domain layer contains the core business rules and has no dependency on Infrastructure or external libraries.

---

### Infrastructure

Responsible for:

- Entity Framework Core
- Repository Implementations
- JWT Generation
- Password Hashing
- Current User Service

Infrastructure implements the interfaces defined by the Application layer.

---

# Project Structure

```
SchoolManagementSystem
│
├── Domain
│   ├── Entities
│   ├── Enums
│   ├── Exceptions
│   └── ValueObjects
│
├── Application
│   ├── Features
│   │
│   ├── Auth
│   │   ├── Commands
│   │   ├── Queries
│   │   └── Services
│   │
│   ├── Users
│   │   ├── Commands
│   │   └── Queries
│   │
│   ├── Classes
│   │   ├── Commands
│   │   └── Queries
│   │
│   ├── Enrollments
│   │   ├── Commands
│   │   └── Queries
│   │
│   ├── Interfaces
│   ├── Mappers
│   └── DependencyInjection.cs
│
├── Infrastructure
│   ├── Data
│   ├── Authentication
│   ├── Repositories
│   ├── Services
│   └── DependencyInjection.cs
│
└── WebAPI
    ├── Controllers
    ├── Middlewares
    ├── Constants
    ├── DependencyInjection
    └── Program.cs
```

---

# Design Patterns

This project applies several software engineering principles and patterns.

- Clean Architecture
- Domain-Driven Design (DDD)
- CQRS
- Repository Pattern
- Dependency Injection
- SOLID Principles

---

# Domain-Driven Design (DDD)

The Domain layer models the core business rules of the system.

## Entities

- User
- SchoolClass
- Enrollment

## Value Objects

- Username
- Email
- Password
- ClassName

Value Objects encapsulate validation and ensure that invalid domain data cannot be created.

---

# CQRS

The project separates **write operations** from **read operations**.

## Commands

Commands modify the application's state.

Examples:

- Create User
- Update User
- Delete User
- Login
- Approve Enrollment
- Reject Enrollment
- Assign Teacher

## Queries

Queries retrieve data without modifying the system.

Examples:

- Get User By Id
- Get Current User
- Get All Users
- Get My Classes
- Get Pending Enrollments

Each command and query has its own Handler.

---

# Dependency Injection

All dependencies are registered using Microsoft's built-in Dependency Injection container.

Examples include:

- Repository Implementations
- Services
- Handlers
- JWT Service
- Password Hasher

---

# Logging

The project uses **Serilog** for structured logging.

Logs include:

- Incoming Requests
- Errors
- Unhandled Exceptions

---

# Error Handling

A Global Exception Middleware provides centralized exception handling and consistent API responses.

Example:

```json
{
    "statusCode":404,
    "message":"User not found",
    "traceId":"..."
}
```

---

# Security

Security features include:

- JWT Authentication
- Role-based Authorization
- Password Hashing
- Current User Service
- Global Exception Handling
- API Rate Limiting

---

# Pagination

Collection endpoints support pagination.

Example:

```
GET /api/users?page=1&pageSize=10
```

---

# API Overview

## Authentication

- POST /api/auth/login

## Users

- GET /api/users
- GET /api/users/{id}
- GET /api/users/me
- PUT /api/users/{id}
- DELETE /api/users/{id}

## Classes

- POST /api/classes
- GET /api/classes
- PUT /api/classes/{id}
- DELETE /api/classes/{id}

## Enrollment

### Student

- POST /api/enrollment
- GET /api/enrollment/my-classes
- DELETE /api/enrollment/request/{id}
- DELETE /api/enrollment/my-classes/{id}

### Administrator

- GET /api/enrollment/pending
- POST /api/enrollment/{id}/approve
- POST /api/enrollment/{id}/reject

---

# Architectural Decisions

This project was intentionally designed with maintainability, scalability, and separation of concerns in mind.

- **Clean Architecture** separates the application into Presentation, Application, Domain, and Infrastructure layers.
- **Domain-Driven Design (DDD)** keeps business rules and validation inside the Domain layer through entities and value objects.
- **Dependency Inversion Principle (DIP)** ensures the Application layer depends only on abstractions while Infrastructure provides the concrete implementations.
- **CQRS** separates write operations (Commands) from read operations (Queries), improving maintainability and preparing the project for MediatR.
- **Repository Pattern** abstracts data access from application logic, keeping business rules independent of Entity Framework Core.
- **Global Exception Middleware** centralizes exception handling and provides consistent API error responses.
- **Structured Logging (Serilog)** captures application events and exceptions for easier debugging.
- **Cancellation Tokens** are propagated through asynchronous operations to support request cancellation and improve resource usage.

---

# Future Improvements

- MediatR
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

# Learning Objectives

This project was built to strengthen understanding of:

- Clean Architecture
- Domain-Driven Design (DDD)
- CQRS
- SOLID Principles
- Repository Pattern
- Dependency Injection
- ASP.NET Core Web API
- JWT Authentication
- Entity Framework Core
- RESTful API Design


