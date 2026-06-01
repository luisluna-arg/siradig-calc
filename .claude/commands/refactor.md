---
description: "Refactor existing code: apply changes in logically grouped batches. Use when restructuring, renaming, extracting, or improving code quality without changing observable behavior."
argument-hint: "Describe what is being refactored and why (e.g., 'extract auth logic into service layer')"
---

# Refactor Workflow

A refactor must not change observable behavior — if behavior changes are required, they belong in a separate feature or fix.

## Step 0 — Classify (if invoked with an issue number)

If `$ARGUMENTS` contains an issue number, invoke `/classify_issue <issue-number>` to fetch the issue and confirm it is a refactor. Use the issue body's **Acceptance Criteria** as the definition of done.

## Step 1 — Apply changes in logical batches

Group changes by concern and apply them in order. Typical groupings for this repo:

- **Repo config** — `.gitignore` or path alias changes required by the refactor (always done first, in isolation)
- **Backend: Domain** — entity restructuring or renaming in `backend/Core/`
- **Backend: Application** — handler consolidation, DTO cleanup, mapping updates in `backend/Application/`
- **Backend: Infra** — EF Core configuration or migration-related restructuring in `backend/Infra/`
- **Backend: API** — controller cleanup, thinning handlers in `backend/Api/`
- **Frontend** — component extraction, prop interface cleanup, data-layer reorganization in `frontend/app/`

Do not mix unrelated layers in a single batch. Do not introduce new behavior.

## Step 2 — Update documentation

After all code changes are done, review whether any of the following need updating:

- **`AGENTS.md`** — if the refactor changes a convention, file pattern, or structural rule agents should follow, update it accordingly.

If nothing needs updating, skip this step.
