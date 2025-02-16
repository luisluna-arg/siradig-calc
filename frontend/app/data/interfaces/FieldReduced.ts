import { FieldType } from "@/data/types/FieldType";

export interface FieldReduced {
  id: string;
  label: string;
  fieldType: FieldType;
  isRequired: boolean;
}