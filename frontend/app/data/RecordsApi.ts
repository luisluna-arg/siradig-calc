import { EntityApi } from "@/data/EntityApi";
import { AxiosInstance } from "axios";
import { RecordPostData } from "@/utils/route/interfaces/RecordPostData";
import { Record as DataRecord } from "@/data/interfaces/Record";

export class RecordsApi extends EntityApi<DataRecord, RecordPostData> {
  constructor(client: AxiosInstance, baseUrl: string) {
    super(client, `${baseUrl}/records`);
  }
}
