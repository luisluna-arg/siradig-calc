import { MetaFunction, type LoaderFunction } from "@remix-run/node";
import ConversionsGrid from "@/components/grids/recordsConversionGrid";
import { ApiClientProvider } from "@/data/ApiClientProvider";

export const loader: LoaderFunction = async () => {
  let apiClient = new ApiClientProvider();
  return await apiClient.Conversions.get();
};

const metaData = { title: "Conversions" };

export const meta: MetaFunction = () => {
  return [metaData];
};

export const handle = metaData;

export default ConversionsGrid;
