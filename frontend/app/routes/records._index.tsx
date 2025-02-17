import { MetaFunction, type LoaderFunction } from "@remix-run/node";
import RecordsGrid from "@/components/recordsGrid";
import { ApiClient } from "@/data/ApiClient";

export const loader: LoaderFunction = async () => {
  let apiClient = new ApiClient();
  return await apiClient.getRecords();
};

const metaData = { title: "Records" };

export const meta: MetaFunction = () => {
  return [metaData];
};

export const handle = metaData;

export default RecordsGrid;
