import { Template } from "@/data/interfaces/Template";
import { RecordValue } from "@/data/interfaces/RecordValue";

export interface Record {
  templateId: string;
  template: Template;
  title: string;
  values: Array<RecordValue>;
  convertedTo: Array<any>;
  convertedFrom: Array<any>;
  id: string;
  createdAt: Date;
  updatedAt: Date;
  deleted: boolean;
}
