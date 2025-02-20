import { MetaFunction, type LoaderFunction } from "@remix-run/node";
import RecordConversionForm from "@/components/recordConversionForm";
import { ApiClient } from "@/data/ApiClient";

export const loader: LoaderFunction = async ({ params }) => {
  const { id: conversionId } = params;
  let apiClient = new ApiClient();
  return await apiClient.getConversion(conversionId!);
};

export const meta: MetaFunction = () => {
  return [
    { title: "Conversion" }
  ];
};

export default RecordConversionForm;
