# AGENTS.md

## Tech Stack

### Frontend
* **Framework:** Remix (file-based routing under `frontend/app/routes/`)
* **Language:** TypeScript
* **UI Components:** Radix UI primitives + Tailwind CSS (shadcn/ui patterns via `clsx`/`class-variance-authority`)
* **HTTP Client:** axios via the data layer (`frontend/app/data/`)
* **Build Tool:** Vite

### Backend
* **Platform:** .NET / C#
* **Architecture:** Clean Architecture
* **API:** ASP.NET Core Web API (`backend/Api`) — controllers only, no business logic
* **Application:** CQRS via MediatR — Commands and Queries (`backend/Application`)
* **Domain:** Core entities (`backend/Core/Entities`)
* **Persistence:** EF Core + PostgreSQL (`backend/Infra`)

## Component Standards
* **Structure:** One component per file under `frontend/app/components/`. Group by concern (e.g., `forms/`, `grids/`, `utils/`).
* **Naming:** PascalCase for components and files. camelCase for hooks and utilities.
* **Props:** Define all props using `interface` or `type`. Prefer explicit destructuring in the function signature.
* **Styling:** Tailwind CSS utility classes. Use `cn()` for conditional class composition.

## Coding Patterns

### Frontend
* **Data Fetching:** API calls live in `frontend/app/data/` (e.g., `CatalogsApi.ts`). Do not call axios directly inside components or loaders — always go through the data layer.
* **Routing:** Routes follow Remix dot-notation under `frontend/app/routes/` (e.g., `records.templates.links.$leftTemplateId.to.$rightTemplateId.tsx`).
* **Typing:** Strict TypeScript. No `any`. Use `Record`, `Pick`, or `Omit` for object shapes.
* **Logic:** Use early returns to avoid deeply nested conditionals in JSX.

### Backend
* **CQRS:** All business logic goes through Commands (mutations) or Queries (reads) in `backend/Application`. Controllers dispatch to MediatR and return results — nothing else.
* **Validation:** FluentValidation validators live in `backend/Application/Validation/`, co-located by feature.
* **Persistence:** EF Core DbContext and entity configurations live in `backend/Infra`. All migrations go in `backend/Infra/Persistence/Migrations/`.
* **DTOs:** Define request/response shapes in `backend/Application/Dtos/`. Map between entities and DTOs using AutoMapper profiles in `backend/Application/Mapping/`.
* **Interface/class co-location:** When an interface `IFoo` is implemented by exactly one class `Foo`, place both in the same file named `Foo.cs`. The interface goes at the top, the class below.
* **Empty interfaces:** Use the compact semicolon form — `public interface IFoo : IBar;` — instead of an empty brace body.

## Key Commands

### Frontend
Run from the `frontend/` directory:
* **Install:** `npm install`
* **Development:** `npm run dev`
* **Build:** `npm run build`
* **Lint:** `npm run lint`

### Backend
Run from the repo root:
* **Run API:** `dotnet run --project backend/Api`
* **Build solution:** `dotnet build backend/siradig-calc.sln`
* **Test:** `dotnet test backend/siradig-calc.sln` (unit tests live in `backend/Tests`)

### Migrations
Run from `backend/Infra/`:
* **Add:** `dotnet ef migrations add <MigrationName> -o Persistence/Migrations --startup-project ../Api`
* **Update DB:** `dotnet ef database update --startup-project ../Api`

See `backend/Infra/README.md` for the full migration reference including rollback commands.

## Formatting & Linting
* **Frontend:** Arrow functions for components. Group imports: React/Remix first, then external libraries, then internal components/utils, then styles.
* **Backend:** Follow existing C# conventions. Use `var` where the type is obvious from the right-hand side.

## General Guidelines
* Do not add "Co-Authored-By:" to commits or pull requests.
* Refer to `docs/architecture.md` for the domain model and system design.
* Refer to `docs/api.md` for the REST API endpoint reference.

## Agent Workflow
* Before suggesting new frontend libraries, check `frontend/package.json` for existing dependencies and versions.
* When adding a new backend feature, follow the CQRS pattern: Command/Query + Handler → registered in `Application` → thin Controller action in `Api`.
* When adding a new EF Core migration, use the command from `backend/Infra/README.md` and run it from the `backend/Infra/` directory.

## Git Rules
* **Never run `git commit`, `git push`, or any destructive git command (`reset`, `rebase`, `force-push`) unless explicitly instructed by the user.**
* Staging files (`git add`) is permitted as part of preparing a commit, but the commit itself must wait for instruction.
* When a skill or slash command (e.g. `/pr`) instructs committing and pushing as part of its workflow, that counts as explicit instruction — no additional confirmation needed.
