export interface ImportResultValue {
  fieldId: string;
  value: string;
}

export interface ImportResult {
  templateId: string;
  title: string;
  values: ImportResultValue[];
}
