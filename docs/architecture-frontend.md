# Frontend Architecture

The frontend is a **Remix** application with server-side rendering. Data fetching and mutations happen in Remix `loader` and `action` functions on the server, which call the backend API via Axios.

## Routing

Routes live under `frontend/app/routes/` using Remix's dot-notation file naming:

| Route file | URL | Purpose |
|---|---|---|
| `_index.tsx` | `/` | Redirects to `/records` |
| `records._index.tsx` | `/records` | Records list |
| `records.add.tsx` | `/records/add` | Create record |
| `records.$id.tsx` | `/records/:id` | View/edit record |
| `records.templates._index.tsx` | `/records/templates` | Templates list |
| `records.templates.add.tsx` | `/records/templates/add` | Create template |
| `records.templates.$id.tsx` | `/records/templates/:id` | View/edit template |
| `records.templates.links._index.tsx` | `/records/templates/links` | Template links list |
| `records.templates.links.$leftTemplateId.to.$rightTemplateId.tsx` | `/records/templates/links/:left/to/:right` | Edit field mappings between two templates |
| `records.conversions._index.tsx` | `/records/conversions` | Conversions list |
| `records.conversions.$id.tsx` | `/records/conversions/:id` | View conversion result |

## Data layer

API clients live in `frontend/app/data/`. All Axios calls go through these classes — never directly in components or loaders.

```
data/
├── ApiClientProvider.ts    # Instantiates and exports all API clients
├── EntityApi.ts            # Base class: post(), put(), delete()
├── EntityReadApi.ts        # Base class: get(), getAll()
├── RecordsApi.ts
├── TemplatesApi.ts
├── TemplateLinksApi.ts
├── ConversionsApi.ts
├── CatalogsApi.ts
└── *.ts                    # Type definitions (Record, Template, RecordValue, etc.)
```

## Component structure

```
components/
├── forms/          # Full-page forms for creating/editing entities
│   ├── record/
│   ├── template/
│   ├── templateLink/
│   └── recordConversion/
├── grids/          # Data table components
│   ├── recordsGrid.tsx
│   ├── templatesGrid.tsx
│   ├── templateLinksGrid.tsx
│   └── recordsConversionGrid.tsx
├── ui/             # Primitive UI components (Radix UI wrappers)
│   ├── button.tsx, dialog.tsx, table.tsx, tabs.tsx, ...
└── utils/          # Composite UI utilities
    ├── comboBox.tsx, labelInput.tsx, toolbar.tsx, ...
```
