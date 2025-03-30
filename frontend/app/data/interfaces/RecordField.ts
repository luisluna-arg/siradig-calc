import { FieldTypeEnum } from "@/data/types/FieldType";
import { FieldLink } from "@/data/interfaces/FieldLink";

export interface RecordField {
  id: string;
  label: string;
  fieldType: FieldTypeEnum;
  isRequired: boolean;
  links: Array<FieldLink>;
  createdAt?: Date;
  updatedAt?: Date;
  deleted?: boolean;
}
