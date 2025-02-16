import { MetaFunction, type LoaderFunction } from "@remix-run/node";
import Template from "@/components/templateForm";
import { ApiClient } from "@/data/ApiClient";

export const loader: LoaderFunction = async ({ params }) => {
  const { id: templateId } = params;
  let apiClient = new ApiClient();
  return await apiClient.getTemplate(templateId!);
};

export const meta: MetaFunction = () => {
  return [
    { title: "Template" }
  ];
};

export default Template;
