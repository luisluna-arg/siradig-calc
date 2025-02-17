import { MetaFunction, type LoaderFunction } from "@remix-run/node";
import ConversionsGrid from "@/components/recordsConversionGrid";
import { ApiClient } from "@/data/ApiClient";

export const loader: LoaderFunction = async () => {
  let apiClient = new ApiClient();
  return await apiClient.getConversions();
};

const metaData = { title: "Conversions" };

export const meta: MetaFunction = () => {
  return [metaData];
};

export const handle = metaData;

export default ConversionsGrid;
