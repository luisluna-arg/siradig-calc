import { TemplateSection } from "@/data/interfaces/TemplateSection";

export interface Template {
  id: string;
  name: string;
  description: string;
  sections: Array<TemplateSection>;
}
