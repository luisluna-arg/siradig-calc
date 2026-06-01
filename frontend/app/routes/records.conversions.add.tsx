import { ActionFunctionArgs, MetaFunction, redirect, type LoaderFunction } from "@remix-run/node";
import RecordConversionFormAdd from "@/components/forms/recordConversion/RecordConversionFormAdd";
import { ApiClientProvider } from "@/data/ApiClientProvider";

export const loader: LoaderFunction = async () => {
  let apiClient = new ApiClientProvider();

  let [recordCatalog, templateCatalog] = await Promise.all([
    apiClient.Catalogs.getRecords(),
    apiClient.Catalogs.getTemplates()
  ]);

  return {
    recordCatalog, templateCatalog
  };
};

export async function action({ request }: ActionFunctionArgs) {
  const formData = await request.formData();
  const recordId = formData.get("recordId") as string;
  const templateId = formData.get("templateId") as string;

  const apiClient = new ApiClientProvider();
  const conversion = await apiClient.Conversions.convert(recordId, templateId);

  return redirect(`/records/conversions/${conversion.id}`);
}

export const meta: MetaFunction = () => {
  return [{ title: "Conversion" }];
};

export default RecordConversionFormAdd;
