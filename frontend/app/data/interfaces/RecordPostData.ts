export interface RecordValuePostData {
  fieldId: string;
  value: string;
}

export interface RecordPostData {
  title: string;
  templateId: string;
  values: Array<RecordValuePostData>;
}
