import {
  ActionFunctionArgs,
  LoaderFunctionArgs,
  MetaFunction,
  redirect,
  type LoaderFunction,
} from "@remix-run/node";
import { ApiClientProvider } from "@/data/ApiClientProvider";
import { AxiosError } from "axios";
import { buildRecordSubmitData } from "@/utils/route/records";
import { Record } from "@/data/interfaces/Record";
import RecordForm from "@/components/forms/record/recordForm";

export const loader: LoaderFunction = async ({
  params,
  request,
}: LoaderFunctionArgs) => {
  const { id: recordId } = params;

  const url = new URL(request.url);
  const searchParams = url.searchParams;

  const apiClient = new ApiClientProvider();

  const queries = [];

  const isEdit = searchParams.get("edit") ?? false;
  const templateId = url.searchParams.get("templateId") as string;

  queries.push(apiClient.Records.get(recordId!));
  queries.push(apiClient.Catalogs.getTemplates());

  if (templateId) {
    queries.push(apiClient.Templates.get(templateId));
  }

  const [record, templateCatalog, template] = await Promise.all(queries);

  return {
    record,
    templateCatalog,
    template: template ?? (record as Record)?.template,
    isEdit,
  };
};

export async function action({ request }: ActionFunctionArgs) {
  const formData = (await request.formData()) as any;

  const recordId = formData.get("id");
  const submitData = buildRecordSubmitData(formData);

  let apiClient = new ApiClientProvider();

  try {
    if (recordId) {
      await apiClient.Records.put(recordId, submitData);
    } else {
      await apiClient.Records.post(submitData);
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

  return redirect("/records");
}

export const meta: MetaFunction = () => {
  return [{ title: "Record" }];
};

export default RecordForm;
