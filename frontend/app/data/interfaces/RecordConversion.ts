import { RecordFlat } from "@/data/interfaces/RecordFlat";
import { FieldComposition } from "@/data/interfaces/FieldComposition";
import { RecordTemplateLink } from "@/data/interfaces/RecordTemplateLink";

export interface RecordConversion {
    id: string,
    haberes: number,
    retenciones: number,
    neto: number,
    recordTemplateLink: RecordTemplateLink,
    values: Array<FieldComposition>,
    source: RecordFlat,
    target: RecordFlat,
}
