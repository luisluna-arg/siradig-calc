export interface TemplateFieldPostData {
  id?: string;
  label: string;
  fieldType: number;
  isRequired: boolean;
}

export interface TemplateSectionPostData {
  id?: string;
  name: string;
  fields: Array<TemplateFieldPostData>;
}

export interface TemplatePostData {
  id?: string;
  name: string;
  description: string;
  sections: Array<TemplateSectionPostData>;
}
