import { Template } from "@/data/interfaces/Template";
import { RecordTemplateFieldLink } from "@/data/interfaces/RecordTemplateFieldLink";

export interface TemplateLink {
  id: string;
  leftTemplate: Template;
  rightTemplate: Template;
  recordTemplateFieldLinks: Array<RecordTemplateFieldLink>;
}
