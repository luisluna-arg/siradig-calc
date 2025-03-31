import { FieldLink } from "@/data/interfaces/FieldLink";
import { FieldType } from "@/data/types/FieldType";

export interface RecordField {
  id: string;
  label: string;
  fieldType: FieldType;
  isRequired: boolean;
  links: Array<FieldLink>;
  createdAt?: Date;
  updatedAt?: Date;
  deleted?: boolean;
}
