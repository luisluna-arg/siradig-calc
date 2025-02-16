import { Template } from "@/data/interfaces/Template";
import { RecordTemplateFieldLink } from "./RecordTemplateFieldLink";

export interface TemplateLink {
  id: string;
  leftTemplate: Template;
  rightTemplate: Template;
  recordTemplateFieldLinks: Array<RecordTemplateFieldLink>;
}
