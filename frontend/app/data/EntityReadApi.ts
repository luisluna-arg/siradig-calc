import { AxiosInstance } from "axios";

export class EntityReadApi<TEntity> {
  protected baseURL: string;
  protected client: AxiosInstance;

  constructor(client: AxiosInstance, baseUrl: string) {
    this.baseURL = baseUrl;
    this.client = client;
  }

  public async getAxiosConfig() {
    if (typeof window === "undefined") {
      // Dynamically import https.Agent only on the server
      const { Agent } = await import("https");
      return {
        httpsAgent: new Agent({ rejectUnauthorized: false }),
      };
    }
    return {};
  }

  public async get(id?: string, path?: string): Promise<TEntity> {
    let result = await this.client.get(
      `${this.baseURL}${path ? "/" + path : ""}` + (id ? `/${id}` : ""),
      await this.getAxiosConfig()
    );
    return result.data as TEntity;
  }
}
