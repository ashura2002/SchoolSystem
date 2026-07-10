# School System API

A RESTful School Management System built with **ASP.NET Core** following **Clean Architecture**, **Domain-Driven Design (DDD)**, and **CQRS** principles.

This project demonstrates how to build scalable, maintainable, and testable backend applications by separating business rules from infrastructure concerns while applying SOLID principles and modern architectural patterns.

---

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Architecture](#architecture)
- [Design Patterns](#design-patterns)
- [Domain-Driven Design](#domain-driven-design)
- [Dependency Injection](#dependency-injection)
- [Logging](#logging)
- [Error Handling](#error-handling)
- [Security](#security)
- [Architectural Decisions](#architectural-decisions)
- [Future Improvements](#future-improvements)
- [Learning Objectives](#learning-objectives)

---

## Features

### Authentication

| Feature | Description |
|---------|-------------|
| JWT Authentication | Secure token-based authentication |
| Role-based Authorization | Admin, Teacher, Student roles |
| Login | Authenticate users and receive JWT token |
| Get Current User | Retrieve logged-in user information |

### User Management

| Feature | Description |
|---------|-------------|
| Create Admin | Register new administrator accounts |
| Create Teacher | Register new teacher accounts |
| Create Student | Register new student accounts |
| Update User | Modify user information |
| Soft Delete User | Mark users as deleted without permanent removal |
| View Active Users | List all non-deleted users with pagination |
| View Deleted Users | List all soft-deleted users with pagination |
| Get User by Id | Retrieve specific user details |

### Class Management

| Feature | Description |
|---------|-------------|
| Create Class | Add new school classes |
| Update Class | Modify class information |
| Delete Class | Remove classes from the system |
| Assign Teacher | Assign a teacher to a class |
| Remove Teacher | Unassign teacher from a class |
| View All Classes | List all classes with pagination |
| View Classes Without Teacher | List unassigned classes |
| View Classes With Teacher | List assigned classes |
| Get Class by Id | Retrieve specific class details |

### Enrollment

#### Student Operations

| Feature | Description |
|---------|-------------|
| Request Enrollment | Submit enrollment request for a class |
| Cancel Pending Enrollment | Withdraw pending enrollment request |
| View My Classes | List enrolled classes |
| Get My Class by Id | View specific enrolled class details |
| Drop Class | Withdraw from an enrolled class |

#### Administrator Operations

| Feature | Description |
|---------|-------------|
| View Pending Enrollments | List all pending enrollment requests |
| Approve Enrollment | Accept student enrollment request |
| Reject Enrollment | Decline student enrollment request |

### Notifications (Domain Events)

| Feature | Description |
|---------|-------------|
| Get All Notifications | Retrieve all notifications for the current user |
| Get Notification by Id | Retrieve specific notification details |
| Mark as Read | Mark notification as read |
| Mark as Unread | Mark notification as unread |
| Delete Notification | Remove notification |

Notifications are created automatically through **Domain Events** when significant actions occur in the system (e.g., enrollment approved, enrollment rejected, teacher assigned to class).

---

## Technologies

### Backend

| Technology | Purpose |
|------------|---------|
| ASP.NET Core Web API | Web framework |
| C# | Programming language |
| Entity Framework Core | ORM for database operations |
| PostgreSQL | Relational database |

### Authentication & Security

| Technology | Purpose |
|------------|---------|
| JWT (JSON Web Tokens) | Token-based authentication |
| Role-based Authorization | Access control |
| BCrypt/PBKDF2 | Password hashing |
| Rate Limiting | API abuse prevention |

### Architecture & Patterns

| Pattern | Purpose |
|---------|---------|
| Clean Architecture | Separation of concerns |
| Domain-Driven Design (DDD) | Business logic modeling |
| CQRS | Command Query Responsibility Segregation |
| Repository Pattern | Data access abstraction |
| Domain Events | Decoupled event handling |
| Dependency Injection | Loose coupling |

### Logging

| Technology | Purpose |
|------------|---------|
| Serilog | Structured logging |

---

## Architecture

The project follows **Clean Architecture**, ensuring that business rules remain independent of frameworks, databases, and external libraries.

### Layer Responsibilities

#### Presentation Layer (WebAPI)

| Responsibility | Description |
|----------------|-------------|
| Controllers | Handle HTTP requests and responses |
| Authentication | JWT token validation |
| Authorization | Role-based access control |
| Middleware | Global exception handling, logging |
| Dependency Injection | Service registration |

#### Application Layer

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

| Responsibility | Description |
|----------------|-------------|
| Entities | Core business objects with identity |
| Value Objects | Immutable objects without identity |
| Domain Events | Events raised by domain entities |
| Domain Exceptions | Business rule violation exceptions |
| Enums | Domain-specific enumerations |

#### Infrastructure Layer

| Responsibility | Description |
|----------------|-------------|
| Entity Framework Core | Database context and configurations |
| Repository Implementations | Data access implementations |
| JWT Generation | Token creation service |
| Password Hashing | Secure password storage |
| Domain Event Dispatching | Event handler execution |
| Current User Service | Authenticated user information |

---
## Design Patterns

This project applies several software engineering principles and patterns:

| Pattern | Implementation |
|---------|----------------|
| **Clean Architecture** | Four-layer separation (Presentation, Application, Domain, Infrastructure) |
| **Domain-Driven Design** | Entities, Value Objects, Domain Events, Aggregates |
| **CQRS** | Separate Command and Query handlers |
| **Repository Pattern** | Abstract data access behind interfaces |
| **Domain Events** | Decouple side effects from core business logic |
| **Dependency Injection** | Constructor injection throughout |
| **SOLID Principles** | Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion |

---

## Domain-Driven Design

The Domain layer models the core business rules of the system.

### Entities

| Entity | Description |
|--------|-------------|
| **User** | Represents system users (Admin, Teacher, Student) |
| **SchoolClass** | Represents a class that can be taught |
| **Enrollment** | Represents a student's enrollment in a class |
| **Notification** | Represents system notifications for users |

### Value Objects

| Value Object | Purpose |
|--------------|---------|
| **Username** | Encapsulates username validation (length, format) |
| **Email** | Encapsulates email validation and normalization |
| **Password** | Encapsulates password strength requirements |
| **ClassName** | Encapsulates class name validation |

### Domain Events in the System

| Event | Trigger | Handler Action |
|-------|---------|----------------|
| `EnrollmentApprovedEvent` | Enrollment approved by admin | Creates notification for student |
| `EnrollmentRejectedEvent` | Enrollment rejected by admin | Creates notification for student |
| `TeacherAssignedEvent` | Teacher assigned to class | Creates notification for teacher |
| `EnrollmentRequestedEvent` | Student requests enrollment | Creates notification for admin |

### Benefits of Domain Events

| Benefit | Description |
|---------|-------------|
| **Decoupling** | Core business logic doesn't know about notifications |
| **Single Responsibility** | Each handler has one job |
| **Extensibility** | Easy to add new side effects without modifying entities |
| **Testability** | Events and handlers can be tested independently |

CQRS
The project separates write operations (Commands) from read operations (Queries).

### Commands

Commands modify the application's state and return minimal data.

| Command | Description |
|---------|-------------|
| `LoginCommand` | Authenticate user and generate JWT |
| `CreateAdminCommand` | Create new admin user |
| `CreateTeacherCommand` | Create new teacher user |
| `CreateStudentCommand` | Create new student user |
| `UpdateUserCommand` | Update user information |
| `DeleteUserCommand` | Soft delete user |
| `CreateClassCommand` | Create new school class |
| `UpdateClassCommand` | Update class information |
| `DeleteClassCommand` | Delete school class |
| `AssignTeacherCommand` | Assign teacher to class |
| `RemoveTeacherCommand` | Remove teacher from class |
| `RequestEnrollmentCommand` | Request enrollment in class |
| `CancelEnrollmentCommand` | Cancel pending enrollment |
| `ApproveEnrollmentCommand` | Approve enrollment request |
| `RejectEnrollmentCommand` | Reject enrollment request |
| `DropEnrollmentCommand` | Drop from enrolled class |
| `MarkAsReadCommand` | Mark notification as read |
| `MarkAsUnreadCommand` | Mark notification as unread |
| `DeleteNotificationCommand` | Delete notification |

Queries
Queries retrieve data without modifying the system.

### Queries

Queries retrieve data without modifying the system.

| Query | Description |
|-------|-------------|
| `GetCurrentUserQuery` | Get authenticated user info |
| `GetUserByIdQuery` | Get user by ID |
| `GetAllUsersQuery` | Get all active users (paginated) |
| `GetDeletedUsersQuery` | Get soft-deleted users (paginated) |
| `GetAllClassesQuery` | Get all classes (paginated) |
| `GetClassByIdQuery` | Get class by ID |
| `GetClassesWithoutTeacherQuery` | Get unassigned classes |
| `GetClassesWithTeacherQuery` | Get assigned classes |
| `GetOwnClassesQuery` | Get teacher's assigned classes |
| `GetPendingEnrollmentsQuery` | Get pending enrollments |
| `GetMyClassesQuery` | Get student's enrolled classes |
| `GetMyClassByIdQuery` | Get specific enrolled class |
| `GetAllNotificationsQuery` | Get user's notifications |
| `GetNotificationByIdQuery` | Get notification by ID |

Each command and query has its own dedicated Handler class.

Logging
The project uses Serilog for structured logging.

## Logging

The project uses **Serilog** for structured logging.

### Log Categories

| Category | Examples |
|----------|----------|
| **Request Logging** | HTTP method, path, status code, duration |
| **Authentication** | Login attempts, token generation |
| **Business Operations** | User created, enrollment approved |
| **Errors** | Exceptions, validation failures |
| **Domain Events** | Event raised, event handled |

### Configuration Example


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


## Error Handling

A Global Exception Middleware provides centralized exception handling and consistent API responses.

### Error Response Format


{
    "statusCode": 404,
    "message": "User not found",
    "traceId": "0HMVFE0A284AM:00000001"
}

### Exception Types

| Exception Type | HTTP Status | Description |
|----------------|-------------|-------------|
| `DomainException` | 400 | Business rule violation |
| `NotFoundException` | 404 | Entity not found |
| `UnauthorizedException` | 401 | Authentication required |
| `ForbiddenException` | 403 | Insufficient permissions |
| `ValidationException` | 422 | Input validation failed |
| `Exception` | 500 | Unexpected server error |

## Security

### Security Features

| Feature | Implementation |
|---------|----------------|
| **JWT Authentication** | Bearer token authentication |
| **Role-based Authorization** | Admin, Teacher, Student roles |
| **Password Hashing** | BCrypt/PBKDF2 with salt |
| **Current User Service** | Extract user from JWT claims |
| **Global Exception Handling** | Prevent sensitive data leakage |
| **Rate Limiting** | Prevent API abuse |
| **Soft Delete** | Data recovery capability |

### Role Permissions

| Endpoint | Admin | Teacher | Student |
|----------|:-----:|:-------:|:-------:|
| Create Users | Yes | No | No |
| Manage Classes | Yes | No | No |
| Assign Teachers | Yes | No | No |
| View Own Classes | Yes | Yes | No |
| Approve Enrollments | Yes | No | No |
| Request Enrollment | No | No | Yes |
| View My Classes | No | No | Yes |
| View Notifications | Yes | Yes | Yes |

Pagination
Collection endpoints support pagination using query parameters.

### Response Format

json
{
    "items": [...],
    "page": 1,
    "pageSize": 10,
    "totalCount": 100,
    "totalPages": 10,
    "hasPreviousPage": false,
    "hasNextPage": true
}

API Reference
Base URL
{{base_url}}/api

---

### Authentication

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/auth/login` | Login and get JWT token | No |
| POST | `/auth/admin` | Create admin account | Yes (Admin) |
| POST | `/auth/teacher` | Create teacher account | Yes (Admin) |
| POST | `/auth/student` | Create student account | Yes (Admin) |

---

### Users

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/users` | Get all active users (paginated) | Yes (Admin) |
| GET | `/users/deleted` | Get all deleted users (paginated) | Yes (Admin) |
| GET | `/users/me` | Get current logged-in user | Yes |
| GET | `/users/{id}` | Get user by ID | Yes (Admin) |
| PUT | `/users/{id}` | Update user | Yes (Admin) |
| DELETE | `/users/{id}` | Soft delete user | Yes (Admin) |

---

### Classes

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/class` | Get all classes (paginated) | Yes |
| GET | `/class/{id}` | Get class by ID | Yes (Admin) |
| GET | `/class/without-teacher` | Get classes without teacher | Yes (Admin) |
| GET | `/class/with-teacher` | Get classes with teacher | Yes (Admin) |
| GET | `/class/own-classes` | Get teacher's classes (paginated) | Yes (Teacher) |
| GET | `/class/own-classes/{id}` | Get teacher's class by ID | Yes (Teacher) |
| POST | `/class` | Create new class | Yes (Admin) |
| PUT | `/class/{id}` | Update class | Yes (Admin) |
| DELETE | `/class/{id}` | Delete class | Yes (Admin) |
| PUT | `/class/{id}/teacher` | Assign teacher to class | Yes (Admin) |
| DELETE | `/class/{id}/teacher` | Remove teacher from class | Yes (Admin) |

---

### Enrollment

#### Student Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/enrollment` | Request enrollment | Yes (Student) |
| GET | `/enrollment/my-classes` | Get enrolled classes (paginated) | Yes (Student) |
| GET | `/enrollment/my-classes/{id}` | Get enrolled class by ID | Yes (Student) |
| PUT | `/enrollment/requests/{id}` | Cancel pending enrollment | Yes (Student) |
| DELETE | `/enrollment/my-classes/{id}` | Drop from class | Yes (Student) |

#### Admin Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/enrollment/pending` | Get pending enrollments (paginated) | Yes (Admin) |
| POST | `/enrollment/{id}/approve` | Approve enrollment | Yes (Admin) |
| POST | `/enrollment/{id}/reject` | Reject enrollment | Yes (Admin) |

---

### Notifications

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/notifications` | Get all notifications | Yes |
| GET | `/notifications/{id}` | Get notification by ID | Yes |
| PUT | `/notifications/{id}/read` | Mark as read | Yes |
| PUT | `/notifications/{id}/unread` | Mark as unread | Yes |
| DELETE | `/notifications/{id}` | Delete notification | Yes |

## Architectural Decisions

This project was intentionally designed with maintainability, scalability, and separation of concerns in mind.

| Decision | Rationale |
|----------|-----------|
| **Clean Architecture** | Separates the application into Presentation, Application, Domain, and Infrastructure layers, making the codebase easier to maintain and test |
| **Domain-Driven Design (DDD)** | Keeps business rules and validation inside the Domain layer through entities and value objects |
| **Domain Events** | Decouples side effects (like notifications) from core business operations, following the Single Responsibility Principle |
| **Dependency Inversion Principle (DIP)** | Ensures the Application layer depends only on abstractions while Infrastructure provides concrete implementations |
| **CQRS** | Separates write operations (Commands) from read operations (Queries), improving maintainability and preparing the project for MediatR |
| **Repository Pattern** | Abstracts data access from application logic, keeping business rules independent of Entity Framework Core |
| **Global Exception Middleware** | Centralizes exception handling and provides consistent API error responses |
| **Structured Logging (Serilog)** | Captures application events and exceptions for easier debugging |
| **Cancellation Tokens** | Propagated through asynchronous operations to support request cancellation and improve resource usage |
| **Soft Delete** | Preserves data integrity and allows recovery of accidentally deleted records |

Future Improvements

## Future Improvements

| Improvement | Description |
|-------------|-------------|
| **MediatR** | Implement mediator pattern for cleaner handler dispatch |
| **FluentValidation** | Add robust input validation |
| **Refresh Token Rotation** | Improve security with rotating refresh tokens |
| **Unit Testing** | Add comprehensive unit tests |
| **Integration Testing** | Add API integration tests |
| **Docker** | Containerize the application |
| **Redis Caching** | Add distributed caching |
| **API Versioning** | Support multiple API versions |
| **Health Checks** | Add health check endpoints |
| **OpenTelemetry** | Add distributed tracing |
| **CI/CD (GitHub Actions)** | Automate build and deployment |
| **SignalR** | Real-time notifications |

---

## Learning Objectives

This project was built to strengthen understanding of:

- Clean Architecture
- Domain-Driven Design (DDD)
- Domain Events and Event-Driven Architecture
- CQRS (Command Query Responsibility Segregation)
- SOLID Principles
- Repository Pattern
- Dependency Injection
- ASP.NET Core Web API
- JWT Authentication
- Entity Framework Core
- RESTful API Design
- Structured Logging
- Global Exception Handling


## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- PostgreSQL
- Visual Studio 2022 / VS Code / Rider

### Installation

**1. Clone the repository**
git clone https://github.com/ashura2002/SchoolSystem.git
cd school-system-api

 Update the connection string in appsettings.json
{
    "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Database=SchoolSystemDb;Username=postgres;Password=yourpassword"
    }
}

**Apply migrations**
dotnet ef database update --project src/Infrastructure --startup-project src/WebAPI

**Run the application**
dotnet run --project src/WebAPI

**Access the API**
 https://localhost:5001 or http://localhost:5000
