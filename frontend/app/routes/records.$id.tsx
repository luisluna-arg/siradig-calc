import {
  ActionFunctionArgs,
  LoaderFunctionArgs,
  MetaFunction,
  redirect,
  type LoaderFunction,
} from "@remix-run/node";
import { ApiClient } from "@/data/ApiClient";
import { AxiosError } from "axios";
import { buildRecordSubmitData } from "@/utils/route/records";
import { Record } from "@/data/interfaces/Record";
import RecordForm from "@/components/recordForm";

export const loader: LoaderFunction = async ({
  params,
  request,
}: LoaderFunctionArgs) => {
  const { id: recordId } = params;

  const url = new URL(request.url);
  const searchParams = url.searchParams;

  const apiClient = new ApiClient();

  const queries = [];

  const isEdit = searchParams.get("edit") ?? false;
  const templateId = url.searchParams.get("templateId") as string;

  queries.push(apiClient.getRecord(recordId!));
  queries.push(apiClient.getTemplateCatalog());

  if (templateId) {
    queries.push(apiClient.getTemplate(templateId));
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

  let apiClient = new ApiClient();

  try {
    if (recordId) {
      await apiClient.putRecord(recordId, submitData);
    } else {
      await apiClient.postRecord(submitData);
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
