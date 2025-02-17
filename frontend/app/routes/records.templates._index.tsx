import { MetaFunction, type LoaderFunction } from "@remix-run/node";
import TemplatesGrid from "@/components/templatesGrid";
import { ApiClient } from "@/data/ApiClient";

export const loader: LoaderFunction = async () => {
  let apiClient = new ApiClient();
  return await apiClient.getTemplates();
};

const metaData = { title: "Templates" };

export const meta: MetaFunction = () => {
  return [metaData];
};

export const handle = metaData;

export default TemplatesGrid;
