---
description: "Resolve a GitHub issue end-to-end: read the issue, pick the right workflow (bug/feature/docs/refactor/infra), implement the fix, then optionally open a PR."
argument-hint: "GitHub issue number (required)"
---

# Resolve GitHub Issue Workflow

## Step 0 — Validate input

`$ARGUMENTS` must be a numeric issue number. If it is empty or non-numeric, stop immediately and tell the user:

> Usage: `/resolve-issue <issue-number>`  
> Example: `/resolve-issue 42`

Do not proceed further.

## Step 1 — Fetch the issue

Run:

```powershell
gh issue view $ARGUMENTS --json number,title,body,labels,state
```

If the command fails (issue not found, no GH auth, etc.), surface the error message and stop.

Parse the response to extract:
- **title** — the issue title
- **body** — the full issue description
- **labels** — array of label names
- **state** — must be `OPEN`; if closed, warn the user and ask whether to continue

## Step 2 — Classify the issue

Invoke `/classify_issue $ARGUMENTS`. It will fetch the issue and return the command to run (e.g. `/bug`, `/feature`, `/refactor`).

If it returns `0`, the issue type is ambiguous — ask the user to confirm the type before continuing.

## Step 3 — Execute the appropriate workflow

Execute the command returned by `/classify_issue`, passing the issue title and relevant body details as context.

Use the issue body's **Acceptance Criteria** (if present) as the definition of done. Implement the changes needed to satisfy those criteria.

## Step 4 — Ask about opening a PR

After the implementation is complete, ask the user:

> The changes for issue #$ARGUMENTS are ready. Would you like me to open a pull request?

If the user says **yes**, invoke the `/pr` workflow. The PR title must follow the project format and reference the issue number:

```
Type: Short description (#<issue-number>)
```

The PR body must close the issue automatically by including:

```
Closes #<issue-number>
```

If the user says **no**, summarize what was done and stop.
