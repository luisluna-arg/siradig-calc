import {
  ActionFunctionArgs,
  MetaFunction,
  redirect,
  type LoaderFunction,
} from "@remix-run/node";
import { ApiClientProvider } from "@/data/ApiClientProvider";
import TemplateLinkAddForm from "@/components/forms/templateLink/templateLinkAddForm";

export const loader: LoaderFunction = async () => {
  const apiClient = new ApiClientProvider();
  const templateCatalog = await apiClient.Catalogs.getTemplates();
  return { templateCatalog };
};

export async function action({ request }: ActionFunctionArgs) {
  const formData = await request.formData();
  const leftTemplateId = formData.get("leftTemplateId") as string;
  const rightTemplateId = formData.get("rightTemplateId") as string;

  const apiClient = new ApiClientProvider();
  await apiClient.TemplateLinks.create(leftTemplateId, rightTemplateId);

  return redirect(
    `/records/templates/links/${leftTemplateId}/to/${rightTemplateId}?edit=true`
  );
}

const metaData = { title: "Add Template Link" };

export const meta: MetaFunction = () => {
  return [metaData];
};

export const handle = metaData;

export default TemplateLinkAddForm;
