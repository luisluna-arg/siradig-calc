import { EntityReadApi } from "@/data/EntityReadApi";
import { AxiosInstance } from "axios";
import { TemplateLinkReduced } from "@/data/interfaces/TemplateLinkReduced";

export class TemplateLinksApi extends EntityReadApi<TemplateLinkReduced> {
  constructor(client: AxiosInstance, baseUrl: string) {
    super(client, `${baseUrl}/records/templates/links`);
  }

  public async getByIds(
    leftTemplateId: string,
    rightTemplateId: string
  ): Promise<TemplateLinkReduced> {
    return await this.get(`${leftTemplateId}/to/${rightTemplateId}`);
  }
}
