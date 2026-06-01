# Architecture

## Overview

siradig-calc is split into a .NET backend (REST API + business logic + persistence) and a Remix frontend (server-side-rendered React app). They communicate over HTTP; the frontend calls the backend API from Remix server-side loaders and actions.

```
Browser ──► Remix server ──► ASP.NET Core API ──► PostgreSQL
                │                    │
            React SSR            MediatR (CQRS)
```

See [architecture-backend.md](architecture-backend.md) and [architecture-frontend.md](architecture-frontend.md) for details.

---

## Domain Model

### Core concepts

| Entity | Description |
|---|---|
| `RecordTemplate` | A named form structure with sections and typed fields. Represents a salary receipt format (e.g., UNPSJB payroll slip, SIRADIG F572). |
| `RecordTemplateSection` | A named group of fields within a template (e.g., "Haberes", "Deducciones"). |
| `RecordTemplateField` | A single field in a section. Has a label, a `FieldTypeMapping` (numeric, text, etc.), and ordering. |
| `Record` | A set of payroll values entered against a specific template. Represents one pay period for one person/organization. |
| `RecordValue` | The actual value for one field in a record. |
| `RecordTemplateLink` | A bidirectional relationship between two templates, enabling conversion. |
| `RecordTemplateFieldLink` | Maps a field in the left template to a field in the right template. Multiple source fields can map to one target field (values are aggregated). |
| `RecordConversion` | The result of converting a `Record` from its template to a linked template. Stores the converted values. |

### Relationships

```
RecordTemplate ──has many──► RecordTemplateSection
                                    │
                             ──has many──► RecordTemplateField
                                                │
                                         FieldTypeMapping

RecordTemplate ◄──RecordTemplateLink──► RecordTemplate
                         │
                  RecordTemplateFieldLink (field-level mappings)

Record ──belongs to──► RecordTemplate
  │
  ──has many──► RecordValue (one per RecordTemplateField)

Record ──has many──► RecordConversion (one per target template it was converted to)
```

---

## Key design decisions

- **No auth** — the system is designed for trusted internal use. CORS is open (`AllowAll`). Add an auth layer (e.g., ASP.NET Core Identity, Auth0) before exposing publicly.
- **Bidirectional template links** — a `RecordTemplateLink` between A and B enables conversion in both directions. Field mappings are stored once and applied symmetrically.
- **Additive conversion** — when multiple source fields map to one target field, their numeric values are summed. This handles cases where a single employer deduction line corresponds to several individual line items in a government form.
- **SSR-first frontend** — Remix loaders keep API credentials and data-fetching logic on the server. The browser only receives rendered HTML and client-side navigation payloads.
