import { AxiosInstance } from "axios";
import { EntityReadApi } from "@/data/EntityReadApi";

export class EntityApi<TEntity, TPostData> extends EntityReadApi<TEntity> {
  constructor(client: AxiosInstance, baseUrl: string) {
    super(client, baseUrl);
  }

  public async post(data?: TPostData): Promise<TEntity> {
    return await this.client.post(
      this.baseURL,
      data,
      await this.getAxiosConfig()
    );
  }

  public async put(id: string, data: any): Promise<TEntity> {
    return await this.client.put(
      `${this.baseURL}/${id}`,
      data,
      await this.getAxiosConfig()
    );
  }

  public async delete(id?: string): Promise<any> {
    return await this.client.delete(
      `${this.baseURL}` + (id ? `/${id}` : ""),
      await this.getAxiosConfig()
    );
  }
}
