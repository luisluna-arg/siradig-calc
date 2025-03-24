import { RecordField } from "@/data/interfaces/RecordField";

export interface TemplateSection {
  id: string,
  name: string,
  description: string,
  fields: Array<RecordField>,
  createdAt: Date,
  updatedAt: Date,
  deleted: boolean
}
