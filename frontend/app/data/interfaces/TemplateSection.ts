import { RecordField } from "@/data/interfaces/RecordField";

export interface TemplateSection {
  name: string,
  description: string,
  fields: Array<RecordField>,
  id: string,
  createdAt: Date,
  updatedAt: Date,
  deleted: boolean
}
