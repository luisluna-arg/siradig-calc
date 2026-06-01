---
description: "Fix a bug: apply the minimal targeted fix. Use when correcting incorrect behavior, crashes, or regressions."
argument-hint: "Describe the bug and its symptoms"
---

# Bug Fix Workflow

## Step 1 — Understand the bug before writing code

Before making any changes:
- Identify the root cause, not just the symptom.
- Locate the affected file(s) and the exact code responsible.
- State the fix plan in one sentence before proceeding.

## Step 2 — Apply the fix

Keep the fix minimal and targeted — only change what is necessary to correct the bug. Do not refactor unrelated code.

If the fix spans multiple concerns (e.g. a data layer bug that also requires a UI guard), address each layer separately in order.

## Step 3 — Update documentation

After the fix is done, review whether any of the following need updating:

- **`AGENTS.md`** — if the fix changes a convention or behavior agents should know about, update it accordingly.
- **`backend/Infra/README.md`** — if the fix affects the migration workflow.

If nothing needs updating, skip this step.
