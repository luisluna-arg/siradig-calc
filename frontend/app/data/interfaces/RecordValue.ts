import { RecordField } from "@/data/interfaces/RecordField";

export interface RecordValue {
  recordId: string;
  fieldId: string;
  field: RecordField;
  value: string;
  id: string;
  createdAt: Date;
  updatedAt: Date;
  deleted: boolean;
}
