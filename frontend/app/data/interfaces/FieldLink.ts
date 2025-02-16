import { FieldReduced } from "@/data/interfaces/FieldReduced";

export interface FieldLink {
  id: string;
  rightField: FieldReduced;
  leftFields: Array<FieldReduced>;
}
