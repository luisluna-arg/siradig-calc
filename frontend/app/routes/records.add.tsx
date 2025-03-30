import {
  ActionFunctionArgs,
  LoaderFunctionArgs,
  MetaFunction,
  redirect,
} from "@remix-run/node";
import { ApiClient } from "@/data/ApiClient";
import { buildRecordSubmitData } from "@/utils/route/records";
import { Catalog } from "@/data/interfaces/Catalog";
import { Template } from "@/data/interfaces/Template";
import RecordEditForm from "@/components/recordEditForm";

export interface RecordsAddLoaderData {
  templateCatalog: Array<Catalog>;
  template: Template;
}

export const loader = async ({ request }: LoaderFunctionArgs) => {
  const url = new URL(request.url);
  const templateId = url.searchParams.get("templateId") as string;

  let apiClient = new ApiClient();

  const queries = [];
  queries.push(apiClient.getTemplateCatalog());

  if (templateId) {
    queries.push(apiClient.getTemplate(templateId));
  }

  const [templateCatalog, template] = await Promise.all(queries);

  return {
    templateCatalog,
    template,
  };
};

export async function action({ request }: ActionFunctionArgs) {
  const formData = (await request.formData()) as any;

  const postData = buildRecordSubmitData(formData);

  let apiClient = new ApiClient();

  await apiClient.postRecord(postData);

  return redirect("/records");
}

export const meta: MetaFunction = () => {
  return [{ title: "Add new record" }];
};

export default RecordEditForm;
