# API Reference

Base URL: `http://localhost:5000`

Swagger UI: `http://localhost:5000/swagger`

All responses are JSON. Error responses follow a standard shape with an `errors` array.

---

## Records

### List records

```
GET /api/records
```

**Query params**

| Param | Type | Description |
|---|---|---|
| `templateId` | GUID | Filter by template |

**Response** — array of `RecordDto`

---

### Get record

```
GET /api/records/{recordId}
```

Returns the record with all its values populated.

---

### Create record

```
POST /api/records
```

**Body**

```json
{
  "title": "UNPSJB Agosto 2024",
  "templateId": "<guid>",
  "values": [
    { "fieldId": "<guid>", "value": "150000" }
  ]
}
```

---

### Update record

```
PUT /api/records/{recordId}
```

**Body** — same shape as create. Replaces all values.

---

### Delete record

```
DELETE /api/records/{recordId}
```

---

### Create record value

```
POST /api/records/value
```

Adds a single value to an existing record.

**Body**

```json
{
  "recordId": "<guid>",
  "fieldId": "<guid>",
  "value": "12500.50"
}
```

---

### Delete record value

```
DELETE /api/records/value/{valueId}
```

---

### Import from PDF

```
POST /api/records/import/pdf
Content-Type: multipart/form-data
```

Parses a payroll receipt PDF with a two-column table layout (label on the left,
amount on the right), extracting the monetary line items grouped by section.
Section headers (rows without a numeric value) become section boundaries, and
totals/subtotals — rows whose label contains `TOTAL`, `NETO`, or `SUBTOTAL`, or
whose value equals the running sum of its section — are skipped.

**Form fields**

| Field | Type | Required | Description |
|---|---|---|---|
| `file` | binary | yes | The PDF file |
| `generateTemplate` | bool | no (default `false`) | Also create a `RecordTemplate` when no match exists |

**Template matching** — the extracted field labels are compared (case-insensitive,
trimmed) against every existing template's field set. A template matches when the
extracted labels are a subset of (or equal to) its field labels.

- When a template matches, it is returned in `templateMatch` and no template is created.
- When `generateTemplate=true` and no template matches, a new `RecordTemplate` is
  created from the extracted sections (all fields numeric) and returned.

**Response**

```json
{
  "templateMatch": {
    "found": true,
    "created": false,
    "templateId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
    "templateName": "Template Name"
  },
  "sections": [
    {
      "name": "Section A",
      "entries": [
        { "label": "Line item 1", "value": 1000.00 },
        { "label": "Line item 2", "value": 250.50 }
      ]
    }
  ]
}
```

`found` is `true` when an existing template matched; `created` is `true` when a
new template was generated for this import.

---

## Record Conversions

Conversions transform a record's values into a different template's field structure using the field mappings defined in a template link.

### List conversions

```
GET /api/records/conversions
```

**Query params**

| Param | Type | Description |
|---|---|---|
| `sourceId` | GUID | Filter by source record |

---

### Get conversion

```
GET /api/records/conversions/{conversionId}
```

---

### Get conversions between two templates

```
GET /api/records/conversions/{sourceId}/to/{targetId}
```

Returns all conversions where the source record uses `sourceId` template and the target uses `targetId` template.

---

### Convert a record

```
POST /api/records/conversions/{sourceId}/to/{targetTemplateId}
```

Converts record `sourceId` into `targetTemplateId`. Requires a template link with field mappings to exist between the two templates.

Returns the created `RecordConversionDto`.

---

### Delete a conversion

```
DELETE /api/records/{sourceId}/conversions/{conversionId}
```

---

### Delete all conversions for a record

```
DELETE /api/records/{sourceId}/conversions
```

---

## Record Templates

### List templates

```
GET /api/records/templates
```

---

### Get template

```
GET /api/records/templates/{id}
```

Returns the template with all sections and fields.

---

### Create template

```
POST /api/records/templates
```

**Body**

```json
{
  "name": "UNPSJB Recibo",
  "description": "Template for UNPSJB payroll receipts"
}
```

---

### Update template

```
PUT /api/records/templates/{recordTemplateId}
```

**Body** — same shape as create.

---

### Delete template

```
DELETE /api/records/templates/{recordTemplateId}
```

---

### Add section

```
POST /api/records/templates/{recordTemplateId}/section
```

**Body**

```json
{
  "name": "Haberes",
  "order": 1
}
```

---

### Add field to section

```
POST /api/records/templates/{recordTemplateId}/section/{sectionId}/field
```

**Body**

```json
{
  "label": "Sueldo Básico",
  "fieldTypeId": "<guid>",
  "order": 1
}
```

---

### Delete field

```
DELETE /api/records/templates/field/{fieldId}
```

---

### Get field types

```
GET /api/records/templates/field-types
```

Returns all available field types (e.g., numeric, text).

---

## Template Links

Template links connect two templates so their records can be converted between them.

### List all links

```
GET /api/records/templates/links
```

---

### Get link between two templates

```
GET /api/records/templates/links/{leftTemplateId}/to/{rightTemplateId}
```

Returns the link including all field-level mappings.

---

### Create link

```
POST /api/records/templates/link/{leftId}/to/{rightId}
```

Creates a bidirectional link between two templates. No body required.

---

### Delete link

```
DELETE /api/records/templates/link/{leftId}/to/{rightId}
```

---

### Map fields between linked templates

```
POST /api/records/templates/link/{leftId}/to/{rightId}/{leftFieldId}/to/{rightFieldId}
```

Adds a field mapping: `leftFieldId` in the left template corresponds to `rightFieldId` in the right template. Multiple left fields can map to the same right field — their values will be summed during conversion.

---

### Remove field mapping

```
DELETE /api/records/templates/link/{leftId}/to/{rightId}/{leftFieldId}/to/{rightFieldId}
```

---

## Catalog

Lightweight endpoints for populating dropdowns and select inputs.

### Records catalog

```
GET /api/catalog/records
```

Returns `[{ id, label }]` for all records.

---

### Templates catalog

```
GET /api/catalog/templates
```

Returns `[{ id, label }]` for all templates.

---

### Field types catalog

```
GET /api/catalog/field-types
```

Returns `[{ id, label }]` for all field types.
