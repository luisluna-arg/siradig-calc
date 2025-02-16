import { FieldTypeEnum } from "@/data/types/FieldType";
import { FieldLink } from "@/data/interfaces/FieldLink";

export interface RecordField {
  label: string;
  fieldType: FieldTypeEnum;
  isRequired: boolean;
  links: Array<FieldLink>;
  id: string;
  createdAt: Date;
  updatedAt: Date;
  deleted: boolean;
}
