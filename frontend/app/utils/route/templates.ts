import {
  TemplateFieldPostData,
  TemplatePostData,
  TemplateSectionPostData,
} from "./interfaces/TemplatePostData";

export function buildTemplateSubmitData(formData: any) {
  let postData: TemplatePostData = {
    name: formData.get("name"),
    description: formData.get("description"),
    sections: [],
  };

  let sectionIndex = 0;
  let fieldIndex = 0;

  function getSectionSelector(sectionIndex: number) {
    return `section[${sectionIndex}]`;
  }

  function getFieldSelector(sectionIndex: number, index: number) {
    return `${getSectionSelector(sectionIndex)}.fields[${index}]`;
  }

  let sectionSelector = getSectionSelector(sectionIndex);

  while (formData.has(`${sectionSelector}.name`)) {
    const section: TemplateSectionPostData = {
      name: formData.get(`${sectionSelector}.name`),
      fields: [],
    };

    let fieldSelector = getFieldSelector(sectionIndex, fieldIndex);
    while (formData.has(`${fieldSelector}.label`)) {
      const field: TemplateFieldPostData = {
        fieldType: formData.get(`${fieldSelector}.fieldType`),
        isRequired: formData.get(`${fieldSelector}.isRequired`),
        label: formData.get(`${fieldSelector}.label`),
        placeholder: formData.get(`${fieldSelector}.placeholder`),
      };

      section.fields.push(field);

      fieldIndex++;
      fieldSelector = getFieldSelector(sectionIndex, fieldIndex);
    }
    fieldIndex = 0;
    sectionIndex++;
    sectionSelector = getFieldSelector(sectionIndex, fieldIndex);
  }

  return postData;
}
