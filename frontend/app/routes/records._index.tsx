import { MetaFunction, type LoaderFunction } from "@remix-run/node";
import RecordsGrid from "@/components/recordsGrid";
import { ApiClient } from "@/data/ApiClient";

export const loader: LoaderFunction = async () => {
  let apiClient = new ApiClient();
  return await apiClient.getRecords();
};
export const meta: MetaFunction = () => {
  return [
    { title: "Records" },
  ];
};

export default RecordsGrid;
