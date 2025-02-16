export enum FieldTypeEnum {
  Text = 0,
  Number = 1,
  Date = 2,
  Email = 3,
  Checkbox = 4,
  Dropdown = 5,
}

type FieldName = "Text" | "Number" | "Date" | "Email" | "Checkbox" | "Dropdown";

export interface FieldType {
  id: FieldTypeEnum;
  name: FieldName;
}

export const fieldTypes: FieldType[] = [
  { id: 0, name: "Text" },
  { id: 1, name: "Number" },
  { id: 2, name: "Date" },
  { id: 3, name: "Email" },
  { id: 4, name: "Checkbox" },
  { id: 5, name: "Dropdown" },
];
