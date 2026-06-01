---
description: "Handles the full Git Flow lifecycle: updates main, branches, and commits changes in logical batches."
argument-hint: "Short description of the feature or fix, optionally followed by an issue number (e.g., 'user-auth-api 42')"
---

# Git Flow & PR Management Skill

## Workflow Steps
1. **Sync Base:** 
   * Ensure GH CLI is installed and authenticated.
   * Ensure the current branch is `main` (or the repository's default branch).
   * Execute `git pull origin main` to ensure the local environment is up to date.
2. **Branching:**
   * Create a new branch using Git Flow naming conventions: `feature/[arg]`, `fix/[arg]`, `refactor/[arg]`, or `docs/[arg]`.
   * Switch to the new branch immediately.
3. **Atomic Commits:**
   * Analyze staged/unstaged changes.
   * Verify no ignored files are accidentally staged (`git status` should not show files that belong in `.gitignore`).
   * Group related file changes into "logical batches" (e.g., all database migrations together, then all service logic). Treat `.gitignore` additions or updates as their own commit (repo config layer).
   * Commit each batch with an imperative message in the project's **commit format** `Scope: Short description` — e.g., `Backend: Add user schema`, `Frontend: Implement auth controller`, `Repo Config: Ignore log artifacts`. **Commits do NOT include a Type segment** (`Feat`/`Fix`/etc.); the Type belongs to the PR title only (step 4).
   * **Scope** is the area/layer touched (e.g., `Frontend`, `Backend`, `Infra`, `Repo Config`, `Docs`, `Tooling`).
4. **Open Pull Request:**
   * Use GH CLI: `gh pr create --title "<title>" --body-file <temp-file>`.
   * PR title must follow the project format: `Type | Scope: Short description (#N)` where Type is one of `Feat`, `Fix`, `Bug`, `Docs`, `Refactor`, `Infra`, or `Chore` — unlike commits, the PR title **adds the Type segment**. If an issue number is present in `$ARGUMENTS`, invoke `/classify_issue <issue-number>` to determine the correct type.
   * PR body must follow the template in `.github/PULL_REQUEST_TEMPLATE.md`:
     * **Purpose** — one paragraph explaining _what_ and _why_.
     * **Changes** — grouped by layer/category (e.g., `Repo Config`, `Infrastructure`, `Client Scaffold`, `Docs`). Each group is a `###` heading with bullet points.
     * **Verification** — numbered steps a reviewer can follow to manually verify the change works.
     * If an issue number was provided in `$ARGUMENTS`, append `Closes #<issue-number>` as the last line of the body so GitHub resolves the issue on merge.
   * Write the body to a temp file and pass via `--body-file` to avoid shell escaping issues (backticks, special chars).

## Constraints
* **Branch Guard:** If the current branch is not `main`, alert the user and stop — continuing might be destructive.
* **Safety Check:** If there are merge conflicts during the `git pull`, stop and alert the user.
* **Batching Intelligence:** Do not commit all files at once if they touch different layers of the stack (e.g., keep `.css` changes separate from `.cs` or `.js` backend logic). Treat `.gitignore` additions or updates as their own commit (repo config layer).
* **No Preamble:** Execute commands directly and report status only.
