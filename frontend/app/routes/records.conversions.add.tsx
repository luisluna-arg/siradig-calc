import { MetaFunction, type LoaderFunction } from "@remix-run/node";
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

export const meta: MetaFunction = () => {
  return [{ title: "Conversión" }];
};

export default RecordConversionFormAdd;
