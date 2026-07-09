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

## Project Structure
