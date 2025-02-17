import axios, { AxiosInstance } from "axios";
import { Template } from "@/data/interfaces/Template";
import { Record as DataRecord } from "@/data/interfaces/Record";
import { TemplateLinkReduced } from "@/data/interfaces/TemplateLinkReduced";
import { RecordConversion } from "@/data/interfaces/RecordConversion";
import { Catalog } from "./interfaces/Catalog";

export class ApiClient {
  private baseURL: string;
  private client: AxiosInstance;

  constructor() {
    this.baseURL =
      typeof window === "undefined"
        ? process.env.API_URL ?? ""
        : import.meta.env.VITE_API_URL ?? "";
    if (!this.baseURL) {
      throw new Error("API_URL environment variable is not defined");
    }

    this.client = axios.create({
      baseURL: this.baseURL,
      headers: {
        "Content-Type": "application/json",
      },
    });
  }

  private async getAxiosConfig() {
    if (typeof window === "undefined") {
      // Dynamically import https.Agent only on the server
      const { Agent } = await import("https");
      return {
        httpsAgent: new Agent({ rejectUnauthorized: false }),
      };
    }
    return {};
  }

  public async get<T>(endpoint: string): Promise<T> {
    let result = await this.client.get(
      `${this.baseURL}/${endpoint}`,
      await this.getAxiosConfig()
    );
    return result.data as T;
  }

  public async delete(endpoint: string, id?: string): Promise<any> {
    return await this.client.delete(
      `${endpoint}` + (id ? `/${id}` : ''),
      await this.getAxiosConfig()
    );
  }

  public async getTemplate(templateId: string): Promise<Template> {
    return await this.get<Template>(`api/records/templates/${templateId}`);
  }

  public async getTemplates(): Promise<Template> {
    return await this.get<Template>(`api/records/templates`);
  }

  public async getRecords(): Promise<DataRecord> {
    return await this.get<DataRecord>(`api/records`);
  }

  public async getRecord(recordId: string): Promise<DataRecord> {
    return await this.get<DataRecord>(`api/records/${recordId}`);
  }

  public async deleteRecord(recordId: string): Promise<any> {
    return await this.delete(`api/records`, recordId);
  }

  public async getConversions(): Promise<RecordConversion> {
    return await this.get<RecordConversion>(`api/records/conversions`);
  }

  public async deleteConversion(sourceId: string, conversionId: string): Promise<RecordConversion> {
    return await this.delete(`api/records/${sourceId}/conversions/${conversionId}`);
  }

  public async getLinks(): Promise<TemplateLinkReduced> {
    return await this.get<TemplateLinkReduced>(`api/records/templates/links`);
  }

  public async getTemplateLink(
    leftTemplateId: string,
    rightTemplateId: string
  ): Promise<TemplateLinkReduced> {
    return await this.get<TemplateLinkReduced>(
      `api/records/templates/links/${leftTemplateId}/to/${rightTemplateId}`
    );
  }

  
  public async getTemplateCatalog(): Promise<Catalog> {
    return await this.get<Catalog>(`api/catalog/templates`);
  }

}
