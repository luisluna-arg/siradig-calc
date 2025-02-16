import { RecordField } from "@/data/interfaces/RecordField";

export interface RecordTemplateFieldLink {
  id: string;
  leftFields: Array<RecordField>;
  rightField: RecordField;
}
