import { MetaFunction, type LoaderFunction } from "@remix-run/node";
import TemplateLinksGrid from "@/components/grids/templateLinksGrid";
import { ApiClientProvider } from "@/data/ApiClientProvider";

export const loader: LoaderFunction = async () => {
  let apiClient = new ApiClientProvider();
  return await apiClient.TemplateLinks.get();
};

const metaData = { title: "Vínculos de templates" };

export const meta: MetaFunction = () => {
  return [metaData];
};

export const handle = metaData;

export default TemplateLinksGrid;
