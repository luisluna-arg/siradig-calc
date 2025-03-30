import { MetaFunction, type LoaderFunction } from "@remix-run/node";
import RecordConversionForm from "@/components/recordConversionForm";
import { ApiClientProvider } from "@/data/ApiClientProvider";

export const loader: LoaderFunction = async ({ params }) => {
  const { id: conversionId } = params;
  let apiClient = new ApiClientProvider();
  return await apiClient.Conversions.get(conversionId!);
};

export const meta: MetaFunction = () => {
  return [{ title: "Conversion" }];
};

export default RecordConversionForm;
