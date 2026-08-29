import { MetaFunction, type LoaderFunction } from "@remix-run/node";
import RecordsGrid from "@/components/grids/recordsGrid";
import { ApiClientProvider } from "@/data/ApiClientProvider";

export const loader: LoaderFunction = async () => {
  let apiClient = new ApiClientProvider();

  const [records, templateCatalog] = await Promise.all([
    apiClient.Records.get(),
    apiClient.Catalogs.getTemplates(),
  ]);

  return { records, templateCatalog };
};

const metaData = { title: "Registros" };

export const meta: MetaFunction = () => {
  return [metaData];
};

export const handle = metaData;

export default RecordsGrid;
