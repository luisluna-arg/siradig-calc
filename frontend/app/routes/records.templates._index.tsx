import { MetaFunction, type LoaderFunction } from "@remix-run/node";
import TemplatesGrid from "@/components/templatesGrid";
import { ApiClient } from "@/data/ApiClient";

export const loader: LoaderFunction = async () => {
  let apiClient = new ApiClient();
  return await apiClient.getTemplates();
};

export const meta: MetaFunction = () => {
  return [
    { title: "Templates" },
  ];
};

export default TemplatesGrid;
