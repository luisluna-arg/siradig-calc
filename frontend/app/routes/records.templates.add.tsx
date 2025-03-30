import { ActionFunctionArgs, MetaFunction, redirect } from "@remix-run/node";
import { ApiClient } from "@/data/ApiClient";
import TemplateEditForm from "@/components/templateEditForm";
import { buildTemplateSubmitData } from "@/utils/route/templates";

export interface TempplatesAddLoaderData {}

export const loader = async () => {
  let apiClient = new ApiClient();

  const queries = [];
  queries.push(apiClient.getFieldTypeCatalog());

  const [fieldTypeCatalog] = await Promise.all(queries);

  return {
    fieldTypeCatalog,
  };
};

export async function action({ request }: ActionFunctionArgs) {
  const formData = (await request.formData()) as any;

  const postData = buildTemplateSubmitData(formData);

  try {
    let apiClient = new ApiClient();

    await apiClient.postTemplate(postData);

    return redirect("/records/templates");
  } catch (error: any) {
    console.log(error);
    return error.response;
  }
}

export const meta: MetaFunction = () => {
  return [{ title: "Add new template" }];
};

export default TemplateEditForm;
