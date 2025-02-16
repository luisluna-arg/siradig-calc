import { MetaFunction, type LoaderFunction } from "@remix-run/node";
import TemplateLinkForm from "@/components/templateLinkForm";
import { ApiClient } from "@/data/ApiClient";

export const loader: LoaderFunction = async ({ params }) => {
  const { leftTemplateId, rightTemplateId } = params;
  let apiClient = new ApiClient();
  return await apiClient.getTemplateLink(leftTemplateId!, rightTemplateId!);
};


export const meta: MetaFunction = () => {
  return [
    { title: "Template Link" },
  ];
};

export default TemplateLinkForm;
