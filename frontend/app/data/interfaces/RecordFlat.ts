import { RecordValue } from "@/data/interfaces/RecordValue";

export interface RecordFlat {
  id: string;
  description: string;
  name: string;
  recordTemplateId: string;
  title: string;
  values: Array<RecordValue>
}
