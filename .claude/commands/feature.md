---
description: "Implement a new feature: apply changes in logically grouped batches. Use when adding new functionality, routes, components, services, or any non-bug-fix work."
argument-hint: "Describe the feature to implement"
---

# New Feature Workflow

Group the planned changes by concern and apply them in order.

## Step 1 — Apply changes in logical batches

Typical groupings for this repo:

- **Repo config** — `.gitignore` or `.env.example` changes needed by the feature (always done first, in isolation)
- **Backend: Domain** — new or updated entities in `backend/Core/Entities/`
- **Backend: Application** — Commands, Queries, DTOs, validators, AutoMapper profiles in `backend/Application/`
- **Backend: Infra** — EF Core migrations, entity configurations in `backend/Infra/`
- **Backend: API** — thin controller actions in `backend/Api/Controllers/`
- **Frontend** — routes, components, data layer (`frontend/app/`)
- **Infrastructure** — docker-compose, CI/CD, or environment changes

Do not mix unrelated layers in a single batch.

## Step 2 — Update documentation

After all code changes are done, review whether any of the following need updating:

- **`AGENTS.md`** — if the feature introduces new conventions, commands, file patterns, or behavior agents should be aware of, update it accordingly.
- **`backend/Infra/README.md`** — if the feature requires a new EF Core migration.

If nothing needs updating, skip this step.
