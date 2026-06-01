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
