import {
  TemplateFieldPostData,
  TemplatePostData,
  TemplateSectionPostData,
} from "@/utils/route/interfaces/TemplatePostData";

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
    let sectionId = formData.get(`${sectionSelector}.id`);
    if (sectionId.indexOf("new-") >= 0) {
      sectionId = undefined;
    }

    const section: TemplateSectionPostData = {
      id: sectionId,
      name: formData.get(`${sectionSelector}.name`),
      fields: [],
    };

    let fieldSelector = getFieldSelector(sectionIndex, fieldIndex);

    while (formData.has(`${fieldSelector}.label`)) {
      let fieldId = formData.get(`${fieldSelector}.id`);
      if (fieldId.indexOf("new-") >= 0) {
        fieldId = undefined;
      }

      const field: TemplateFieldPostData = {
        id: fieldId,
        fieldType: parseInt(formData.get(`${fieldSelector}.fieldType`)),
        isRequired: formData.get(`${fieldSelector}.isRequired`) === "on",
        label: formData.get(`${fieldSelector}.label`),
      };

      section.fields.push(field);

      fieldIndex++;
      fieldSelector = getFieldSelector(sectionIndex, fieldIndex);
    }

    postData.sections.push(section);
    fieldIndex = 0;
    sectionIndex++;
    sectionSelector = getSectionSelector(sectionIndex);
  }

  return postData;
}
