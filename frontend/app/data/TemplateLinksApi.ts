import { EntityReadApi } from "@/data/EntityReadApi";
import { AxiosInstance } from "axios";
import { TemplateLinkReduced } from "@/data/interfaces/TemplateLinkReduced";

export class TemplateLinksApi extends EntityReadApi<TemplateLinkReduced> {
  private linkBaseUrl: string;

  constructor(client: AxiosInstance, baseUrl: string) {
    super(client, `${baseUrl}/records/templates/links`);
    this.linkBaseUrl = `${baseUrl}/records/templates/link`;
  }

  public async getByIds(
    leftTemplateId: string,
    rightTemplateId: string
  ): Promise<TemplateLinkReduced> {
    return await this.get(`${leftTemplateId}/to/${rightTemplateId}`);
  }

  public async create(leftTemplateId: string, rightTemplateId: string): Promise<void> {
    await this.client.post(
      `${this.linkBaseUrl}/${leftTemplateId}/to/${rightTemplateId}`,
      {},
      await this.getAxiosConfig()
    );
  }

  public async deleteByIds(leftTemplateId: string, rightTemplateId: string): Promise<void> {
    await this.client.delete(
      `${this.linkBaseUrl}/${leftTemplateId}/to/${rightTemplateId}`,
      await this.getAxiosConfig()
    );
  }
}
