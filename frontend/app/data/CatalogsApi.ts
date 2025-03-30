import { EntityReadApi } from "@/data/EntityReadApi";
import { AxiosInstance } from "axios";
import { Catalog } from "@/data/interfaces/Catalog";

export class CatalogsApi extends EntityReadApi<any> {
  constructor(client: AxiosInstance, baseUrl: string) {
    super(client, `${baseUrl}/catalog`);
  }

  public async getTemplates(): Promise<Catalog<string>> {
    return await this.get(undefined, `templates`);
  }

  public async getFieldTypes(): Promise<Catalog<number>> {
    return await this.get(undefined, `field-types`);
  }
}
