import { TemplateLinkReduced } from "@/data/interfaces/TemplateLinkReduced";
import { TemplateSection } from "@/data/interfaces/TemplateSection";

export interface Template {
  name: string;
  description: string;
  links: Array<TemplateLinkReduced>;
  sections: Array<TemplateSection>;
  id: string;
  createdAt: Date;
  updatedAt: Date;
  deleted: boolean;
}
