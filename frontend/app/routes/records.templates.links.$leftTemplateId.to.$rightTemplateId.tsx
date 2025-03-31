import {
  LoaderFunctionArgs,
  MetaFunction,
  type LoaderFunction,
} from "@remix-run/node";
import { TemplateLinkForm } from "@/components/templateLinkForm";
import { ApiClientProvider } from "@/data/ApiClientProvider";

export const loader: LoaderFunction = async ({
  params,
  request,
}: LoaderFunctionArgs) => {
  const { leftTemplateId, rightTemplateId } = params;

  const searchParams = new URL(request.url).searchParams;

  let apiClient = new ApiClientProvider();

  const queries = [];

  const isEdit = searchParams.get("edit") ?? false;

  queries.push(
    apiClient.TemplateLinks.getByIds(leftTemplateId!, rightTemplateId!)
  );

  queries.push(apiClient.Catalogs.getFieldTypes());

  const [templateLink, fieldTypeCatalog] = await Promise.all(queries);

  return {
    templateLink,
    isEdit,
    fieldTypeCatalog,
  };
};

export const meta: MetaFunction = () => {
  return [{ title: "Template Link" }];
};

export default TemplateLinkForm;
