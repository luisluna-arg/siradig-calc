import {
  MetaFunction,
  LoaderFunctionArgs,
  type LoaderFunction,
  ActionFunctionArgs,
  redirect,
} from "@remix-run/node";
import Template from "@/components/templateForm";
import { ApiClientProvider } from "@/data/ApiClientProvider";
import { AxiosError } from "axios";
import { buildTemplateSubmitData } from "@/utils/route/templates";

export const loader: LoaderFunction = async ({
  params,
  request,
}: LoaderFunctionArgs) => {
  const { id: templateId } = params;

  const url = new URL(request.url);
  const searchParams = url.searchParams;

  let apiClient = new ApiClientProvider();

  const queries = [];

  const isEdit = searchParams.get("edit") ?? false;

  queries.push(apiClient.Templates.get(templateId!));

  queries.push(apiClient.Catalogs.getFieldTypes());

  const [template, fieldTypeCatalog] = await Promise.all(queries);

  return {
    template,
    isEdit,
    fieldTypeCatalog,
  };
};

export async function action({ request }: ActionFunctionArgs) {
  const formData = (await request.formData()) as any;

  const templateId = formData.get("id");
  const submitData = buildTemplateSubmitData(formData);

  let apiClient = new ApiClientProvider();

  try {
    if (templateId) {
      await apiClient.Templates.put(templateId, submitData);
    } else {
      await apiClient.Templates.post(submitData);
    }
  } catch (error) {
    const axiosError = error as AxiosError;
    console.log(axiosError);
    return {
      status: axiosError.response?.status,
      statusText: axiosError.response?.statusText,
      errorData: axiosError.response?.data,
    };
  }

  return redirect("/records/templates");
}

export const meta: MetaFunction = () => {
  return [{ title: "Template" }];
};

export default Template;
