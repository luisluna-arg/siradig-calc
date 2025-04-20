import { MetaFunction, type LoaderFunction } from "@remix-run/node";
import RecordsGrid from "@/components/grids/recordsGrid";
import { ApiClientProvider } from "@/data/ApiClientProvider";

export const loader: LoaderFunction = async () => {
  let apiClient = new ApiClientProvider();
  return await apiClient.Records.get();
};

const metaData = { title: "Records" };

export const meta: MetaFunction = () => {
  return [metaData];
};

export const handle = metaData;

export default RecordsGrid;
