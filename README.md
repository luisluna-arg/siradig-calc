# siradig-calc

A payroll record management system for Argentine universities and organizations. It allows creating structured salary templates, entering payroll records against those templates, and converting records between different template formats using configurable field mappings.

## What it does

Organizations use different salary receipt formats (e.g., UNPSJB payroll slips, SIRADIG F572 income-tax declarations, Patagonian employer formats). **siradig-calc** lets you:

- Define **Record Templates** — structured forms with sections and typed fields (e.g., "Haberes" and "Deducciones" sections).
- Enter **Records** — actual payroll data for a given month against a template.
- Link two templates together with **field mappings**, so corresponding fields are associated across formats.
- **Convert** a record from one template to another using those mappings, aggregating values when multiple source fields map to a single target field.

## Tech Stack

| Layer | Technology |
|---|---|
| Frontend | Remix · React 18 · TypeScript · Tailwind CSS · Radix UI · Axios |
| Backend | ASP.NET Core 9 · C# · CQRS (MediatR) · FluentValidation |
| Database | PostgreSQL · EF Core 9 |

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js ≥ 20](https://nodejs.org/)
- PostgreSQL instance

## Running locally

### Backend

```bash
# From repo root
dotnet run --project backend/Api
```

The API starts at `http://localhost:5000`. Swagger UI is available at `/swagger`.

Set the database connection string in `backend/Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "PostgresDb": "Host=localhost;Database=siradig;Username=...;Password=..."
  }
}
```

**Migrations** (run from `backend/Infra/`):

```bash
dotnet ef database update --startup-project ../Api
```

See [`backend/Infra/README.md`](backend/Infra/README.md) for the full migration reference.

### Frontend

```bash
cd frontend
npm install
npm run dev
```

The app starts at `http://localhost:5173`. Configure the API base URL in `frontend/.env`:

```
API_URL=http://localhost:5000
```

## Project structure

```
siradig-calc/
├── backend/
│   ├── Api/            # ASP.NET Core controllers, entry point
│   ├── ApiFramework/   # Shared middleware and configuration
│   ├── Application/    # CQRS commands, queries, DTOs, validators
│   ├── Core/           # Domain entities
│   └── Infra/          # EF Core DbContext, configurations, migrations
├── frontend/
│   └── app/
│       ├── routes/     # Remix file-based routes
│       ├── components/ # UI components (forms, grids, primitives)
│       ├── data/       # Axios API clients and type definitions
│       └── utils/      # Shared helpers
├── .claude/            # Claude Code configuration and slash commands
└── .github/            # PR template
```

## Documentation

- [`docs/architecture.md`](docs/architecture.md) — domain model, backend layers, frontend structure
- [`docs/api.md`](docs/api.md) — REST API endpoint reference
- [`backend/Infra/README.md`](backend/Infra/README.md) — EF Core migration commands
