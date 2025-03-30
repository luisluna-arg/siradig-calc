import { EntityApi } from "@/data/EntityApi";
import { AxiosInstance } from "axios";
import { TemplatePostData } from "@/utils/route/interfaces/TemplatePostData";
import { Template } from "@/data/interfaces/Template";

export class TemplatesApi extends EntityApi<Template, TemplatePostData> {
  constructor(client: AxiosInstance, baseUrl: string) {
    super(client, `${baseUrl}/records/templates`);
  }
}
