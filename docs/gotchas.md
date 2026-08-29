# Gotchas & Lessons Learned

Concrete pitfalls found in this codebase, written down so they don't get
rediscovered the hard way. Read this before touching adjacent code — each
entry names the files where the mistake actually happened.

## Frontend

### Shared "read-only" helper components must take a `disabled` prop, never hardcode it
`app/components/utils/labelInput.tsx` and `labelCombobox.tsx` used to hardcode
`disabled={true}` on their `<Input>`/`ComboBox`. They're shared by both a
display screen and an edit screen for template links
(`templateLinkFormDisplay.tsx` / `templateLinkEditForm.tsx`). Because the
prop was hardcoded, the "edit" screen was silently just as read-only as the
"display" screen — nothing was broken in event handling, the edit form
simply couldn't edit anything. If a shared component is used in both a
read-only and an editable context, `disabled` (or similar) must be a real
prop with a sane default, never baked in.

### A screen that renders like a form isn't necessarily wired to anything
`templateLinkEditForm.tsx` rendered a `<Form method="post">` with labeled
inputs, but the route had no `action`, there was no submit button, and the
backend has no endpoint to bulk-update a template link. Before assuming a
reported "button doesn't work" bug is a JS issue (event propagation, missing
`stopPropagation`, etc.), check whether the screen has a real path to the
backend at all: a submit handler / route `action`, and a matching backend
command. Grep the frontend `data/*Api.ts` client for the entity and confirm
each UI affordance has a real HTTP call backing it.

### Cross-check delete/action URLs against the actual backend route + verb, not just the shape
`ConversionsApi.deleteByIds()` built `DELETE /records/conversions/{sourceId}/to/{id}`
— which is exactly the URL *shape* of the unrelated `POST .../conversions/{sourceId}/to/{targetTemplateId}`
"create conversion" route. ASP.NET Core matches routes by path template
first; if the path matches but no action handles that HTTP verb, you get a
**405**, not a 404. A path that "looks right" isn't enough — find the actual
controller action (verb + route template) the client is supposed to hit and
match it exactly.

## Backend

### EF Core: a missing `.Include()`/`.ThenInclude()` fails silently, not loudly
Entities here initialize collection navigation properties to `= []`
(e.g. `RecordTemplate.Sections`). If a query loads the parent without
including a nested collection, that collection comes back as an **empty
array**, not null and not an error — it just looks like "no data" to
whatever reads it. `GetRecordTemplateLinkQuery` loaded `LeftTemplate`/
`RightTemplate` without `.ThenInclude(t => t.Sections).ThenInclude(s => s.Fields)`,
so the DTO's `sections` were always `[]` even though the data existed.
When a frontend screen needs nested data, trace the query's `.Include()`
chain all the way down to what the DTO actually exposes — don't assume a
`.Include(l => l.RightTemplate)` gives you everything reachable from it.

### Watch for copy-pasted filter predicates in composite-key lookups
`GetRecordTemplateLinkQuery` had:
```csharp
.FirstAsync(l => l.LeftTemplateId == request.LeftTemplateId &&
    l.LeftTemplateId == request.LeftTemplateId, ...)
```
— checking `LeftTemplateId` twice and never checking `RightTemplateId`.
Classic copy-paste-and-forget-to-rename bug. Any composite-key filter
(`X == a && Y == b`) is worth a second look for an accidentally duplicated
clause, especially right after a refactor or when the predicate was
clearly cloned from a similar query.

### FK `DeleteBehavior.Restrict` violations must be translated, not left as raw 500s
Deleting a `Record` still referenced by a `RecordConversion` correctly
fails at the DB level (`RecordTemplateConversionConfiguration.cs` sets
`DeleteBehavior.Restrict` on purpose — that's the right call). The bug was
that `ExceptionHandlingMiddleware` had no handling for `DbUpdateException`,
so the FK violation fell into the generic catch-all and came back as an
unhandled 500 with a raw Postgres error. When a DB constraint is
intentionally restrictive, the middleware/handler needs to translate that
into a clean 4xx (409 for "still referenced") with a message a user can act
on — a `Restrict` constraint firing is expected domain behavior, not a
server error.

## Working with this repo (agent-specific)

### Never run mutating commands against the user's live containers as "verification"
Rebuilding a Docker image and restarting a container to confirm a fix
builds/starts is fine. Actually calling the API to create/delete real rows,
or running write queries via `docker exec ... psql`, against the user's
running dev environment is **not** something to do unprompted — it mutates
real data outside version control and the user may not want that, even in
a local/dev database. Default verification is `dotnet build`, `npm run
build`, and `npm run lint`; only hit a live endpoint with a request that
changes data if the user has explicitly said to.
