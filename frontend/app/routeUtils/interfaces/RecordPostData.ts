export interface RecordValuePostData {
  id: string;
  fieldId: string;
  value: string;
}

export interface RecordPostData {
  id?: string;
  title: string;
  templateId: string;
  values: Array<RecordValuePostData>;
}
