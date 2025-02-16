import { Template } from "./interfaces/Template";
import { Record as DataRecord } from "./interfaces/Record";
import axios from "axios";
import { Agent } from "https";
import { TemplateLinkReduced } from "./interfaces/TemplateLinkReduced";

export class ApiClient {
  private baseURL: string;
  private httpsAgent: Agent;

  constructor() {
    this.baseURL = process.env.API_URL!;
    if (!this.baseURL) {
      throw new Error("API_URL environment variable is not defined");
    }
    const httpsAgent = new Agent({ rejectUnauthorized: false });
    this.httpsAgent = httpsAgent;
  }

  private async request<T>(
    endpoint: string,
    options: RequestInit = {}
  ): Promise<T> {
    const url = `${this.baseURL}${endpoint}`;
    const response = await fetch(url, options);

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(
        `Error: ${response.status} ${response.statusText} - ${errorText}`
      );
    }

    return response.json() as Promise<T>;
  }

  public async get<T>(endpoint: string, headers: HeadersInit = {}): Promise<T> {
    let result = await axios.get(`${this.baseURL}/${endpoint}`, {
      httpsAgent: this.httpsAgent,
    });

    return result.data as T;
  }

  // public async post<T, U>(
  //   endpoint: string,
  //   body: U,
  //   headers: HeadersInit = {}
  // ): Promise<T> {
  //   return this.request<T>(endpoint, {
  //     method: "POST",
  //     headers: {
  //       "Content-Type": "application/json",
  //       ...headers,
  //     },
  //     body: JSON.stringify(body),
  //   });
  // }

  public async getTemplate(templateId: string): Promise<Template> {
    return await this.get<Template>(`api/records/templates/${templateId}`);
  }

  public async getTemplates(): Promise<Template> {
    return await this.get<Template>(`api/records/templates`);
  }

  public async getRecord(recordId: string): Promise<DataRecord> {
    return await this.get<DataRecord>(`api/records/${recordId}`);
  }

  public async getRecords(): Promise<DataRecord> {
    return await this.get<DataRecord>(`api/records`);
  }

  public async getLinks(): Promise<TemplateLinkReduced> {
    return await this.get<TemplateLinkReduced>(`api/records/templates/links`);
  }

  public async getTemplateLink(leftTemplateId: string, rightTemplateId: string): Promise<TemplateLinkReduced> {
    return await this.get<TemplateLinkReduced>(`api/records/templates/links/${leftTemplateId}/to/${rightTemplateId}`);
  }
}
