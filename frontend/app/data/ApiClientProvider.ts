import axios, { AxiosInstance } from "axios";
import { CatalogsApi } from "@/data/CatalogsApi";
import { ConversionsApi } from "@/data/ConversionsApi";
import { RecordsApi } from "@/data/RecordsApi";
import { TemplateLinksApi } from "@/data/TemplateLinksApi";
import { TemplatesApi } from "@/data/TemplatesApi";

export class ApiClientProvider {
  private baseURL: string;
  private client: AxiosInstance;

  public Catalogs: CatalogsApi;
  public Conversions: ConversionsApi;
  public Records: RecordsApi;
  public TemplateLinks: TemplateLinksApi;
  public Templates: TemplatesApi;

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

    const apiBaseUrl = `${this.baseURL}/api`;

    this.Catalogs = new CatalogsApi(this.client, apiBaseUrl);
    this.Conversions = new ConversionsApi(this.client, apiBaseUrl);
    this.Records = new RecordsApi(this.client, apiBaseUrl);
    this.TemplateLinks = new TemplateLinksApi(this.client, apiBaseUrl);
    this.Templates = new TemplatesApi(this.client, apiBaseUrl);
  }
}
