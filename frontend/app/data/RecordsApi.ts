import { EntityApi } from "@/data/EntityApi";
import { AxiosInstance } from "axios";
import { RecordPostData } from "@/utils/route/interfaces/RecordPostData";
import { Record as DataRecord } from "@/data/interfaces/Record";
import { ImportResult } from "@/data/interfaces/ImportResult";

export class RecordsApi extends EntityApi<DataRecord, RecordPostData> {
  constructor(client: AxiosInstance, baseUrl: string) {
    super(client, `${baseUrl}/records`);
  }

  public async importCsv(templateId: string, file: File): Promise<ImportResult> {
    const formData = new FormData();
    formData.append("file", file);
    formData.append("templateId", templateId);
    const result = await this.client.postForm(
      `${this.baseURL}/import/csv`,
      formData,
      await this.getAxiosConfig()
    );
    return result.data as ImportResult;
  }

  public async importPdf(templateId: string, file: File): Promise<ImportResult> {
    const formData = new FormData();
    formData.append("file", file);
    formData.append("templateId", templateId);
    const result = await this.client.postForm(
      `${this.baseURL}/import/pdf`,
      formData,
      await this.getAxiosConfig()
    );
    return result.data as ImportResult;
  }
}
