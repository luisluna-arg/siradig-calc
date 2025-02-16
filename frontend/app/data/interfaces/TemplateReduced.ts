import { TemplateSection } from "@/data/interfaces/TemplateSection";

export interface TemplateReduced {
  id: string;
  name: string;
  description: string;
  sections: Array<TemplateSection>;
}
