import { MetaFunction, type LoaderFunction } from "@remix-run/node";
import TemplateLinksGrid from "@/components/templateLinksGrid";
import { ApiClient } from "@/data/ApiClient";

export const loader: LoaderFunction = async () => {
  let apiClient = new ApiClient();
  return await apiClient.getLinks();
};

export const meta: MetaFunction = () => {
  return [
    { title: "Template Links" },
  ];
};

export default TemplateLinksGrid;
