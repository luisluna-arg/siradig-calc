import { MetaFunction, type LoaderFunction } from "@remix-run/node";
import TemplateLinkForm from "@/components/templateLinkForm";
import { ApiClientProvider } from "@/data/ApiClientProvider";

export const loader: LoaderFunction = async ({ params }) => {
  const { leftTemplateId, rightTemplateId } = params;
  let apiClient = new ApiClientProvider();
  return await apiClient.TemplateLinks.getByIds(
    leftTemplateId!,
    rightTemplateId!
  );
};

export const meta: MetaFunction = () => {
  return [{ title: "Template Link" }];
};

export default TemplateLinkForm;
