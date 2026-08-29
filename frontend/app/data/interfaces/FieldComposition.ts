import { FieldValue } from "@/data/interfaces/FieldValue";

export interface FieldComposition {
  fieldId: string;
  label: string;
  fieldType: number;
  isRequired: boolean;
  value: string;
  sourceFields: Array<FieldValue>;
}
