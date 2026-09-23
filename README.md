# TaskFlow

TaskFlow is a multi-tenant project and task management system (similar in spirit to Trello/Asana), built from scratch in ASP.NET Core to demonstrate Clean Architecture, JWT authentication, and a full MVC frontend that consumes its own REST API.

## Features

- **Workspaces** with multiple members and role-based access (Owner/Admin/Member)
- **Projects** and **Tasks** with status tracking
- **Comments** on tasks
- JWT-based authentication, used by both the API and the MVC frontend (via a secure HttpOnly cookie)
- Full CRUD through a REST API, documented with Swagger
- Unit tests covering domain logic and repository mocking

## Architecture

TaskFlow follows Clean Architecture, split into four projects with a strict, inward-only dependency direction:

```
TaskFlow.Web            → MVC + Web API (controllers, views, JWT issuing)
TaskFlow.Infrastructure → EF Core, Identity, repository implementations
TaskFlow.Application    → interfaces, DTOs, mapping (no framework dependencies)
TaskFlow.Domain         → entities and business rules (zero dependencies)
```

`TaskFlow.Domain` has no project references at all — it doesn't know EF Core, ASP.NET Core, or anything outside plain C# exists. Every entity protects its own invariants through private setters and guarded methods (e.g. `TaskItem.Rename()`, `TaskItem.MoveTo()`) rather than exposing public setters.

## Tech stack

- ASP.NET Core 8 (MVC + Web API)
- Entity Framework Core + SQL Server
- ASP.NET Core Identity
- JWT Bearer authentication
- Swagger / Swashbuckle
- xUnit + Moq

## Key design decisions

- **JWT everywhere, not just the API.** The MVC frontend calls its own Web API over HTTP, attaching a JWT stored in a secure, HttpOnly, SameSite cookie — never in `localStorage`, to reduce exposure to XSS. This mirrors a Backend-for-Frontend (BFF) pattern.
- **Unit of Work over per-repository saves.** Repositories only stage changes; `IUnitOfWork.SaveChangesAsync()` is the single commit point, so multi-entity operations (e.g. creating a workspace and its owner membership together) succeed or fail as one unit.
- **DTOs at every boundary.** Domain entities never cross into a controller response — mapping extension methods (`entity.ToDto()`) convert them first, keeping persistence details out of the API contract.

## Running locally

1. Clone the repo
2. Update the connection string in `TaskFlow.Web/appsettings.json` if your SQL Server instance differs from `(localdb)\mssqllocaldb`
3. Set the JWT signing key via User Secrets rather than committing it:
   ```
   dotnet user-secrets set "Jwt:Key" "your-own-development-secret" --project TaskFlow.Web
   ```
4. Apply migrations:
   ```
   dotnet ef database update --project TaskFlow.Infrastructure --startup-project TaskFlow.Web
   ```
5. Run `TaskFlow.Web` — Swagger is available at `/swagger`, the MVC app at `/`

## Testing

```
dotnet test
```

Covers domain entity validation rules and a mocked repository test using Moq.

## Project status

Built as a structured learning project covering the full ASP.NET Core stack end to end: Domain modeling → EF Core → Identity/JWT → Web API → MVC → cross-cutting concerns → testing.
