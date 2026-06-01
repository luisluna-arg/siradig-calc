---
description: "Make infrastructure changes safely: create a branch first and apply changes in logically grouped commits. Use when modifying docker-compose files, CI/CD workflows, Dockerfiles, or any infrastructure configuration."
argument-hint: "Describe what infra change needs to be made"
---

# Infra Changes Workflow

Before touching any files, set up a branch. Then apply changes in logical batches, each with its own commit.

## Step 0 — Pre-flight checks

Run the following checks before doing anything else. If any check fails, stop and inform the user — do not proceed.

1. **Check for uncommitted or unstaged changes**:
   ```
   git status --porcelain
   ```
   If the output is non-empty, cancel with:
   > "There are uncommitted or unstaged changes in the working tree. Please commit, stash, or discard them before proceeding with infra changes."

2. **Ensure you are on `main`**:
   ```
   git branch --show-current
   ```
   If the current branch is not `main`, cancel with:
   > "You are currently on branch `<branch>`, not `main`. Please switch to `main` before starting infra changes."

3. **Pull the latest `main`**:
   ```
   git pull origin main
   ```
   Confirm the pull succeeded before continuing.

## Step 1 — Create a branch

Derive a short, descriptive branch name from the user's request using the prefix `infra/` (e.g. `infra/split-deploy-jobs`, `infra/add-ssl-cert`).

Run:
```
git checkout -b <branch-name>
```

Confirm the branch was created before proceeding.

## Step 2 — Apply changes in logical batches

Group the planned changes by concern. Each group becomes one commit. Typical groupings for this repo:

- **Repo config** — `.gitignore` updates for new build artifacts or tooling outputs (always committed first, in isolation)
- **CI/CD** — files under `.github/workflows/`
- **Container config** — `Dockerfile` and any variant Dockerfiles
- **Compose / infra** — docker-compose files and any reverse proxy or service mesh config
- **Mixed** — if a change spans groups, split by layer (e.g. compose changes first, then workflow changes that depend on them)

For each batch:
1. Make the file edits.
2. Stage only the files for that batch: `git add <files>`
3. Commit with the project's **commit format** `Scope: Short description` (no Type prefix): `git commit -m "Infra: <what and why>"`

Do not mix unrelated files in a single commit.

## Step 3 — Update documentation

After all infra changes are committed, review whether any of the following need updating:

- **`AGENTS.md`** — if the change introduces new services, ports, environment variables, or deployment steps agents should be aware of, update it accordingly.
- **`backend/Infra/README.md`** — if the change affects the migration or database workflow.

If updates are needed, commit them as a separate batch: `git commit -m "Docs: Update AGENTS.md for <change>"`
If nothing needs updating, skip this step.
