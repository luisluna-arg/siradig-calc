import { RecordPostData } from "@/utils/route/interfaces/RecordPostData";

export function buildRecordSubmitData(formData: any) {
  let postData: RecordPostData = {
    templateId: formData.get("templateId"),
    title: formData.get("title"),
    values: [],
  };

  let sectionIndex = 0;
  let index = 0;

  function getBase(sectionIndex: number, index: number) {
    return `section[${sectionIndex}].values[${index}]`;
  }

  let baseSelector = getBase(sectionIndex, index);

  while (formData.has(`${baseSelector}.value`)) {
    while (formData.has(`${baseSelector}.value`)) {
      const value = formData.get(`${baseSelector}.value`);

      if (value) {
        let fieldValue: any = {
          fieldId: formData.get(`${baseSelector}.fieldId`),
          value: value,
        };

        const valueId = formData.get(`${baseSelector}.valueId`) ?? undefined;

        if (valueId) {
          fieldValue.id = valueId;
        }

        postData.values.push(fieldValue);
      }
      index++;
      baseSelector = getBase(sectionIndex, index);
    }
    index = 0;
    sectionIndex++;
    baseSelector = getBase(sectionIndex, index);
  }

  return postData;
}
