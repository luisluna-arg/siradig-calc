import { Template } from "@/data/interfaces/Template";

export interface TemplateLink {
  id: string;
  leftTemplate: Template;
  rightTemplate: Template;
}
