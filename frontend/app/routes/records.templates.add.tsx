import { ActionFunctionArgs, MetaFunction, redirect } from "@remix-run/node";
import { ApiClientProvider } from "@/data/ApiClientProvider";
import TemplateEditForm from "@/components/forms/template/templateEditForm";
import { buildTemplateSubmitData } from "@/utils/route/templates";

export interface TempplatesAddLoaderData {}

export const loader = async () => {
  let apiClient = new ApiClientProvider();

  const queries = [];
  queries.push(apiClient.Catalogs.getFieldTypes());

  const [fieldTypeCatalog] = await Promise.all(queries);

  return {
    fieldTypeCatalog,
  };
};

export async function action({ request }: ActionFunctionArgs) {
  const formData = (await request.formData()) as any;

  const postData = buildTemplateSubmitData(formData);

  try {
    let apiClient = new ApiClientProvider();

    await apiClient.Templates.post(postData);

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
