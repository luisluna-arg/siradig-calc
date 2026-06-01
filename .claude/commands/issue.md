---
description: "Create a GitHub issue: draft and submit a well-structured issue via GH CLI. Use for bug reports, feature requests, refactor tasks, or any backlog item."
argument-hint: "Short description of what the issue is about (e.g., 'reorganize unit tests', 'add Redis caching to sessions')"
---

# Issue Creation Workflow

## Step 1 — Determine issue type and scope

Identify the type from the argument or context:
- **bug** — incorrect behavior, crash, or regression
- **feature** — new functionality
- **refactor** — structural or code quality improvement with no behavior change
- **chore** — maintenance, dependency updates, config changes
- **docs** — documentation only

## Step 2 — Draft the issue body

Write a body with the following sections (omit sections that don't apply):

```
## Summary
One paragraph: what this issue is about and why it matters.

## Current Behavior / State
Describe the current situation (for bugs: what happens; for refactors: current structure).

## Proposed Change
Describe what the result should look like after the issue is resolved.
Include code snippets, directory trees, or examples where helpful.

## Acceptance Criteria
- [ ] Specific, testable outcome 1
- [ ] Specific, testable outcome 2
```

## Step 3 — Choose a title

Title must follow the project format: `Type | Scope: Short description`

Where `Type` is one of: `Feat`, `Fix`, `Bug`, `Docs`, `Refactor`, `Infra`, `Chore`

Examples:
- `Bug | {Scope}: Token refresh loop on expired session`
- `Refactor | {Scope}: Move unit tests out of app/ into tests/`
- `Feat | {Scope}: Add Redis-backed session expiry`

## Step 4 — Submit via GH CLI

Write the body to a temp file to avoid shell escaping issues, then run:

```powershell
$body = @'
<body content>
'@
$body | Set-Content "$env:TEMP\issue-body.md"
gh issue create --title "<title>" --body-file "$env:TEMP\issue-body.md"
```

Report the created issue URL when done.

## Constraints

- Do not add labels, assignees, or milestones unless the user asks.
- Keep the body concise — avoid padding or filler prose.
- Never guess technical details; if scope is unclear, ask one focused question before drafting.
