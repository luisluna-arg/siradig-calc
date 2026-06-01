# Backend Architecture

The backend follows **Clean Architecture** with four projects:

## `backend/Core` — Domain

Pure domain entities with no dependencies. All EF Core configuration lives in `Infra`, not here.

## `backend/Application` — Business Logic

CQRS pattern using MediatR. Every operation is either a **Command** (mutation) or a **Query** (read).

```
Application/
├── Commands/   # Write operations (Create, Update, Delete, Convert, Link/Unlink)
├── Queries/    # Read operations (Get, List, Catalog)
├── Dtos/       # Request and response shapes
├── Mapping/    # AutoMapper profiles
├── Validation/ # FluentValidation validators (one per command)
├── Converters/ # Record conversion logic (IRecordConverter, value reduction strategies)
└── Common/     # Pipeline behaviors (validation middleware)
```

**Command flow:**
```
Controller → MediatR.Send(Command) → Validator → Handler → EF Core → Response
```

**Record conversion** (`ConvertRecordCommand`) works by:
1. Loading the source record with all its values.
2. Loading the target template and the field mappings between templates.
3. For each target field, finding all source fields that map to it.
4. Aggregating mapped values using `ValuesReducerStrategy` (numeric addition by default).
5. Persisting the result as a `RecordConversion`.

## `backend/ApiFramework` — Shared Infrastructure

Middleware, exception handling, and common API configuration shared across controllers.

## `backend/Api` — Presentation

Thin ASP.NET Core controllers. Each controller action does one thing: build the request object and call `MediatR.Send()`.

Controllers are split by resource and by operation type (Query vs Command) to keep files small:

| Controller | Responsibility |
|---|---|
| `RecordTemplatesQueryController` | GET endpoints for templates, links, field types |
| `RecordTemplatesCommandController` | POST/PUT/DELETE for templates, sections, fields, links |
| `RecordsQueryController` | GET endpoints for records and conversions |
| `RecordCommandController` | POST/PUT/DELETE for records, values, conversions |
| `CatalogQueryController` | Lightweight catalog lists used to populate dropdowns |

**OpenAPI / Swagger** is generated automatically and served at `/swagger` in development.
