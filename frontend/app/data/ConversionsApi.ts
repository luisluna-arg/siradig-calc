import { EntityApi } from "@/data/EntityApi";
import { AxiosInstance } from "axios";
import { RecordConversion } from "@/data/interfaces/RecordConversion";

export class ConversionsApi extends EntityApi<RecordConversion, any> {
  constructor(client: AxiosInstance, baseUrl: string) {
    super(client, `${baseUrl}/records/conversions`);
  }

  public async convert(sourceId: string, targetTemplateId: string): Promise<RecordConversion> {
    const result = await this.client.post(
      `${this.baseURL}/${sourceId}/to/${targetTemplateId}`,
      {},
      await this.getAxiosConfig()
    );
    return result.data as RecordConversion;
  }

  public async delete(_id?: string): Promise<any> {
    throw "Method not allow for Conversions";
  }

  public async deleteByIds(
    sourceId: string,
    targetTemplateId: string
  ): Promise<any> {
    return super.delete(`${sourceId}/to/${targetTemplateId}`);
  }
}
