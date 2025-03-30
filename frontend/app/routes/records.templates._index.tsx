import { MetaFunction, type LoaderFunction } from "@remix-run/node";
import TemplatesGrid from "@/components/templatesGrid";
import { ApiClientProvider } from "@/data/ApiClientProvider";

export const loader: LoaderFunction = async () => {
  let apiClient = new ApiClientProvider();
  return await apiClient.Templates.get();
};

const metaData = { title: "Templates" };

export const meta: MetaFunction = () => {
  return [metaData];
};

export const handle = metaData;

export default TemplatesGrid;
