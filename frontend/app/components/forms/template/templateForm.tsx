import { useLoaderData } from "@remix-run/react";
import TemplateEditForm from "@/components/forms/template/templateEditForm";
import TemplateDisplay from "@/components/forms/template/templateDisplay";

export default function TemplateForm() {
  const { isEdit } = useLoaderData() as {
    isEdit: boolean;
  };

  if (isEdit) {
    return <TemplateEditForm />;
  } else {
    return <TemplateDisplay />;
  }
}
