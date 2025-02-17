import { Template } from "@/data/interfaces/Template";
import { RecordValue } from "@/data/interfaces/RecordValue";
import { RecordTemplateFieldLink } from "./RecordTemplateFieldLink";

export interface RecordTemplateLink {
  id: string;
  sourceTemplateId: string;
  targetTemplateId: string;
}
