import {
  ActionFunctionArgs,
  LoaderFunctionArgs,
  MetaFunction,
  redirect,
} from "@remix-run/node";
import AddRecordForm from "@/components/addRecordForm";
import { ApiClient } from "@/data/ApiClient";
import { Catalog } from "@/data/interfaces/Catalog";
import { Template } from "@/data/interfaces/Template";
import { RecordPostData } from "@/data/interfaces/RecordPostData";

export interface RecordsAddLoaderData {
  templateCatalog: Array<Catalog>;
  template: Template;
}

export const loader = async ({ request }: LoaderFunctionArgs) => {
  const url = new URL(request.url);
  const templateId = url.searchParams.get("templateId") as string; // Obtener ?id=123

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

  const postData = buildPostData(formData);

  let apiClient = new ApiClient();

  await apiClient.postRecord(postData);

  return redirect("/records");
}

export const meta: MetaFunction = () => {
  return [{ title: "Add new record" }];
};

export default AddRecordForm;

function buildPostData(formData: any) {
  let postData: RecordPostData = {
    templateId: formData.get("templateId"),
    title: formData.get("title"),
    values: [],
  };

  let sectionIndex = 0;
  let index = 0;

  function getBase(sectionIndex: number, index: number) {
    return `section[${sectionIndex}].values[${index}]`;
  }

  let baseSelector = getBase(sectionIndex, index);

  while (formData.has(`${baseSelector}.value`)) {
    while (formData.has(`${baseSelector}.value`)) {
      const value = formData.get(`${baseSelector}.value`);

      if (value) {
        postData.values.push({
          fieldId: formData.get(`${baseSelector}.fieldId`),
          value: value,
        });
      }
      index++;
      baseSelector = getBase(sectionIndex, index);
    }
    index = 0;
    sectionIndex++;
    baseSelector = getBase(sectionIndex, index);
  }

  return postData;
}
