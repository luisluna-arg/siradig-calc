import { MetaFunction, type LoaderFunction } from "@remix-run/node";
import RecordForm from "@/components/recordForm";
import { ApiClient } from "@/data/ApiClient";

export const loader: LoaderFunction = async ({ params }) => {
  const { id: recordId } = params;
  let apiClient = new ApiClient();
  return await apiClient.getRecord(recordId!);
};

export const meta: MetaFunction = () => {
  return [
    { title: "Record" },
  ];
};

export default RecordForm;
