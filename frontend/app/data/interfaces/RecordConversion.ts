import { RecordFlat } from "@/data/interfaces/RecordFlat";
import { RecordValue } from "@/data/interfaces/RecordValue";
import { RecordTemplateLink } from "@/data/interfaces/RecordTemplateLink";

export interface RecordConversion {
    id: string,
    haberes: number,
    retenciones: number,
    neto: number,
    recordTemplateLink: RecordTemplateLink,
    values: Array<RecordValue>,
    source: RecordFlat,
    target: RecordFlat,
}
